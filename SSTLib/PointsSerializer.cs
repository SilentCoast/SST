using System;
using System.Collections.Generic;
using System.Xml;

namespace SSTLib
{
    public class PointsSerializer : IPointsSerializer<ThePoint>
    {
        private static class Glossary
        {
            public static string points => "points";
            public static string point => "point";
            public static string x => "x";
            public static string y => "y";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        /// <exception cref="XmlException"></exception>
        /// <exception cref="InvalidCastException"></exception>
        public IList<ThePoint> Deserialize(XmlDocument document)
        {
            var Points = new List<ThePoint>();
            foreach (XmlElement child in document.ChildNodes)
            {
                if (child.Name == Glossary.points)
                {
                    foreach (XmlElement point in child.ChildNodes)
                    {
                        Points.Add(new ThePoint(
                            Convert.ToDouble(point.GetAttribute(Glossary.x)),
                            Convert.ToDouble(point.GetAttribute(Glossary.y))));
                    }
                }
            }
            return Points;
        }

        public XmlDocument Serialize(IList<ThePoint> points)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement pointsElement = doc.CreateElement(Glossary.points);
            doc.AppendChild(pointsElement);
            foreach (ThePoint point in points)
            {
                XmlElement pointNode = doc.CreateElement(Glossary.point);
                pointNode.SetAttribute(Glossary.x, point.X.ToString());
                pointNode.SetAttribute(Glossary.y, point.Y.ToString());
                pointsElement.AppendChild(pointNode);
            }
            return doc;
        }
    }
}
