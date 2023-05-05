using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using SystemKadr.Common.Logging;

namespace SystemKadr.Common
{
    [DataContract]
    public class XmlSerializer<T> where T : class
    {
        public virtual MemoryStream WriteObject()
        {
            using var log = Loger.Create<T>("WriteObject");

            DataContractSerializer serializer = new(typeof(T), XmlSerializerTypes.AsArray());

            using var output = new MemoryStream();
            using var writer = XmlDictionaryWriter.CreateTextWriter(output, Encoding.UTF8);

            serializer.WriteObject(writer, this);

            return output;
        }

        public virtual void ReadObject(Stream input)
        {
            using var log = Loger.Create<T>("ReadObject");

            DataContractSerializer serializer = new(typeof(T), XmlSerializerTypes.AsArray());

            using var reader = XmlDictionaryReader.CreateTextReader(input, new XmlDictionaryReaderQuotas());

            Initialize(serializer.ReadObject(reader, false) as T);
        }

        protected virtual void Initialize(T? obj)
        {
            using var log = Loger.Create<XmlSerializer<T>>("Initialize");

            if (obj != null)
            {
                typeof(T).GetFields().ToList().ForEach(x =>
                {
                    x?.SetValue(this, x?.GetValue(obj));
                });

                typeof(T).GetProperties().ToList().ForEach(x =>
                {
                    x?.SetValue(this, x?.GetValue(obj));
                });
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new();

            sb.Append($"{GetType().Name}:");

            typeof(T).GetFields().ToList().ForEach(x =>
            {
                sb.Append($"{x?.Name}={x?.GetValue(this)}|");
            });

            typeof(T).GetProperties().ToList().ForEach(x =>
            {
                sb.Append($"{x?.Name}={x?.GetValue(this)}|");
            });

            return sb.ToString();
        }
    }

    public static class XmlSerializerTypes
    {
        private static readonly List<Type> _typesList = new() { typeof(object) };

        public static void Register<T>()
        {
            var type = typeof(T);

            if (!_typesList.Contains(type))
                _typesList.Add(type);
        }
        public static Type[] AsArray() => _typesList.ToArray();
    }
}
