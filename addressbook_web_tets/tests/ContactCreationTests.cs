﻿using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using System.Linq;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace addressbook_web_tets
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {
        [Test]
        public void ContactTest()
        {
            ContactDatas contact = new ContactDatas("n100", "l100");
            List<ContactDatas> oldContacts = app.Contacts.GetContactList();
            app.Contacts.Create(contact);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(oldContacts.Count + 1, app.Contacts.GetContactCount());
            List<ContactDatas> newContacts = app.Contacts.GetContactList();
            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(oldContacts.SequenceEqual(newContacts));
        }
    }
}