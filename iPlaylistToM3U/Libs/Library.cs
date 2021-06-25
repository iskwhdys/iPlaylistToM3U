using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace musikkuLibrary.Libs {

	public class Library {

		public string TargetLibraryXmlPath { get; set; }

		public string CopyTarget { get; set; }

		public int CurrentTrackId { get; set; }

		public int CurrentPlaylistId { get; set; }

		public List<Track> Tracks { get; set; } = new List<Track>();

		public List<Playlist> Playlists { get; set; } = new List<Playlist>();

		public void RegistTrack(Track t) {

			t.Id = CurrentTrackId++;

			Tracks.Add(t);
		}

		public void RegistPlaylist(Playlist p) {

			p.Id = CurrentPlaylistId++;

			Playlists.Add(p);

		}

		public void Clear() {
			Playlists.ForEach(p => p.Tracks.Clear());
			Playlists.ForEach(p => p.Childs.Clear());
			Playlists.Clear();
			Tracks.Clear();
		}








	}
}
