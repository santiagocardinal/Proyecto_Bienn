namespace Library;

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