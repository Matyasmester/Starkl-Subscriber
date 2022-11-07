using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace Headless_Broswer
{
    public class MailingList
    {
        public string URL;
        public List<IWebElement> Elements;

        public MailingList(string URL, List<IWebElement> Elements)
        {
            this.URL = URL;
            this.Elements = Elements;
        }
    }
}
