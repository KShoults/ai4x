using System;

namespace Engine
{
    /// <summary>
    /// Represents a position in the sector. Inherits from Tuple{float, float}.
    /// </summary>
    /// <remarks>
    /// We needed a tuple wrapper with default values for serialization.
    /// </remarks>
    public class Position : Tuple<float, float>
    {
        /// <summary>
        /// Inititializes an instance of the <c>Position</c> class with default
        /// values.
        /// </summary>
        public Position() : base(0f, 0f)
        {

        }

        /// <summary>
        /// Initializes an instance of the <c>Positions</c> class with
        /// specified x and y values.
        /// </summary>
        /// <param name="xPosition">
        /// the first part of the backing tuple.
        /// </param>
        /// <param name="yPosition">
        /// the second part of the backing tuple.
        /// </param>
        public Position(float xPosition, float yPosition) : base(xPosition, yPosition)
        {

        }
    }
}
