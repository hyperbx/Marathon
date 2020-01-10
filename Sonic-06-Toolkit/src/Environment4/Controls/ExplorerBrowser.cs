using System.Web;
using System.Windows.Forms;

namespace Toolkit.Environment4
{
    public partial class ExplorerBrowser : WebBrowser
    {
        public ExplorerBrowser() { InitializeComponent(); }

        public string CurrentLocation {
            get {
                if (Url != null) return HttpUtility.UrlDecode(Url.ToString().Replace("file:///", "").Replace("/", @"\") + @"\").Replace("file:", "");
                else return string.Empty;
            }
            set { HttpUtility.UrlDecode(value.Replace("file:///", "").Replace("/", @"\") + @"\").Replace("file:", ""); }
        }

        private void ExplorerBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e) { CurrentLocation = Url.ToString(); }
    }
}
