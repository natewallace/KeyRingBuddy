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
using System.IO;
using System.Xml.Serialization;
using KeyRingBuddy.Framework;
using KeyRingBuddy.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KeyRingBuddyTests
{
    /// <summary>
    /// Tests for the account class.
    /// </summary>
    [TestClass]
    public class AccountTests
    {
        /// <summary>
        /// Test the constructor.
        /// </summary>
        [TestMethod]
        public void Account_ConstructorTest()
        {
            Account a = new Account("Test Account", "My Tests", "http://www.google.com");
            Assert.AreNotEqual<Guid>(Guid.Empty, a.Id);
            Assert.AreEqual<string>("Test Account", a.Name);
            Assert.AreEqual<string>("My Tests", a.Category);
            Assert.AreEqual<string>("http://www.google.com", a.Site);
            Assert.IsNotNull(a.Details);
        }

        /// <summary>
        /// Test the static Equals method.
        /// </summary>
        [TestMethod]
        public void Account_StaticEqualsTest()
        {
            Account a = new Account("test account", "test category", "testSite");
            Account b = new Account("test account2", "test category2", "testSite2");
            Account c = new Account("test account2", "test category2", "testSite2");
            Account d = null;

            Assert.IsTrue(Account.Equals(a, a));
            Assert.IsFalse(Account.Equals(a, b));
            Assert.IsFalse(Account.Equals(a, c));
            Assert.IsFalse(Account.Equals(a, d));
        }

        /// <summary>
        /// Test the Equals method.
        /// </summary>
        [TestMethod]
        public void Account_EqualsTest()
        {
            Account a = new Account("test account", "test category", "testSite");
            Account b = new Account("test account2", "test category2", "testSite2");
            Account c = new Account("test account2", "test category2", "testSite2");
            Account d = null;

            Assert.IsTrue(a.Equals(a));
            Assert.IsFalse(a.Equals(b));
            Assert.IsFalse(a.Equals(c));
            Assert.IsFalse(a.Equals(d));
        }

        /// <summary>
        /// Test the serialization methods.
        /// </summary>
        [TestMethod]
        public void Account_XmlSerializeTest()
        {
            Account a = new Account("test account", "test category", "testSite");

            XmlSerializer ser = new XmlSerializer(typeof(Account));
            using (StringWriter writer = new StringWriter())
            {
                ser.Serialize(writer, a);
                string xml = writer.ToString();
                Console.Write(xml);

                using (StringReader reader = new StringReader(xml))
                {
                    Account aReadIn = (Account)ser.Deserialize(reader);
                    Assert.AreEqual<Guid>(a.Id, aReadIn.Id);
                    Assert.AreEqual<string>("test account", aReadIn.Name);
                    Assert.AreEqual<string>("test category", aReadIn.Category);
                    Assert.AreEqual<string>("testSite", aReadIn.Site);
                    Assert.AreEqual<int>(0, aReadIn.Details.Count);
                }
            }

            a.Details.Add(new AccountDetail("testDetail1", "1"));
            a.Details.Add(new AccountDetail("testDetail2", "2"));

            using (StringWriter writer = new StringWriter())
            {
                ser.Serialize(writer, a);
                string xml = writer.ToString();
                Console.Write(xml);

                using (StringReader reader = new StringReader(xml))
                {
                    Account aReadIn = (Account)ser.Deserialize(reader);
                    Assert.AreEqual<Guid>(a.Id, aReadIn.Id);
                    Assert.AreEqual<string>("testSite", aReadIn.Site);

                    Assert.AreEqual<int>(2, aReadIn.Details.Count);
                    Assert.AreEqual<string>("testDetail1", aReadIn.Details[0].Name);
                    Assert.AreEqual<string>("1", aReadIn.Details[0].Value);
                    Assert.AreEqual<string>("testDetail2", aReadIn.Details[1].Name);
                    Assert.AreEqual<string>("2", aReadIn.Details[1].Value);
                }
            }
        }
    }
}
