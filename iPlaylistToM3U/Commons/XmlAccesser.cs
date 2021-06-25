using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Serialization;
using System.IO;

namespace iPlaylistToM3U.Commons {

	public static class XmlAccesser {

		public static T Deserialize<T>(string path) {
			XmlSerializer serializer = new XmlSerializer(typeof(T));

			StreamReader sr = new StreamReader(path, new UTF8Encoding(false));

			T obj = (T)serializer.Deserialize(sr);

			sr.Close();

			return obj;

		}

		public static void Serialize<T>(string path, T obj) {

			XmlSerializer serializer = new XmlSerializer(typeof(T));

			StreamWriter sw = new StreamWriter(path, false, new UTF8Encoding(false));

			serializer.Serialize(sw, obj);

			sw.Close();

		}
	}
}
