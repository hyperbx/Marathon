// Marathon is licensed under the MIT License:
/* 
 * MIT License
 * 
 * Copyright (c) 2020 HyperPolygon64
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
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;

namespace Marathon.Toolkit.Forms
{
    public partial class ErrorHandler : Form
    {
        Exception _Exception = new Exception("If you're reading this, something went horrifically wrong...") { Source = "Marathon Error Handler" };
        bool _Reported = false;

        public ErrorHandler(Exception ex)
        {
            InitializeComponent();

            _Exception = ex;

            RichTextBox_Error.Text = BuildExceptionLog();
        }

        /// <summary>
        /// Builds the exception log for the RichTextBox control.
        /// </summary>
        /// <param name="GitHub">Enables markdown for a better preview on GitHub.</param>
        private string BuildExceptionLog(bool GitHub = false)
        {
            StringBuilder exception = new StringBuilder();

            if (GitHub) exception.AppendLine("```");

            exception.AppendLine("Marathon Toolkit" + $"{Program.GetExtendedInformation()} ({Program.Architecture()})");

            if (!string.IsNullOrEmpty(_Exception.GetType().Name))
                exception.AppendLine($"\nType: {_Exception.GetType().Name}");

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

            if (GitHub) exception.AppendLine("```");

            return exception.ToString();
        }

        /// <summary>
        /// Warns the user that their feedback could be valuable. :(
        /// </summary>
        private void MarathonErrorHandler_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_Reported)
            {
                DialogResult closeWarning = MarathonMessageBox.Show("Are you sure you want to exit without reporting this?\n" +
                                                                    "Submitting issues can help contributors fix problems faster...",
                                                                    "Exit without reporting?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (closeWarning == DialogResult.No)
                    e.Cancel = true;
            }
        }

        /// <summary>
        /// Exits the application upon clicking.
        /// </summary>
        private void ButtonFlat_Close_Click(object sender, EventArgs e) => Close();

        /// <summary>
        /// Launches the GitHub issues page with the required fields filled out.
        /// </summary>
        private void ButtonFlat_GitHub_Click(object sender, EventArgs e)
        {
            Process.Start($"https://github.com/HyperPolygon64/Marathon/issues/new" +
                          $"?title=[Toolkit] " +
                          $"&body={Uri.EscapeDataString(BuildExceptionLog(true))}");

            _Reported = true;
        }

        /// <summary>
        /// Copies the exception log to the clipboard.
        /// </summary>
        private void ButtonFlat_Copy_Click(object sender, EventArgs e)
            => Clipboard.SetText(BuildExceptionLog(true)); // Use GitHub markdown anyway, in case people can't read.
    }
}