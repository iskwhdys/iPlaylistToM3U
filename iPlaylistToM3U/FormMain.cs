﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;

using musikkuLibrary.Libs;
using musikkuLibrary.Libs.Import;
using iPlaylistToM3U.Commons;

namespace iPlaylistToM3U
{
	public partial class FormMain : Form
	{

		private Library library = new Library();

		public FormMain() {
			InitializeComponent();


			if (File.Exists(Const.Setting.LibraryXmlPath)) {
				library = XmlAccesser.Deserialize<Library>(Const.Setting.LibraryXmlPath);
				tvPlaylist.Nodes.Clear();
				SetPlaylist(library, tvPlaylist.Nodes, -1);
			}
			else {
				library = new Library();
			}

			tbLibraryXmlPath.Text = library.TargetLibraryXmlPath;
			tbCopyTarget.Text = library.CopyTarget;
		}

		private void btnReadLibraryXml_Click(object sender, EventArgs e) {

			if (File.Exists(tbLibraryXmlPath.Text) == false) {
				MessageBox.Show(tbLibraryXmlPath.Text + "が存在しません。");
				return;
			}


			tvPlaylist.Nodes.Clear();
			if (library != null) {
				library = new Library();
			}

			var im = new ImportManager();
			im.ImportFromiTunes(tbLibraryXmlPath.Text, library);

			SetPlaylist(library, tvPlaylist.Nodes, -1);
		}

		private void SetPlaylist(Library lib, TreeNodeCollection tnc, int id) {
			foreach (var child in lib.Playlists.Where(p => p.ParentId == id)) {

				var node = new PlaylistNode(child);

				node.SetTrackItem(lib);

				tnc.Add(node);

				SetPlaylist(lib, node.Nodes, child.Id);

				if (child.Open) {
					node.Expand();
				}
			}
		}

		private void tvPlaylist_AfterExpand(object sender, TreeViewEventArgs e) {
			(e.Node as PlaylistNode).Playlist.Open = true;
		}

		private void tvPlaylist_AfterCollapse(object sender, TreeViewEventArgs e) {
			(e.Node as PlaylistNode).Playlist.Open = false;
		}
		private void tvPlaylist_AfterSelect(object sender, TreeViewEventArgs e) {
			tbTracks.Text = string.Join(
				Environment.NewLine, (e.Node as PlaylistNode).Playlist.Tracks.Select(
					t => library.Tracks.FirstOrDefault(track => track.Id == t).Name
				)
			);
		}

		private void btnOpenLibraryXml_Click(object sender, EventArgs e) {
			var res = ofdLibraryImport.ShowDialog();
			if (res == DialogResult.Cancel) return;

			tbLibraryXmlPath.Text = ofdLibraryImport.FileName;
		}

		bool isParentChecking = false;
		private void tvPlaylist_AfterCheck(object sender, TreeViewEventArgs e) {

			(e.Node as PlaylistNode).Playlist.Check = e.Node.Checked;
			CheckedChildNodes(e.Node as PlaylistNode);
			isParentChecking = true;
			CheckedParentNodes(e.Node as PlaylistNode);
			isParentChecking = false;
		}

		private void CheckedChildNodes(PlaylistNode node) {
			if (isParentChecking) return;
			foreach (PlaylistNode pNode in node.Nodes) {
				pNode.Checked = node.Checked;
				pNode.Playlist.Check = node.Checked;

				CheckedChildNodes(pNode);
			}
		}

		private void CheckedParentNodes(PlaylistNode node) {

			if (node.Parent != null && node.Checked) {
				var pNode = node.Parent as PlaylistNode;
				pNode.Checked = node.Checked;
				pNode.Playlist.Check = node.Checked;
				//CheckedParentNodes(pNode);
			}
		}


		private void FormMain_FormClosing(object sender, FormClosingEventArgs e) {

			library.CopyTarget = tbCopyTarget.Text;
			library.TargetLibraryXmlPath = tbLibraryXmlPath.Text;
			XmlAccesser.Serialize(Const.Setting.LibraryXmlPath, library);
		}

		private void btnOpenCopyTarget_Click(object sender, EventArgs e) {
			var res = ofdTargetFolder.ShowDialog();
			if (res == DialogResult.Cancel) return;

			tbCopyTarget.Text = ofdTargetFolder.SelectedPath;
		}

