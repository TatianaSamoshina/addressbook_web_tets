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
using System.IO;

namespace addressbook_web_tets
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {
        public static IEnumerable<ContactDatas> RandomContactDataProvider()
        {
            List<ContactDatas> contact = new List<ContactDatas>();
            for (int i = 0; i < 5; i++)
            {
                contact.Add(new ContactDatas(GenerateRandomString(20), GenerateRandomString(20)));
            }
            return contact;
        }

        public static IEnumerable<ContactDatas> ContactDataFromFile()
        {
            List<ContactDatas> contact = new List<ContactDatas>();
            string[] lines = File.ReadAllLines(@"contacts.csv");
            foreach (string l in lines)
            {
                string[] parts = l.Split(',');
                contact.Add(new ContactDatas(parts[0], parts[1]));
            }
            return contact;
        }

        [Test, TestCaseSource("RandomContactDataProvider")]
        public void ContactTest(ContactDatas contact)
        {
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