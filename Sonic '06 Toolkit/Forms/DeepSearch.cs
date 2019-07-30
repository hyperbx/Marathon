using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Sonic_06_Toolkit
{
    public partial class DeepSearch : Form
    {
        List<string> checkedARCsList = new List<string>() { };

        public DeepSearch()
        {
            InitializeComponent();
        }

        private void DeepSearch_Load(object sender, EventArgs e)
        {
            s06PathBox.Text = Properties.Settings.Default.gamePath;
        }

        private void S06PathBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var mods = Directory.GetFiles(s06PathBox.Text, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".arc"));

                foreach (var item in mods)
                {
                    clb_ARCs.Items.Add(Path.GetFileName(item));
                }
            }
            catch { }
        }

        private void S06PathButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog s06PathBrowser = new FolderBrowserDialog();
            s06PathBrowser.Description = "Select your SONIC THE HEDGEHOG (2006) game directory...";
            if (s06PathBrowser.ShowDialog() == DialogResult.OK)
            {
                s06PathBox.Text = s06PathBrowser.SelectedPath;
                Properties.Settings.Default.gamePath = s06PathBrowser.SelectedPath;
                Properties.Settings.Default.Save();
            }
        }

        private void Btn_Search_Click(object sender, EventArgs e)
        {
            ProcessStrings();
        }

        private void ProcessStrings()
        {
            list_Search.Items.Clear();

            var files = Directory.GetFiles(s06PathBox.Text, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".arc"));

            foreach (string selectedARC in clb_ARCs.CheckedItems)
            {
                foreach (var ARC in files)
                {
                    if (ARC.Contains(selectedARC))
                    {
                        using (FileStream stream = new FileStream(ARC, FileMode.Open))
                        {
                            // here is some stream 'stream' which contains any number of bytes, where we search for various strings inside of it
                            FindStrings(stream, new string[] { searchBox.Text }, Path.GetFileName(ARC));
                        }
                    }
                }
            }
        }

        private void FindStrings(Stream stream, string[] str, string arc)
        {
            int[] pos = new int[str.Length];
            byte[] buf = new byte[1024];
            int len = 0;

            while (true)
            {
                // read data from stream into buffer, and exit if length is 0
                if ((len = stream.Read(buf, 0, buf.Length)) == 0)
                    break;

                // check str array for every character, making sure they are up-to-date
                for (int x = 0; x < str.Length; x++)
                {
                    for (int i = 0; i < len; i++)
                    {
                        if (str[x][pos[x]] == buf[i])
                        {

                            // there is a match, increment position and check if we finished the string
                            if (++pos[x] == str[x].Length)
                            {
                                // why yes, this string is now finished, print out the information
                                list_Search.Items.Add($"Found instances of {str[x]} in {arc}");

                                // reset position
                                pos[x] = 0;
                            }

                            // no match, reset position to 0
                        }
                        else pos[x] = 0;
                    }
                }
            }
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Btn_SelectAll_Click(object sender, EventArgs e)
        {
            //Checks all available checkboxes.
            for (int i = 0; i < clb_ARCs.Items.Count; i++) clb_ARCs.SetItemChecked(i, true);
            btn_Search.Enabled = true;
        }

        private void Btn_DeselectAll_Click(object sender, EventArgs e)
        {
            //Unchecks all available checkboxes.
            for (int i = 0; i < clb_ARCs.Items.Count; i++) clb_ARCs.SetItemChecked(i, false);
            btn_Search.Enabled = false;
        }

        private void Clb_ARCs_SelectedIndexChanged(object sender, EventArgs e)
        {
            clb_ARCs.ClearSelected(); //Removes the blue highlight on recently checked boxes.

            //Enables/disables the Encode button, depending on whether a box has been checked.
            if (clb_ARCs.CheckedItems.Count > 0)
            {
                btn_Search.Enabled = true;
            }
            else
            {
                btn_Search.Enabled = false;
            }
        }
    }
}
