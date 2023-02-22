using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Engine
{
    /// <summary>
    /// Static class <c>GameManager</c> contains methods used to create and
    /// advance games. It also keeps up to date lists for object lookup.
    /// </summary>
    public static class GameManager
    {
        /// <summary>
        /// the entire game map.
        /// </summary>
        public static Sector GameSector { get; private set; }
        /// <summary>
        /// The randomizer that should be used throughout the game.
        /// </summary>
        public static Random GameRandom = new Random();

        // Next id when generating factions
        private static int s_nextFactionId = 0;
        // Next id when generating everything else categorized by GameObjectType
        private static readonly int[] s_nextGameObjectIds = new int[Enum.GetNames(typeof(GameObjectType)).Length];
        // The list of Factions referenced by FactionId
        private static readonly SortedDictionary<int, Faction> s_factions = new SortedDictionary<int, Faction>();
        // The lookup list for GameObjects is categorized by Faction,
        // then GameObjectType. It's then sorted by GameObjectId.
        // I'm sacrificing readability for lookup speed here,
        // but I think it's necessary.
        private static readonly List<SortedDictionary<int, GameObject>[]> s_gameObjects = new List<SortedDictionary<int, GameObject>[]>();
        // The shuffled list of star names that can still be used in this game
        private static readonly Stack<string> s_starNameList = new Stack<string>();
        // Set the absolute path to the StarNames.txt relative to the assembly's directory
        private static readonly string s_nameListPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + Path.DirectorySeparatorChar + "StarNames.txt";
        // The number of the turn that needs to be calculated next
        private static int s_currentTurn;



        /// <summary>
        /// Creates a new game with default game and sector settings. 
        /// </summary>
        public static Gamestate NewGame()
        {
            // Initialize Gamestate for new game
            if (!InitializeNewGame())
            {
                return CreateGamestate();
            }
            s_currentTurn = 1;

            // Create sector
            GameSector = new Sector();
            GameSector.GenerateSystems();
            //GenerateFactions();

            // Create the Gamestate and return it
            return CreateGamestate();
        }

        /// <summary>
        /// Advances a game by one turn and returns the resulting gamestate.
        /// </summary>
        /// <param name="gamestate">
        /// the instance of <c>Gamestate</c> to be advanced.
        /// </param>
        /// <param name="orders">
        /// an array of <c>Order</c> instances to be applied before the
        /// turn advances.
        /// </param>
        public static Gamestate EndTurn(Gamestate gamestate, Order[] orders)
        {
            // Initialize self from Gamestate
            InitializeFromGameState(gamestate);
            // Convert orders array into a dictionary with the objectId as the
            // key.
            var ordersDict = new Dictionary<int, Order>();
            foreach (Order o in orders)
            {
                int objectId = o.OwnerId;
                ordersDict.Add(objectId, o);
            }
            // Apply orders to player's ships/planets and initiate end turn
            // calculations.
            foreach (SortedDictionary<int, GameObject>[] faction in s_gameObjects)
            {
                foreach (SortedDictionary<int, GameObject> objectTypes in faction)
                {
                    foreach (KeyValuePair<int, GameObject> kvp in objectTypes)
                    {
                        GameObject gameObject = kvp.Value;
                        Order newOrder = null;
                        if (ordersDict.ContainsKey(gameObject.ObjectId))
                        {
                            newOrder = ordersDict[gameObject.ObjectId];
                        }
                        gameObject.EndTurn(newOrder);
                    }
                }
            }

            s_currentTurn += 1;
            // Create the new Gamestate and return it
            return CreateGamestate();
        }

        /// <summary>
        /// Adds an object to the object list and returns its unique id.
        /// </summary>
        /// <param name="gameObject">
        /// an instance of <c>GameObject</c> to be added.
        /// </param>
        public static int RegisterGameObject(GameObject gameObject)
        {
            // Get the next id for the ObjectType
            int id = s_nextGameObjectIds[(int)gameObject.Type];

            s_gameObjects[gameObject.OwnerId][(int)gameObject.Type].Add(gameObject.ObjectId, gameObject);

            // Return the next id for the ObjectType
            s_nextGameObjectIds[((int)gameObject.Type)] = id++;
            return id;
        }

        /// <summary>
        /// Adds a faction to the faction list and returns its unique id.
        /// </summary>
        /// <param name="faction">
        /// an instance of <c>Faction</c> to be added.
        /// </param>
        public static int RegisterFaction(Faction faction)
        {
            // Get the next available FactionId
            int id = s_nextFactionId;
            s_nextFactionId++;

            s_factions.Add(id,faction);

            // Add a new faction category to the GameObjects list
            // First initialize a new SortedDictionary<Gameobjects>[] for the faction
            SortedDictionary<int, GameObject>[] newArray = new SortedDictionary<int, GameObject>[Enum.GetNames(typeof(GameObjectType)).Length];
            // Then initialize each SortedDictionary for each GameObjectType
            for (int i = 0; i < newArray.Length; i++)
            {
                newArray[i] = new SortedDictionary<int, GameObject>();
            }
            s_gameObjects.Add(newArray);

            return id;
        }

        /// <summary>
        /// Pops and returns a new name out of the name stack.
        /// </summary>
        public static string GenerateStarName()
        {
            try
            {
                return s_starNameList.Pop();
            }
            catch (InvalidOperationException)
            {
                Console.Error.WriteLine("Name list is empty.");
                return "";
            }
        }

        /*public static GameObject LookupGameObject(Faction owner, GameObjectType type)
        {

        }*/


        /// <summary>
        /// Attempts to initialize the engine for a new game. Returns a
        /// boolean to indicate success.
        /// </summary>
        private static bool InitializeNewGame()
        {
            // Initialize the various lists and arrays we use
            InitializeLists();
            if (!ShuffleStarNameList())
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Attempts to initialize the engine from a gamestate. Returns a
        /// boolean to indicate success.
        /// </summary>
        /// <param name="gamestate">
        /// An instance of <c>Gamestate</c> to initialize from.
        /// </param>
        private static bool InitializeFromGameState(Gamestate gamestate)
        {
            // Initialize the various lists and arrays we use
            InitializeLists();

            // Check for an invalid sector
            if (gamestate.GameSector.SectorSize == 0 || gamestate.GameSector.NumStarSystems == 0)
            {
                Console.Error.WriteLine("GameSector is invalid (has 0 size or systems).");
                return false;
            }
            GameSector = gamestate.GameSector;

            // Populate Factions list and NextFactionId
            if (!PopulateFactionsFromList(gamestate.Factions))
            {
                return false;
            }

            if (!PopulateGameObjectsFromList(gamestate.GameObjects))
            {
                return false;
            }

            // Set the current turn
            s_currentTurn = gamestate.CurrentTurn;

            return true;
        }

        /// <summary>
        /// Initializes the various needed lookup lists.
        /// </summary>
        private static void InitializeLists()
        {
            // Initialize NextGameObjectIds array
            for (int i = 0; i < s_nextGameObjectIds.Length; i++)
            {
                s_nextGameObjectIds[i] = 0;
            }
        }

        /// <summary>
        /// Shuffles the names from the star namelist and returns success
        /// state.
        /// </summary>
        private static bool ShuffleStarNameList()
        {
            // The unshuffled name list
            List<string> uNameList = new List<string>();

            // Get the sorted namelist from file
            // Note: Borrowed this namelist from AI War 2
            try
            {
                using (StreamReader reader = File.OpenText(s_nameListPath))
                {
                    string name;
                    while((name = reader.ReadLine()) != null)
                    {
                        uNameList.Add(name);
                    }
                }
            }
            catch (Exception e )
            {
                if (e is FileNotFoundException || e is DirectoryNotFoundException)
                {
                    Console.Error.WriteLine("Error: The namelist could not be found at " + s_nameListPath + ".");
                    return false;
                }
                else
                {
                    throw e;
                }

            }

            // Shuffle the list and push it into NameList
            int n = uNameList.Count;
            while (n > 1)
            {
                n--;
                int k = GameRandom.Next(n+1);
                s_starNameList.Push(uNameList[k]);
                uNameList.RemoveAt(k);
            }

            return true;
        }

        /// <summary>
        /// Turns the current state of the game into a simplified class that
        /// only contains the data we need to save then returns it.
        /// </summary>
        private static Gamestate CreateGamestate()
        {
            // Create and populate the lists we need to save
            var savedFactions = new List<Faction>();
            foreach (KeyValuePair<int, Faction> f in s_factions)
            {
                savedFactions.Add(f.Value);
            }

            List<GameObject> savedGameObjects = new List<GameObject>();
            foreach (SortedDictionary<int, GameObject>[] owner in s_gameObjects)
            {
                foreach (SortedDictionary<int, GameObject> objectType in owner)
                {
                    foreach (KeyValuePair<int, GameObject> o in objectType)
                    {
                        savedGameObjects.Add(o.Value);
                    }
                }
            }

            return new Gamestate
            (
                s_currentTurn,
                GameSector,
                savedFactions,
                savedGameObjects
            );
        }

        /// <summary>
        /// Populates <c>Factions</c> dictionary from a list of <c>Faction</c>
        /// instances andreturns false if factions are inconsistent.
        /// </summary>
        /// <remarks>
        /// Verifies the consistency of factions list and sets NextFactionId.
        /// </remarks>
        /// <param name="factions">
        /// a list of <c>Faction</c> instances to populate from.
        /// </param>
        private static bool PopulateFactionsFromList(List<Faction> factions)
        {
            foreach (Faction f in factions)
            {
                try
                {
                    s_factions.Add(f.FactionId, f);
                }
                catch (ArgumentException)
                {
                    Console.Error.WriteLine("Faction with duplicate FactionId detected.");
                    return false;
                }
            }

            for (int i = 0; i < s_factions.Count; i++)
            {
                if (!s_factions.ContainsKey(i))
                {
                    Console.Error.WriteLine("Inconsistent FactionIds detected");
                    return false;
                }
            }

            // We only know this works because we checked for consistency
            s_nextFactionId = s_factions.Count;

            return true;
        }

        /// <summary>
        /// Populates <c>GameObjects</c> list from list of <c>GameObject</c>
        /// instances and returns false if gameobjects are inconsistent.
        /// </summary>
        /// <remarks>
        /// Verifies the consistency of GameObjects list and sets
        /// NextGameObjectId list. Assumes <c>Factions</c> list is populated
        /// and consistent.
        /// </remarks>
        /// <param name="objects">
        /// a list of <c>GameObject</c> instances to populate from.
        /// </param>
        private static bool PopulateGameObjectsFromList(List<GameObject> objects)
        {
            // Prepare the GameObjects list

            // Determine how many GameObjectTypes we have
            int numGameObjectTypes = Enum.GetValues(typeof(GameObjectType)).Length;

            for (int f = 0; f < s_factions.Count; f++)
            {
                // Add a GameObject dictionary by GameObjectType for each faction
                SortedDictionary<int, GameObject>[] factionUnits = new SortedDictionary<int, GameObject>[numGameObjectTypes];

                // Add a GameObject dictionary for each GameObjectType
                for (int i = 0; i < numGameObjectTypes; i++)
                {
                    factionUnits[i] = new SortedDictionary<int, GameObject>();
                }

                s_gameObjects.Add(factionUnits);
            }
            // GameObjects list is now prepared

            // Populate the GameObjects list
            foreach (GameObject o in objects)
            {
                // Make sure the ObjectId isn't in any faction with the same GameObjectType
                foreach (KeyValuePair<int, Faction> f in s_factions)
                {
                    if (s_gameObjects[f.Key][(int)o.Type].ContainsKey(o.ObjectId))
                    {
                        Console.Error.WriteLine("GameObject with duplicate ObjectId detected.");
                        return false;
                    }
                }
                    s_gameObjects[o.OwnerId][(int)o.Type].Add(o.ObjectId, o);
            }

            // Determine the NextGameObjectIds by checking every faction at the GameObjectType for the highest id
            for (int i = 0; i < s_nextGameObjectIds.Length; i++)
            {
                int highestId = 0;
                for (int f = 0; f < s_factions.Count; f++)
                {
                    // Last is slow but this is only run once per end turn action
                    int highestKey = s_gameObjects[f][i].Last().Key;
                    if (highestKey > highestId)
                    {
                        highestId = highestKey;
                    }
                }
                s_nextGameObjectIds[i] = highestId + 1;
            }

            return true;
        }

        // private static void GenerateFactions()
        // {
        //     var gaia = new Faction("Gaia");
        //     var player1 = new Player("Player 1");
        // }
    }
}
