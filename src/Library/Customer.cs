namespace Library;

// SRP: Customer tiene la responsabilidad de representar a un cliente
// y gestionar su información personal, sus etiquetas e interacciones.
// EXPERT: Customer es el experto en conocer toda la información relacionada
// con un cliente específico.
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
    private Seller _seller;
    private bool isActive;


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

    public Seller Seller
    {
        get{return this._seller; }
        set { this._seller = value; }
    }
    
    public bool IsActive
    {
        get{return isActive; }
        set { isActive = value; }
    }
    
    public bool HasPendingCustomerReply { get; private set; }
    
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
        this.IsActive = true;
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

    // POLIMORFISMO: Utiliza genéricos y pattern matching para filtrar
    // interacciones por tipo. Aprovecha que T debe ser un subtipo de Interaction
    // SRP: Responsabilidad específica de filtrar interacciones por tipo
    public List<T> GetInteractionsByType<T>() where T : Interaction
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
    
    public DateTime GetLastInteraction()
    {
        if (this._interactions == null || this._interactions.Count == 0)
        {
            throw new InvalidOperationException("No hay interacciones registradas.");
        }

        DateTime last_interaction = this._interactions[0].Date;

        foreach (var otraInteraccion in _interactions)
        {
            if (otraInteraccion.Date > last_interaction)
            {
                last_interaction = otraInteraccion.Date;
            }
        }

        return last_interaction;
    }
    
    public void Deactivate()
    {
        this.isActive = false;
    }
    
    public void Activate()
    {
        this.isActive = true;
    }
    
    public bool CheckIsActive()
    {
        return this.isActive;
    }
    
    
    public void MarkAsPending()
    {
        this.HasPendingCustomerReply = true;
        this.IsActive = false;
    }

    public void MarkAsActive()
    {
        this.HasPendingCustomerReply = false;
        this.IsActive = true;
    }

    public void MarkLastReceivedAsResponded()
    {
        var last = Interactions.LastOrDefault(i => i.Type == ExchangeType.Received && !i.HasResponse);
        if (last != null) last.HasResponse = true;
    }
    
    
    public List<Interaction> GetUnansweredInteractions()
    {
        List<Interaction> unanswered = new List<Interaction>();
    
        foreach (Interaction interaction in _interactions)
        {
            if (interaction.Type == ExchangeType.Received && !interaction.HasResponse)
            {
                unanswered.Add(interaction);
            }
        }
    
        return unanswered;
    }
    
 
    public override string ToString()
    {
        return $"Id: {this.Id}, Name: {this.Name}, Family Name: {this.FamilyName}, Mail: {this.Mail}, Phone: {this.Phone},  Gender: {this.Gender}, BirthDate: {this.BirthDate}, Tags: {this.Tags.Count}";
    }
}