namespace Library;

// POLIMORFISMO: FileWriter implementa la interfaz IWriter,
// lo que permite que sea usado polimórficamente donde se espere un IWriter.
// Cualquier código que trabaje con IWriter puede usar FileWriter sin conocer
// los detalles específicos de la implementación.
//
// LSP: FileWriter puede sustituir a IWriter en cualquier contexto.
// Cumple con el contrato definido por la interfaz (aunque la implementación
// está pendiente).
//
// SRP: FileWriter tiene la responsabilidad única de escribir interacciones
// a un archivo. No se encarga de lógica de negocio, validaciones complejas
// de interacciones, ni gestión de colecciones.
public class FileWriter: IWriter
{

    public string path { get; set; }
    
    // - Decidir el formato de serialización (JSON, XML, CSV, texto plano, etc.)
    public void Write(Interaction interaction)
    {
        
        throw new NotImplementedException();
    }
}