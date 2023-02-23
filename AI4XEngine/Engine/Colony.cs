using System.Collections.Generic;

namespace Engine
{
    /// <summary>
    /// Class <c>Colony</c> represents a faction's control over a system.
    /// </summary>
    public class Colony : GameObject
    {
        /// <summary>
        /// A list of <c>District</c> instances representing the districts
        /// that have been built.
        /// </summary>
        public List<District> Districts { get; set; } = new List<District>();
        /// <summary>
        /// The instance of the <c>StarSystem</c> class where the colony is
        /// located.
        /// </summary>
        public StarSystem ColonyLocation { get; set; }

        /// <summary>
        /// Instantiate an instance of class <c>Colony</c> with the given
        /// ownerId and a GameObjectType of colony.
        /// </summary>
        /// <param name="ownerId">
        /// An int referencing the id of the owning faction.
        /// </param>
        public Colony(int ownerId) : base(ownerId, GameObjectType.Colony)
        {

        }

        /// <summary>
        /// Applies a new order to this colony and calculates the
        /// colony's state for the next turn.
        /// </summary>
        /// <param name="newOrder">
        /// the new orders for this colony (optional).
        /// </param>
        /// <returns></returns>
        public override int EndTurn(Order newOrder = null)
        {
            if (newOrder != null)
                CurrentOrder = newOrder;
            
            return 0;
        }
    }
}
