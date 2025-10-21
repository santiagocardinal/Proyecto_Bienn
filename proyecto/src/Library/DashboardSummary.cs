namespace Library;

// SRP: DashboardSummary tiene una única responsabilidad: encapsular
// y transportar datos del resumen/dashboard del sistema.
// Es un DTO (Data Transfer Object) que agrupa información relacionada.
//
// EXPERT: DashboardSummary es experto en conocer y proporcionar
// la información resumida del dashboard. Encapsula los datos que
// conforman el resumen ejecutivo del sistema.
//
// Esta clase sigue el patrón DTO (Data Transfer Object):
// - Agrupa datos relacionados
// - No contiene lógica de negocio compleja
// - Facilita el transporte de información entre capas/componentes
public class DashboardSummary
{
    
    public List<Customer> RecentInteractions { get; set; }
    
  
    public List<Meeting> UpcomingMeetings { get; set; }
    
    public int TotalCustomers { get; set; }
    
    public DashboardSummary(List<Customer> recentInteractions, List<Meeting> upcomingMeetings, int totalCustomers)
    {
        this.RecentInteractions = recentInteractions;
        this.UpcomingMeetings = upcomingMeetings;
        this.TotalCustomers = totalCustomers;
    }
}