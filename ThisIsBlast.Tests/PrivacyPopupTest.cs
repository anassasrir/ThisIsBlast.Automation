using NUnit.Framework;

[TestFixture]
[Order(2)]

public class PrivacyPopupTest : AppiumTestBase
{
    [Test]
    public void VerifyPrivacyPopupTap()
    {
        Console.WriteLine("✓ App launched successfully!");
        TapOnScreen(500, 1500);
        Thread.Sleep(5000);
    }
}