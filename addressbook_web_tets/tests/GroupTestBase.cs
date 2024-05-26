//using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit;


namespace addressbook_web_tets
{
    public class GroupTestBase : AuthTestBase
    {
        [TearDown]
        public void CompareGroupsUI_DB() 
        {
            if (PERFORM_LONG_UI_CHECK)
            { 
                List<GroupDatacs> fromUI  = app.Groups.GetGroupList();
                List<GroupDatacs> fromDB = GroupDatacs.GetAll();
                fromUI.Sort();
                fromDB.Sort();
                Assert.AreEqual(fromUI, fromDB);
            }
        }
    }
}
