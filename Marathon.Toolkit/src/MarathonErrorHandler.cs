using System;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;

namespace Marathon
{
    public partial class MarathonErrorHandler : Form
    {
        Exception _Exception = new Exception("If you're reading this, something went horrifically wrong...") { Source = "Marathon Error Handler" };
        bool _Reported = false;

        public MarathonErrorHandler(Exception ex)
        {
            InitializeComponent();

            _Exception = ex;

            RichTextBox_Error.Text = BuildException();
        }

        /// <summary>
        /// Builds the exception for the RichTextBox control.
        /// </summary>
        /// <param name="GitHub">Enables markdown for a better preview on GitHub.</param>
        private string BuildException(bool GitHub = false)
        {
            StringBuilder exception = new StringBuilder();

            if (GitHub) exception.AppendLine("Marathon Information:\n```");

            exception.AppendLine($"Project Marathon - {Program.GlobalVersion} ({Program.Architecture()}) " +
                                (Program.RunningAsAdmin() ? "[Administrator]" : "[User]"));

            if (GitHub)
                exception.AppendLine("```\nException Information:\n```");
            else
                exception.AppendLine();

            if (!string.IsNullOrEmpty(_Exception.GetType().Name))
                exception.AppendLine($"Type: {_Exception.GetType().Name}");

            if (!string.IsNullOrEmpty(_Exception.Message))
                exception.AppendLine($"Message: {_Exception.Message}");

            if (!string.IsNullOrEmpty(_Exception.Source))
                exception.AppendLine($"Source: {_Exception.Source}");

            if (_Exception.TargetSite != null)
                exception.AppendLine($"Function: {_Exception.TargetSite}");

            if (!string.IsNullOrEmpty(_Exception.StackTrace))
                exception.AppendLine($"\nStack Trace: \n{_Exception.StackTrace}");

            if (_Exception.InnerException != null)
                exception.AppendLine($"\nInner Exception: \n{_Exception.InnerException}");

            if (GitHub) exception.Append("```");

            return exception.ToString();
        }

        /// <summary>
        /// Warns the user that their feedback could be valuable. :(
        /// </summary>
        private void MarathonErrorHandler_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_Reported)
            {
                DialogResult closeWarning = MessageBox.Show("Are you sure you want to exit without reporting this?\n" +
                                                            "Submitting issues can help contributors fix problems faster...",
                                                            "Exit without reporting?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (closeWarning == DialogResult.No)
                    e.Cancel = true;
            }
        }

        /// <summary>
        /// Exits the application upon clicking.
        /// </summary>
        private void ButtonFlat_Close_Click(object sender, EventArgs e) => Application.Exit();

        /// <summary>
        /// Launches the GitHub issues page with the required fields filled out.
        /// </summary>
        private void ButtonFlat_GitHub_Click(object sender, EventArgs e)
        {
            Process.Start($"https://github.com/HyperPolygon64/Marathon/issues/new" +
                               $"?title=[Toolkit] " +
                               $"&body={Uri.EscapeDataString(BuildException(true))}");

            _Reported = true;
        }
    }
}