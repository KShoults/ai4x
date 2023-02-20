namespace Engine
{
    /// <summary>
    /// Enum <c>GameObjectType</c> represents the various types of gameobjects
    /// in use in the engine. We mainly use this to keep track of how many
    /// types we have.
    /// </summary>
    public enum GameObjectType
    {
        /// <summary>
        /// an owned system.
        /// </summary>
        Colony,
        /// <summary>
        /// an owned fleet.
        /// </summary>
        Fleet
    }
}
