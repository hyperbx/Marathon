using System;
using System.IO;
using Marathon.Toolkit.Controls;
using WeifenLuo.WinFormsUI.Docking;

namespace Marathon.Toolkit.Forms
{
    public partial class FileExtensionWizard : DockContent
    {
        public FileExtensionWizard(DockPanel parent, string file)
        {
            InitializeComponent();

            switch (Path.GetExtension(file))
            {
                case ".arc":
                {
                    FileExtensionWizardTask exploreArchive = new FileExtensionWizardTask()
                    {
                        TaskName = "Explore archive",
                        TaskDescription = "Explore the contents of this archive."
                    };

                    FileExtensionWizardTask extractArchive = new FileExtensionWizardTask()
                    {
                        TaskName = "Extract archive",
                        TaskDescription = "Extract the contents of this archive and navigate using Marathon Explorer."
                    };

                    exploreArchive.Activated += delegate
                    {
                        new ArchiveExplorer(file).Show(parent);
                        Close();
                    };

                    extractArchive.Activated += delegate { throw new NotImplementedException(); };

                    FlowLayoutPanel_Tasks.Controls.AddRange(new[] { exploreArchive, extractArchive });

                    break;
                }

                case ".bin":
                {
                    FileExtensionWizardTask commonPackage = new FileExtensionWizardTask()
                    {
                        TaskName = "Edit package container",
                        TaskDescription = "Modify the contents of a generic BINA package."
                    };

                    FileExtensionWizardTask collisionMesh = new FileExtensionWizardTask()
                    {
                        TaskName = "Preview collision mesh",
                        TaskDescription = "View the collision mesh."
                    };

                    FileExtensionWizardTask saveData = new FileExtensionWizardTask()
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
            }
        }
    }
}
