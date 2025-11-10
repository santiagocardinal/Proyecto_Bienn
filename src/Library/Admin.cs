namespace Library;

// Esta clase cumple con LSP (Liskov Substitution Principle):
// Admin es un tipo específico de User y puede sustituir a User en cualquier contexto
// sin romper la funcionalidad del programa.

/// <summary>
/// Representa a un administrador del sistema.
/// Hereda de <see cref="User"/> y posee los mismos datos básicos de usuario,
/// pudiendo realizar tareas administrativas y de gestión general.
/// </summary>
public class Admin : User
{
    // Patrón EXPERT: Admin delega a User (su clase base experta) 
    // la responsabilidad de inicializar los datos comunes de usuario
    public Admin(string name, string mail, string phone, string id)
        : base(name, mail, phone, id)
    {
       
    }
    
}