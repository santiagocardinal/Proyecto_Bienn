namespace Library;

public class Tag
{
    private string id;
    private string name;
    private string description;

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