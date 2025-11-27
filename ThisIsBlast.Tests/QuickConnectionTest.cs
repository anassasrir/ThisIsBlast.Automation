using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;

[TestFixture]
public class QuickConnectionTest
{
    [Test]
    public void VerifyAppiumConnection()
    {
        var commandTimeout = TimeSpan.FromSeconds(60);
        var options = new AppiumOptions
        {
            PlatformName = "Android",
            AutomationName = AutomationName.AndroidUIAutomator2,
            DeviceName = "Android Emulator"
        };

        options.App = Environment.GetEnvironmentVariable(@"C:\Users\asrir\Downloads\161.apk");
        options.AddAdditionalAppiumOption("appPackage", "com.kiragan.blastjam");
        options.AddAdditionalAppiumOption("appActivity", "com.google.firebase.MessagingUnityPlayerActivity");
        options.AddAdditionalAppiumOption("installOptions", new string[] { "--no-verify" });
        options.AddAdditionalAppiumOption("disableIdlingResource", true);
        options.AddAdditionalAppiumOption("noReset", true);

        // Use Appium server root URL (Appium v2 removes the default /wd/hub prefix).
        using var driver = new AndroidDriver(new Uri("http://127.0.0.1:4723"), options, commandTimeout);


        System.Threading.Thread.Sleep(10000);

        Console.WriteLine("✓ App launched successfully!");
        Console.WriteLine($"Window size: {driver.Manage().Window.Size}");

        // Perform a tap at grid position x=500, y=1500 using the driver-supported mobile execute method.
        driver.ExecuteScript("mobile: clickGesture", new Dictionary<string, object>
        {
            ["x"] = 500,
            ["y"] = 1500
        });
        System.Threading.Thread.Sleep(10000);

        driver.ExecuteScript("mobile: clickGesture", new Dictionary<string, object>
        {
            ["x"] = 350,
            ["y"] = 1800
        }); 
        driver.ExecuteScript("mobile: clickGesture", new Dictionary<string, object>
        {
            ["x"] = 750,
            ["y"] = 1800
        });
        
        System.Threading.Thread.Sleep(50000);

        //driver.Quit();
    }
}