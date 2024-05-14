﻿using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using System.Text.RegularExpressions;

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

        public ContactHelper Remove(int p)
        {
            manager.Navigator.ReturnToHomePage();
            SelectContactDelete(p);
            RemoveContact();
            manager.Navigator.ReturnToHomePage();
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

        
        public ContactDatas GetContactInformationFromTable(int index)
        {
            manager.Navigator.ReturnToHomePage();
            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"));
            string lastName = cells[1].Text;
            string firstName = cells[2].Text;
            string address = cells[3].Text;
            string allPhones = cells[5].Text;

            return new ContactDatas(firstName, lastName)
            {
                Address = address,
                AllPhones = allPhones
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

            return new ContactDatas(firstName, lastName)
            {
                Address=address,
                HomePhone=homePhone,
                MobilePhone=mobilePhone,
                WorkPhone=workPhone
            };
        }
        /*public void InitContactModification(int index)
        {
            driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"))[7]
                .FindElement(By.TagName("a")).Click();
        }*/

        public int GetNumberOfSearchResults()
        {
            manager.Navigator.ReturnToHomePage();
            string text = driver.FindElement(By.Id("search_count")).Text;
            Match m = new Regex(@"\d+").Match(text);
            return Int32.Parse(m.Value);
        }
    }
    
}
