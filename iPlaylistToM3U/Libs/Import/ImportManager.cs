using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;
using System.Xml.Schema;
using System.IO;
using System.Xml.Serialization;

namespace musikkuLibrary.Libs.Import {

	public static class ImportManager {

		public static void ImportFromiTunes(string xmlPath, Library lib) {

			XmlDocument xml = new XmlDocument();
			xml.Load(xmlPath);

			var ixc = new iTunesConverter();
			var playlistNode = ixc.GetConvertedPlaylistXmlNode(xml);

			var pList = new List<Playlist>();

			foreach (XmlNode node in playlistNode) {
				var p = ixc.ToMusikkuPlaylist(node);
				p.Tag = node;
				pList.Add(p);
				lib.RegistPlaylist(p);
			}

			foreach (var p in pList) {

				string parentId = null;

				foreach (XmlNode node in (p.Tag as XmlNode).ChildNodes) {
					if (node.Name == "ParentPersistentID") {
						parentId = node.InnerText;
						break;
					}
				}

				if (parentId == null) continue;

				foreach (var pl in pList) {
					foreach (XmlNode child in (pl.Tag as XmlNode).ChildNodes) {
						if (child.Name == "PlaylistPersistentID" && child.InnerText == parentId) {
							p.ParentId = pl.Id;
							pl.Childs.Add(p.Id);
							break;
						}
					}
					if (p.ParentId != -1) {
						break;
					}
				}
			}

			foreach (var p in pList) {
				p.Tag = null;
			}
			
			var trackNode = ixc.GetConvertedTracksXmlNode(xml);
			foreach (XmlNode node in trackNode) {
				lib.RegistTrack(Track.ConvertFromiTunes(node));
			}
		}
	}
}

