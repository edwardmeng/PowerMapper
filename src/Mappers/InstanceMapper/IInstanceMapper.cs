namespace Wheatech.EmitMapper
{
    /// <summary>
    /// The instance mapping execution strategy.
    /// </summary>
    /// <typeparam name="TSource">The type of source object.</typeparam>
    /// <typeparam name="TTarget">The type of target object.</typeparam>
    public interface IInstanceMapper<in TSource, TTarget>
    {
        /// <summary>
        /// Execute a mapping from the source object to a new target object.
        /// </summary>
        /// <param name="source">Source object to map from</param>
        /// <returns>Mapped target object</returns>
        TTarget Map(TSource source);

        /// <summary>
        /// Execute a mapping from the source object to the existing target object.
        /// </summary>
        /// <param name="source">Source object to map from</param>
        /// <param name="target">Target object to map into</param>
        void Map(TSource source, TTarget target);
    }
}
