namespace Library;

// SRP: IWriter define un contrato con una única responsabilidad:
// escribir/persistir una interacción. No especifica CÓMO se escribe,
// solo que las clases que implementen esta interfaz deben poder hacerlo.
public interface IWriter
{ 
    void Write(Interaction interaction);
}