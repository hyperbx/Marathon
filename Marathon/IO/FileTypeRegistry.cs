using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Marathon.IO
{
    public class FileTypeRegistry
    {
        private static readonly Lazy<Dictionary<Type, FileTypeAttribute>> _attributes = new
        (
            () =>
            {
                return Assembly.GetExecutingAssembly().GetTypes()
                    .Where(x => typeof(FileBase).IsAssignableFrom(x))
                    .Select(x => new { Type = x, Attribute = x.GetCustomAttribute<FileTypeAttribute>() })
                    .Where(x => x.Attribute != null)
                    .ToDictionary(x => x.Type, x => x.Attribute);
            },

            isThreadSafe: true
        );

        public static Dictionary<Type, FileTypeAttribute> GetAttributes()
        {
            return _attributes.Value;
        }

        public static FileTypeAttribute GetAttribute(Type in_type)
        {
            return _attributes.Value[in_type];
        }

        public static FileTypeAttribute GetAttribute<T>()
        {
            return GetAttribute(typeof(T));
        }

        public static Type GetTypeFromPath(string in_path)
        {
            foreach (var attr in _attributes.Value)
            {
                if (!attr.Value.IsMatchingPattern(in_path))
                    continue;

                return attr.Key;
            }

            return null;
        }
    }
}
