using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using System.Diagnostics;

[TestFixture]
[Order(1)]
[Ignore("Temporarily ignored")]
public class QuickConnectionTest : AppiumTestBase
{
    [Test]
    public void VerifyAppiumConnection()
    {
        Console.WriteLine("✓ App launched successfully!");
        Console.WriteLine($"Window size: {Driver.Manage().Window.Size}");
    }
}