		private int PlaylistCreateMax;
		private int PlaylistCreateCount;

		private string MediaRoot;
		private void btnCopy_Click(object sender, EventArgs e) {
			if (Directory.Exists(tbCopyTarget.Text) == false) {
				MessageBox.Show(tbCopyTarget.Text + "が存在しません。");
				return;
			}

			btnCopy.Enabled = false;

			string playlistPath = Path.Combine(tbCopyTarget.Text, "Playlist");
			Directory.CreateDirectory(Path.Combine(tbCopyTarget.Text, "PlaylistItem"));

			MediaRoot = "/storage/sdcard1/" + tbCopyTarget.Text.Remove(0, 3).Replace("\\", "/") + "/PlaylistItem/";

			var createDirPathList = new List<string>();
			foreach (PlaylistNode node in tvPlaylist.Nodes) {
				CreatePlaylistFolderDirectory(createDirPathList, playlistPath, node.Playlist);
			}

			PlaylistCreateMax = library.Playlists.Count(p => p.Check);
			var rootPlaylistList = new List<Playlist>();
			foreach (PlaylistNode item in tvPlaylist.Nodes) {
				rootPlaylistList.Add(item.Playlist);
			}

			bgwCopy.RunWorkerAsync(new object[] { playlistPath, createDirPathList, rootPlaylistList });
		}

		private void bgwCopy_DoWork(object sender, DoWorkEventArgs e) {
			var playlistPath = (e.Argument as object[])[0] as string;
			if (Directory.Exists(playlistPath)) {
				Directory.Delete(playlistPath, true);
			}
			Directory.CreateDirectory(playlistPath);

			var createDirPath = (e.Argument as object[])[1] as List<string>;
			for (int i = 0; i < createDirPath.Count; i++) {
				Directory.CreateDirectory(createDirPath[i]);
				bgwCopy.ReportProgress((int)(((double)i) / ((double)createDirPath.Count) * 100.0) + 1000);
			}

			PlaylistCreateCount = 0;
			var rootPlaylist = (e.Argument as object[])[2] as List<Playlist>;
			foreach (var playlist in rootPlaylist) {
				CreatePlaylistFile(playlistPath, playlist);
			}

			var copyTrack = new List<int>();
			foreach (var playlist in library.Playlists.Where(p => p.Check)) {
				foreach (var track in GetCheckedPlaylistTrack(playlist)) {
					if (copyTrack.Contains(track.Id) == false) {
						copyTrack.Add(track.Id);
					}
				}
			}

			string playlistItemPath = Path.Combine(tbCopyTarget.Text, "PlaylistItem");

			foreach (var file in Directory.GetFiles(playlistItemPath,"*",SearchOption.AllDirectories)) {
				int existFileId = int.Parse(Path.GetFileNameWithoutExtension(file));
				if (copyTrack.Contains(existFileId) == false) {
					File.Delete(file);
				}
				else {
					copyTrack.Remove(existFileId);
				}
			}
			

			for (int i = 0; i < copyTrack.Count; i++) {
				int id = copyTrack[i];
				var track = library.Tracks.First(t => t.Id == id);

				string dstDir = GetPlaylistItemPath(playlistItemPath, id);
				if (Directory.Exists(dstDir) == false) {
					Directory.CreateDirectory(dstDir);
				}

				string target = Path.Combine(dstDir, id + Path.GetExtension(track.Location));

				File.Copy(track.Location, target);
				bgwCopy.ReportProgress((int)(((double)i) / ((double)copyTrack.Count) * 100.0) + 3000);

			}
		}

		private string GetPlaylistItemIds(int num) {
			string id = num.ToString();

			if (id.Length == 5) {
				return id.Substring(0, 1) + "0000/" + id.Substring(0, 3) + "00/";
			}
			else if (id.Length == 4) {
				return "00000/" +  "0" + id.Substring(0, 2) + "00/";
			}
			else if (id.Length == 3) {
				return "00000/" + "00" + id[0] + "00/";
			}
			else if (id.Length < 3) {
				return "00000/00000/";
			}

			return "";
		}

		private string GetPlaylistItemPath(string orgPath, int num) {
			string id = num.ToString();

			if(id.Length == 5) {
				return Path.Combine(orgPath, id.Substring(0, 1) + "0000", id.Substring(0, 3) + "00");
			}
			else if(id.Length == 4) {
				return Path.Combine(orgPath, "00000", "0" + id.Substring(0, 2) + "00");
			}
			else if(id.Length == 3) {
				return Path.Combine(orgPath, "00000", "00" + id[0] + "00");
			}
			else if(id.Length < 3) {
				return Path.Combine(orgPath, "00000", "00000");
			}

			return orgPath;
		}


