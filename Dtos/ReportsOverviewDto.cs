using NuGet.Protocol.Plugins;

namespace cheez_ims_api.Dtos;

public class ReportOverviewInventoryValueByCategory
{
    public string Category { get; set; }
    public decimal Value { get; set; }
}

public class ReportOverviewInventoryStatus
{
    public int InStock { get; set; }
    public int LowStock { get; set; }
    public int OutOfStock { get; set; }
    public int Discontinued { get; set; }
}

public class ReportOverviewSalesTrend
{
    public string Month { get; set; }
    public decimal Sales { get; set; }
    public int Orders { get; set; }
}

public class ReportOverviewTopProducts
{
    public string ProductName { get; set; }
    public decimal Sales { get; set; }
}

public class ReportsOverviewDto
{
    public ReportOverviewInventoryStatus InventoryStatus { get; set; }
    public ReportOverviewSalesTrend[] SalesTrend { get; set; }
    public ReportOverviewInventoryValueByCategory[] InventoryValue { get; set; }
    public ReportOverviewTopProducts[] TopProducts { get; set; }
}