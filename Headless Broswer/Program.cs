using System;
using System.Collections.Generic;
using System.Threading;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.IO;

namespace Headless_Broswer
{
    class Program
    {
        private static string EMAIL = File.ReadAllText("email.txt");
        private const string URL = "https://www.starkl.hu/katalogus";
        private const string GeckoDriverPath = @"C:\\Cs Projects\\Headless Broswer\\_latestgecko";
        private const string FirefoxPath = @"C:\Program Files\Mozilla Firefox\firefox.exe";

        static void Main()
        {
            FirefoxProfile profile = new FirefoxProfile();

            FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(GeckoDriverPath);

            service.FirefoxBinaryPath = FirefoxPath;

            profile.SetPreference("permissions.default.stylesheet", 2);
            profile.SetPreference("permissions.default.image", 2);
            profile.SetPreference("permissions.default.script", 2);

            FirefoxOptions options = new FirefoxOptions()
            {
                Profile = profile,
                PageLoadStrategy = PageLoadStrategy.Normal
            };

            // Hide browser window
            options.AddArgument("--headless");

            IWebDriver driver = new FirefoxDriver(service, options);

            driver.Url = URL;

            SubscribeToStarkl(driver);

            Thread.Sleep(5000);

            driver.Quit();
        }
        
        static public void SubscribeToStarkl(IWebDriver driver)
        {
            // ha meglátod ezt akkor 2 napom van hátra.

            Actions actions = new Actions(driver);

            // driver.Manage().Window.Minimize();

            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;

            // actions.KeyDown(Keys.Alt).KeyUp(Keys.Alt).SendKeys("nsn").Perform();

            // Disable CSS this way because of stupid frozen value elsewhere
            executor.ExecuteScript("for ( i=0; i<document.styleSheets.length; i++) {void(document.styleSheets.item(i).disabled=true);}");

            IWebElement COOKIESOKBUTTON = driver.FindElement(By.ClassName("eu-cookies-ok"));

            IWebElement checkbox1 = driver.FindElement(By.Id("email1check"));

            IWebElement checkbox2 = driver.FindElement(By.Id("email2check"));

            IWebElement emailBox1 = driver.FindElement(By.Id("email1"));
            IWebElement emailBox2 = driver.FindElement(By.Id("email2"));

            IWebElement confirmButton = driver.FindElement(By.XPath("//input[@style='background-color: #cb2e63;']"));

            COOKIESOKBUTTON.Click();

            emailBox1.SendKeys(EMAIL);
            emailBox2.SendKeys(EMAIL);

            // wait.Until((drv) => drv.FindElement(By.Id("mailing_kat_1")).Displayed == true);

            checkbox1.Click();
            checkbox2.Click();

            actions.ScrollToElement(confirmButton);

            confirmButton.Submit();
        }

    }
}
