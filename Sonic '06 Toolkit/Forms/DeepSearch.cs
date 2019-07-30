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
            list_Search.Items.Clear();

            var files = Directory.GetFiles(s06PathBox.Text, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".arc"));
            UnicodeEncoding unicode = new UnicodeEncoding(true, false, false);
            byte[] buffer = new byte[1024];

            foreach (string selectedARC in clb_ARCs.CheckedItems)
            {
                foreach (var ARC in files)
                {
                    if (ARC.Contains(selectedARC))
                    {
                        List<string> results = new List<string>();

                        using (FileStream fs = new FileStream(ARC, FileMode.Open))
                        {
                            byte[] strbuf = new byte[1024];
                            int len = 0, pos = 0;

                            while ((len = fs.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                for (int i = 0; i < len; i += 2)
                                {

                                    // check if the next 2 bytes are an end token. Check second byte last because its most likely 0 anyway but we gotta be sure...
                                    if ((buffer[i + 1] == 0 || buffer[i + 1] == 0xA) && buffer[i] == 0)
                                    {
                                        // this is a null terminator or a newline
                                        results.Add(unicode.GetString(strbuf, 0, pos));
                                        pos = 0;

                                    }
                                    else
                                    {
                                        // if the buffer we allocated is too small, reallocate a larger array...
                                        if (pos >= strbuf.Length)
                                        {
                                            byte[] _sbuf = strbuf;
                                            strbuf = new byte[_sbuf.Length << 1];
                                            Array.Copy(_sbuf, strbuf, _sbuf.Length);
                                        }

                                        // copy next 2 bytes into strbuffer... If file size is not divisible by 2, this is gonna be a real problem here
                                        strbuf[pos++] = buffer[i];
                                        strbuf[pos++] = buffer[i + 1];
                                    }
                                }
                            }

                            // if the last string is not terminated, print it anyway
                            if (pos > 0) results.Add(unicode.GetString(strbuf, 0, pos));
                        }

                        foreach (var item in results)
                        {
                            list_Search.Items.Add($"Found instances of {item} in {Path.GetFileName(ARC)}");
                        }

                        //byte[] bytes;
                        //string hexString;

                        //bytes = File.ReadAllBytes(ARC).Take(4).ToArray();
                        //hexString = BitConverter.ToString(bytes); hexString = hexString.Replace("-", " ");

                        //if (hexString != "55 AA 38 2D") MessageBox.Show("Invalid ARC file detected.", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //else
                        //{
                        //    bytes = File.ReadAllBytes(ARC).ToArray();
                        //    hexString = BitConverter.ToString(bytes); hexString = hexString.Replace("-", " ");

                        //    byte[] query = Encoding.ASCII.GetBytes(searchBox.Text);
                        //    var queryBytes = BitConverter.ToString(query); queryBytes = queryBytes.Replace("-", " ");

                        //    if (hexString.Contains(queryBytes))
                        //    {
                        //        list_Search.Items.Add($"Found instances of {searchBox.Text} in {Path.GetFileName(ARC)}");
                        //    }

                        //    Array.Clear(bytes, 0, bytes.Length);
                        //    Array.Clear(query, 0, query.Length);
                        //}
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
