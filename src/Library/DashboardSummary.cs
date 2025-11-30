namespace Library;

// SRP: DashboardSummary tiene una única responsabilidad: encapsular
// y transportar datos del resumen/dashboard del sistema.
// Es un DTO (Data Transfer Object) que agrupa información relacionada.
//
// EXPERT: DashboardSummary es experto en conocer y proporcionar
// la información resumida del dashboard. Encapsula los datos que
// conforman el resumen ejecutivo del sistema.
//

/// <summary>
/// Representa un resumen general del sistema (dashboard).
/// Es un DTO (Data Transfer Object) que agrupa información relevante sobre:
/// - Clientes con interacciones recientes
/// - Reuniones próximas
/// - Total de clientes registrados
/// </summary>
public class DashboardSummary
{
    
    public List<Interaction> RecentInteractions { get; set; }
    
  
    public List<Meeting> UpcomingMeetings { get; set; }
    
    public int TotalCustomers { get; set; }
    
    public DashboardSummary(List<Interaction> recentInteractions, List<Meeting> upcomingMeetings, int totalCustomers)
    {
        this.RecentInteractions = recentInteractions;
        this.UpcomingMeetings = upcomingMeetings;
        this.TotalCustomers = totalCustomers;
    }
}