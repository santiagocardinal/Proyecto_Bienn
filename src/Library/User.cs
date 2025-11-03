namespace Library;

// Esta clase cumple con SRP: tiene una única responsabilidad, 
// que es representar los datos de un usuario del sistema.

public class User
{
    // Campos privados: encapsulación de datos
    private string name;
    private string mail;
    private string phone;
    private string id;

    // Patrón EXPERT: Esta clase es experta en conocer y gestionar
    public string Name { get { return name; } set { name = value; } }
    public string Mail { get { return mail; } set { mail = value; } }
    public string Phone { get { return phone; } set { phone = value; } }
    public string Id { get { return id; } set { id = value; } }

    // Patrón EXPERT: El User es responsable de su propia inicialización
    public User(string name, string mail, string phone, string id)
    {
        Name = name;
        Mail = mail;
        Phone = phone;
        Id = id;
    }
    
    
}