namespace Library.Tests;

public class ExchangeTypeTests
{
    [Test]
    public void ExchangeType_Sent()
    {
        ExchangeType type = ExchangeType.Sent;

        Assert.That(type, Is.EqualTo(ExchangeType.Sent));
    }

    [Test]
    public void ExchangeType_Received()
    {
        ExchangeType type = ExchangeType.Received;

        Assert.That(type, Is.EqualTo(ExchangeType.Received));
    }
    
    [Test]
    public void ExchangeType_TwoValues()
    {
        var values = Enum.GetValues(typeof(ExchangeType));
        
        Assert.That(values.Length, Is.EqualTo(2));
    }

    [Test]
    public void ExchangeType_ExactlyTwoValues()
    {
        var values = Enum.GetValues(typeof(ExchangeType));

        Assert.That(values, Contains.Item(ExchangeType.Sent));
        Assert.That(values, Contains.Item(ExchangeType.Received));
    }

    [Test]
    public void ExchangeType_CompareCases()
    {
        ExchangeType type1 = ExchangeType.Sent;
        ExchangeType type2 = ExchangeType.Sent;
        ExchangeType type3 = ExchangeType.Received;

        Assert.That(type1, Is.EqualTo(type2));
        Assert.That(type1, Is.Not.EqualTo(type3));
    }

    [Test]
    public void ExchangeType_ConvertToString()
    {
        
        string sentString = ExchangeType.Sent.ToString();
        string receivedString = ExchangeType.Received.ToString();

        Assert.That(sentString, Is.EqualTo("Sent"));
        Assert.That(receivedString, Is.EqualTo("Received"));
    }
    
    [Test]
    public void ExchangeType_CanBeReassigned()
    {
        ExchangeType type = ExchangeType.Sent;
        type = ExchangeType.Received;

        Assert.That(type, Is.EqualTo(ExchangeType.Received));
    }
}