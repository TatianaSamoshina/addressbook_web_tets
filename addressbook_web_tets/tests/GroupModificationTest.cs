using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_web_tets
{
    [TestFixture]
    public class GroupModificationTest : TestBase
    {
        [Test]
        public void GroupModificationTests()
        {
            GroupDatacs newData = new GroupDatacs("qw", "as", "zx");
            app.Groups.Modify(1, newData);
        }
    }
}
