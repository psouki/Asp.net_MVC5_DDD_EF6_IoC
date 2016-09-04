using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace RecipeMs.Infra.ImportData
{
    public class Deserializer
    {
        public static ICollection<TEntity> GetDesirialized<TEntity>(string path) where TEntity : class
        {
            ICollection<TEntity> result = new List<TEntity>();
            XmlSerializer deSerializer = new XmlSerializer(typeof(TEntity));

            using (XmlReader reader = XmlReader.Create(path))
            {
                reader.MoveToContent();
                while (reader.Read())
                {
                    if(reader.NodeType != XmlNodeType.Element) continue;
                    XElement el = XNode.ReadFrom(reader) as XElement;
                    if(el == null) continue;
                    StringReader sr = new StringReader(el.ToString());
                    TEntity entity = (TEntity) deSerializer.Deserialize(sr);
                    result.Add(entity);
                }

            }
            return result;
        }

    }
}
