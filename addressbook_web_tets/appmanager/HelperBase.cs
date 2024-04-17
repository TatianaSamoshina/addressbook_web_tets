using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Firefox;

namespace addressbook_web_tets
{
    public class HelperBase
    {
        public ApplicationManager manager;
        public IWebDriver driver;
        public HelperBase(ApplicationManager manager)
        {
            this.manager=manager;
            driver = manager.Driver;
        }
    }
}