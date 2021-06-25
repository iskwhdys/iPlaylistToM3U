using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPlaylistToM3U.Commons {

	public class Const {

		public class Application {

			public static readonly string Name = "iPlaylistToM3U";

			public static readonly string Version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

			public static readonly string TitleBar = Name;// + " Ver." + Version;

		}

		public class Setting {

			public static readonly string LibraryXmlPath = "iPlaylistToM3U.xml";

			public static readonly string SettingXmlPath = "iPlaylistToM3USetting.xml";
			
		}


	}


}
