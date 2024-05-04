﻿using NUnit.Framework;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Threading;
using OpenQA.Selenium.Support.UI;

namespace addressbook_web_tets
{
    public class TestBase
    {
        public ApplicationManager app;
        [SetUp]
        public void SetupApplicationManager()
        {
            app = new ApplicationManager();
            app.Navigator.OpenHomePage();
        }

        [TearDown]
        public void TeardownTest()
        {
            app.Stop();
        }

    }
}
