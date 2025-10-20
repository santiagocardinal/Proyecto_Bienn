namespace Library.Tests;

public class SellerManagerTests
{
    private SellerManager _sellerManager;
    private Seller _testSeller;
    private Customer _testCustomer;

    [SetUp]
    public void Setup()
    {
        _sellerManager = new SellerManager();
        _testSeller = new Seller("Carlos", "carlos@mail.com", "099111222", "S1");
        _testCustomer = new Customer("C1", "Juan", "Pérez", "juan@mail.com", "099123456", "M", DateTime.Now.AddYears(-30));
    }

    [Test]
    public void CreateSeller_ValidSeller_AddsToList()
    {
        // Act
        _sellerManager.CreateSeller(_testSeller);
        
        // Assert
        Assert.That(_sellerManager.Sellers, Does.Contain(_testSeller));
        Assert.That(_sellerManager.Sellers.Count, Is.EqualTo(1));
    }

    [Test]
    public void CreateSeller_MultipleSellers_AddsAllToList()
    {
        // Arrange
        Seller seller2 = new Seller("Ana", "ana@mail.com", "099222333", "S2");
        Seller seller3 = new Seller("Pedro", "pedro@mail.com", "099333444", "S3");
        
        // Act
        _sellerManager.CreateSeller(_testSeller);
        _sellerManager.CreateSeller(seller2);
        _sellerManager.CreateSeller(seller3);
        
        // Assert
        Assert.That(_sellerManager.Sellers.Count, Is.EqualTo(3));
        Assert.That(_sellerManager.Sellers, Does.Contain(_testSeller));
        Assert.That(_sellerManager.Sellers, Does.Contain(seller2));
        Assert.That(_sellerManager.Sellers, Does.Contain(seller3));
    }

    [Test]
    public void DeleteSeller_ExistingSeller_RemovesFromList()
    {
        // Arrange
        _sellerManager.CreateSeller(_testSeller);
        
        // Act
        _sellerManager.DeleteSeller(_testSeller);
        
        // Assert
        Assert.That(_sellerManager.Sellers, Does.Not.Contain(_testSeller));
        Assert.That(_sellerManager.Sellers.Count, Is.EqualTo(0));
    }

    [Test]
    public void DeleteSeller_NonExistingSeller_DoesNotThrowError()
    {
        // Arrange
        Seller nonExisting = new Seller("NoExiste", "noexiste@mail.com", "099999999", "S99");
        
        // Act & Assert
        Assert.DoesNotThrow(() => _sellerManager.DeleteSeller(nonExisting));
    }

    [Test]
    public void SuspendSeller_ValidSeller_SetsSuspendedToTrue()
    {
        // Arrange
        _sellerManager.CreateSeller(_testSeller);
        
        // Act
        _sellerManager.SuspendSeller(_testSeller);
        
        // Assert
        Assert.That(_testSeller.IsSuspended, Is.True);
    }

    [Test]
    public void SuspendSeller_MultipleSellers_OnlySuspendsTargetSeller()
    {
        // Arrange
        Seller seller2 = new Seller("Ana", "ana@mail.com", "099222333", "S2");
        _sellerManager.CreateSeller(_testSeller);
        _sellerManager.CreateSeller(seller2);
        
        // Act
        _sellerManager.SuspendSeller(_testSeller);
        
        // Assert
        Assert.That(_testSeller.IsSuspended, Is.True);
        Assert.That(seller2.IsSuspended, Is.False);
    }

