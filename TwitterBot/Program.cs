using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.IO;

namespace TwitterBot
{
    class Program
    {
        static void Main(string[] args)
        {


            ChromeDriverService chSvc = ChromeDriverService.CreateDefaultService(".");
            ChromeOptions chOption = new ChromeOptions();
            chOption.AddArguments("user-data-dir=ChromeProfile Location");
            chOption.AcceptInsecureCertificates = true;
            IWebDriver webDriver = new ChromeDriver(chSvc, chOption);
            for (int i = 0; i < 3; i++)
            {
                webDriver.Navigate().GoToUrl("https://twitter.com/following");
                System.Threading.Thread.Sleep(3000);
                IJavaScriptExecutor js = (IJavaScriptExecutor)webDriver;
                string script = "(()=>{const o=({seconds:o})=>new Promise(e=>{console.log(`WAITING FOR ${o} SECONDS...`),setTimeout(e,1e3*o)}),e=async()=>{window.scrollTo(0,document.body.scrollHeight),await o({seconds:1});const c=Array.from(document.querySelectorAll(\'[data-testid$=\"-unfollow\"]\')),t=c.length;if(0===t)return console.log(\"NO ACCOUNTS FOUND, SO I THINK WE\'RE DONE\"),void console.log(\"RELOAD PAGE AND RE-RUN SCRIPT IF ANY WERE MISSED\");console.log(`UNFOLLOWING ${t} USERS...`),await Promise.all(c.map(async e=>{e.click(),await o({seconds:1}),document.querySelector(\'[data-testid=\"confirmationSheetConfirm\"]\').click()})),await o({seconds:2}),e()};e()})();";
                js.ExecuteScript(script);
            }
            webDriver.Close();

        }
    }
}
