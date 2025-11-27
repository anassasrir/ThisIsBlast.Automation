using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using System.Diagnostics;

[SetUpFixture]
public class TestRunSetup
{
    public static AndroidDriver Driver { get; private set; }

    private const string ApkPath = "./ThisIsBlast.Tests/TestApps/157.apk";
    private const string AppiumServer = "http://127.0.0.1:4723";
    private const int CommandTimeoutSeconds = 60;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        // Install apk once via adb (requires adb in PATH)
        try
        {
            var psi = new ProcessStartInfo("adb", $"install -r \"{ApkPath}\"")
            {
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false
            };
            using var p = Process.Start(psi);
            p.WaitForExit();
            Console.WriteLine($"adb exit code: {p.ExitCode}");
        }
        catch (System.Exception ex)
        {
            Console.WriteLine($"adb install failed: {ex.Message}");
            // continue — driver may still start if apk already installed
        }

        var options = new AppiumOptions();
        options.PlatformName = "Android";
        options.AutomationName = AutomationName.AndroidUIAutomator2;
        options.DeviceName = "Android Emulator";
        options.AddAdditionalAppiumOption("appPackage", "com.kiragan.blastjam");
        options.AddAdditionalAppiumOption("appActivity", "com.google.firebase.MessagingUnityPlayerActivity");
        options.AddAdditionalAppiumOption("noReset", true); // preserve state between fixtures
        options.AddAdditionalAppiumOption("disableIdlingResource", true);

        Driver = new AndroidDriver(new Uri(AppiumServer), options, TimeSpan.FromSeconds(CommandTimeoutSeconds));
        System.Threading.Thread.Sleep(3000);
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        Driver?.Quit();
    }
}