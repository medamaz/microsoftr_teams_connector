using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace microsftteams
{
    class ConnectionSl
    {
        string browser;
        IWebDriver Driver;
        string urlg = "https://teams.microsoft.com/go#";
        string chromedriverfile = "";
        string chromeBinaryLocation = "";
        public string Chromedriverfile{ get => chromedriverfile; set => chromedriverfile = value;}
        public string ChromeBinaryLocation { get => chromeBinaryLocation; set => chromeBinaryLocation = value; }
        public string Browser { get => browser; set => browser = value; }

        public string Login(string url , string Email , string Password)
        {
            try
            {
                int time = Convert.ToInt32(DateTime.Now.Hour);
                //------------------------------------this condition check if the client have a classes in the current time or not---------------- -
                if (url.Equals(""))
                {
                    return "classe not found at the time :  " + time.ToString() + " : " + DateTime.Now.Minute.ToString();
                }
                //---------------------------------------------------------------------------------
                else
                {
                    //declaration css object required------------------------------------------------------------------------------
                    var EMAILFIELD = (By.Id("i0116"));
                    var PASSWORDFIELD = (By.Id("i0118"));
                    var NEXTBUTTON = (By.Id("idSIButton9"));
                    var Rejoindre = (By.ClassName("ts-btn-primary"));
                    var Contunue = (By.ClassName("ts-btn-fluent-secondary-alternate"));
                    var Hand = (By.ClassName("icons-hand-unfilled"));
                    //---------------------------------------------------------------------------------------------------------------
                    if (Browser.Equals("chrome"))
                    {
                        if (chromedriverfile.Equals("") && chromeBinaryLocation.Equals(""))
                        {
                            var options = new ChromeOptions();
                            options.BinaryLocation = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
                            Driver = new ChromeDriver(@"P:\Downloads\Compressed\Microsft_Teams_Connector_Final_App\Microsft_Teams_Connector_Final_App\microsftteams\WebDriver", options);
                        }
                        else if (chromedriverfile != "" && chromeBinaryLocation.Equals(""))
                        {
                        var options = new ChromeOptions();
                        options.BinaryLocation = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
                        Driver = new ChromeDriver(chromedriverfile, options);
                        }
                        else if (chromedriverfile.Equals("") && chromeBinaryLocation!="")
                        {
                            var options = new ChromeOptions();
                            options.BinaryLocation = chromeBinaryLocation;
                            Driver = new ChromeDriver(@"P:\Downloads\Compressed\Microsft_Teams_Connector_Final_App\Microsft_Teams_Connector_Final_App\microsftteams\WebDriver", options);
                        }
                        else
                        {
                            var options = new ChromeOptions();
                            options.BinaryLocation = chromeBinaryLocation;
                            Driver = new ChromeDriver(chromedriverfile, options);
                        }
                }
                    
                    else if (Browser.Equals("firefox"))
                        Driver = new FirefoxDriver(@"P:\Downloads\Compressed\Microsft_Teams_Connector_Final_App\Microsft_Teams_Connector_Final_App\microsftteams\WebDriver");
                    else
                        Driver = new EdgeDriver(@"P:\Downloads\Compressed\Microsft_Teams_Connector_Final_App\Microsft_Teams_Connector_Final_App\microsftteams\WebDriver");
                    Driver.Manage().Window.Maximize();
                    WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(30));
                    wait.Until(Driver => Driver.Navigate()).GoToUrl(this.urlg);
                    Thread.Sleep(3000);
                    wait.Until(Driver => Driver.FindElement(EMAILFIELD)).SendKeys(Email);
                    wait.Until(Driver => Driver.FindElement(NEXTBUTTON)).Click();
                    Thread.Sleep(1000);
                    wait.Until(Driver => Driver.FindElement(PASSWORDFIELD)).SendKeys(Password);
                    wait.Until(Driver => Driver.FindElement(NEXTBUTTON)).Click();
                    Thread.Sleep(1000);
                    wait.Until(Driver => Driver.FindElement(NEXTBUTTON)).Click();
                    Thread.Sleep(10000);
                    wait.Until(Driver => Driver.Navigate()).GoToUrl(url);
                    Thread.Sleep(1000);
                    wait.Until(Driver => Driver.Navigate()).GoToUrl(url);
                    wait.Until(Driver => Driver.FindElement(Rejoindre)).Click();
                    Thread.Sleep(5000);
                    wait.Until(Driver => Driver.FindElement(Contunue)).Click();
                    Thread.Sleep(1000);
                    wait.Until(Driver => Driver.FindElement(Rejoindre)).Click();
                    return "connection succes";
                }
            }
            catch (Exception ex)
            {
                return(ex.Message);
            }
        }
    }
}
