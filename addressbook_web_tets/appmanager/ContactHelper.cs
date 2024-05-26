using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Text.RegularExpressions;
//using System.Security.Policy;
//using System.Reflection;
using OpenQA.Selenium.Support.UI;

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
            return this;
        }

        public ContactHelper Modify(int v, ContactDatas newData)
        {
            manager.Navigator.ReturnToHomePage();
            SelectContact(v);
            FillContactForm(newData);
            SubmitContactModification();
            manager.Navigator.ReturnToHomePage();
            return this;
        }

        public ContactHelper Modify2(ContactDatas contact, ContactDatas newData)
        {
            manager.Navigator.ReturnToHomePage();
            SelectContact(contact.IdContact);
            FillContactForm(newData);
            SubmitContactModification();
            manager.Navigator.ReturnToHomePage();
            return this;
        }

        public ContactHelper Remove(int p)
        {
            manager.Navigator.ReturnToHomePage();
            SelectContactDelete(p);
            RemoveContact();
            manager.Navigator.ReturnToHomePage();
            return this;
        }

        public ContactHelper Removed(ContactDatas contact)
        {
            manager.Navigator.ReturnToHomePage();
            SelectContactDelete(contact.IdContact);
            RemoveContact();
            manager.Navigator.ReturnToHomePage();
            return this;
        }

        public ContactHelper SelectContactDelete(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index + 1) + "]")).Click();
            return this;
        }
        public ContactHelper SelectContactDelete(string index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]' and @value='" + index + "'])")).Click();
            return this;
        }


        public ContactHelper SelectContact(int index)
        {
            driver.FindElement(By.XPath("//table[@id='maintable']/tbody/tr[" + (index + 2) + "]/td[8]/a/img")).Click();
            return this;
        }

        public ContactHelper SelectContact(string index)
        {
            driver.FindElements(By.Name("entry"))[int.Parse(index)].FindElements(By.TagName("td"))[7].FindElement(By.TagName("a")).Click();
            return this;
        }

        public bool IsContactPresent(int index)
        {
            try
            {
                driver.FindElement(By.XPath("//table[@id='maintable']/tbody/tr[" + (index + 1) + "]/td[8]/a/img"));
                return true;
            }
            catch (NoSuchElementException)  {  return false; }
        }
    
        public bool IsContactDeletePresent(int index)
        {
            try
            {
                driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + index + "]"));
                return true;
            }
            catch (NoSuchElementException) { return false; }
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
       
        public ContactDatas GetContactInformationFromTable(int index)
        {
            manager.Navigator.ReturnToHomePage();
            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"));
            string lastName = cells[1].Text;
            string firstName = cells[2].Text;
            string address = cells[3].Text;
            string allPhones = cells[5].Text;
            string allEmails = cells[4].Text;

            return new ContactDatas(firstName, lastName)
            {
                Address = address,
                AllPhones = allPhones,
                AllEmails = allEmails
            };
        }

        public ContactDatas GetContactInformationFromEditForm(int index)
        {
            manager.Navigator.ReturnToHomePage();
            SelectContact(0);
            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");
            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");
            string email = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");

            return new ContactDatas(firstName, lastName)
            {
                Address=address,
                HomePhone=homePhone,
                MobilePhone=mobilePhone,
                WorkPhone=workPhone,
                Email= email,
                Email2 = email2,
                Email3 = email3
            };
        }

        /*public void InitContactModification(int index)
        {
            driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"))[7]
                .FindElement(By.TagName("a")).Click();
        }*/

        public void InitContactProperties(int index)
        {
            driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"))[6]
                .FindElement(By.TagName("a")).Click();
        }

        public int GetNumberOfSearchResults()
        {
            manager.Navigator.ReturnToHomePage();
            string text = driver.FindElement(By.Id("search_count")).Text;
            Match m = new Regex(@"\d+").Match(text);
            return Int32.Parse(m.Value);
        }

        public string FormatContactDetails(ContactDatas contact)
        {                      
            string fullName = $"{contact.FName} {contact.LName}".Trim(); // Имя
            string address = string.IsNullOrWhiteSpace(contact.Address) ? "" : contact.Address.Trim(); //адрес, если не пустой
            string phones = string.Join("\n", //телефоны + пробел после префиксов
                new string[] {
            CleanUp(contact.HomePhone, "H: "),
            CleanUp(contact.MobilePhone, "M: "),
            CleanUp(contact.WorkPhone, "W: ")
                }.Where(p => !string.IsNullOrWhiteSpace(p))).Trim();           
            string emails = string.Join("\n", // email'ы
                new string[] { contact.Email, contact.Email2, contact.Email3 }
                .Where(e => !string.IsNullOrWhiteSpace(e))).Trim();


            StringBuilder result = new StringBuilder();

            result.Append(fullName);
            if (!string.IsNullOrWhiteSpace(address))
            {
                if (result.Length > 0) result.Append("\n");
                result.Append(address);
            }
            if (!string.IsNullOrWhiteSpace(phones))
            {
                if (result.Length > 0) result.Append("\n\n");
                result.Append(phones);
            }
            if (!string.IsNullOrWhiteSpace(emails))
            {
                if (result.Length > 0) result.Append("\n\n");
                result.Append(emails);
            }

            return result.ToString().Trim();
        }
        public string GetContactInformationFromPropertiesAsText(int index)
        {
            manager.Navigator.ReturnToHomePage();
            InitContactProperties(index);
            IWebElement contentElement = driver.FindElement(By.Id("content"));
            string innerText = contentElement.Text;
            return innerText;
        }

        private string CleanUp(string phone, string prefix)
        {
            if (string.IsNullOrWhiteSpace(phone))
            {
                return "";
            }
            return $"{prefix}{phone.Trim()}";
        }

        public void AddContactToGroup(ContactDatas contact, GroupDatacs group)
        {
            manager.Navigator.ReturnToHomePage();
            ClearGroupFilter();
            SelectContactDelete(contact.IdContact);
            SelectGroupToAdd(group.Name);
            CommitAddContactToGroup();
        }

        public void CommitAddContactToGroup()
        {
            driver.FindElement(By.Name("add")).Click();
        }

        public void SelectGroupToAdd(string name)
        {
            new SelectElement(driver.FindElement(By.Name("to_group"))).SelectByText(name);
        }

        public void ClearGroupFilter()
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText("[all]");
        }

        public void DeleteContactToGroup(ContactDatas contact, GroupDatacs group)
        {
            manager.Navigator.ReturnToHomePage();
            SelectGroupFilter(group.Name);
            Thread.Sleep(2000); // Задержка в 2 секунды для обновления страницы
            Console.WriteLine($"Attempting to find contact with Id {contact.IdContact} in group {group.Name}");
            IWebElement contactElement = driver.FindElement(By.XPath("//input[@name='selected[]']")); //выбрать первый контакт в группе
            string contactId = contactElement.GetAttribute("value"); //значение атрибута value для первого контакта
            Console.WriteLine($"First contact in group has Id {contactId}");       
            RemoveContactFromGroup(contactId); // Выбор первого контакта
            RemoveContact();
        }

        private void RemoveContactFromGroup(string index)
        {
            driver.FindElement(By.XPath($"//input[@name='selected[]' and @value='{index}']")).Click();
        }

        public void SelectContactDeleteToGroup(string idContact)
        {
            driver.FindElement(By.XPath("//input[@name='selected[]' and @value='" + idContact + "']")).Click();
        }

        public void SelectGroupFilter(string name)
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText(name);
        }
    }   
}
