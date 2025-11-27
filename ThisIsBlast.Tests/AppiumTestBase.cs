using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using NUnit.Framework;
using System.Collections.Generic;

public abstract class AppiumTestBase
{
    protected AndroidDriver Driver { get; set; } = TestRunSetup.Driver;

    [OneTimeSetUp]
    public virtual void OneTimeSetUp()
    {
        // Install app only once before all tests
        var commandTimeout = TimeSpan.FromSeconds(60);
        var options = new AppiumOptions
        {
            PlatformName = "Android",
            AutomationName = AutomationName.AndroidUIAutomator2,
            DeviceName = "Android Emulator"
        };

        options.App = "./ThisIsBlast.Tests/TestApps/157.apk";
        options.AddAdditionalAppiumOption("appPackage", "com.kiragan.blastjam");
        options.AddAdditionalAppiumOption("appActivity", "com.google.firebase.MessagingUnityPlayerActivity");
        options.AddAdditionalAppiumOption("installOptions", new string[] { "--no-verify" });
        options.AddAdditionalAppiumOption("disableIdlingResource", true);
        options.AddAdditionalAppiumOption("noReset", false); // Install fresh once

        Driver = new AndroidDriver(new Uri("http://127.0.0.1:4723"), options, commandTimeout);
        System.Threading.Thread.Sleep(3000);
    }

    [SetUp]
    public virtual void SetUp()
    {
        // Reuse driver for each test without reinstalling
        if (Driver == null)
        {
            var commandTimeout = TimeSpan.FromSeconds(60);
            var options = new AppiumOptions
            {
                PlatformName = "Android",
                AutomationName = AutomationName.AndroidUIAutomator2,
                DeviceName = "Android Emulator"
            };

            options.AddAdditionalAppiumOption("appPackage", "com.kiragan.blastjam");
            options.AddAdditionalAppiumOption("appActivity", "com.google.firebase.MessagingUnityPlayerActivity");
            options.AddAdditionalAppiumOption("disableIdlingResource", true);
            options.AddAdditionalAppiumOption("noReset", true); // Keep app state between tests

            Driver = new AndroidDriver(new Uri("http://127.0.0.1:4723"), options, commandTimeout);
        }
    }

    [TearDown]
    public virtual void TearDown()
    {
        // Don't quit driver, keep it for next test
    }

    [OneTimeTearDown]
    public virtual void OneTimeTearDown()
    {
        // Only quit driver after all tests complete
        Driver?.Quit();
    }

    protected void TapOnScreen(int x, int y)
    {
        Driver.ExecuteScript("mobile: clickGesture", new Dictionary<string, object>
        {
            ["x"] = x,
            ["y"] = y
        });
    }
}