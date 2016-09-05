using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PowerMapper.Properties;

namespace PowerMapper
{
    /// <summary>
    /// Represents a collection of convention objects that inherit from <see cref="IConvention"/>.
    /// </summary>
    public sealed class ConventionCollection
    {
        private readonly List<IConvention> _conventions = new List<IConvention>();
        private bool _readonly;

        private void CheckReadOnly()
        {
            if (_readonly)
            {
                throw new NotSupportedException(Strings.Collection_ReadOnly);
            }
        }

        internal void SetReadOnly()
        {
            if (!_readonly)
            {
                _readonly = true;
                _conventions.ForEach(conversation => conversation.SetReadOnly());
            }
        }

        /// <summary>
        /// Removes all conventions from the collection.
        /// </summary>
        public void Clear()
        {
            CheckReadOnly();
            _conventions.Clear();
        }

        /// <summary>
        /// Adds a convention to the collection.
        /// </summary>
        /// <param name="convention"></param>
        public void Add(IConvention convention)
        {
            CheckReadOnly();
            if (convention == null)
            {
                throw new ArgumentNullException(nameof(convention));
            }
            _conventions.Add(convention);
        }

        /// <summary>
        /// Adds a convention to the collection through a callback expression.
        /// </summary>
        /// <param name="action">The callback expression to apply the convention.</param>
        public void Add(Action<ConventionContext> action)
        {
            CheckReadOnly();
            Add(new LambdaConvention(action));
        }

        /// <summary>
        /// Adds a convention to the collection through the type of the convention.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool Add<T>()
            where T : IConvention, new()
        {
            CheckReadOnly();
            if (!Contains<T>())
            {
                Add(new T());
                return true;
            }
            return false;
        }

        /// <summary>
        /// Remove all the conventions with the specified type.
        /// </summary>
        /// <param name="conventionType">The specified type of the conventions to be removed.</param>
        /// <returns>The number of conventions removed from the <see cref="ConventionCollection"/>.</returns>
        public int Remove(Type conventionType)
        {
            CheckReadOnly();
            if (conventionType == null)
            {
                throw new ArgumentNullException(nameof(conventionType));
            }
#if NetCore
            return _conventions.RemoveAll(convention => conventionType.GetTypeInfo().IsInstanceOfType(convention));
#else
            return _conventions.RemoveAll(conventionType.IsInstanceOfType);
#endif
        }

        /// <summary>
        /// Remove all the conventions with the specified type.
        /// </summary>
        /// <typeparam name="T">The specified type of the conventions to be removed.</typeparam>
        /// <returns>The number of conventions removed from the <see cref="ConventionCollection"/>.</returns>
        public int Remove<T>()
               where T : IConvention
        {
            CheckReadOnly();
            return Remove(typeof(T));
        }

        /// <summary>
        /// Removes the first occurrence of a specific convention from the <see cref="ConventionCollection"/>.
        /// </summary>
        /// <param name="convention">The convention to remove from the <see cref="ConventionCollection"/>.</param>
        /// <returns><c>true</c> if <paramref name="convention"/> is successfully removed; otherwise, <c>false</c>.</returns>
        public bool Remove(IConvention convention)
        {
            CheckReadOnly();
            if (convention == null)
            {
                throw new ArgumentNullException(nameof(convention));
            }
            return _conventions.Remove(convention);
        }

        /// <summary>
        /// Retrieves the first convention occurrence of the specified type for the configuration purpose.
        /// </summary>
        /// <param name="conventionType">The specified type to retrieve the convention.</param>
        /// <returns>The first convention occurrence of the specified type if there is any convention is the type of <paramref name="conventionType"/>, otherwise <see langword="null"/>.</returns>
        public object Configure(Type conventionType)
        {
            CheckReadOnly();
            if (conventionType == null)
            {
                throw new ArgumentNullException(nameof(conventionType));
            }
#if NetCore
            return _conventions.FirstOrDefault(convention => conventionType.GetTypeInfo().IsInstanceOfType(convention));
#else
            return _conventions.FirstOrDefault(conventionType.IsInstanceOfType);
#endif
        }

        /// <summary>
        /// Retrieves the first convention occurrence of the specified type for the configuration purpose.
        /// </summary>
        /// <typeparam name="T">The specified type to retrieve the convention.</typeparam>
        /// <returns>The first convention occurrence of the specified type if there is any convention is the type of <typeparamref name="T"/>, otherwise the default value of <typeparamref name="T"/>.</returns>
        public T Configure<T>()
            where T : IConvention
        {
            return (T)Configure(typeof(T));
        }

        /// <summary>
        /// Determines whether any convention is the specified type.
        /// </summary>
        /// <param name="conventionType">The specified type to examine the conventions.</param>
        /// <returns><c>true</c> if any convention type is <paramref name="conventionType"/>; otherwise, <c>false</c>.</returns>
        public bool Contains(Type conventionType)
        {
            if (conventionType == null)
            {
                throw new ArgumentNullException(nameof(conventionType));
            }
#if NetCore
            return _conventions.Any(convention => conventionType.GetTypeInfo().IsInstanceOfType(convention));
#else
            return _conventions.Any(conventionType.IsInstanceOfType);
#endif
        }

        /// <summary>
        /// Determines whether any convention is the specified type.
        /// </summary>
        /// <typeparam name="T">The specified type to examine the conventions.</typeparam>
        /// <returns><c>true</c> if any convention type is <typeparamref name="T"/>; otherwise, <c>false</c>.</returns>
        public bool Contains<T>()
               where T : IConvention
        {
            return Contains(typeof(T));
        }

        internal void Apply(ConventionContext context)
        {
            foreach (var convention in _conventions)
            {
                convention.Apply(context);
            }
        }
    }
}
