using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_web_tets
{
    [TestFixture]
    public class LoginTests : TestBase//AuthTestBase
    {
        [Test]
        public void LoginWithValidCredentials() 
        {
            //prepare
            app.Auth.Logout();
            //action
            AccountData account = new AccountData("admin", "secret");
            app.Auth.Login(account);
            //verification
            Assert.That(app.Auth.IsLoggedIn(account), Is.True, "Пользователь не вошел в систему.");
        }


        [Test]
        public void LoginWithInvalidCredentials()
        {
            //prepare
            app.Auth.Logout();
            //action
            AccountData account = new AccountData("admin", "123456");
            app.Auth.Login(account);
            //verification
            Assert.That(app.Auth.IsLoggedIn(account), Is.False, "Пользователь вошел в систему.");
            //Assert.IsFalse(app.Auth.IsLoggedIn(account));
        }
    }
}
