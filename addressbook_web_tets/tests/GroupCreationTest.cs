using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

namespace addressbook_web_tets
{
    [TestFixture]
    public class GroupCreationTests : TestBase
    {
        [Test]
        public void GroupCreationTest()
        {
            app.Navigator.GoToGroupsPage();
            app.Groups
                .InitGroupCreation()
                .FillGroupForm(new GroupDatacs("qw","as","zx"))
                .SubmitGroupCreation();
            app.Navigator.ReturnToGroupPage();
            app.Auth.Logout();
        }
    }
}