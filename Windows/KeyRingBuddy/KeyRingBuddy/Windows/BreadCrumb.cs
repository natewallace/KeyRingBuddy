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

namespace KeyRingBuddy.Windows
{
    /// <summary>
    /// A bread crumb.
    /// </summary>
    public class BreadCrumb
    {
        #region Properties

        /// <summary>
        /// The name for the bread crumb.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// An object linked to the bread crumb.
        /// </summary>
        public object Tag { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="tag">Tag.</param>
        public BreadCrumb(string name, object tag)
        {
            Name = name;
            Tag = tag;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Use the Tag property to check for equality.
        /// </summary>
        /// <param name="obj">The object to compare with this one.</param>
        /// <returns>true if the object is logically equal to this one, false if it isn't.</returns>
        public override bool Equals(object obj)
        {
            if (obj is BreadCrumb)
                return Object.Equals((obj as BreadCrumb).Tag, this.Tag);
            else
                return false;
        }

        /// <summary>
        /// Use the Tag properties hash code.
        /// </summary>
        /// <returns>The Tag properties hash code.</returns>
        public override int GetHashCode()
        {
            return (Tag == null) ? 0 : Tag.GetHashCode();
        }

        /// <summary>
        /// The Name property is returned.
        /// </summary>
        /// <returns>The Name property.</returns>
        public override string ToString()
        {
            return Name;
        }

        #endregion
    }
}
