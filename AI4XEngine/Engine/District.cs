namespace Engine
{
    /// <summary>
    /// Struct <c>District</c> represents a single district that can
    /// be built at a colony.
    /// </summary>
    public struct District
    {
        /// <summary>
        /// The type of this district.
        /// </summary>
        public DistrictType DistrictType { get; set; }
        /// <summary>
        /// The instance of the <c>Colony</c> class where the district is
        /// located.
        /// </summary>
        public Colony OwningColony { get; set; }
        /// <summary>
        /// The integer amount of refined metals allocated to the construction
        /// of this district.
        /// </summary>
        public int Progress { get; set; }
    }
}
