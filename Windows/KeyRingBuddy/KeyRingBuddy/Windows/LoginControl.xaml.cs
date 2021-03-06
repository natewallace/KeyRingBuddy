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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KeyRingBuddy.Windows
{
    /// <summary>
    /// Interaction logic for LoginControl.xaml
    /// </summary>
    public partial class LoginControl : UserControl
    {
        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public LoginControl()
        {
            InitializeComponent();
        }

        #endregion

        #region Properties

        /// <summary>
        /// The icon displayed.
        /// </summary>
        public UIElement Icon
        {
            get { return borderIcon.Child; }
            set { borderIcon.Child = value; }
        }

        /// <summary>
        /// The caption displayed.
        /// </summary>
        public string Caption
        {
            get { return textBlockCaption.Text; }
            set { textBlockCaption.Text = value; }
        }

        /// <summary>
        /// The password entered by the user.
        /// </summary>
        public string Password
        {
            get { return passwordBox.Password; }
            set { passwordBox.Password = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Display an error message.
        /// </summary>
        /// <param name="message">The message to display.</param>
        public void SetErrorMessage(string message)
        {
            textBlockError.Text = message;
            textBlockError.Foreground = Brushes.Firebrick;
        }

        /// <summary>
        /// Display a warning message.
        /// </summary>
        /// <param name="message">The message to display.</param>
        public void SetWarningMessage(string message)
        {
            textBlockError.Text = message;
            textBlockError.Foreground = Brushes.Goldenrod;
        }

        /// <summary>
        /// Raises the LoginClick event.
        /// </summary>
        /// <param name="e">Arguments to pass with the event.</param>
        protected virtual void OnLoginClick(EventArgs e)
        {
            if (LoginClick != null)
                LoginClick(this, e);
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Enable/disable login button based on password box being empty or not.
        /// </summary>
        /// <param name="sender">Object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (Console.CapsLock)
                SetWarningMessage("Caps lock is on.");
            else
                SetErrorMessage(null);
        }

        /// <summary>
        /// Raises the LoginClick event.
        /// </summary>
        /// <param name="sender">Object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(Password))
                SetErrorMessage("A password is required.");
            else
                OnLoginClick(EventArgs.Empty);
        }

        /// <summary>
        /// Raises the LoginClick event if the enter key is pressed.
        /// </summary>
        /// <param name="sender">Object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void passwordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                OnLoginClick(EventArgs.Empty);
        }

        #endregion

        #region Events

        /// <summary>
        /// Raised when the user clicks the login button.
        /// </summary>
        public event EventHandler LoginClick;

        #endregion
    }
}
