using NUnit.Framework;
//using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_web_tets
{
    public class ContactTestBase : AuthTestBase
    {
        [TearDown]
        public void CompareContactsUI_DB()
        {
            if (PERFORM_LONG_UI_CHECK)
            {
                List<ContactDatas> fromUI = app.Contacts.GetContactList();
                List<ContactDatas> fromDB = ContactDatas.GetAll();
                fromUI.Sort();
                fromDB.Sort();
                Assert.AreEqual(fromUI, fromDB);
            }
        }
    }
}
