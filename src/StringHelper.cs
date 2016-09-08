using System.Linq;

namespace PowerMapper
{
    /// <summary>
    /// Utility methods for <see cref="string"/>.
    /// </summary>
    public static class StringHelper
    {
        /// <summary>
        /// Indicates whether a specified string is <c>null</c>, empty, or consists only of white-space characters.
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <returns><c>true</c> if the value parameter is null or <see cref="System.String.Empty"/>, or if <paramref name="value"/> consists exclusively of white-space characters.</returns>
        public static bool IsNullOrWhiteSpace(string value)
        {
            return value == null || value.All(char.IsWhiteSpace);
        }
    }
}
