using JourneyPlanner.PageObjects;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JourneyPlanner
{
    public class Context
    {
        public WebDriver Driver { get; set; }
        public IWait<IWebDriver> WebDriverWait { get; set; }
    }
}
