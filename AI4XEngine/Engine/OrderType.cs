namespace Engine
{
    /// <summary>
    /// The <c>OrderType</c> enum represents the various types of orders that
    /// can be issued to gameobjects.
    /// </summary>
    public enum OrderType
    {
        /// <summary>
        /// explore an unexplored system.
        /// </summary>
        Explore,
        /// <summary>
        /// Expand to an explored, unowned system.
        /// </summary>
        Expand,
        /// <summary>
        /// Build a new district on an owned system.
        /// </summary>
        Build,
        /// <summary>
        /// Attack an explored, enemy system.
        /// </summary>
        Attack,
        /// <summary>
        /// Defend an owned system.
        /// </summary>
        Defend
    }
}
