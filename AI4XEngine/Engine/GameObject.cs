namespace Engine
{
    /// <summary>
    /// Class <c>GameObject</c> specifically refers to ownable and orderable
    /// elements of the game.
    /// </summary>
    public class GameObject
    {
        /// <summary>
        /// the unique id of the <c>GameObject</c>'s owner.
        /// </summary>
        public int OwnerId { get; set; }
        /// <summary>
        /// the <c>GameObjectType</c> of this <c>GameObject</c>.
        /// </summary>
        public GameObjectType Type { get; set; }
        /// <summary>
        /// the unique id of this <c>GameObject</c>.
        /// </summary>
        /// <remarks>
        /// this id is unique across factions but not types.
        /// </remarks>
        public int ObjectId { get; set; }
        /// <summary>
        /// the next order of this <c>GameObject</c>.
        /// </summary>
        public Order CurrentOrder { get; set; }



        private GameObject()
        {

        }

        /// <summary>
        /// Initializes an instance of the <c>GameObject</c> class.
        /// </summary>
        /// <param name="ownerId">
        /// <summary>
        /// the unique id of the <c>GameObject</c>'s owner.
        /// </summary>
        /// </param>
        /// <param name="type">
        /// <summary>
        /// the <c>GameObjectType</c> of this <c>GameObject</c>.
        /// </summary></param>
        public GameObject(int ownerId, GameObjectType type)
        {
            OwnerId = ownerId;
            Type = type;
            ObjectId = GameManager.RegisterGameObject(this);
        }

        /// <summary>
        /// Changes the owner of this gameobject.
        /// </summary>
        /// <param name="newOwner">
        /// the unique id of the new owner.
        /// </param>
        /// <remarks>
        /// Will probably need extra code when ownership of a GameObject
        /// changes such as clearing its current orders.
        /// </remarks>
        public void ChangeOwner(int newOwner)
        {
            OwnerId = newOwner;
        }
    }
}
