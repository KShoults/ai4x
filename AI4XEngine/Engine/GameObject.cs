namespace Engine
{
    /// <summary>
    /// Class <c>GameObject</c> specifically refers to ownable and orderable
    /// elements of the game.
    /// </summary>
    public abstract class GameObject
    {
        /// <summary>
        /// the <c>GameObject</c>'s owner.
        /// </summary>
        public Faction Owner { get; set; }
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
        /// <param name="owner">
        /// <summary>
        /// an instance of the <c>Faction</c> class owning this object.
        /// </summary>
        /// </param>
        /// <param name="type">
        /// <summary>
        /// the <c>GameObjectType</c> of this <c>GameObject</c>.
        /// </summary></param>
        public GameObject(Faction owner, GameObjectType type)
        {
            Owner = owner;
            Type = type;
            ObjectId = GameManager.RegisterGameObject(this);
        }

        /// <summary>
        /// Changes the owner of this gameobject.
        /// </summary>
        /// <param name="newOwner">
        /// an instance of the <c>Faction</c> class to change ownership to.
        /// </param>
        /// <remarks>
        /// Will probably need extra code when ownership of a GameObject
        /// changes such as clearing its current orders.
        /// </remarks>
        public void ChangeOwner(Faction newOwner)
        {
            Owner = newOwner;
        }

        /// <summary>
        /// Applies a new order and calculates the gameobject's state for the
        /// next turn.
        /// </summary>
        /// <param name="newOrder">
        /// the new orders for this object (optional).
        /// </param>
        /// <returns></returns>
        public abstract int EndTurn(Order newOrder = null);
    }
}
