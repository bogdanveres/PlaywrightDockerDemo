using System.Threading;
using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;

namespace PlaywrightSharp
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Test1()
        {
            //using var playwright = await Playwright.CreateAsync();
            //await using var browser = await playwright.Chromium.LaunchAsync(new() { Headless = false });
            //var page = await browser.NewPageAsync();
            //await page.GotoAsync("https://playwright.dev/dotnet");
            //await page.ScreenshotAsync(new() { Path = "screenshot.png" });

            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
            });
            var context = await browser.NewContextAsync(new BrowserNewContextOptions
            {
                RecordVideoDir = "videos/",
                RecordVideoSize = new RecordVideoSize() { Width = 640, Height = 480 }
            });
            // Open new page


            await context.Tracing.StartAsync(new TracingStartOptions
            {
                Screenshots = true,
                Snapshots = true
            });


            var page = await context.NewPageAsync();
            // Go to http://datingapp-pipeline.azurewebsites.net/
            await page.GotoAsync("http://datingapp-pipeline.azurewebsites.net/");
            // Click [placeholder="Username"]
            await page.ClickAsync("[placeholder=\"Username\"]");
            // Fill [placeholder="Username"]
            await page.FillAsync("[placeholder=\"Username\"]", "vinnie");
            // Press Tab
            await page.PressAsync("[placeholder=\"Username\"]", "Tab");
            // Fill [placeholder="Password"]
            await page.FillAsync("[placeholder=\"Password\"]", "password");
            // Click text=Login
            await page.RunAndWaitForNavigationAsync(async () =>
            {
                await page.ClickAsync("text=Login");
            }/*, new PageWaitForNavigationOptions
        {
            UrlString = "http://datingapp-pipeline.azurewebsites.net/members"
        }*/);
            // Click text=Welcome Vinnie
            await page.ClickAsync("text=Welcome Vinnie");
            // Click text=Logout
            await page.ClickAsync("text=Logout");
            // Assert.AreEqual("http://datingapp-pipeline.azurewebsites.net/", page.Url);

            await context.Tracing.StopAsync(new TracingStopOptions
            {
                Path = "trace.zip"
            });

            await context.CloseAsync();

            

        }

        [Test]
        public static async Task Main()
        {
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
            });
            var context = await browser.NewContextAsync();
            // Open new page
            var page = await context.NewPageAsync();
            // Go to https://www.wikipedia.org/


            

            await page.GotoAsync("https://www.wikipedia.org/");
        }

        [Test]
        public static async Task Add()
        {
            int a = 1;
            int b = 1;
            Assert.AreEqual(a + b, 2, "There was an error");
        }

        [Test]
        public static async Task Sub()
        {
            int a = 1;
            int b = 1;
            Assert.AreEqual(a - b, 0, "There was an error");
        }
    }
}
