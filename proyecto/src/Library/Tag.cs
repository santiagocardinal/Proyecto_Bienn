namespace Library;

// SRP: Tag tiene una única responsabilidad, que es representar
// una etiqueta/categoría con su información básica.

public class Tag
{
    private string id;
    private string name;
    private string description;

    // Patrón EXPERT: Tag es experto en conocer y proporcionar
    // su propia información (id, nombre, descripción)
    public string Id
    {
        get { return id; }
        set { id = value; }
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    // Patrón EXPERT: Tag es responsable de su propia inicialización
    // y conoce qué datos necesita para existir
    public Tag(string id, string name, string description)
    {
        this.Id = id;
        this.Name = name;
        this.Description = description;
    }
    public override string ToString()
    {
        return $"{this.Id}, {this.Name}, {this.Description}," ;
    }
}