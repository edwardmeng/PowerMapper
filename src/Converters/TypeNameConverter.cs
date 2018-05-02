#if !NETSTANDARD
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Compilation;
using System.Web.Hosting;

namespace PowerMapper
{
    internal static class TypeNameConverter
    {
        /// <summary>
        /// Returns the index of the comma separating the type from the assembly, or
        /// -1 of there is no assembly
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        private static int CommaIndexInTypeName(string typeName)
        {

            // Look for the last comma
            int commaIndex = typeName.LastIndexOf(',');

            // If it doesn't have one, there is no assembly
            if (commaIndex < 0)
                return -1;

            // It has a comma, we need to account for the generics syntax.
            // E.g. it could be "SomeType[int,string]

            // Check for a ]
            int rightBracketIndex = typeName.LastIndexOf(']');

            // If it has one, and it's after the last comma, there is no assembly
            if (rightBracketIndex > commaIndex)
                return -1;

            // The comma that we want is the first one after the last ']'
            commaIndex = typeName.IndexOf(',', rightBracketIndex + 1);

            // There is an assembly
            return commaIndex;
        }

        /// <summary>
        /// Returns true if the type string contains an assembly specification
        /// </summary>
        private static bool TypeNameContainsAssembly(ref string typeName, out string assemblyName)
        {
            assemblyName = null;
            var index = CommaIndexInTypeName(typeName);
            if (index > 0)
            {
                assemblyName = typeName.Substring(index + 1).Trim();
                typeName = typeName.Substring(0, index).Trim();
                return true;
            }
            return false;
        }

        /*
        * Look for a type by name in a collection of assemblies.  If it exists in multiple assemblies,
        * throw an error.
        */
        // Assert reflection in order to call assembly.GetType()
        private static Type GetTypeFromAssemblies(IEnumerable<Assembly> assemblies, string typeName, bool ignoreCase)
        {
            if (assemblies == null)
                return null;

            Type type = null;

            foreach (var assembly in assemblies)
            {
                var t = assembly.GetType(typeName, false /*throwOnError*/, ignoreCase);

                if (t == null)
                    continue;

                // If we had already found a different one, it's an ambiguous type reference
                if (type != null && t != type)
                {
                    throw new AmbiguousMatchException(string.Format(CultureInfo.CurrentCulture, Strings.Converter_AmbiguousTypes, typeName, GetAssemblyPathFromType(type), GetAssemblyPathFromType(t)));
                }

                // Keep track of it
                type = t;
            }

            return type;
        }

        /// <summary>
        /// Return a standard path from a file:// url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static string FilePathFromFileUrl(string url)
        {
            Uri uri = new Uri(url);
            string path = uri.LocalPath;
            return HttpUtility.UrlDecode(path);
        }

        private static string GetAssemblyPathFromType(Type t)
        {
            return FilePathFromFileUrl(t.Assembly.EscapedCodeBase);
        }

        /// <summary>
        /// Finds a type in the top-level assemblies, or in assemblies that are defined in configuration, 
        /// by using a case-insensitive search and optionally throwing an exception on failure.
        /// </summary>
        /// <param name="typeName">The name of the type.</param>
        /// <param name="throwOnError">
        /// <c>true</c> to throw an exception if a <see cref="Type"/> cannot be generated for the type name; otherwise, <c>false</c>.
        /// </param>
        /// <param name="ignoreCase"><c>true</c> if <paramref name="typeName"/> is case-sensitive; otherwise, <c>false</c>.</param>
        /// <returns>A <see cref="Type"/> object that represents the requested <paramref name="typeName"/> parameter.</returns>
        public static Type GetType(string typeName, bool throwOnError, bool ignoreCase)
        {
            if (string.IsNullOrEmpty(typeName) || typeName.Trim().Length == 0) return null;
            typeName = typeName.Trim();
            var originTypeName = typeName;
            Type type;
            Exception currentException = null;
            try
            {
                type = HostingEnvironment.IsHosted
                           ? BuildManager.GetType(typeName, throwOnError, ignoreCase)
                           : Type.GetType(typeName, throwOnError, ignoreCase);
                if (type != null) return type;
            }
            catch (Exception ex)
            {
                currentException = ex;
            }
            try
            {
                string assemblyNameString;
                if (TypeNameContainsAssembly(ref typeName, out assemblyNameString))
                {
                    var assemblyName = new AssemblyName(assemblyNameString);
                    var publicKeyToken = assemblyName.GetPublicKeyToken();
                    type = GetTypeFromAssemblies(AppDomain.CurrentDomain.GetAssemblies().Where(assembly =>
                    {
                        var name = assembly.GetName();
                        if (assemblyName.ProcessorArchitecture != ProcessorArchitecture.None)
                        {
                            return assemblyName.ProcessorArchitecture == name.ProcessorArchitecture;
                        }
                        if (publicKeyToken != null)
                        {
                            var publicKey = name.GetPublicKeyToken();
                            if (publicKey == null || !publicKeyToken.SequenceEqual(publicKey)) return false;
                        }
                        var cultureName = assemblyName.CultureInfo?.Name;
                        if (!string.IsNullOrEmpty(cultureName) && string.Equals(cultureName, "neutral", StringComparison.OrdinalIgnoreCase))
                        {
                            return string.Equals(cultureName, name.CultureInfo?.Name);
                        }
                        if (assemblyName.Version != null)
                        {
                            return assemblyName.Version == name.Version;
                        }
                        return assemblyName.Name == name.Name;
                    }), typeName, ignoreCase);
                }
                else
                {
                    type = GetTypeFromAssemblies(AppDomain.CurrentDomain.GetAssemblies(), typeName, ignoreCase);
                }
                if (type == null)
                {
                    if (throwOnError)
                    {
                        if (currentException != null)
                        {
                            throw currentException;
                        }
                        throw new TypeLoadException(string.Format(CultureInfo.CurrentCulture, Strings.Converter_CannotFindType, originTypeName));
                    }
                }
                return type;
            }
            catch (Exception)
            {
                if (throwOnError)
                {
                    throw;
                }
                return null;
            }
        }
    }
}
#endif