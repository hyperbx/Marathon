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

            string @extension = Path.GetExtension(file);

            switch (@extension)
            {
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
                        TaskDescription = "View the collision mesh and/or export as desired."
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
