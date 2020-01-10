using System;
using Ookii.Dialogs;
using System.Windows.Forms;

namespace Toolkit.Environment4
{
    public partial class BatchLocation : UserControl
    {
        public BatchLocation() { InitializeComponent(); }

        private void Button_Location_Click(object sender, EventArgs e) {
            VistaFolderBrowserDialog browseLocation = new VistaFolderBrowserDialog() {
                Description = "Please select a folder...",
                UseDescriptionForTitle = true
            };

            if (browseLocation.ShowDialog() == DialogResult.OK)
                TextBox_Location.Text = browseLocation.SelectedPath;
        }
    }
}
