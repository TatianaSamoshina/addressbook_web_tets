using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using OpenQA.Selenium.Support.UI;
using System.Threading;

namespace addressbook_web_tets
{
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager manager) : base(manager) { }
        public ContactHelper Create(ContactDatas contact)
        {
            manager.Navigator.GoToContactsPage();
            FillContactForm(contact);
            SubmitContactCreation();
            manager.Navigator.ReturnToHomePage();
            //manager.Auth.Logout();
            return this;
        }

        public ContactHelper Modify(int v, ContactDatas newData)
        {
            manager.Navigator.ReturnToHomePage();
            SelectContact(v);
            FillContactForm(newData);
            SubmitContactModification();
            manager.Navigator.ReturnToHomePage();
            //manager.Auth.Logout();
            return this;
        }

        public ContactHelper Remove(int p)
        {
            manager.Navigator.ReturnToHomePage();
            SelectContactDelete(p);
            RemoveContact();
            manager.Navigator.ReturnToHomePage();
            //manager.Auth.Logout();
            return this;
        }


        public ContactHelper SelectContact(int index)
        {
            driver.FindElement(By.XPath("//table[@id='maintable']/tbody/tr[" + (index + 2) + "]/td[8]/a/img")).Click();
            return this;
        }

        public bool IsContactPresent(int index)
        {
            try
            {
                driver.FindElement(By.XPath("//table[@id='maintable']/tbody/tr[" + (index + 1) + "]/td[8]/a/img"));
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    
        public bool IsContactDeletePresent(int index)
        {
            try
            {
                driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + index + "]"));
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        public ContactHelper SelectContactDelete(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index + 1) + "]")).Click();
            return this;
        }

        public ContactHelper RemoveContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            contactCashe = null;
            return this;
        }

        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            contactCashe = null;
            return this;
        }


        public ContactHelper FillContactForm(ContactDatas contact)
        {
            Type(By.Name("firstname"), contact.FName);
            Type(By.Name("lastname"), contact.LName);
            return this;
        }
        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.XPath("//div[@id='content']/form/input[20]")).Click();
            contactCashe = null;
            return this;
        }

        private List<ContactDatas> contactCashe = null;
        public List<ContactDatas> GetContactList()
        {

            if (contactCashe == null)
            {
                contactCashe = new List<ContactDatas>();
                manager.Navigator.ReturnToHomePage();
                IWebElement table = driver.FindElement(By.Id("maintable"));
                ICollection<IWebElement> rows = table.FindElements(By.CssSelector("tbody tr[name='entry']"));

                foreach (IWebElement row in rows)
                {

                    // Получаем текст из ячеек Last name и First name
                    string lastName = row.FindElement(By.XPath("./td[2]")).Text;
                    string firstName = row.FindElement(By.XPath("./td[3]")).Text;

                    contactCashe.Add(new ContactDatas(firstName, lastName)
                    {
                        IdContact = row.FindElement(By.TagName("input")).GetAttribute("value")
                    });
                }
            }
            return new List<ContactDatas>(contactCashe);
        }

        public int GetContactCount()
        {
            return driver.FindElements(By.CssSelector("tbody tr[name='entry']")).Count;
        }
    }
    
}
