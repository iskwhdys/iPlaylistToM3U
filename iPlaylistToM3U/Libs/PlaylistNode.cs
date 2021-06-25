using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace musikkuLibrary.Libs {

	public class PlaylistNode : TreeNode{

		public Playlist Playlist { get; set; }
		
		public PlaylistNode(Playlist p) : base(p.Name) {
			Playlist = p;
			this.ImageIndex = (int)p.Type;
			this.SelectedImageIndex = (int)p.Type;
			this.Checked = p.Check;
			p.Tag = this;

		}




	}
}
