using System;
using System.Collections.Generic;
using System.Linq;
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

            // Удаляем контакт
            app.Contacts.Remove(1);
        }
    }
}
