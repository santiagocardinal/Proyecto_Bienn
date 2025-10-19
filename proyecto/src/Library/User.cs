namespace Library;

public class User
{
    private string name;
    private string mail;
    private string phone;
    private string id;

    public string Name { get { return name; } set { name = value; } }
    public string Mail { get { return mail; } set { mail = value; } }
    public string Phone { get { return phone; } set { phone = value; } }
    public string Id { get { return id; } set { id = value; } }


    public User(string name, string mail, string phone, string id)
    {
        Name = name;
        Mail = mail;
        Phone = phone;
        Id = id;
    }
}

//