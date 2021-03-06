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

using KeyRingBuddy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for ItemListControl.xaml
    /// </summary>
    public partial class ItemListControl : UserControl
    {
        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public ItemListControl()
        {
            InitializeComponent();
        }

        #endregion

        #region Properties

        /// <summary>
        /// The caption displayed.
        /// </summary>
        public string Caption
        {
            get { return textBlockCaption.Text; }
            set { textBlockCaption.Text = value; }
        }

        /// <summary>
        /// The currently selected item or null if there isn't one.
        /// </summary>
        public Item SelectedItem 
        {
            get 
            {
                foreach (ToggleButton button in stackPanel.Children)
                    if (button.IsChecked.HasValue && button.IsChecked.Value)
                        return button.Tag as Item;

                return null;
            }
            set
            {
                foreach (ToggleButton button in stackPanel.Children)
                {
                    if (Object.Equals(value, button.Tag))
                    {
                        button.IsChecked = true;
                        break;
                    }
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Clear out items.
        /// </summary>
        public void ClearItems()
        {
            bool isSelectedItem = (SelectedItem != null);
            stackPanel.Children.Clear();
            if (isSelectedItem)
                OnSelectedItemChanged(EventArgs.Empty);
        }

        /// <summary>
        /// Add an item to the view.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public void AddItem(Item item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            int index = 0;
            for (; index < stackPanel.Children.Count; index++)
            {
                ToggleButton b = stackPanel.Children[index] as ToggleButton;
                Item c = b.Tag as Item;
                int order = String.Compare(item.Name, c.Name);
                if (order <= 0)
                    break;
            }

            ToggleButton button = new ToggleButton();
            button.Style = FindResource("ChromeListButtonStyle") as Style;
            button.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            button.HorizontalAlignment = HorizontalAlignment.Stretch;
            button.Tag = item;

            if (item.Icon == null)
            {
                button.Content = item.Name;
            }
            else
            {
                DockPanel dp = new DockPanel();
                dp.LastChildFill = true;

                Image img = new Image();
                img.Height = 16;
                img.Width = 16;
                img.VerticalAlignment = VerticalAlignment.Center;
                img.Source =  item.Icon;
                DockPanel.SetDock(img, Dock.Left);
                dp.Children.Add(img);

                TextBlock tb = new TextBlock();
                tb.Text = item.Name;
                tb.Margin = new Thickness(10, 0, 10, 0);
                tb.VerticalAlignment = VerticalAlignment.Center;
                dp.Children.Add(tb);

                button.Content = dp;
            }

            if (index == stackPanel.Children.Count)
                stackPanel.Children.Add(button);
            else
                stackPanel.Children.Insert(index, button);
        }

        /// <summary>
        /// Remove the given item from the list.
        /// </summary>
        /// <param name="item">Object that raised the event.</param>
        public void RemoveItem(Item item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            for (int i = 0; i < stackPanel.Children.Count; i++)
            {
                if (Object.Equals(item, (stackPanel.Children[i] as ToggleButton).Tag))
                {
                    bool isSelected = SelectedItem != null;
                    stackPanel.Children.RemoveAt(i);
                    if (isSelected && SelectedItem == null)
                        OnSelectedItemChanged(EventArgs.Empty);

                    break;
                }
            }
        }

        /// <summary>
        /// Raises the SelectedItemChanged event.
        /// </summary>
        /// <param name="e">Arguments to pass with the event.</param>
        protected virtual void OnSelectedItemChanged(EventArgs e)
        {
            if (SelectedItemChanged != null)
                SelectedItemChanged(this, e);
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Mark the newly selected category.
        /// </summary>
        /// <param name="sender">Object that raised the event.</param>
        /// <param name="e">Event arguments.</param>
        private void stackPanel_Checked(object sender, RoutedEventArgs e)
        {
            foreach (ToggleButton button in stackPanel.Children)
            {
                if (button != e.Source)
                {
                    button.IsChecked = false;
                    button.IsEnabled = true;
                }
                else
                {
                    button.IsEnabled = false;
                }
            }

            OnSelectedItemChanged(EventArgs.Empty);
            (e.Source as ToggleButton).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        }

        #endregion

        #region Events

        /// <summary>
        /// Raised when the SelectedItem is changed.
        /// </summary>
        public event EventHandler SelectedItemChanged;

        #endregion
    }
}
