using System.Collections.Generic;

namespace Engine
{
    /// <summary>
    /// Class <c>Gamestate</c> contains all of the data needed to serialize
    /// and deserialize the game. The state of the engine can be completely
    /// reconstructed from this one object.
    /// </summary>
    /// <remarks>
    /// A GameState object should only be used to transfer data to/from the
    /// EngineInterface during saving and loading. It should never be used
    /// internally within the engine or sent to the FrontendInterface.
    /// </remarks>
    public class Gamestate
    {
        /// <summary>
        /// the <c>Sector</c> of the represented game.
        /// </summary>
        public Sector GameSector { get; set; }
        /// <summary>
        /// a list of <c>Faction</c> instances in the represented game.
        /// </summary>
        /// <remarks>
        /// This list is unsorted.
        /// </remarks>
        public List<Faction> Factions { get; set; }
        /// <summary>
        /// a list of <c>Gameobject</c> instances in the represented game.
        /// </summary>
        /// <remarks>
        /// This list is unsorted.
        /// </remarks>
        public List<GameObject> GameObjects { get; set; }
        /// <summary>
        /// the current turn in the represented game.
        /// </summary>
        public int CurrentTurn { get; set; }



        private Gamestate()
        {

        }

        /// <summary>
        /// Initializes an instance of the <c>Gamestate</c> class with the
        /// specified values.
        /// </summary>
        /// <param name="currentTurn">
        /// the current turn in the represented game.
        /// </param>
        /// <param name="gameSector">
        /// <summary>
        /// the <c>Sector</c> of the represented game.
        /// </summary>
        /// </param>
        /// <param name="factions">
        /// <summary>
        /// a list of <c>Faction</c> instances in the represented game.
        /// </summary>
        /// <remarks>
        /// This list is unsorted.
        /// </remarks></param>
        /// <param name="gameObjects">
        /// <summary>
        /// a list of <c>Gameobject</c> instances in the represented game.
        /// </summary>
        /// <remarks>
        /// This list is unsorted.
        /// </remarks>
        /// </param>
        public Gamestate(int currentTurn, Sector gameSector, List<Faction> factions, List<GameObject> gameObjects)
        {
            CurrentTurn = currentTurn;
            GameSector = gameSector;
            Factions = factions;
            GameObjects = gameObjects;
        }
    }
}
