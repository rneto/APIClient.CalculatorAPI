using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace APIClient.CalculatorAPI.Common
{
    public class Serializer
    {
        public static XElement ToXElement<T>(T o)
        {
            return ToXDocument<T>(o, string.Empty).Root;
        }

        public static XDocument ToXDocument<T>(T o, string defaultNamespace)
        {
            XmlSerializer serializer;
            if (string.IsNullOrEmpty(defaultNamespace))
            {
                serializer = new XmlSerializer(o.GetType());
            }
            else
            {
                serializer = new XmlSerializer(o.GetType(), defaultNamespace);
            }

            XDocument target = new XDocument();
            using (XmlWriter writer = target.CreateWriter())
            {
                serializer.Serialize(writer, o);
            }

            return target;
        }

        public static T Deserialize<T>(XElement doc)
        {
            return Deserialize<T>(doc, null);
        }

        public static T Deserialize<T>(XElement doc, string rootElementName)
        {
            XmlRootAttribute rootAttribute = (!string.IsNullOrEmpty(rootElementName))
                ? new XmlRootAttribute(rootElementName)
                : null;

            XmlSerializer serializer = (rootAttribute != null)
                ? new XmlSerializer(typeof(T), rootAttribute)
                : new XmlSerializer(typeof(T));

            using (var reader = doc.CreateReader())
            {
                return (T)serializer.Deserialize(reader);
            }
        }
    }
}
