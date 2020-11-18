// Marathon is licensed under the MIT License:
/* 
 * MIT License
 * 
 * Copyright (c) 2020 HyperBE32
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using Marathon.Toolkit.Controls;
using Marathon.IO.Formats.Archives;
using WeifenLuo.WinFormsUI.Docking;

namespace Marathon.Toolkit.Forms
{
    public partial class TaskDashboard : MarathonDockContent
    {
        /// <summary>
        /// Input states for the tasks.
        /// </summary>
        public enum TaskState
        {
            New, // Input file is temporary.
            Open // Input file is to be opened.
        }

        private DockPanel DockParent;
        private string File;
        private TaskState State;

        public TaskDashboard(DockPanel parent, string file, TaskState state = TaskState.Open)
        {
            InitializeComponent();

            DockParent = parent;
            File = file;
            State = state;
        }

        /// <summary>
        /// Loads the tasks.
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            string extension = Path.GetExtension(File); // Output: .blah

            switch (extension)
            {
                case ".arc":
                {
                    switch (State)
                    {
                        case TaskState.New:
                        {
                            new ArchiveExplorer(new U8Archive() { Location = File }).Show(DockParent, InheritanceRibbon);

                            Close();

                            break;
                        }

                        case TaskState.Open:
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
                                new ArchiveExplorer(new U8Archive(File, true, true, false)).Show(DockParent, InheritanceRibbon);

                                Close();
                            };

                            extractArchive.Activated += delegate { throw new NotImplementedException(); };

                            FlowLayoutPanel_Tasks.Controls.AddRange(new[] { exploreArchive, extractArchive });

                            break;
                        }
                    }

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
                        new SaveEditor(File).Show(DockParent, InheritanceRibbon);

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

            Initialise();

            base.OnLoad(e);
        }

        /// <summary>
        /// Makes the form visible if any task is present.
        /// </summary>
        private void Initialise()
        {
            List<int> _TaskWidthList = new List<int>();

            // Make window visible.
            if (FlowLayoutPanel_Tasks.Controls.Count != 0)
                Opacity = 100;

            // No tasks available, just close.
            else
                Close();

            foreach (Control control in FlowLayoutPanel_Tasks.Controls)
            {
                _TaskWidthList.Add(control.Width);

                Height += 44; // Default height for all task controls.
            }

            if (_TaskWidthList.Count != 0)
                Width = _TaskWidthList.Max() + 72; // Add extra width for padding.

            // Recenter the form after resize.
            /* This wouldn't have been necessary, but moving everything into the Load event
               means the form will move from the centre after being resized dynamically. */
            {
                Rectangle area = Screen.FromControl(this).WorkingArea;

                Top  = (area.Height - Height) / 2;
                Left = (area.Width - Width) / 2;
            }

            // Set new minimum size after initialising.
            MinimumSize = Size;
        }
    }
}
