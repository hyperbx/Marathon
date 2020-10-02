using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using Marathon.Toolkit.Controls;
using WeifenLuo.WinFormsUI.Docking;
using Marathon.IO.Formats.Archives;

namespace Marathon.Toolkit.Forms
{
    public partial class TaskDashboard : Form
    {
        public TaskDashboard(DockPanel parent, string file)
        {
            InitializeComponent();

            string extension = Path.GetExtension(file); // Output: .blah

            switch (extension)
            {
                case ".arc":
                case ".wad":
                {
                    TaskDashboardOption exploreArchive = new TaskDashboardOption()
                    {
                        TaskName = "Explore archive",
                        TaskDescription = "Explore the contents of this archive."
                    };

                    TaskDashboardOption extractArchive = new TaskDashboardOption()
                    {
                        TaskName = "Extract archive",
                        TaskDescription = "Extract the contents of this archive and navigate using Marathon Explorer."
                    };

                    exploreArchive.Activated += delegate
                    {
                        if (extension == ".arc")
                        {
                            new ArchiveExplorer(new U8Archive(file) { StoreInMemory = false }).Show(parent);
                        }
                        else if (extension == ".wad")
                        {
                            new ArchiveExplorer(new WADHArchive(file) { StoreInMemory = false }).Show(parent);
                        }

                        Close();
                    };

                    extractArchive.Activated += delegate { throw new NotImplementedException(); };

                    FlowLayoutPanel_Tasks.Controls.AddRange(new[] { exploreArchive, extractArchive });

                    break;
                }

                case ".bin":
                {
                    TaskDashboardOption commonPackage = new TaskDashboardOption()
                    {
                        TaskName = "Edit package container",
                        TaskDescription = "Modify the contents of a generic BINA package."
                    };

                    TaskDashboardOption collisionMesh = new TaskDashboardOption()
                    {
                        TaskName = "Preview collision mesh",
                        TaskDescription = "View the collision mesh."
                    };

                    TaskDashboardOption saveData = new TaskDashboardOption()
                    {
                        TaskName = "Modify save data",
                        TaskDescription = "Alter your save data like a real cheater."
                    };

                    commonPackage.Activated += delegate { throw new NotImplementedException(); };

                    collisionMesh.Activated += delegate { throw new NotImplementedException(); };

                    saveData.Activated += delegate
                    {
                        new SaveEditor(file).Show(parent);

                        Close();
                    };

                    FlowLayoutPanel_Tasks.Controls.AddRange(new[] { commonPackage, collisionMesh, saveData });

                    break;
                }

                default:
                {
                    MarathonMessageBox.Show("This file extension is unknown - please select a format best suited for this file.");

                    break;
                }
            }

            CalculateFormSize();
        }

        /// <summary>
        /// Calculates the form size by the highest task control width and default height.
        /// </summary>
        private void CalculateFormSize()
        {
            List<int> _TaskWidthList = new List<int>();

            foreach (Control control in FlowLayoutPanel_Tasks.Controls)
            {
                _TaskWidthList.Add(control.Width);

                Height += 44; // Default height for all task controls.
            }

            if (_TaskWidthList.Count != 0)
                Width = _TaskWidthList.Max() + 72; // Add extra width for padding.
        }
    }
}
