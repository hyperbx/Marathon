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
using System.Text;
using System.Windows.Forms;
using Marathon.Components;

namespace Marathon.Toolkit.Forms
{
    public partial class ErrorHandler : Form
    {
        private Exception _Exception = new Exception("If you're reading this, something went horrifically wrong...") { Source = "Marathon Error Handler" };

        private bool _Reported = false;

        /* Storage for 'error corrected' exception strings.
           In this case, strings that have been checked for null or empty already.
           May be used elsewhere when necessary, but it's just used for GitHub for now. */
        private string _ECC_Type,
                       _ECC_Message,
                       _ECC_Source,
                       _ECC_Function,
                       _ECC_StackTrace,
                       _ECC_InnerException;

        public ErrorHandler(Exception ex)
        {
            InitializeComponent();

            _Exception = ex;

            MarathonRichTextBox_Error.Text = BuildExceptionLog();
        }

        /// <summary>
        /// Builds the exception log for the RichTextBox control.
        /// </summary>
        /// <param name="markdown">Enables markdown for a better preview with services that use it.</param>
        private string BuildExceptionLog(bool markdown = false)
        {
            StringBuilder exception = new StringBuilder();

            if (markdown) exception.AppendLine("```");

            exception.AppendLine("Marathon Toolkit" + $"{Program.GetExtendedInformation()} ({Program.Architecture()})");

            if (!string.IsNullOrEmpty(_Exception.GetType().Name))
                exception.AppendLine($"\nType: {_ECC_Type = _Exception.GetType().Name}");

            if (!string.IsNullOrEmpty(_Exception.Message))
                exception.AppendLine($"Message: {_ECC_Message = _Exception.Message}");

            if (!string.IsNullOrEmpty(_Exception.Source))
                exception.AppendLine($"Source: {_ECC_Source = _Exception.Source}");

            if (_Exception.TargetSite != null)
                exception.AppendLine($"Function: {_ECC_Function = _Exception.TargetSite.ToString()}");

            if (!string.IsNullOrEmpty(_Exception.StackTrace))
                exception.AppendLine($"\nStack Trace: \n{_ECC_StackTrace = _Exception.StackTrace}");

            if (_Exception.InnerException != null)
                exception.AppendLine($"\nInner Exception: \n{_ECC_InnerException = _Exception.InnerException.ToString()}");

            if (markdown) exception.AppendLine("```");

            return exception.ToString();
        }

        /// <summary>
        /// Warns the user that their feedback could be valuable. :(
        /// </summary>
        private void MarathonErrorHandler_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_Reported)
            {
                DialogResult closeWarning = MarathonMessageBox.Show
                (
                    "Are you sure you want to exit without reporting this?\n" +
                    "Submitting issues can help contributors fix problems faster...",
                    "Exit without reporting?",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (closeWarning == DialogResult.No)
                    e.Cancel = true;
            }
        }

        /// <summary>
        /// Exits the application upon clicking.
        /// </summary>
        private void ButtonDark_Close_Click(object sender, EventArgs e) => Close();

        /// <summary>
        /// Launches the GitHub issues page with the required fields filled out.
        /// </summary>
        private void ButtonDark_GitHub_Click(object sender, EventArgs e)
        {
            // Generate source and message issue.
            Program.InvokeFeedback
            (
                "[" + (string.IsNullOrEmpty(_ECC_Source) ? "Marathon.Toolkit" : _ECC_Source) + "] " +
                (string.IsNullOrEmpty(_ECC_Message) ? string.Empty : $"'{_ECC_Message}'"),
                BuildExceptionLog(true)
            );

            _Reported = true;
        }

        /// <summary>
        /// Copies the exception log to the clipboard.
        /// </summary>
        private void ButtonDark_Copy_Click(object sender, EventArgs e)
            => Clipboard.SetText(BuildExceptionLog(true)); // Use markdown anyway, in case people can't read.
    }
}