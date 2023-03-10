namespace Engine
{
    /// <summary>
    /// Class <c>Faction</c> represents an entity that can own and order
    /// gameobjects.
    /// </summary>
    public class Faction
    {
        /// <summary>
        /// the unique id of this faction.
        /// </summary>
        public int FactionId { get; set; }
        /// <summary>
        /// the name of this faction.
        /// </summary>
        public string Name { get; set; }



        private Faction()
        {

        }

        /// <summary>
        /// Initializes an instance of the <c>Faction</c> class with the
        /// given name.
        /// </summary>
        /// <param name="name"></param>
        public Faction(string name)
        {
            Name = name;
            FactionId = GameManager.RegisterFaction(this);
        }
    }
}
