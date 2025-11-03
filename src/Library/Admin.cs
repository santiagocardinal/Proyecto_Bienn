namespace Library;

// Esta clase cumple con LSP (Liskov Substitution Principle):
// Admin es un tipo específico de User y puede sustituir a User en cualquier contexto
// sin romper la funcionalidad del programa.
public class Admin : User
{
    // Patrón EXPERT: Admin delega a User (su clase base experta) 
    // la responsabilidad de inicializar los datos comunes de usuario
    public Admin(string name, string mail, string phone, string id)
        : base(name, mail, phone, id)
    {
       
    }
    
}