namespace Library.Tests;

public class ExchangeTypeTests
{
    // ============================================
    // PRUEBAS DE VALORES DEL ENUM
    // ============================================

    [Test]
    public void ExchangeType_HasSentValue()
    {
        // Act
        ExchangeType type = ExchangeType.Sent;

        // Assert
        Assert.That(type, Is.EqualTo(ExchangeType.Sent));
    }

    [Test]
    public void ExchangeType_HasReceivedValue()
    {
        // Act
        ExchangeType type = ExchangeType.Received;

        // Assert
        Assert.That(type, Is.EqualTo(ExchangeType.Received));
    }

    // ============================================
    // PRUEBAS DE CANTIDAD DE VALORES
    // ============================================

    [Test]
    public void ExchangeType_HasExactlyTwoValues()
    {
        // Act
        var values = Enum.GetValues(typeof(ExchangeType));

        // Assert
        Assert.That(values.Length, Is.EqualTo(2));
    }

    [Test]
    public void ExchangeType_ContainsAllExpectedValues()
    {
        // Act
        var values = Enum.GetValues(typeof(ExchangeType));

        // Assert
        Assert.That(values, Contains.Item(ExchangeType.Sent));
        Assert.That(values, Contains.Item(ExchangeType.Received));
    }

    // ============================================
    // PRUEBAS DE COMPARACIÓN
    // ============================================

    [Test]
    public void ExchangeType_SentAndReceivedAreDifferent()
    {
        // Act & Assert
        Assert.That(ExchangeType.Sent, Is.Not.EqualTo(ExchangeType.Received));
    }

    [Test]
    public void ExchangeType_CanBeCompared()
    {
        // Arrange
        ExchangeType type1 = ExchangeType.Sent;
        ExchangeType type2 = ExchangeType.Sent;
        ExchangeType type3 = ExchangeType.Received;

        // Assert
        Assert.That(type1, Is.EqualTo(type2));
        Assert.That(type1, Is.Not.EqualTo(type3));
    }

    // ============================================
    // PRUEBAS DE CONVERSIÓN
    // ============================================

    [Test]
    public void ExchangeType_CanConvertToString()
    {
        // Act
        string sentString = ExchangeType.Sent.ToString();
        string receivedString = ExchangeType.Received.ToString();

        // Assert
        Assert.That(sentString, Is.EqualTo("Sent"));
        Assert.That(receivedString, Is.EqualTo("Received"));
    }

    [Test]
    public void ExchangeType_CanParseFromString()
    {
        // Act
        ExchangeType sentParsed = (ExchangeType)Enum.Parse(typeof(ExchangeType), "Sent");
        ExchangeType receivedParsed = (ExchangeType)Enum.Parse(typeof(ExchangeType), "Received");

        // Assert
        Assert.That(sentParsed, Is.EqualTo(ExchangeType.Sent));
        Assert.That(receivedParsed, Is.EqualTo(ExchangeType.Received));
    }

    [Test]
    public void ExchangeType_HasNumericValues()
    {
        // Act
        int sentValue = (int)ExchangeType.Sent;
        int receivedValue = (int)ExchangeType.Received;

        // Assert
        Assert.That(sentValue, Is.EqualTo(0));
        Assert.That(receivedValue, Is.EqualTo(1));
    }

    // ============================================
    // PRUEBAS DE USO EN VARIABLES
    // ============================================

  
    [Test]
    public void ExchangeType_CanBeReassigned()
    {
        // Act
        ExchangeType type = ExchangeType.Sent;
        type = ExchangeType.Received;

        // Assert
        Assert.That(type, Is.EqualTo(ExchangeType.Received));
    }

    // ============================================
    // PRUEBAS DE SWITCH
    // ============================================

    [Test]
    public void ExchangeType_WorksInSwitchStatement()
    {
        // Arrange
        ExchangeType type = ExchangeType.Sent;
        string result = "";

        // Act
        switch (type)
        {
            case ExchangeType.Sent:
                result = "Enviado";
                break;
            case ExchangeType.Received:
                result = "Recibido";
                break;
        }

        // Assert
        Assert.That(result, Is.EqualTo("Enviado"));
    }
}