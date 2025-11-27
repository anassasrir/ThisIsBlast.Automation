using NUnit.Framework;

[TestFixture]
[Order(3)]
[Ignore("Temporarily ignored")]

public class LevelOneTest : AppiumTestBase
{
    
    [Test]
    public void VerifyShootingInLevel()
    {
        
        TapOnScreen(350, 1800);
        TapOnScreen(750, 1800);
    }
}