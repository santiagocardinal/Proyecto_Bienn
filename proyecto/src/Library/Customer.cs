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
    private List<Interaction> _interactions;

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
    public List<Interaction> Interactions
    {
        get { return this._interactions; }
        set { this._interactions = value; }
    }

    public Customer(string id, string name, string familyName, string mail, string phone, string gender, DateTime birthDate)
    {
        this.Id = id;
        this.Name = name;
        this.FamilyName = familyName;
        this.Mail = mail;
        this.Phone = phone;
        this.Gender = gender;
        this.BirthDate = birthDate;
        this.Interactions = new List<Interaction>();
        this.Tags = new List<Tag>();
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

    public void AddInteraction(Interaction interaction)
    {
        this.Interactions.Add(interaction);
    }
    
    public List<Interaction> GetInteraction()
    {
        return this.Interactions;
    }

    public List<T> GetInteractionsByType<T>() where T : Interaction //Utiliza las interacciones y busca a partir de ellas.
    {
        List<T> result = new List<T>();

        foreach (var interaction in _interactions)
        {
            if (interaction is T typedInteraction)
            {
                result.Add(typedInteraction);
            }
        }

        return result;
    }
    
    public List<Interaction> GetInteractionsByDate(DateTime date)
    {
        List<Interaction> result = new List<Interaction>();

        foreach (var interaction in _interactions)
        {
            if (interaction.Date.Date == date.Date) // Solo día, ignorando hora
            {
                result.Add(interaction);
            }
        }

        return result;
    }
    
    
    public override string ToString()
    {
        return $"Id: {this.Id}, Name: {this.Name}, Family Name: {this.FamilyName}, Mail: {this.Mail}, Phone: {this.Phone},  Gender: {this.Gender}, BirthDate: {this.BirthDate}, Tags: {this.Tags.Count}";
    }
} 