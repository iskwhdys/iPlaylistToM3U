using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace musikkuLibrary.Libs {

	public class Playlist {

		public int Id { get; set; } = -1;
		public int ParentId { get; set; } = -1;
		public string Name { get; set; }
		public string Description { get; set; }
		public EPlaylistType Type { get; set; }
		public List<int> Tracks { get; set; }
		public List<int> Childs { get; set; }

        [XmlIgnore]
        public object Tag { get; set; }

		public bool Open { get; set; } 
		public bool Check { get; set; }

		public override string ToString() {
			return Id + ", " + Name + ", " + string.Join(", ", Tracks);
		}
	}






}
