namespace BS.Application.ViewModels.AR;

public class ARInvoiceListVM
{
    public int ID { get; set; }

    public string Type { get; set; }

    public string Status { get; set; }

    public DateTime? InvoiceDate { get; set; }

    public DateTime? DueDate { get; set; }

    public string Code { get; set; }

    public string Customer { get; set; }

    public decimal? TotalAmount { get; set; }

    public decimal? BalanceAmount { get; set; }
}
