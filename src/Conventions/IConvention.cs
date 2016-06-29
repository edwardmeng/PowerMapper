namespace Wheatech.EmitMapper
{
    /// <summary>
    /// The interface the type mapping conventions.
    /// </summary>
    public interface IConvention
    {
        /// <summary>
        /// Applies the convention to type member mappings.
        /// </summary>
        /// <param name="context">The context to apply conventions.</param>
        void Apply(ConventionContext context);

        /// <summary>
        /// Sets the convention to be read only.
        /// </summary>
        void SetReadOnly();
    }
}
