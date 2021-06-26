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

	public class iTunesConverter {

		public Playlist ToMusikkuPlaylist(XmlNode node) {
			Playlist p = new Playlist();
			p.Type = EPlaylistType.Playlist;

			foreach (XmlNode n in node.ChildNodes) {
				string val = n.InnerText;
				if (string.IsNullOrEmpty(val)) continue;

				switch (n.Name) {
					case "PlaylistID": p.Id = int.Parse(val); break;
					case "Name": p.Name = val; break;
					case "Description": p.Description = val; break;
					case "PlaylistItems": p.Tracks = val.Split(',').Select(spl => int.Parse(spl)).ToList(); break;
					case "Folder": p.Type = EPlaylistType.Folder; break;
					case "Master": p.Type = EPlaylistType.System; break;
				}
			}
			return p;
		}

	
		public XmlNode GetConvertedPlaylistXmlNode(XmlDocument xml) {

			List<Dictionary<string, string>> playlists = new List<Dictionary<string, string>>();

			XmlNode root = GetPlaylistXmlNode(xml);

			foreach (XmlNode node in root.ChildNodes) {
				if (node.Name == "dict") {
					playlists.Add(GetPlaylistDatas(node));
				}
			}

			var dml = new XmlDocument();
			var playlistsElement = dml.CreateElement("Playlists");
			foreach (var playlist in playlists) {
				var playlistElement = dml.CreateElement("Playlist");
				foreach (var ti in playlist) {
					var ele = dml.CreateElement(ti.Key.Replace(" ", ""));
					ele.InnerText = ti.Value;
					playlistElement.AppendChild(ele);
				}
				playlistsElement.AppendChild(playlistElement);
			}
			return playlistsElement;

		}

		public XmlNode GetConvertedTracksXmlNode(XmlDocument xml) {

			List<Dictionary<string, string>> tracks = new List<Dictionary<string, string>>();

			XmlNode root = GetTracksXmlNode(xml);

			foreach (XmlNode node in root.ChildNodes) {
				if (node.Name == "dict") {
					tracks.Add(GetTrackDatas(node));
				}
			}

			var dml = new XmlDocument();
			var tracksElement = dml.CreateElement("Tracks");
			foreach (var track in tracks) {
				XmlElement trackElement = dml.CreateElement("Track");

				foreach (var ti in track) {
					var ele = dml.CreateElement(ti.Key.Replace(" ", ""));
					ele.InnerText = ti.Value;
					trackElement.AppendChild(ele);
				}

				tracksElement.AppendChild(trackElement);
			}
			return tracksElement;
		}


		private XmlNode GetPlaylistXmlNode(XmlDocument xml) {
			foreach (XmlNode top in xml.ChildNodes) {
				if (top.Name == "plist") {
					foreach (XmlNode plist in top.ChildNodes) {
						if (plist.Name == "dict") {
							foreach (XmlNode node in plist.ChildNodes) {
								if (node.Name == "array") {
									return node;
								}
							}
						}
					}

				}
			}
			return null;
		}

		private XmlNode GetTracksXmlNode(XmlDocument xml) {
			foreach (XmlNode top in xml.ChildNodes) {
				if (top.Name == "plist") {
					foreach (XmlNode plist in top.ChildNodes) {
						if (plist.Name == "dict") {
							foreach (XmlNode node in plist.ChildNodes) {
								if (node.Name == "dict") {
									return node;
								}
							}
						}
					}

				}
			}
			return null;
		}


		private Dictionary<string, string> GetTrackDatas(XmlNode dict) {

			var res = new Dictionary<string, string>();

			for (int i = 0; i < dict.ChildNodes.Count; i += 2) {
				XmlNode key = dict.ChildNodes[i];

				if (res.ContainsKey(key.InnerText)) {
					throw new Exception("キー重複");
				}

				if (i + 1 >= dict.ChildNodes.Count) {
					throw new Exception("項目数不一致");
				}

				XmlNode val = dict.ChildNodes[i + 1];

				if (string.IsNullOrEmpty(val.InnerText)) {
					res.Add(key.InnerText, val.Name);
				}
				else {
					res.Add(key.InnerText, val.InnerText);
				}
			}
			return res;
		}

		private Dictionary<string, string> GetPlaylistDatas(XmlNode dict) {

			var res = new Dictionary<string, string>();

			for (int i = 0; i < dict.ChildNodes.Count; i += 2) {
				XmlNode key = dict.ChildNodes[i];

				if (res.ContainsKey(key.InnerText)) {
					throw new Exception("キー重複");
				}

				if (i + 1 >= dict.ChildNodes.Count) {
					throw new Exception("項目数不一致");
				}

				if (key.InnerText == "Playlist Items") continue;

				XmlNode val = dict.ChildNodes[i + 1];

				if (string.IsNullOrEmpty(val.InnerText) && key.InnerText != "Description") {
					res.Add(key.InnerText, val.Name);
				}
				else {
					res.Add(key.InnerText, val.InnerText);
				}
			}

			var str = new List<string>();
			foreach (XmlNode arr in dict.ChildNodes[dict.ChildNodes.Count - 1].ChildNodes) {
				if (arr.ChildNodes.Count > 1) {
					str.Add(arr.ChildNodes[1].InnerText);
				}

			}

			res.Add("Playlist Items", string.Join(",", str));

			return res;
		}

	}
}
