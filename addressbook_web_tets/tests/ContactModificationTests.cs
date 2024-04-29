using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_web_tets
{
    [TestFixture]
    public class ContactModificationTests : AuthTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            // Проверяем наличие контакта перед его модификацией
            if (!app.Contacts.IsContactPresent(1))
            {
                ContactDatas newData = new ContactDatas("n1", "l1");
                app.Contacts.Create(newData);
            }

            // Модифицируем контакт
            ContactDatas newDataForModification = new ContactDatas("newName", "newLastName");
            app.Contacts.Modify(1, newDataForModification);
        }
    }
}
