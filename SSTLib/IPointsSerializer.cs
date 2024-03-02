using System.Collections.Generic;
using System.Xml;

namespace SSTLib
{
    public interface IPointsSerializer<T> where T : PointBase
    {
        XmlDocument Serialize(IList<T> points);
        IList<T> Deserialize(XmlDocument document);
    }
}