		private void bgwCopy_ProgressChanged(object sender, ProgressChangedEventArgs e) {
			lblStatus.Text = e.ProgressPercentage + " / 100";
		}

		private void bgwCopy_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
			lblStatus.Text = "完了";
			btnCopy.Enabled = true; ;

		}
		private void CreatePlaylistFolderDirectory(List<string> createDirPath, string currentPath, Playlist playlist) {
			if (playlist.Check == false) return;
			if (playlist.Type != EPlaylistType.Folder) return;

			string createPath = Path.Combine(currentPath, RemoveInvalidPathCharacter(playlist.Name));
			createDirPath.Add(createPath);


			foreach (int childPId in library.Playlists.First(p => p.Id == playlist.Id).Childs) {
				var pl = library.Playlists.First(p => p.Id == childPId);
				CreatePlaylistFolderDirectory(createDirPath, createPath, pl);
			}
		}

		private void CreatePlaylistFile(string currentPath, Playlist playlist) {

			if (playlist.Check == false) return;
			string m3uPath;
			string playlistName = RemoveInvalidPathCharacter(playlist.Name);

			var str = new StringBuilder();
			
			if (playlist.Type == EPlaylistType.Folder) {
				m3uPath = Path.Combine(currentPath, "_" + playlistName + ".m3u");
				foreach (var track in GetCheckedPlaylistTrack(playlist)) {
					str.AppendLine("#EXTINF:-1," + (track.Artist == "" ? "アーティスト名なし" : track.Artist.Replace("-", "_")) + " - " + track.Name.Replace("-", "_"));
					str.AppendLine(MediaRoot + GetPlaylistItemIds(track.Id) + track.Id + Path.GetExtension(track.Location));
				}
			}
			else {
				m3uPath = Path.Combine(currentPath, playlistName + ".m3u");

				foreach (var id in playlist.Tracks) {
					Track track = library.Tracks.First(t => t.Id == id);
					str.AppendLine("#EXTINF:-1," + (track.Artist == "" ? "アーティスト名なし" : track.Artist.Replace("-", "_")) + " - " + track.Name.Replace("-", "_"));
					str.AppendLine(MediaRoot + GetPlaylistItemIds(id) + id + Path.GetExtension(track.Location));
				}
			}

			File.WriteAllText(m3uPath, str.ToString());
			PlaylistCreateCount++;
			bgwCopy.ReportProgress((int)(((double)PlaylistCreateCount) / ((double)PlaylistCreateMax) * 100.0) + 2000);

			foreach (int childPId in library.Playlists.First(p => p.Id == playlist.Id).Childs) {
				var pl = library.Playlists.First(p => p.Id == childPId);
				CreatePlaylistFile(Path.Combine(currentPath, playlistName), pl);
			}

		}

		private IEnumerable<Track> GetCheckedPlaylistTrack(Playlist playlist) {

			foreach (int pid in playlist.Childs) {
				var p = library.Playlists.First(t => t.Id == pid);
				if (p.Type == EPlaylistType.Folder) {
					foreach (var track in GetCheckedPlaylistTrack(p)) {
						yield return track;
					}
				}
				else if(p.Check) {
					foreach (int tid in p.Tracks) {
						Track track = library.Tracks.First(t => t.Id == tid);
						yield return track;
					}

				}
			}
		}

		private string RemoveInvalidPathCharacter(string path) {

			var invalidChars = Path.GetInvalidFileNameChars();
			var converted = string.Concat(
			  path.Select(c => invalidChars.Contains(c) ? '_' : c));
			return converted;

		}

		private void button1_Click(object sender, EventArgs e) {
			string baseDir = Path.Combine(tbCopyTarget.Text, "PlaylistItem");
			var files = Directory.GetFiles(baseDir, "*.*");
			foreach (var item in files) {
				int id = int.Parse( Path.GetFileNameWithoutExtension(item));

				string dstDir = GetPlaylistItemPath(baseDir, id);

				string target = Path.Combine(dstDir, Path.GetFileName(item));

				File.Move(item, target);

			}
		}
	}
}