    [Test]
    public void GetPendingResponses_WithUnansweredInteractions_ReturnsPendingList()
    {
        // Arrange
        _sellerManager.CreateSeller(_testSeller);
        
        // Crear interacciones recibidas sin respuesta
        Interaction pending1 = new Interaction(DateTime.Now.AddDays(-2), "Consulta sin responder 1", ExchangeType.Received, _testCustomer);
        Interaction pending2 = new Interaction(DateTime.Now.AddDays(-1), "Consulta sin responder 2", ExchangeType.Received, _testCustomer);
        
        _testSeller.addInteraction(pending1);
        _testSeller.addInteraction(pending2);
        
        // Act
        List<Interaction> pendingResponses = _sellerManager.GetPendingResponses(_testSeller);
        
        // Assert
        Assert.That(pendingResponses, Is.Not.Null);
        Assert.That(pendingResponses.Count, Is.EqualTo(2));
        Assert.That(pendingResponses, Does.Contain(pending1));
        Assert.That(pendingResponses, Does.Contain(pending2));
    }

    [Test]
    public void GetPendingResponses_WithAnsweredInteractions_DoesNotReturnThem()
    {
        // Arrange
        _sellerManager.CreateSeller(_testSeller);
        
        // Crear interacción recibida y marcarla como respondida
        Interaction answeredInteraction = new Interaction(DateTime.Now.AddDays(-3), "Consulta respondida", ExchangeType.Received, _testCustomer);
        answeredInteraction.MarkAsResponded();
        
        _testSeller.addInteraction(answeredInteraction);
        
        // Act
        List<Interaction> pendingResponses = _sellerManager.GetPendingResponses(_testSeller);
        
        // Assert
        Assert.That(pendingResponses, Is.Not.Null);
        Assert.That(pendingResponses.Count, Is.EqualTo(0));
        Assert.That(pendingResponses, Does.Not.Contain(answeredInteraction));
    }

    [Test]
    public void GetPendingResponses_WithSentInteractions_DoesNotReturnThem()
    {
        // Arrange
        _sellerManager.CreateSeller(_testSeller);
        
        // Crear interacción enviada (no recibida)
        Interaction sentInteraction = new Interaction(DateTime.Now.AddDays(-1), "Mensaje enviado", ExchangeType.Sent, _testCustomer);
        
        _testSeller.addInteraction(sentInteraction);
        
        // Act
        List<Interaction> pendingResponses = _sellerManager.GetPendingResponses(_testSeller);
        
        // Assert
        Assert.That(pendingResponses, Is.Not.Null);
        Assert.That(pendingResponses.Count, Is.EqualTo(0));
        Assert.That(pendingResponses, Does.Not.Contain(sentInteraction));
    }

    [Test]
    public void GetPendingResponses_MixedInteractions_ReturnsOnlyUnansweredReceived()
    {
        // Arrange
        _sellerManager.CreateSeller(_testSeller);
        
        // Crear diferentes tipos de interacciones
        Interaction pending = new Interaction(DateTime.Now.AddDays(-2), "Pendiente", ExchangeType.Received, _testCustomer);
        Interaction answered = new Interaction(DateTime.Now.AddDays(-3), "Respondida", ExchangeType.Received, _testCustomer);
        answered.MarkAsResponded();
        Interaction sent = new Interaction(DateTime.Now.AddDays(-1), "Enviada", ExchangeType.Sent, _testCustomer);
        
        _testSeller.addInteraction(pending);
        _testSeller.addInteraction(answered);
        _testSeller.addInteraction(sent);
        
        // Act
        List<Interaction> pendingResponses = _sellerManager.GetPendingResponses(_testSeller);
        
        // Assert
        Assert.That(pendingResponses.Count, Is.EqualTo(1));
        Assert.That(pendingResponses, Does.Contain(pending));
        Assert.That(pendingResponses, Does.Not.Contain(answered));
        Assert.That(pendingResponses, Does.Not.Contain(sent));
    }

    [Test]
    public void GetPendingResponses_NoInteractions_ReturnsEmptyList()
    {
        // Arrange
        _sellerManager.CreateSeller(_testSeller);
        
        // Act
        List<Interaction> pendingResponses = _sellerManager.GetPendingResponses(_testSeller);
        
        // Assert
        Assert.That(pendingResponses, Is.Not.Null);
        Assert.That(pendingResponses.Count, Is.EqualTo(0));
    }
}