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
            app.Contacts.Remove(0);
            //Проверка
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(oldContacts.Count - 1, app.Contacts.GetContactCount());
            List<ContactDatas> newContacts = app.Contacts.GetContactList();
            ContactDatas toBeRemoved = oldContacts[0];
            oldContacts.RemoveAt(0);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(oldContacts.SequenceEqual(newContacts));
            foreach (ContactDatas contact in newContacts)
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreNotEqual(contact.IdContact, toBeRemoved.IdContact);
            }
        }
    }
}
