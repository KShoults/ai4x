using System.Collections.Generic;

namespace Engine
{
    /// <summary>
    /// Class <c>Colony</c> represents a faction's control over a system.
    /// </summary>
    public class Colony : GameObject
    {
        /// <summary>
        /// A list of DistrictTypes representing the districts that have been
        /// built.
        /// </summary>
        public List<DistrictType> Districts { get; set; } = new List<DistrictType>();

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


    }
}
