using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Marathon.Components.Helpers
{
    /// <summary>
    /// <para>Extension class for RichTextBox.</para>
    /// <para><see href="https://stackoverflow.com/a/33542937">Learn more...</see></para>
    /// </summary>
    public static class RichTextBoxExtensions
    {
        [DllImport(@"User32.dll", EntryPoint = @"SendMessage", CharSet = CharSet.Auto)]
        private static extern int SendMessageRefRect(IntPtr hWnd, uint msg, int wParam, ref RECT rect);

        [DllImport(@"user32.dll", EntryPoint = @"SendMessage", CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, ref Rectangle lParam);

        private const int EM_GETRECT = 0xB2;
        private const int EM_SETRECT = 0xB3;

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public readonly int Left;
            public readonly int Top;
            public readonly int Right;
            public readonly int Bottom;

            private RECT(int left, int top, int right, int bottom)
            {
                Left = left;
                Top = top;
                Right = right;
                Bottom = bottom;
            }

            public RECT(Rectangle r) : this(r.Left, r.Top, r.Right, r.Bottom) { }
        }

        private static Rectangle GetFormattingRect(this TextBoxBase textbox)
        {
            var rect = new Rectangle();

            SendMessage(textbox.Handle, EM_GETRECT, (IntPtr)0, ref rect);

            return rect;
        }

        private static void SetFormattingRect(this TextBoxBase textbox, Rectangle rect)
        {
            var rc = new RECT(rect);

            SendMessageRefRect(textbox.Handle, EM_SETRECT, 0, ref rc);
        }

        public static void SetInnerMargins(this TextBoxBase textBox, int left, int top, int right, int bottom)
        {
            var rect = textBox.GetFormattingRect();
            var newRect = new Rectangle(left, top, rect.Width - left - right, rect.Height - top - bottom);

            textBox.SetFormattingRect(newRect);
        }
    }
}
