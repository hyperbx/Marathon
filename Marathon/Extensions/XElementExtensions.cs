using System;
using System.Xml.Linq;

namespace Marathon.Extensions
{
    public static class XElementExtensions
    {
        public static T GetAttributeValue<T>(this XElement in_element, string in_name, T in_defaultValue = default)
        {
            var attribute = in_element.Attribute(in_name);

            if (attribute == null)
                return in_defaultValue;

            return (T)Convert.ChangeType(attribute.Value, typeof(T));
        }

        public static T GetElementValue<T>(this XElement in_element, T in_defaultValue = default)
        {
            if (in_element == null)
                return in_defaultValue;

            return (T)Convert.ChangeType(in_element.Value, typeof(T));
        }

        public static T GetDescendantElementValue<T>(this XElement in_element, string in_name, T in_defaultValue = default)
        {
            var element = in_element.Element(in_name);

            if (element == null)
                return in_defaultValue;

            return (T)Convert.ChangeType(element.Value, typeof(T));
        }
    }
}
