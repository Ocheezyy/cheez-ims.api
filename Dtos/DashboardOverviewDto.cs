namespace cheez_ims_api.Dtos;

public class DashboardOverviewDto
{
    public required string Month { get; set; }
    public required DateTime SortDate { get; set; }
    public required decimal TotalAmount { get; set; }
}