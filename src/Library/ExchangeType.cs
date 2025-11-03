namespace Library;

// SRP: ExchangeType tiene una única responsabilidad: definir los dos estados
// posibles de una interacción (enviada o recibida).
// 
// Este enum proporciona type-safety y claridad semántica en el código,
// evitando el uso de strings mágicos ("sent", "received") o booleanos
// que serían menos expresivos y más propensos a errores.

public enum ExchangeType 
{
    Sent,      
    Received   
}