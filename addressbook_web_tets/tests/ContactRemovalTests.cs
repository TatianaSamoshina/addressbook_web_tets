using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_web_tets
{
    [TestFixture]
    public class ContactRemovalTests : AuthTestBase//TestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            // Проверяем наличие контакта перед его удалением
            if (!app.Contacts.IsContactPresent(1))
            {
                ContactDatas contact = new ContactDatas("n1", "l1");
                app.Contacts.Create(contact);
            }

            List<ContactDatas> oldContacts = app.Contacts.GetContactList();
            // Удаляем контакт
            app.Contacts.Remove(1);
            List<ContactDatas> newContacts = app.Contacts.GetContactList();
            oldContacts.RemoveAt(1);
            //Проверка
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(oldContacts.SequenceEqual(newContacts));
        }
    }
}
