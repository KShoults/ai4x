namespace Engine
{
    /// <summary>
    /// Class <c>StarSystem</c> represents the physical attributes of an
    /// in-game solar system. It does not contain the faction-based elements
    /// that may be present in a system.
    /// </summary>
    public class StarSystem
    {
        /// <summary>
        /// the unique name of the star system.
        /// </summary>
        public string StarName { get; set; }
        /// <summary>
        /// the spectral class of the star.
        /// </summary>
        public StellarClass StarClass { get; set; }
        /// <summary>
        /// the size of the star system.
        /// </summary>
        /// <remarks>
        /// Not so much the size of the star but how much usable stuff there
        /// is in the system.
        /// </remarks>
        public int Size { get; set; }
        /// <summary>
        /// the amount of mineral wealth in the system.
        /// </summary>
        public int Minerals { get; set; }
        /// <summary>
        /// the star's position in the sector
        /// </summary>
        public Position StarPosition { get; set; }



        private StarSystem()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <c>StarSystem</c> class.
        /// </summary>
        /// <param name="starClass">
        /// the <c>StellarClass</c> representing the new star's spectral
        /// class.
        /// </param>
        /// <param name="size">
        /// the size of the new system.
        /// </param>
        /// <param name="xPosition">
        /// the horizontal position in the sector of the new system.
        /// </param>
        /// <param name="yPosition">
        /// the vertical position in the sector of the new system.
        /// </param>
        /// <param name="minerals">
        /// the amount of mineral wealth in the new system.
        /// </param>
        public StarSystem(StellarClass starClass, int size, float xPosition,
                          float yPosition, int minerals)
        {
            StarName = GameManager.GenerateStarName();
            StarClass = starClass;
            Size = size;
            StarPosition = new Position(xPosition, yPosition);
            Minerals = minerals;
        }
    }
}
