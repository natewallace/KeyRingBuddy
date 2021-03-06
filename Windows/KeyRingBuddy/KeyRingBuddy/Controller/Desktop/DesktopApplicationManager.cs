﻿/*
 * Copyright (c) 2015 Nathaniel Wallace
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using KeyRingBuddy.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KeyRingBuddy.Controller.Desktop
{
    /// <summary>
    /// Application manager for desktop app.
    /// </summary>
    public class DesktopApplicationManager : ApplicationManager<IDesktopController>
    {
        #region Fields

        /// <summary>
        /// The desktop view to use.
        /// </summary>
        private IDesktopView _view;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="view">The desktop view to use.</param>
        public DesktopApplicationManager(IDesktopView view)
        {
            if (view == null)
                throw new ArgumentNullException("view");

            _view = view;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Move back one controller.
        /// </summary>
        public void Back()
        {
            _view.Back();
        }

        /// <summary>
        /// Restore a controller.
        /// </summary>
        /// <param name="controller">The controller to restore.</param>
        public void RestoreController(IDesktopController controller)
        {
            if (controller == null)
                throw new ArgumentNullException("controller");

            if (CloseController(CurrentController))
                OpenController(controller, FlowDirection.LeftToRight);
        }

        /// <summary>
        /// Display the controller in the view.
        /// </summary>
        /// <param name="controller">The controller to open.</param>
        /// <param name="direction">The direction to animate the transiont of the content.</param>
        protected void OpenController(IDesktopController controller, FlowDirection direction)
        {
            if (controller == null)
            {
                _view.SetContent(null, FlowDirection.LeftToRight, false);
                _view.ClearFunctions();
            }
            else
            {
                _view.SetContent(controller.GetContent(), direction, controller.StartWithFocus());
                _view.AddController(controller);
                _view.SetCurrentController(controller);
                for (int i = 0; ; i++)
                {
                    IEnumerable<string> functions = controller.GetFunctionNames(i);
                    if (functions == null || functions.Count() == 0)
                        break;

                    foreach (string function in functions)
                        _view.AddFunction(function, i);
                }

                controller.Opened();
            }
        }

        /// <summary>
        /// Display the controller in the view.
        /// </summary>
        /// <param name="controller">The controller to open.</param>
        protected override void OpenController(IDesktopController controller)
        {
            OpenController(controller, FlowDirection.RightToLeft);
        }

        /// <summary>
        /// Hide the controller in the view.
        /// </summary>
        /// <param name="controller">The controller to close.</param>
        /// <returns>true if the controller was closed, false if it wasn't.</returns>
        protected override bool CloseController(IDesktopController controller)
        {
            if (controller != null)
            {
                if (!controller.Closing())
                    return false;

                controller.Closed();

                _view.ClearFunctions();

                if (!controller.CanRestore())
                {
                    _view.RemoveController(controller);
                    RemoveController(controller);
                }

                return true;
            }
            else
            {
                return true;
            }
        }

        #endregion
    }
}
