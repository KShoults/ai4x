namespace Engine
{
    /// <summary>
    /// Class <c>Order</c> represents the instructions given to a unit
    /// that are executed during the end turn calculation.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// determines whether this order should be automatically
        /// reissued at the start of the next turn.
        /// </summary>
        public bool Repeating { get; set; }
        /// <summary>
        /// the faction id of the target's owner.
        /// </summary>
        public int TargetId { get; set; }
        /// <summary>
        /// determines the action to take for this order.
        /// </summary>
        public OrderType Type { get; set; }

        /// <summary>
        /// Instantiates an instance of class <c>Order</c> with the given
        /// value of repeating.
        /// </summary>
        /// <param name="repeating">
        /// a boolean determining whether the order is automatically
        /// reissued.
        /// </param>
        /// <param name="targetId">
        /// an int representing the faction id of the target's owner.
        /// </param>
        /// <param name="type">
        /// the <c>OrderType</c> of this order.
        /// </param>
        public Order(bool repeating, int targetId, OrderType type)
        {
            Repeating = repeating;
            TargetId = targetId;
            Type = type;
        }
    }
}
