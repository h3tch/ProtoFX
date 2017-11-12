using System.Collections.Generic;

namespace System.Reflection
{
    public static class AssemblyExtensions
    {
        public static IEnumerable<TypeInfo> GetTypesByAttribute(this Assembly assembly,
            string attrName, StringComparison comparisonType = StringComparison.CurrentCulture)
        {
            foreach (var type in assembly.DefinedTypes)
            {
                if (type.GetAttribute(attrName, comparisonType) != null)
                    yield return type;
            }
        }
    }
}
