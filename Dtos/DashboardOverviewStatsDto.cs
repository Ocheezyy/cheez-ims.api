namespace cheez_ims_api.Dtos;

public class DashboardOverviewStatsDto
{
    public required int TotalInventory { get; set; }
    public required int LowStockInventory { get; set; }
    public required decimal TotalValue { get; set; }
    public required int ActiveOrders { get; set; }
}