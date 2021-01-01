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

using System.Reflection;
using System.Windows.Forms;
using System.ComponentModel;

namespace Marathon.Helpers
{
    public class DesignHelper
    {
        /// <summary>
        /// Returns if the process is running in Visual Studio.
        /// </summary>
        ///
        /// <remarks>
        /// Not the most ideal solution, and probably the least efficient - but if it fixes nested controls
        /// breaking Design View in all possible ways, I'm okay with it for now.
        /// </remarks>
        public static bool RunningInDesigner(Control ctrl = null)
        {
            // Should be the first result given when in Design View.
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return true;

            // Simplest way to check outside of a control, if all else fails.
            if (Assembly.GetExecutingAssembly().Location.Contains("VisualStudio"))
                return true;

            // Now we check if the input control has anything nested.
            while (ctrl != null)
            {
                if (ctrl.Site != null && ctrl.Site.DesignMode)
                    return true;

                ctrl = ctrl.Parent;
            }

            // Guess we're runtime. ¯\_(ツ)_/¯
            return false;
        }
    }
}
