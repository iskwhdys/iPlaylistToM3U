using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.Xml.Serialization;

using System.Xml;
using System.Xml.Schema;
using System.IO;


namespace musikkuLibrary.Libs {

	public class Track {
		public int Id { get; set; } = -1;
		public string Name { get; set; } = string.Empty;
		public string Location { get; set; } = string.Empty;

		public string Artist { get; set; } = string.Empty;
		public string AlbumArtist { get; set; } = string.Empty;
		public string Composer { get; set; } = string.Empty;
		public string Album { get; set; } = string.Empty;
		public string Grouping { get; set; } = string.Empty;

		public int DiscNumber { get; set; } = 0;
		public int DiscCount { get; set; } = 0;

		public int TrackNumber { get; set; } = 0;
		public int TrackCount { get; set; } = 0;

		public int PlayCount { get; set; } = 0;
		public int SkipCount { get; set; } = 0;

		public int Rating { get; set; } = 0;

		public string Comments { get; set; } = string.Empty;

		public object Tag { get; set; }

		public override string ToString() {
			return Id + ", " + Name;
		}


		public string Kind { get; set; } = string.Empty;
		public int Size { get; set; }
		public int TotalTime { get; set; }
		public int Year { get; set; }
		public DateTime DateModified { get; set; }
		public DateTime DateAdded { get; set; }
		public int BitRate { get; set; }
		public int SampleRate { get; set; }
		public int PlayDate { get; set; }
		public DateTime PlayDateUTC { get; set; }
		public DateTime SkipDate { get; set; }
		public int Normalization { get; set; }
		public bool Compilation { get; set; }
		public int ArtworkCount { get; set; }
		public string PersistentID { get; set; } = string.Empty;
		public string TrackType { get; set; } = string.Empty;
		public int FileFolderCount { get; set; }
		public int LibraryFolderCount { get; set; }
		public string SortName { get; set; } = string.Empty;
		public int AlbumRating { get; set; }
		public bool AlbumRatingComputed { get; set; }
		public string SortAlbum { get; set; } = string.Empty;
		public int StopTime { get; set; }
		public int VolumeAdjustment { get; set; }
		public string Genre { get; set; } = string.Empty;
		public int StartTime { get; set; }
		public int BPM { get; set; }
		public string SortArtist { get; set; } = string.Empty;
		public bool HasVideo { get; set; }
		public bool HD { get; set; }
		public int VideoWidth { get; set; }
		public int VideoHeight { get; set; }
		public bool RatingComputed { get; set; }

		public static string SecToTimeString(int sec) {
			return new TimeSpan(0, 0, 0, sec).ToString(@"mm\:ss");
		}

		public static string MsecToTimeString(int msec) {
			return new TimeSpan(0, 0, 0, 0, msec).ToString(@"mm\:ss");
		}

		public object[] GetDatas() {
			return new object[]{
				Name,
				Id,
				Artist,
				AlbumArtist,
				Composer,
				Album,
				Grouping,
				DiscNumber,
				DiscCount,
				TrackNumber,
				TrackCount,
				Location,
				Rating,
				PlayCount,
				SkipCount,
				Comments,
				Kind,
				Size,
				Track.MsecToTimeString(TotalTime),
				Year,
				DateModified,
				DateAdded,
				BitRate,
				SampleRate,
				PlayDate,
				PlayDateUTC,
				SkipDate,
				Normalization,
				Compilation,
				ArtworkCount,
				PersistentID,
				TrackType,
				FileFolderCount,
				LibraryFolderCount,
				SortName,
				AlbumRating,
				AlbumRatingComputed,
				SortAlbum,
				StopTime,
				VolumeAdjustment,
				Genre,
				StartTime,
				BPM,
				SortArtist,
				HasVideo,
				HD,
				VideoWidth,
				VideoHeight,
				RatingComputed };
		}

