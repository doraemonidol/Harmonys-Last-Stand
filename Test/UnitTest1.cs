using Logic.MainCharacters;

namespace Test;

public class Tests
{
    [SetUp]
    public void Setup()
    {
        var newCharacter = new Aurelia();
    }

    [Test]
    public void Test1()
    {
        Setup();
        Assert.Pass();
    }
}