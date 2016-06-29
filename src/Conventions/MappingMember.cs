using System;
using System.Reflection;

namespace Wheatech.EmitMapper
{
    /// <summary>
    /// Represents the member(property or field) metadata of the mapping source or target type.
    /// </summary>
    public abstract class MappingMember : IMemberBuilder
    {
        /// <summary>
        /// Gets the class that declares this member.
        /// </summary>
        /// <value>The <see cref="Type"/> object for the class that declares this member.</value>
        public abstract Type DeclaringType { get; }

        /// <summary>
        /// Gets the name of the member.
        /// </summary>
        /// <value>The name of the member.</value>
        public abstract string MemberName { get; }

        /// <summary>
        /// Gets the type of value of the member.
        /// </summary>
        /// <value>The type of value of the member.</value>
        public abstract Type MemberType { get; }

        /// <summary>
        /// The original CLR reflection member.
        /// </summary>
        public abstract MemberInfo ClrMember { get; }

        /// <summary>
        /// Determines whether the member can be read.
        /// </summary>
        /// <param name="includeNonPublic"><c>true</c> if the non public accessor should be included, otherwise <c>false</c>.</param>
        /// <returns><c>true</c> if the member can be read, otherwise <c>false</c>.</returns>
        public abstract bool CanRead(bool includeNonPublic);

        /// <summary>
        /// Determines whether the member can be written.
        /// </summary>
        /// <param name="includeNonPublic"><c>true</c> if the non public accessor should be included, otherwise <c>false</c>.</param>
        /// <returns><c>true</c> if the member can be written, otherwise <c>false</c>.</returns>
        public abstract bool CanWrite(bool includeNonPublic);

        internal abstract void EmitSetter(CompilationContext context);

        internal abstract void EmitGetter(CompilationContext context);

        void IMemberBuilder.EmitGetter(CompilationContext context)
        {
            EmitGetter(context);
        }

        void IMemberBuilder.EmitSetter(CompilationContext context)
        {
            EmitSetter(context);
        }
    }
}
