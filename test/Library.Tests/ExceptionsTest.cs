namespace Library.Tests;

public class ExceptionsTests
{
    // ========== CLIENTES ==========
    
    [Test]
    public void NotExistingCustomerException_Constructor()
    {
        // Act
        var exception = new Exceptions.NotExistingCustomerException();
        
        // Assert
        Assert.That(exception, Is.InstanceOf<Exception>());
        Assert.That(exception.Message, Is.EqualTo("No existe un cliente con los datos proporcionados."));
    }

    [Test]
    public void NotExistingCustomerException_CanBeThrown()
    {
        // Act & Assert
        Assert.Throws<Exceptions.NotExistingCustomerException>(() => 
            throw new Exceptions.NotExistingCustomerException());
    }

    [Test]
    public void DuplicatedCustomerException_WithCustomer_CreatesWithCustomerId()
    {
        // Arrange
        var customer = new Customer("C123", "Juan", "Pérez", "juan@mail.com", "099123456", "M", DateTime.Now.AddYears(-30));
        
        // Act
        var exception = new Exceptions.DuplicatedCustomerException(customer);
        
        // Assert
        Assert.That(exception, Is.InstanceOf<Exception>());
        Assert.That(exception.Message, Does.Contain("C123"));
        Assert.That(exception.Message, Is.EqualTo("Ya existe un cliente con ID 'C123'."));
    }

    [Test]
    public void DuplicatedCustomerException_CanBeThrown()
    {
        // Arrange
        var customer = new Customer("C123", "Juan", "Pérez", "juan@mail.com", "099123456", "M", DateTime.Now.AddYears(-30));
        
        // Act & Assert
        Assert.Throws<Exceptions.DuplicatedCustomerException>(() => 
            throw new Exceptions.DuplicatedCustomerException(customer));
    }

    [Test]
    public void InvalidFieldException_WithFieldName_CreatesWithFieldInMessage()
    {
        // Arrange
        string fieldName = "invalidField";
        
        // Act
        var exception = new Exceptions.InvalidFieldException(fieldName);
        
        // Assert
        Assert.That(exception, Is.InstanceOf<Exception>());
        Assert.That(exception.Message, Does.Contain(fieldName));
        Assert.That(exception.Message, Is.EqualTo("El campo 'invalidField' no es válido para ser modificado."));
    }

    [Test]
    public void InvalidFieldException_CanBeThrown()
    {
        // Act & Assert
        Assert.Throws<Exceptions.InvalidFieldException>(() => 
            throw new Exceptions.InvalidFieldException("testField"));
    }

    // ========== VENDEDORES ==========
    
    [Test]
    public void SellerNotFoundException_WithId_CreatesWithIdInMessage()
    {
        // Arrange
        string sellerId = "S123";
        
        // Act
        var exception = new Exceptions.SellerNotFoundException(sellerId);
        
        // Assert
        Assert.That(exception, Is.InstanceOf<Exception>());
        Assert.That(exception.Message, Does.Contain(sellerId));
        Assert.That(exception.Message, Is.EqualTo("No se encontró el vendedor con ID S123."));
    }

    [Test]
    public void SellerNotFoundException_CanBeThrown()
    {
        // Act & Assert
        Assert.Throws<Exceptions.SellerNotFoundException>(() => 
            throw new Exceptions.SellerNotFoundException("S999"));
    }

    [Test]
    public void DuplicateSellerException_WithId_CreatesWithIdInMessage()
    {
        // Arrange
        string sellerId = "S456";
        
        // Act
        var exception = new Exceptions.DuplicateSellerException(sellerId);
        
        // Assert
        Assert.That(exception, Is.InstanceOf<Exception>());
        Assert.That(exception.Message, Does.Contain(sellerId));
        Assert.That(exception.Message, Is.EqualTo("Ya existe un vendedor con ID 'S456'."));
    }

    [Test]
    public void DuplicateSellerException_CanBeThrown()
    {
        // Act & Assert
        Assert.Throws<Exceptions.DuplicateSellerException>(() => 
            throw new Exceptions.DuplicateSellerException("S789"));
    }

    // ========== TAGS ==========
    
    [Test]
    public void DuplicateTagException_WithTagName_CreatesWithTagInMessage()
    {
        // Arrange
        string tagName = "VIP";
        
        // Act
        var exception = new Exceptions.DuplicatedTagException(tagName);
        
        // Assert
        Assert.That(exception, Is.InstanceOf<Exception>());
        Assert.That(exception.Message, Does.Contain(tagName));
        Assert.That(exception.Message, Is.EqualTo("Ya existe una Tag con ID 'VIP'."));
    }

    [Test]
    public void DuplicateTagException_CanBeThrown()
    {
        // Act & Assert
        Assert.Throws<Exceptions.DuplicatedTagException>(() => 
            throw new Exceptions.DuplicatedTagException("Premium"));
    }

    // ========== INTERACCIONES ==========
    
    [Test]
    public void InteractionNotFoundException_Constructor_CreatesWithCorrectMessage()
    {
        // Act
        var exception = new Exceptions.InteractionNotFoundException();
        
        // Assert
        Assert.That(exception, Is.InstanceOf<Exception>());
        Assert.That(exception.Message, Is.EqualTo("No se encontró la interacción solicitada."));
    }

    [Test]
    public void InteractionNotFoundException_CanBeThrown()
    {
        // Act & Assert
        Assert.Throws<Exceptions.InteractionNotFoundException>(() => 
            throw new Exceptions.InteractionNotFoundException());
    }

    [Test]
    public void QuoteNotFoundException_Constructor_CreatesWithCorrectMessage()
    {
        // Act
        var exception = new Exceptions.QuoteNotFoundException();
        
        // Assert
        Assert.That(exception, Is.InstanceOf<Exception>());
        Assert.That(exception.Message, Is.EqualTo("No se encontró una Quote que coincida con los datos proporcionados."));
    }

    [Test]
    public void QuoteNotFoundException_CanBeThrown()
    {
        // Act & Assert
        Assert.Throws<Exceptions.QuoteNotFoundException>(() => 
            throw new Exceptions.QuoteNotFoundException());
    }

    // ========== FECHAS ==========
    
    [Test]
    public void InvalidDateRangeException_Constructor_CreatesWithCorrectMessage()
    {
        // Act
        var exception = new Exceptions.InvalidDateRangeException();
        
        // Assert
        Assert.That(exception, Is.InstanceOf<Exception>());
        Assert.That(exception.Message, Is.EqualTo("La fecha inicial debe ser anterior a la fecha final."));
    }

    [Test]
    public void InvalidDateRangeException_CanBeThrown()
    {
        // Act & Assert
        Assert.Throws<Exceptions.InvalidDateRangeException>(() => 
            throw new Exceptions.InvalidDateRangeException());
    }
    
}