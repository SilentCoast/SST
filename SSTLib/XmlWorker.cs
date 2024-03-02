using System.Xml;

namespace SSTLib
{
    public static class XmlWorker
    {
        public static XmlDocument Load(string filename)
        {
            var doc = new XmlDocument();
            doc.Load(filename);
            return doc;
        }
        public static void Save(string filename,XmlDocument doc)
        {
            doc.Save(filename);
        }
    }
}
