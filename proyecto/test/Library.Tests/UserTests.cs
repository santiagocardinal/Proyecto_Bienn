namespace Library.Tests;

public class UserTests
{
    private User user;

    [SetUp]
    public void Setup()
    {
        user = new User("Juan Pérez", "juan@email.com", "099123456", "12345678");
    }
    
    [Test]
    public void ValidData()
    {
        // Arrange
        string expectedName = "Juan Pérez";
        string expectedMail = "juan@email.com";
        string expectedPhone = "099123456";
        string expectedId = "12345678";

        // Act
        User newUser = new User(expectedName, expectedMail, expectedPhone, expectedId);

        // Assert
        Assert.That(newUser.Name, Is.EqualTo(expectedName));
        Assert.That(newUser.Mail, Is.EqualTo(expectedMail));
        Assert.That(newUser.Phone, Is.EqualTo(expectedPhone));
        Assert.That(newUser.Id, Is.EqualTo(expectedId));
    }

    [Test]
    public void WithNullValues()
    {
        // Act
        User newUser = new User(null, null, null, null);

        // Assert
        Assert.That(newUser.Name, Is.Null);
        Assert.That(newUser.Mail, Is.Null);
        Assert.That(newUser.Phone, Is.Null);
        Assert.That(newUser.Id, Is.Null);
    }

    [Test]
    public void Name_SetValidValue_UpdatesSuccessfully()
    {
        // Arrange
        string newName = "Pedro García";

        // Act
        user.Name = newName;

        // Assert
        Assert.That(user.Name, Is.EqualTo(newName));
    }

    [Test]
    public void ValidMail()
    {
        // Arrange
        string newMail = "pedro@email.com";

        // Act
        user.Mail = newMail;

        // Assert
        Assert.That(user.Mail, Is.EqualTo(newMail));
    }

    [Test]
    public void ValidPhone()
    {
        // Arrange
        string newPhone = "098765432";

        // Act
        user.Phone = newPhone;

        // Assert
        Assert.That(user.Phone, Is.EqualTo(newPhone));
    }

    [Test]
    public void ValidId()
    {
        // Arrange
        string newId = "87654321";

        // Act
        user.Id = newId;

        // Assert
        Assert.That(user.Id, Is.EqualTo(newId));
    }
    
    [Test]
    public void MultipleUsers()
    {
        // Arrange
        User user1 = new User("Juan", "juan@test.com", "099111111", "11111111");
        User user2 = new User("Pedro", "pedro@test.com", "099222222", "22222222");

        // Act
        user1.Name = "Carlos";

        // Assert
        Assert.That(user1.Name, Is.EqualTo("Carlos"));
        Assert.That(user2.Name, Is.EqualTo("Pedro"));
    }
}