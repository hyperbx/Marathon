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
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace Marathon.Components
{
    /// <summary>
    /// A drop-in replacement for WinForms ListView controls that works around graphical bugs for OwnerDraw.
    /// </summary>
    public partial class MarathonListView : UserControl
    {
        /// <summary>
        /// The items in the ListView.
        /// </summary>
        [
            DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
            Localizable(true),
            MergableProperty(false),
            Category("Behavior"),
            Browsable(true),
            Description("The ListView control being used.")
        ]
        public ObjectListViewDark ListView => ObjectListViewDark_ObjectListView;

        public MarathonListView()
        {
            InitializeComponent();

            // Set whitespace colour.
            Panel_Whitespace.BackColor = ObjectListViewDark_ObjectListView.ColumnBackColour;
        }

        /// <summary>
        /// Resizes the list view itself to hide the stupid white area.
        /// </summary>
        private void CompensateHeaders(bool includeScrollBarDimensions = false)
        {
            ObjectListViewDark_ObjectListView.Size = new Size
            (
                // Width calculated by total sum of column width.
                ObjectListViewDark_ObjectListView.Columns.Cast<ColumnHeader>().Sum(x => x.Width),

                // Height calculated by host height.
                includeScrollBarDimensions ?
                Height + SystemInformation.HorizontalScrollBarHeight :
                Height
            );
        }

        /// <summary>
        /// Adjust list view size upon resize.
        /// </summary>
        protected override void OnResize(EventArgs e)
        {
            CompensateHeaders();

            base.OnResize(e);
        }

        /// <summary>
        /// Adjust list view size upon column width changing.
        /// </summary>
        private void ListView_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
            => CompensateHeaders(true);

        /// <summary>
        /// Adjust list view size once column widths changed.
        /// </summary>
        private void ListView_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
            => CompensateHeaders(true);
    }
}
