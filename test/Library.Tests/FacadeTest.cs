namespace Library.Tests;

public class FacadeTests
{
   private Customer _testCustomer;
   private Seller _testSeller;

    [SetUp]
    public void Setup()
    {
        _testCustomer = new Customer("C1", "Juan", "Pérez", "juan@mail.com", "099123456", "M", DateTime.Now.AddYears(-30));
        _testSeller = new Seller("Carlos", "carlos@mail.com", "099111222", "S1");
    }

    [TearDown]
    public void TearDown()
    {
        // Limpia todos los datos creados en los tests
        Facade.cm.Customers.Clear();
        Facade.sm.Sellers.Clear();
    }

    // ============= CREACIÓN DE CLIENTE =============
    
    [Test]
    public void CreateCustomer_ReturnsSuccessMessage()
    {
        // Act
        string result = Facade.CreateCustomer("C1", "Juan", "Pérez", "juan@mail.com", "099123456", "M", "2009-05-10");
    
        // Assert
        string expected = "Cliente creado correctamente\n" +
                          "ID:           C1\n" +
                          "Nombre:       Juan Pérez\n" +
                          "Email:        juan@mail.com\n" +
                          "Teléfono:     099123456\n" +
                          "Género:       M\n" +
                          "Fecha Nac.:   10/05/2009";
    
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void CreateCustomer_DuplicateId_ReturnsErrorMessage()
    {
        // Arrange
        Facade.CreateCustomer("C1", "Juan", "Pérez", "juan@mail.com", "099123456", "M", "2000-05-10");
        
        // Act
        string result = Facade.CreateCustomer("C1", "Pedro", "García", "pedro@mail.com", "099999999", "M", "2007-01-29");
        
        // Assert
        Assert.That(result, Does.Contain("Ya existe un cliente"));
    }

    // ============= MODIFICACIÓN DE CLIENTE =============
    
    [Test]
    public void ModifyCustomer_ReturnsSuccessMessage()
    {
        // Arrange
        Facade.CreateCustomer("C1", "Juan", "Pérez", "juan@mail.com", "099123456", "M", "2000-05-10");
        
        // Act
        string result = Facade.ModifyCustomer("C1", "name", "Juan Carlos");
        
        // Assert
        Assert.That(result, Is.EqualTo("Cliente modificado correctamente."));
    }
    
    [Test]
    public void NonExistingCustomer_ReturnsErrorMessage()
    {
        // Act
        string result = Facade.ModifyCustomer("C999", "name", "Test");
        
        // Assert
        Assert.That(result, Does.Contain("No existe"));
    }

    // ============= BÚSQUEDAS =============
    
    [Test]
    public void SearchCustomer_ByName_ReturnsName()
    {
        // Arrange
        Facade.CreateCustomer("C1", "Juan", "Pérez", "juan@mail.com", "099123456", "M", "2000-05-10");
    
        // Act
        string result = Facade.SearchCostumer_ByName("Juan");
    
        // Assert
        string expected = "Clinete buscado por su NOMBRE:\n" +
                          "Clientes con nombre 'Juan':\n" +
                          "- Juan Pérez (ID: C1)\n";
    
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void SearchCustomer_ByName_ReturnsErrorMessage()
    {
        // Act
        string result = Facade.SearchCostumer_ByName("No existe");
        
        // Assert
        Assert.That(result, Does.Contain("No existe"));
    }

    [Test]
    public void SearchCustomer_ByFamilyName_ReturnsFamilyName()
    {
        // Arrange
        Facade.CreateCustomer("C1", "Juan", "Pérez", "juan@mail.com", "099123456", "M", "2007-01-29");
    
        // Act
        string result = Facade.SearchCostumer_ByFamilyName("Pérez");
    
        // Assert
        string expected = "Cliente buscado por su APELLIDO:\n" +
                          "Clientes con apellido 'Pérez':\n" +
                          "- Juan Pérez (ID: C1)\n";
    
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void SearchCustomer_ByPhone_ReturnsPhone()
    {
        // Arrange
        Facade.CreateCustomer("C1", "Juan", "Pérez", "juan@mail.com", "099123456", "M", "2007-05-10");
    
        // Act
        string result = Facade.SearchCostumer_ByPhone("099123456");
    
        // Assert
        string expected = "Cliente encontrado por su NUMERO DE TELEFONO:\n" +
                          "ID:       C1\n" +
                          "Nombre:   Juan Pérez\n" +
                          "Mail:     juan@mail.com\n" +
                          "Teléfono: 099123456";
    
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void SearchCustomer_ByMail_ReturnsMail()
    {
        // Arrange
        Facade.CreateCustomer("C1", "Juan", "Pérez", "juan@mail.com", "099123456", "M", "2000-05-10");
    
        // Act
        string result = Facade.SearchCostumer_ByMail("juan@mail.com");
    
        // Assert
        string expected = "Cliente encontrado por su MAIL:\n" +
                          "ID:       C1\n" +
                          "Nombre:   Juan Pérez\n" +
                          "Mail:     juan@mail.com\n" +
                          "Teléfono: 099123456";
    
        Assert.That(result, Is.EqualTo(expected));
    }

    // ============= MOSTRAR CLIENTES POR VENDEDOR =============
    
    [Test]
    public void ShowCustomers_BySellerId_ReturnsCustomerList()
    {
        // Arrange
        Facade.cm.AddCustomer(_testCustomer);
        Facade.sm.CreateSeller(_testSeller);
        Facade.AssignCustomer("C1", "S1");
        
        // Act
        string result = Facade.ShowCustomers_BySellerId("S1");
        
        // Assert
        Assert.That(result, Does.Contain("Juan"));
    }

    [Test]
    public void ShowCustomers_BySellerId_ReturnsMessage()
    {
        // Arrange
        Facade.sm.CreateSeller(_testSeller);
        
        // Act
        string result = Facade.ShowCustomers_BySellerId("S1");
        
        // Assert
        Assert.That(result, Is.EqualTo("El vendedor no tiene clientes asignados."));
    }

    [Test]
    public void ShowCustomers_BySellerId_ReturnsErrorMessage()
    {
        // Act
        string result = Facade.ShowCustomers_BySellerId("S999");
        
        // Assert
        Assert.That(result, Does.Contain("No se encontró el vendedor"));
    }

    // ============= AGREGAR CUSTOMER =============
    
    [Test]
    public void AddCustomer_ValidCustomer_ReturnsSuccessMessage()
    {
        // Act
        string result = Facade.AddCustomer(_testCustomer);
        
        // Assert
        Assert.That(result, Is.EqualTo("Cliente agregado correctamente."));
    }

    [Test]
    public void AddCustomer_DuplicateCustomer_ReturnsErrorMessage()
    {
        // Arrange
        Facade.AddCustomer(_testCustomer);
        
        // Act
        string result = Facade.AddCustomer(_testCustomer);
        
        // Assert
        Assert.That(result, Does.Contain("Ya existe un cliente"));
    }

    // ============= AGREGAR TAG A CLIENTE =============
    
    [Test]
    public void AddTagCustomer_ReturnsSuccessMessage()
    {
        // Arrange
        Facade.cm.AddCustomer(_testCustomer);
        
        // Act
        string result = Facade.AddTag_Customer("C1", "T1");
        
        // Assert
        Assert.That(result, Does.Contain("La Tag:\n  'T1'\n No existe en el sistema. Créala primero con CreateTag."));
    }

    [Test]
    public void AddTagCustomer_ReturnsErrorMessage1()
    {
        // Arrange
        Facade.cm.AddCustomer(_testCustomer);
        Facade.AddTag_Customer("C1", "T1");
        
        // Act
        string result = Facade.AddTag_Customer("C1", "T2");
        
        // Assert
        Assert.That(result, Does.Contain("La Tag:\n  'T2'\n No existe en el sistema. Créala primero con CreateTag."));
    }

    [Test]
    public void AddTagCustomer_ReturnsErrorMessage2()
    {
        // Act
        string result = Facade.AddTag_Customer("C999", "T1");
        
        // Assert
        Assert.That(result, Does.Contain("No existe"));
    }

    // ============= ÚLTIMA INTERACCIÓN =============
    
    [Test]
    public void LastInteraction_ReturnsLastInteraction()
    {
        Facade.cm.AddCustomer(_testCustomer);
        Facade.sm.CreateSeller(_testSeller);

        Facade.CallRegister(
            DateTime.Now.ToString("yyyy-MM-dd"),
            "Consulta",
            ExchangeType.Received.ToString(),
            "C1",
            "S1"
        );

        string result = Facade.LastInteraction("C1");

        Assert.That(result, Is.Not.Empty);
    }

    [Test]
    public void LastInteraction_ReturnsMessage()
    {
        // Arrange
        Facade.cm.AddCustomer(_testCustomer);
        
        // Act
        string result = Facade.LastInteraction("C1");
        
        // Assert
        Assert.That(result, Is.EqualTo("No hay interacciones registradas."));
    }

    [Test]
    public void LastInteraction_ReturnsErrorMessage()
    {
        // Act
        string result = Facade.LastInteraction("C999");
        
        // Assert
        Assert.That(result, Does.Contain("No existe"));
    }

    // ============= INTERACCIONES SIN RESPUESTA =============
    
    [Test]
    public void UnansweredInteractions_ReturnsReport()
    {
        Facade.cm.AddCustomer(_testCustomer);
        Facade.sm.CreateSeller(_testSeller);

        Facade.CallRegister(
            DateTime.Now.ToString("yyyy-MM-dd"),
            "Consulta",
            ExchangeType.Received.ToString(),
            "C1",
            "S1"
        );

        string result = Facade.UnansweredInteractions("C1");

        Assert.That(result, Does.Contain("sin responder"));
    }

    [Test]
    public void UnansweredInteractions_ReturnsMessage()
    {
        // Arrange
        Facade.cm.AddCustomer(_testCustomer);
        
        // Act
        string result = Facade.UnansweredInteractions("C1");
        
        // Assert
        Assert.That(result, Is.EqualTo("No hay interacciones sin responder."));
    }

    [Test]
    public void UnansweredInteractions_ReturnsErrorMessage()
    {
        // Act
        string result = Facade.UnansweredInteractions("C999");
        
        // Assert
        Assert.That(result, Does.Contain("No existe"));
    }

    // ============= ASIGNAR CLIENTE A VENDEDOR =============
    
    
    [Test]
    public void AssignCustomer_ReturnsSuccessMessage()
    {
        // Arrange
        Facade.cm.AddCustomer(new Customer("C1", "Juan", "Perez", "mail@mail.com", "099", "M", DateTime.Now));
        Facade.sm.CreateSeller(new Seller("Carlos", "carlos@mail.com", "099111222", "S1"));
    
        // Act
        string result = Facade.AssignCustomer("C1", "S1");
    
        // Assert
        string expected = "Cliente asignado correctamente\n\n" +
                          "Cliente:\n" +
                          "  • ID: C1\n" +
                          "  • Nombre: Juan Perez\n" +
                          "  • Email: mail@mail.com\n\n" +
                          "Asignado a vendedor:\n" +
                          "  • ID: S1\n" +
                          "  • Nombre: Carlos\n\n" +
                          "Asignación realizada el " + DateTime.Now.ToString("dd/MM/yyyy HH:mm");
    
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void AssignCustomer_ReturnsErrorMessage()
    {
        // Arrange
        Facade.sm.CreateSeller(_testSeller);
        
        // Act
        string result = Facade.AssignCustomer("C999", "S1");
        
        // Assert
        Assert.That(result, Does.Contain("No existe"));
    }

    [Test]
    public void AssignCustomer_SellerNotFound_ReturnsErrorMessage()
    {
        // Arrange
        Facade.cm.AddCustomer(_testCustomer);
        
        // Act
        string result = Facade.AssignCustomer("C1", "S999");
        
        // Assert
        Assert.That(result, Does.Contain("No se encontró el vendedor"));
    }

    // ============= REGISTRO DE INTERACCIONES =============
    
    [Test]
    public void CallRegister_Success()
    {
        Facade.cm.AddCustomer(_testCustomer);
        Facade.sm.CreateSeller(_testSeller);
        
        Facade.AssignCustomer(_testCustomer.Id, _testSeller.Id);
        string result = Facade.CallRegister(
            DateTime.Now.ToString("yyyy-MM-dd"),
            "Consulta",
            ExchangeType.Received.ToString(),
            "C1",
            "S1"
        );

        Assert.That(result, Does.Contain("Interacción registrada correctamente"));
    }

    [Test]
    public void CallRegister_CustomerNotFound()
    {
        Facade.sm.CreateSeller(_testSeller);

        string result = Facade.CallRegister(
            DateTime.Now.ToString("yyyy-MM-dd"),
            "Consulta",
            ExchangeType.Received.ToString(),
            "C999",
            "S1"
        );

        Assert.That(result, Does.Contain("No existe"));
    }

    [Test]
    public void MeetingRegister_Success()
    {
        Facade.cm.AddCustomer(_testCustomer);
        Facade.sm.CreateSeller(_testSeller);
        Facade.AssignCustomer(_testCustomer.Id, _testSeller.Id);

        string result = Facade.MeetingRegister(
            "Oficina",
            DateTime.Now.AddDays(5).ToString("yyyy-MM-dd"),
            "Reunión",
            ExchangeType.Sent.ToString(),
            "C1",
            "S1"
        );

        Assert.That(result, Does.Contain("Interacción registrada correctamente"));
    }

    [Test]
    public void MessageRegister_Success()
    {
        Facade.cm.AddCustomer(_testCustomer);
        Facade.sm.CreateSeller(_testSeller);
        Facade.AssignCustomer(_testCustomer.Id, _testSeller.Id);

        string result = Facade.MessageRegister(
            DateTime.Now.ToString("yyyy-MM-dd"),
            "Mensaje",
            ExchangeType.Sent.ToString(),
            "C1",
            "S1"
        );

        Assert.That(result, Does.Contain("Interacción registrada correctamente"));
    }

    [Test]
    public void MailRegister_Success()
    {
        Facade.cm.AddCustomer(_testCustomer);
        Facade.sm.CreateSeller(_testSeller);
        Facade.AssignCustomer(_testCustomer.Id, _testSeller.Id);

        string result = Facade.MailRegister(
            DateTime.Now.ToString("yyyy-MM-dd"),
            "Email",
            ExchangeType.Received.ToString(),
            "C1",
            "S1"
        );

        Assert.That(result, Does.Contain("Interacción registrada correctamente"));
    }


    [Test]
    public void QuoteRegister_ReturnsSuccessMessage()
    {
        // Arrange
        Facade.cm.AddCustomer(_testCustomer);
        Facade.sm.CreateSeller(_testSeller);

        string date = DateTime.Now.ToString("yyyy-MM-dd");

        // Act
        string result = Facade.QuoteRegister(
            date,
            "Cotización",
            "Sent",
            "1000.0",
            "Producto X",
            "C1",
            "S1"
        );

        // Assert
        Assert.That(result, Does.Contain("El cliente 'C1' no está asignado al vendedor 'S1'."));
    }

    // ============= SALE FROM QUOTE =============
    
    [Test]
    public void SaleFromQuote_ReturnsSuccessMessage()
    {
        // Arrange
        Facade.cm.AddCustomer(_testCustomer);
        Facade.sm.CreateSeller(_testSeller);
        Facade.AssignCustomer(_testCustomer.Id, _testSeller.Id);

        string date = DateTime.Now.ToString("yyyy-MM-dd");

        Facade.QuoteRegister(
            "2025-10-12",
            "Cotización",
            "Sent",
            "1000.0",
            "Producto X",
            "C1",
            "S1"
        );

        // Act
        string result = Facade.SaleFromQuote(
            "2025-10-12",
            "Cotización",
            "Sent",
            "1000.0",
            "Producto X",
            "C1",
            "S1"
        );

        // Assert
        Assert.That(result, Does.Contain("Interacción registrada correctamente."));
    }


    [Test]
    public void SaleFromQuote_ReturnsErrorMessage()
    {
        // Arrange
        Facade.cm.AddCustomer(_testCustomer);
        Facade.sm.CreateSeller(_testSeller);

        string date = DateTime.Now.ToString("yyyy-MM-dd");

        // Act
        string result = Facade.SaleFromQuote(
            date,
            "Cotización",
            "Sent",
            "1000.0",
            "Producto X",
            "C1",
            "S1"
        );

        // Assert
        Assert.That(result, Does.Contain("Quote"));
    }
    

    [Test]
    public void SaleFromQuote_SellerNotFound_ReturnsErrorMessage()
    {
        // Arrange
        Facade.cm.AddCustomer(_testCustomer);

        string date = DateTime.Now.ToString("yyyy-MM-dd");

        // Act
        string result = Facade.SaleFromQuote(
            date,
            "Cotización",
            "Sent",
            "1000.0",
            "Producto X",
            "C1",
            "S999"
        );

        // Assert
        Assert.That(result, Does.Contain("vendedor"));
    }

    // ============= GET CUSTOMER INTERACTIONS =============
    
    [Test]
    public void GetCustomerInteractions_ReturnsInteractionsList()
    {
        // Arrange
        Facade.cm.AddCustomer(_testCustomer);
        Facade.sm.CreateSeller(_testSeller);
        Facade.AssignCustomer(_testCustomer.Id, _testSeller.Id);
        Facade.CallRegister(
            DateTime.Now.ToString("yyyy-MM-dd"),
            "Consulta",
            "Received",
            "C1",
            "S1"
        );

        // Act
        var customer = Facade.cm.SearchById("C1");
        List<Interaction> result = customer.Interactions;

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.GreaterThan(0));
    }

    [Test]
    public void GetCustomerInteractions_ReturnsEmptyList()
    {
        // Arrange
        Facade.cm.AddCustomer(_testCustomer);

        // Act
        var customer = Facade.cm.SearchById("C1");
        List<Interaction> result = customer.Interactions;

        // Assert
        Assert.That(result.Count, Is.EqualTo(0));
    }

    [Test]
    public void GetCustomerInteractions_CustomerNotFound_ReturnsEmptyList()
    {
        // Act
        List<Interaction> result = new List<Interaction>();

        // Assert
        Assert.That(result.Count, Is.EqualTo(0));
    }

    [Test]
    public void GetCustomerInteractions_ReturnsCero()
    {
        // Arrange
        Facade.cm.AddCustomer(_testCustomer);
        Facade.sm.CreateSeller(_testSeller);

        DateTime today = DateTime.Now;

        Facade.CallRegister(
            today.ToString("yyyy-MM-dd"),
            "Consulta hoy",
            "Received",
            "C1",
            "S1"
        );

        Facade.CallRegister(
            today.AddDays(-5).ToString("yyyy-MM-dd"),
            "Consulta hace 5 días",
            "Received",
            "C1",
            "S1"
        );

        var customer = Facade.cm.SearchById("C1");

        // Act
        var result = customer.Interactions
            .Where(i => i.Date.Date == today.Date)
            .ToList();

        // Assert
        Assert.That(result.Count, Is.EqualTo(0));
    }
}