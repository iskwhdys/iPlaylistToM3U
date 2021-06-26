using System;
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

			if (String.IsNullOrEmpty(tbLibraryXmlPath.Text)) {
				MessageBox.Show("「ライブラリ.xmlのパス」が入力されていません。");
				return;
			}
			if (File.Exists(tbLibraryXmlPath.Text) == false) {
				MessageBox.Show("ファイル「" + tbLibraryXmlPath.Text + "」が存在しません。");
				return;
			}

			tvPlaylist.Nodes.Clear();
			library = new Library();
			ImportManager.ImportFromiTunes(tbLibraryXmlPath.Text, library);

			SetPlaylist(library, tvPlaylist.Nodes, -1);
		}

		private void SetPlaylist(Library lib, TreeNodeCollection tnc, int id) {
			foreach (var child in lib.Playlists.Where(p => p.ParentId == id)) {

				var node = new PlaylistNode(child);

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

		private void btnCopy_Click(object sender, EventArgs e) {
			if (String.IsNullOrEmpty(tbCopyTarget.Text)) {
				MessageBox.Show("「コピー先」が入力されていません。");
				return;
			}
			if (Directory.Exists(tbCopyTarget.Text) == false) {
				MessageBox.Show("フォルダ「" + tbCopyTarget.Text + "」が存在しません。");
				return;
			}

			btnCopy.Enabled = false;
			btnCancelCopy.Enabled = true;

			string playlistPath = Path.Combine(tbCopyTarget.Text, "Playlist");
			Directory.CreateDirectory(Path.Combine(tbCopyTarget.Text, "PlaylistItem"));

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


		private void btnCancelCopy_Click(object sender, EventArgs e) {
			btnCancelCopy.Enabled = false;
			bgwCopy.CancelAsync();
			lblStatus.Text = "停止中...";
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
				if (bgwCopy.CancellationPending) {
					e.Cancel = true;
					return;
				}
				bgwCopy.ReportProgress(0, "ステップ(1/3) フォルダ作成中(" + i + "/" + createDirPath.Count + ")...");
			}

			PlaylistCreateCount = 0;
			var rootPlaylist = (e.Argument as object[])[2] as List<Playlist>;
			foreach (var playlist in rootPlaylist) {
				if (bgwCopy.CancellationPending) {
					e.Cancel = true;
					return;
				}
				CreatePlaylistFile(playlistPath, "../PlaylistItem/", playlist, e);
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

			foreach (var file in Directory.GetFiles(playlistItemPath, "*", SearchOption.AllDirectories)) {
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

				if (bgwCopy.CancellationPending) {
					e.Cancel = true;
					return;
				}
				bgwCopy.ReportProgress(0, "ステップ(3/3) ファイルコピー中(" + i + "/" + copyTrack.Count + ")...");
			}
		}

		private string GetPlaylistItemIds(int num) {
			return String.Join("/", num.ToString().Reverse()) + "/";
		}

		private string GetPlaylistItemPath(string orgPath, int num) {
			return  Path.Combine(orgPath, Path.Combine( num.ToString().Reverse().Select(c => c.ToString()).ToArray()));
		}


		private void bgwCopy_ProgressChanged(object sender, ProgressChangedEventArgs e) {
			lblStatus.Text = e.UserState.ToString();
		}

		private void bgwCopy_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
			if (e.Cancelled) {
				lblStatus.Text = "中止しました";
			}
			else {
				lblStatus.Text = "完了";
			}
			btnCopy.Enabled = true;
			btnCancelCopy.Enabled = false;
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

		private void CreatePlaylistFile(string currentPath, string relativePath, Playlist playlist, DoWorkEventArgs e) {
			if (bgwCopy.CancellationPending) {
				e.Cancel = true;
				return;
			}
			bgwCopy.ReportProgress(0, "ステップ(2/3) プレイリスト作成中(" + PlaylistCreateCount + "/" + PlaylistCreateMax + ")...");

			if (playlist.Check == false) return;
			string m3uPath;
			string playlistName = RemoveInvalidPathCharacter(playlist.Name);

			var str = new StringBuilder();

			if (playlist.Type == EPlaylistType.Folder) {
				m3uPath = Path.Combine(currentPath, "_" + playlistName + ".m3u");
				foreach (var track in GetCheckedPlaylistTrack(playlist)) {
					str.AppendLine("#EXTINF:-1," + (track.Artist == "" ? "アーティスト名なし" : track.Artist.Replace("-", "_")) + " - " + track.Name.Replace("-", "_"));
					str.AppendLine(relativePath + GetPlaylistItemIds(track.Id) + track.Id + Path.GetExtension(track.Location));
				}
			}
			else {
				m3uPath = Path.Combine(currentPath, playlistName + ".m3u");

				foreach (var id in playlist.Tracks) {
					Track track = library.Tracks.First(t => t.Id == id);
					str.AppendLine("#EXTINF:-1," + (track.Artist == "" ? "アーティスト名なし" : track.Artist.Replace("-", "_")) + " - " + track.Name.Replace("-", "_"));
					str.AppendLine(relativePath + GetPlaylistItemIds(id) + id + Path.GetExtension(track.Location));
				}
			}

			File.WriteAllText(m3uPath, str.ToString());
			PlaylistCreateCount++;

			foreach (int childPId in library.Playlists.First(p => p.Id == playlist.Id).Childs) {
				var pl = library.Playlists.First(p => p.Id == childPId);
				CreatePlaylistFile(Path.Combine(currentPath, playlistName), "../" + relativePath, pl, e);
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
				else if (p.Check) {
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
	}
}
