using System.IO;

namespace Lab2
{
    public class XmlSerializer<T>
    {
        public XmlSerializer(string fileName = null)
        {
            _formatter = new System.Xml.Serialization.XmlSerializer(typeof(T));
            _fileName = fileName;
        }

        private readonly System.Xml.Serialization.XmlSerializer _formatter;
        private string _fileName;


        public void Serialize(T obj, string fileName = null)
        {
            if (fileName == null)
                fileName = _fileName;

            using (var stream = new FileStream(fileName, FileMode.Create))
            {
                _formatter.Serialize(stream, obj);
            }
        }

        public virtual T Deserialize()
        {
            if (!File.Exists(_fileName))
                return default(T);

            using (var stream = new FileStream(_fileName, FileMode.OpenOrCreate))
            {
                return (T)_formatter.Deserialize(stream);
            }
        }
    }
}