		//public static string[] GetDataNames() {
		//	return new[] {
		//		"Name",
		//		"Id",
		//		"Artist",
		//		"AlbumArtist",
		//		"Composer",
		//		"Album",
		//		"Grouping",
		//		"DiscNumber",
		//		"DiscCount",
		//		"TrackNumber",
		//		"TrackCount",
		//		"Location",
		//		"Rating",
		//		"PlayCount",
		//		"SkipCount",
		//		"Comments",
		//		"Kind",
		//		"Size",
		//		"TotalTime",
		//		"Year",
		//		"DateModified",
		//		"DateAdded",
		//		"BitRate",
		//		"SampleRate",
		//		"PlayDate",
		//		"PlayDateUTC",
		//		"SkipDate",
		//		"Normalization",
		//		"Compilation",
		//		"ArtworkCount",
		//		"PersistentID",
		//		"TrackType",
		//		"FileFolderCount",
		//		"LibraryFolderCount",
		//		"SortName",
		//		"AlbumRating",
		//		"AlbumRatingComputed",
		//		"SortAlbum",
		//		"StopTime",
		//		"VolumeAdjustment",
		//		"Genre",
		//		"StartTime",
		//		"BPM",
		//		"SortArtist",
		//		"HasVideo",
		//		"HD",
		//		"VideoWidth",
		//		"VideoHeight",
		//		"RatingComputed" };
		//}

		public static Track ConvertFromiTunes(XmlNode node) {
			Track t = new Track();
			foreach (XmlNode n in node) {
				string val = n.InnerText;
				switch (n.Name) {
					case "TrackID": t.Id = int.Parse(val) * -1; break;
					case "Name": t.Name = val; break;
					case "Artist": t.Artist = val; break;
					case "AlbumArtist": t.AlbumArtist = val; break;
					case "Composer": t.Composer = val; break;
					case "Album": t.Album = val; break;
					case "Grouping": t.Grouping = val; break;
					case "DiscNumber": t.DiscNumber = int.Parse(val); break;
					case "DiscCount": t.DiscCount = int.Parse(val); break;
					case "TrackNumber": t.TrackNumber = int.Parse(val); break;
					case "TrackCount": t.TrackCount = int.Parse(val); break;
					case "Location": t.Location = Uri.UnescapeDataString(val).Replace(@"file://localhost/", ""); break;
					case "Rating": t.Rating = int.Parse(val); break;
					case "PlayCount": t.PlayCount = int.Parse(val); break;
					case "SkipCount": t.SkipCount = int.Parse(val); break;
					case "Comments": t.Comments = val; break;

					case "Kind": t.Kind = val; break;
					case "Size": t.Size = int.Parse(val); break;
					case "TotalTime": t.TotalTime = int.Parse(val); break;
					case "Year": t.Year = int.Parse(val); break;
					case "DateModified": t.DateModified = DateTime.Parse(val); break;
					case "DateAdded": t.DateAdded = DateTime.Parse(val); break;
					case "BitRate": t.BitRate = int.Parse(val); break;
					case "SampleRate": t.SampleRate = int.Parse(val); break;
					case "PlayDate":
						//t.PlayDate = int.Parse(val);
						//var date =  new DateTime(long.Parse(val));
						//if (date == DateTime.Now) {
						//}
						break;
					case "PlayDateUTC": t.PlayDateUTC = DateTime.Parse(val); break;
					case "SkipDate": t.SkipDate = DateTime.Parse(val); break;
					case "Normalization": t.Normalization = int.Parse(val); break;
					case "Compilation": t.Compilation = bool.Parse(val); break;
					case "ArtworkCount": t.ArtworkCount = int.Parse(val); break;
					case "PersistentID": t.PersistentID = val; break;
					case "TrackType": t.TrackType = val; break;
					case "FileFolderCount": t.FileFolderCount = int.Parse(val); break;
					case "LibraryFolderCount": t.LibraryFolderCount = int.Parse(val); break;
					case "SortName": t.SortName = val; break;
					case "AlbumRating": t.AlbumRating = int.Parse(val); break;
					case "AlbumRatingComputed": t.AlbumRatingComputed = bool.Parse(val); break;
					case "SortAlbum": t.SortAlbum = val; break;
					case "StopTime": t.StopTime = int.Parse(val); break;
					case "VolumeAdjustment": t.VolumeAdjustment = int.Parse(val); break;
					case "Genre": t.Genre = val; break;
					case "StartTime": t.StartTime = int.Parse(val); break;
					case "BPM": t.BPM = int.Parse(val); break;
					case "SortArtist": t.SortArtist = val; break;
					case "HasVideo": t.HasVideo = bool.Parse(val); break;
					case "HD": t.HD = bool.Parse(val); break;
					case "VideoWidth": t.VideoWidth = int.Parse(val); break;
					case "VideoHeight": t.VideoHeight = int.Parse(val); break;
					case "RatingComputed": t.RatingComputed = bool.Parse(val); break;
				}
			}

			return t;
		}



	}


	
}
