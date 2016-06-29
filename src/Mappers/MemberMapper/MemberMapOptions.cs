using System;

namespace Wheatech.EmitMapper
{
    /// <summary>
    /// Specify options that control the member matching algorithm.
    /// </summary>
    [Flags]
    public enum MemberMapOptions
    {
        /// <summary>
        /// The default strategy for member matching algorithm.
        /// </summary>
        Default = 0x00,
        /// <summary>
        /// Specifies that the case of the member name should not be considered when matching.
        /// </summary>
        IgnoreCase = 0x01,
        /// <summary>
        /// Specifies that non-public members are to be included in the matching algorithm.
        /// </summary>
        NonPublic = 0x02,
        /// <summary>
        /// Specifies that non-primitive members are to be included in the matching algorithm.
        /// </summary>
        Hierarchy = 0x04
    }
}
