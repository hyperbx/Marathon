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
using System.Reflection;
using System.Windows.Forms;

namespace Marathon.Components
{
    public partial class OptionsFieldStringType : UserControl
    {
        private string _OptionName, _OptionDescription, _OptionField;
        private bool _OptionIsPath;
        private PropertyInfo _OptionProperty;

        /// <summary>
        /// The name of the option.
        /// </summary>
        public string OptionName
        {
            get => _OptionName;

            set => Label_Title.Text = _OptionName = value;
        }

        /// <summary>
        /// The description given to the option.
        /// </summary>
        public string OptionDescription
        {
            get => _OptionDescription;

            set => Label_Description.Text = _OptionDescription = value;
        }

        /// <summary>
        /// Determines if the string is a path.
        /// </summary>
        public bool OptionIsPath
        {
            get => _OptionIsPath;

            set
            {
                ButtonDark_Browse.Visible = _OptionIsPath = value;

                if (value)
                {
                    TextBoxDark_String.Width -= 31;
                }
                else
                {
                    TextBoxDark_String.Width += 31;
                }
            }
        }

        /// <summary>
        /// The property assigned to this option.
        /// </summary>
        public PropertyInfo OptionProperty
        {
            get => _OptionProperty;

            set
            {
                _OptionProperty = value;

                if (_OptionProperty != null)
                    TextBoxDark_String.Text = (string)_OptionProperty.GetValue(value);
            }
        }

        /// <summary>
        /// The string assigned to this option.
        /// </summary>
        private string OptionField
        {
            get => _OptionField;

            set
            {
                _OptionField = value;

                if (OptionProperty != null)
                    OptionProperty.SetValue(OptionProperty, value);
            }
        }

        private void ButtonDark_Browse_Click(object sender, EventArgs e)
        {
            // TODO
        }

        public OptionsFieldStringType()
            => InitializeComponent();

        /// <summary>
        /// Sets the property to the current text.
        /// </summary>
        private void TextBoxDark_String_TextChanged(object sender, EventArgs e)
            => OptionField = TextBoxDark_String.Text;
    }
}
