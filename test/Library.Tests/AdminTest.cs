using NUnit.Framework;
using Library;

namespace Library.Tests;

[TestFixture]
public class AdminTests
{
    [Test]
    public void Constructor_Creator()
    {
        string name = "Juan Pérez";
        string mail = "juan.perez@example.com";
        string phone = "59899123456";
        string id = "12345678";
        
        var admin = new Admin(name, mail, phone, id);

        Assert.That(admin.Name, Is.EqualTo(name));
        Assert.That(admin.Mail, Is.EqualTo(mail));
        Assert.That(admin.Phone, Is.EqualTo(phone));
        Assert.That(admin.Id, Is.EqualTo(id));
    }
    
    [Test]
    public void Constructor_CanHandleNullName()
    {
        string name = null;
        string mail = "agustooo@gmail.com";
        string phone = "098284444";
        string id = "12345678";
        
        var admin = new Admin(name, mail, phone, id);
        
        Assert.That(admin.Name, Is.EqualTo(name));
    }

    [Test]
    public void Constructor_CanHandleNullMail()
    {
        string name = "Jose Luis";
        string mail = null;
        string phone = "987777333";
        string id = "21345678";
        
        var admin = new Admin(name, mail, phone, id);
        
        Assert.That(admin.Mail, Is.EqualTo(mail));
    }

    [Test]
    public void Constructor_CanNullPhone()
    {
        string name = "Juan Pedro";
        string mail = "josepedro1@gmail.com";
        string phone = null;
        string id = "32145678";
        
        var admin = new Admin(name, mail, phone, id);

        Assert.That(admin.Phone, Is.EqualTo(phone));
    }

    [Test]
    public void Constructor_CanNullId()
    {
        string name = "Maria Pia";
        string mail = "mariapiaa@gmailc.com";
        string phone = "98774463";
        string id = null;
        
        var admin = new Admin(name, mail, phone, id);
        
        Assert.That(admin.Id, Is.EqualTo(id));
    }

    [Test]
    public void Admin_CanBeCompared()
    {
        // Arrange & Act
        var admin1 = new Admin("Admin 1", "admin1@test.com", "111", "1");
        var admin2 = new Admin("Admin 2", "admin2@test.com", "222", "2");

        // Assert
        Assert.That(admin1, Is.Not.EqualTo(admin2));
        Assert.That(admin1, Is.Not.SameAs(admin2));
    }
}