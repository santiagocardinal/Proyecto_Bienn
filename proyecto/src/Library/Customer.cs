namespace Library;

public class Customer
{
    private string id;
    private string name;
    private string familyName;
    private string mail;
    private string phone;
    private string gender;
    private DateTime birthDate;
    private List<Tag> tags;

    public string Id
    {
        get { return this.id; }
        set { this.id = value; }
    }

    public string Name
    {
        get { return this.name; }
        set { this.name = value; }
    }

    public string FamilyName
    {
        get { return this.familyName; }
        set { this.familyName = value; }
    }

    public string Mail
    {
        get { return this.mail; }
        set { this.mail = value; }
    }

    public string Phone
    {
        get { return this.phone; }
        set { this.phone = value; }
    }

    public string Gender
    {
        get { return this.gender; }
        set { this.gender = value; }
    }
    public DateTime BirthDate
    {
        get { return this.birthDate; }
        set { this.birthDate = value; }
    }

    public List<Tag> Tags
    {
        get { return this.tags; }
        set { this.tags = value; }
    }

    public Customer(string id, string name, string familyName, string mail, string phone, string gender, DateTime birthDate)
    {
        this.id = id;
        this.name = name;
        this.familyName = familyName;
        this.mail = mail;
        this.phone = phone;
        this.gender = gender;
        this.birthDate = birthDate;
    }

    public bool IsValidMail(string mail)
    {
        if (this.Mail == mail)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddTag(Tag tag)
    {
        this.Tags.Add(tag);
    }
    
    public void RemoveTag(Tag tag)
    {
        this.Tags.Remove(tag);
    }
    
    public override string ToString()
    {
        return $"Id: {this.Id}, Name: {this.Name}, Family Name: {this.FamilyName}, Mail: {this.Mail}, Phone: {this.Phone},  Gender: {this.Gender}, BirthDate: {this.BirthDate}, Tags: {this.Tags.Count}";
    }
} 