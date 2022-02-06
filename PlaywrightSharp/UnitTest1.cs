using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;

namespace PlaywrightSharp
{
    public class Tests
    {
        [Test]
        public static async Task Main()
        {
            string _browserConfig = TestContext.Parameters.Get("browser");

            if (_browserConfig.Contains("Firefox"))
            {

                using var playwright = await Playwright.CreateAsync();
                await using var browser = await playwright.Firefox.LaunchAsync(new BrowserTypeLaunchOptions
                {
                    //headless run is required when test is running in Docker with Firefox
                    Headless = true,
                });
                var context = await browser.NewContextAsync();
                // Open new page
                var page = await context.NewPageAsync();
                // Go to https://www.wikipedia.org/

                await page.GotoAsync("https://www.wikipedia.org/");
            }
        }

    }
}
