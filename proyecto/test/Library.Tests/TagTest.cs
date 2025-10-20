using Library;

namespace Library.Tests;
/// <summary>
/// ublic Tag(string id, string name, string description)
//{
 //   this.Id = id;
  //  this.Name = name;
  //  this.Description = description;
//}

//public override string ToString()
//{
//    return $"{this.Id}, {this.Name}, {this.Description}," ;
//}
/// </summary>
public class TagTest
{
    [Test]
    public void Tag_ValidParameter()
    {
        string id = "12345678";
        string name = "Lucrecio";
        string descripcion = "Laptops y ventiladores";

        var tag = new Tag(id,name,descripcion);
        
        Assert.That(tag.Id,Is.EqualTo(id));
        Assert.That(tag.Name,Is.EqualTo(name));
        Assert.That(tag.Description,Is.EqualTo(descripcion));
    }

    public void Tag_NullId()
    {
        string id = null;
        string name = "Lucrecio";
        string descripcion = "Laptops y ventiladores";

        var tag = new Tag(id,name,descripcion);
        
        Assert.That(tag.Id,Is.EqualTo(null));
        Assert.That(tag.Name,Is.EqualTo(name));
        Assert.That(tag.Description,Is.EqualTo(descripcion));
    }
    public void Tag_NullDescription()
    {
        string id = "12345678";
        string name = "Lucrecio";
        string descripcion = null;

        var tag = new Tag(id,name,descripcion);
        
        Assert.That(tag.Id,Is.EqualTo(id));
        Assert.That(tag.Name,Is.EqualTo(name));
        Assert.That(tag.Description,Is.EqualTo(null));
    }
    public void Tag_NullName()
    {
        string id = "12345678";
        string name = null;
        string descripcion = "Laptops y ventiladores";

        var tag = new Tag(id,name,descripcion);
        
        Assert.That(tag.Id,Is.EqualTo(id));
        Assert.That(tag.Name,Is.EqualTo(null));
        Assert.That(tag.Description,Is.EqualTo(descripcion));
    }
    public void Tag_ParameterNotNull()
    {
        string id = "12345678";
        string name = "Lucrecio";
        string descripcion = "Laptops y ventiladores";

        var tag = new Tag(id,name,descripcion);
        
        Assert.That(tag.Id,Is.Not.EqualTo(null));
        Assert.That(tag.Name,Is.Not.EqualTo(null));
        Assert.That(tag.Description,Is.Not.EqualTo(null));
    }

    public void Tag_ToStringMethod()
    {
        string id = "12345678";
        string name = "Lucrecio";
        string descripcion = "Laptops y ventiladores";

        var tag = new Tag(id, name, descripcion);
        string result = tag.ToString();
        
        Assert.That(result,Does.Contain("Laptops y ventiladores"));
    }
}