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

using System.Linq;
using ComponentFactory.Krypton.Ribbon;
using ComponentFactory.Krypton.Toolkit;

namespace Marathon.Components.Helpers
{
    public class RibbonHelper
    {
        /// <summary>
        /// An array of KryptonRibbonTabs representing the default ribbon upon launch.
        /// </summary>
        public static KryptonRibbonTab[] DefaultRibbonTabs;

        /// <summary>
        /// An array of KryptonContextMenuItemBases representing the default ribbon file menu upon launch.
        /// </summary>
        public static KryptonContextMenuItemBase[] DefaultRibbonAppButtonMenu;

        /// <summary>
        /// Returns whether the ribbon is already default or not.
        /// </summary>
        public static bool IsRibbonDefault(KryptonRibbon ribbon)
        {
            // Something must've really messed up if we made it here.
            if (ribbon == null)
                return false;

            return ribbon.RibbonTabs.ToArray().SequenceEqual(DefaultRibbonTabs) &&
                   ribbon.RibbonAppButton.AppButtonMenuItems.ToArray().SequenceEqual(DefaultRibbonAppButtonMenu);
        }

        /// <summary>
        /// Sets up the ribbon's controls based on input.
        /// </summary>
        public static void SetupRibbon
        (
            KryptonRibbon ribbon,
            KryptonRibbonTab[] tabs,
            KryptonContextMenuItemBase[] appButtonMenu,
            bool appendDefault = false
        )
        {
            if (ribbon != null)
            {
                // Clear the requested ribbon controls.
                ribbon.RibbonTabs.Clear();
                ribbon.RibbonAppButton.AppButtonMenuItems.Clear();

                if (tabs != null)
                {
                    // Add the requested ribbon tabs.
                    ribbon.RibbonTabs.AddRange(tabs);

                    // Append the default ribbon tabs if requested.
                    if (appendDefault)
                        ribbon.RibbonTabs.AddRange(DefaultRibbonTabs);
                }

                // Keeping defaults so we don't have to create them every time.
                ribbon.RibbonAppButton.AppButtonMenuItems.AddRange(DefaultRibbonAppButtonMenu);

                // Add the requested ribbon app button menu items.
                if (appButtonMenu != null)
                    ribbon.RibbonAppButton.AppButtonMenuItems.AddRange(appButtonMenu);
            }
        }
    }
}
