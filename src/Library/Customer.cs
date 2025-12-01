namespace Library;

// SRP: Customer tiene la responsabilidad de representar a un cliente
// y gestionar su información personal, sus etiquetas e interacciones.
// EXPERT: Customer es el experto en conocer toda la información relacionada
// con un cliente específico.

/// <summary>
/// Representa a un cliente del sistema, con su información personal,
/// etiquetas e historial de interacciones.
/// Contiene comportamientos para gestionar su estado y sus relaciones.
/// </summary>
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
    
    /// <summary>
    /// Filtra las interacciones del cliente por tipo utilizando genéricos.
    /// Devuelve solo las interacciones que coincidan con el tipo especificado.
    /// </summary>
    /// <typeparam name="T">Tipo de interacción (por ejemplo: Sale, Meeting, Call).</typeparam>
    /// <returns>Lista de interacciones del tipo solicitado.</returns>
    
    public List<Interaction> GetInteractionsByTypeName(string typeName)
    {
        List<Interaction> result = new List<Interaction>();
        foreach (var interaction in _interactions)
        {
            if (interaction.GetType().Name.Equals(typeName, StringComparison.OrdinalIgnoreCase))
            {
                result.Add(interaction);
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
    
    public List<Interaction> GetInteractionsByTypeAndDate(string typeName, DateTime date)
    {
        List<Interaction> result = new List<Interaction>();
        foreach (var interaction in _interactions)
        {
            bool matchesType = string.IsNullOrEmpty(typeName) || 
                               interaction.GetType().Name.Equals(typeName, StringComparison.OrdinalIgnoreCase);
            bool matchesDate = interaction.Date.Date == date.Date;
        
            if (matchesType && matchesDate)
            {
                result.Add(interaction);
            }
        }
        return result;
    }
    
    
    public DateTime? GetLastInteraction()
    {
        if (_interactions == null || _interactions.Count == 0)
            return null;

        return _interactions.Max(i => i.Date);
    }

    
    /*public DateTime GetLastInteraction()
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
    }*/
    
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
    
    // Obtener las últimas 5 interacciones
    public List<Interaction> GetRecentInteractions()
    {
        List<Interaction> result = new List<Interaction>();
    
        // Ordenar por fecha (más recientes primero)
        List<Interaction> ordenadas = new List<Interaction>(_interactions);
        ordenadas.Sort((a, b) => b.Date.CompareTo(a.Date));
    
        // Tomar solo las primeras 5
        for (int i = 0; i < 5 && i < ordenadas.Count; i++)
        {
            result.Add(ordenadas[i]);
        }
    
        return result;
    }

// Obtener las próximas 5 reuniones
    public List<Meeting> GetUpcomingMeetings()
    {
        List<Meeting> reuniones = new List<Meeting>();
    
        // Buscar solo las reuniones que son en el futuro
        foreach (var interaction in _interactions)
        {
            if (interaction is Meeting meeting && meeting.Date > DateTime.Now)
            {
                reuniones.Add(meeting);
            }
        }
    
        // Ordenar por fecha (más próximas primero)
        reuniones.Sort((a, b) => a.Date.CompareTo(b.Date));
    
        // Tomar solo las primeras 5
        List<Meeting> result = new List<Meeting>();
        for (int i = 0; i < 5 && i < reuniones.Count; i++)
        {
            result.Add(reuniones[i]);
        }
    
        return result;
    }
    
    
    // En la clase Customer
    public bool HasQuote(DateTime date, string topic, ExchangeType type, double amount, string description)
    {
        foreach (var interaction in _interactions)
        {
            if (interaction is Quote quote)
            {
                if (quote.Date == date && 
                    quote.Topic == topic && 
                    quote.Type == type && 
                    Math.Abs(quote.Amount - amount) < 0.0001 && 
                    quote.Description == description)
                {
                    return true;
                }
            }
        }
        return false;
    }
    // Modifica FindQuote en Customer para incluir description
    public Quote FindQuote(DateTime date, string topic, ExchangeType type, double amount, string description = null)
    {
        foreach (var interaction in _interactions)
        {
            if (interaction is Quote quote)
            {
                bool matches = quote.Date == date && 
                               quote.Topic == topic && 
                               quote.Type == type && 
                               Math.Abs(quote.Amount - amount) < 0.0001;
            
                // Si se proporciona description, también validarla
                if (description != null)
                    matches = matches && quote.Description == description;
            
                if (matches)
                    return quote;
            }
        }
        return null;
    }

    public bool HasSale(DateTime date, string topic, ExchangeType type, string product, double amount)
    {
        foreach (var interaction in _interactions)
        {
            if (interaction is Sale sale)
            {
                if (sale.Date == date && 
                    sale.Topic == topic && 
                    sale.Type == type && 
                    sale.Product == product && 
                    sale.Amount.Amount == amount)
                {
                    return true;
                }
            }
        }
        return false;
    }
    
    
    public override string ToString()
    {
        return $"Id: {this.Id}, Name: {this.Name}, Family Name: {this.FamilyName}, Mail: {this.Mail}, Phone: {this.Phone},  Gender: {this.Gender}, BirthDate: {this.BirthDate}, Tags: {this.Tags.Count}";
    }
}