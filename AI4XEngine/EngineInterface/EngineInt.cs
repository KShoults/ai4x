using Engine;
using System.IO;
using System.Xml.Serialization;

namespace EngineInterface
{
    /// <summary>
    /// Class <c>EngineInt</c> provides outside implementers access to the
    /// engine's classes and methods. It should act as the sole entry point
    /// for outside code through the <c>NewGame</c>, <c>EndTurn</c>, and 
    /// <c>GatherReport</c> methods.
    /// </summary>
    public static class EngineInt
    {
        private const string SaveExtension = ".esav";

        /// <summary>
        /// Method <c>NewGame</c> creates a new game and saves the
        /// result to file.
        /// </summary>
        /// <param name="savePath">
        /// the fully qualified path where the resulting file should be saved.
        /// </param>
        public static int NewGame(string savePath)
        {
            Gamestate gamestate = GameManager.NewGame();
            SaveGamestate(savePath, gamestate);
            return 0;
        }

        /// <summary>
        /// Method <c>EndTurn</c> advances a savename by one turn and saves
        /// the result to file, overwriting the original file.
        /// </summary>
        /// <param name="savePath">
        /// the fully qualified path of the save to advance.
        /// </param>
        /// <param name="orders">
        /// a list of Order objects that are to be applied to units before
        /// the save is advanced.
        /// </param>
        public static int EndTurn(string savePath, string[] orders)
        {
            Gamestate oldGamestate = LoadGamestate(savePath);
            Gamestate newGamestate = GameManager.EndTurn(oldGamestate, orders);
            SaveGamestate(savePath, newGamestate);
            return 0;
        }



        /// <summary>
        /// Method <c>LoadGamestate</c> loads a save and deserializes it into a
        /// <c>Gamestate</c> object.
        /// </summary>
        /// <param name="savePath">
        /// the fully qulalified path of the save to be loaded.
        /// </param>
        private static Gamestate LoadGamestate(string savePath)
        {
            XmlSerializer reader = new XmlSerializer(typeof(Gamestate));
            Gamestate gamestate;
            using (FileStream file = File.Open(savePath + SaveExtension,
                                               FileMode.Open))
            {
                gamestate = (Gamestate)reader.Deserialize(file);
            }
            return gamestate;
        }

        /// <summary>
        /// Method <c>SaveGamestate</c> serializes a <c>Gamestate</c> object
        /// and save it.
        /// </summary>
        /// <param name="savePath">
        /// the fully qulalified path to save to.
        /// </param>
        /// <param name="gamestate">
        /// The gamestate to be serialized and saved
        /// </param>
        private static void SaveGamestate(string savePath, Gamestate gamestate)
        {
            XmlSerializer writer = new XmlSerializer(typeof(Gamestate));
            using (FileStream file = File.Create(savePath + SaveExtension))
            {
                writer.Serialize(file, gamestate);
            }
        }
    }
}
