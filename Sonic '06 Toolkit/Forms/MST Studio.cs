using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sonic_06_Toolkit
{
    public partial class MST_Studio : Form
    {
        public MST_Studio()
        {
            InitializeComponent();
        }

        void MST_Studio_Load(object sender, EventArgs e)
        {
            #region Getting MSTs...
            //Adds all MSTs in the current path to the CheckedListBox.
            foreach (string MST in Directory.GetFiles(Global.currentPath, "*.mst", SearchOption.TopDirectoryOnly))
            {
                if (File.Exists(MST))
                {
                    clb_MSTs.Items.Add(Path.GetFileName(MST));
                }
            }
            //Checks if there are any MSTs in the directory.
            if (clb_MSTs.Items.Count == 0)
            {
                MessageBox.Show("There are no MSTs to decode in this directory.", "No MSTs available", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            #endregion

            split_MSTStudio.Visible = false;
            tm_ItemCheck.Start();
        }

        void Btn_SelectAll_Click(object sender, EventArgs e)
        {
            //Checks all available checkboxes.
            for (int i = 0; i < clb_MSTs.Items.Count; i++) clb_MSTs.SetItemChecked(i, true);
            btn_Decode.Enabled = true;
        }

        void Btn_DeselectAll_Click(object sender, EventArgs e)
        {
            //Unchecks all available checkboxes.
            for (int i = 0; i < clb_MSTs.Items.Count; i++) clb_MSTs.SetItemChecked(i, false);
            btn_Decode.Enabled = false;
        }

        void Clb_MSTs_SelectedIndexChanged(object sender, EventArgs e)
        {
            clb_MSTs.ClearSelected(); //Removes the blue highlight on recently checked boxes.

            //Enables/disables the Convert button, depending on whether a box has been checked.
            if (clb_MSTs.CheckedItems.Count > 0)
            {
                btn_Decode.Enabled = true;
            }
            else
            {
                btn_Decode.Enabled = false;
            }
        }

		void Btn_Decode_Click(object sender, EventArgs e) {
			//MST Decoder will go here...
			UnicodeEncoding unicode = new UnicodeEncoding(true, false, false);
			byte[] buffer = new byte[1024];

			// arg0 is assumed to be a file to split
			foreach (string file in clb_MSTs.CheckedItems) {
				List<string> results = new List<string>();			// <- BTW Hyper this array contains ALL the lines read from the file, including any bugged non-unicode lines.

				using (FileStream fs = new FileStream(Global.currentPath + file, FileMode.Open)) {
					byte[] strbuf = new byte[1024];
					int len = 0, pos = 0;

					while ((len = fs.Read(buffer, 0, buffer.Length)) > 0) {
						for (int i = 0;i < len;i += 2) {

							// check if the next 2 bytes are an end token. Check second byte last because its most likely 0 anyway but we gotta be sure...
							if ((buffer[i + 1] == 0 || buffer[i + 1] == 0xA) && buffer[i] == 0) {
								// this is a null terminator or a newline
								results.Add(unicode.GetString(strbuf, 0, pos));
								pos = 0;

							} else {
								// if the buffer we allocated is too small, reallocate a larger array...
								if (pos >= strbuf.Length) {
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
			}
		}
	}
}
