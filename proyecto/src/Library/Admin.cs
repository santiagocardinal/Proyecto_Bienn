namespace Library;

public class Admin : User
{
    private List<Seller> sellers;

    public List<Seller> Sellers
    {
        get
        {
            return sellers;
        }
        set { sellers = value; }
    }

    public Admin(string name, string mail, string phone, string id) 
        : base(name, mail, phone, id)
    {
        sellers = new List<Seller>();
    }
    public bool CreateSeller(User user)
    {
        if (user == null)
        {
            return false;
        }

        foreach (var person in sellers)
        {
            if (person.Id == user.Id)
            {
                return false;
            }
        }
        //INTENTO DE USAR EL CREATOR QSY, REVISARLO EN UN FUTURO :(
        Seller newSeller = new Seller(user.Name, user.Mail, user.Phone,user.Id);
        sellers.Add(newSeller);
        return true;
    }

    public bool SuspendSeller(string userId)
    {
        foreach (var person in sellers)
        {
            if (person.Id == userId)
            {
                person.IsSuspended = true;
                Console.WriteLine($"Se ha suspendido al vendedor {person.Name} cuya cédula es {person.Id}.");
                return true;
            }
        }

        Console.WriteLine("No se encontró al vendedor.");
        return false;
    }

    //HABLAR CON MARCELO POR LO DE DELATESELLER
    /*public bool DeleteSeller(string userId)
    {
        foreach (var person in sellers)
        {
            if (person.Id == userId)
            {
                person.IsDeleted = true;
                Console.WriteLine($"Se ha eliminado al vendedor {person.Name} cuya cédula es {person.Id}.");
                return true;
            }
        }

        Console.WriteLine("No se encontró al vendedor.");
        return false;
    }*/
}