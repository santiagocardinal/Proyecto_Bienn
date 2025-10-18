namespace Library;

public class Admin : User
{
    private List<Seller> sellers;

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

        foreach (Seller person in sellers)
        {
            if (person.Id == user.Id)
            {
                return false;
            }
        }
        //INTENTO DE USAR EL CREATOR QSY, REVISARLO EN UN FUTURO :(
        Seller newSeller = new Seller(user.Id, user.Name);
        sellers.Add(newSeller);
        return true;
    }

    public bool SuspendSeller(string userId)
    {
        foreach (Seller person in sellers)
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

    public bool DeleteSeller(string userId)
    {
        foreach (Seller person in sellers)
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
    }
}