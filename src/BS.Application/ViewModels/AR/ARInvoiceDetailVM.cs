namespace BS.Application.ViewModels.AR;

public class ARInvoiceDetailVM
{
    public int InvoiceID { get; set; }

    public ARInvoice Invoice { get; set; }

    public IEnumerable<ARTerm> Terms { get; set; }

    public IEnumerable<ARInvoiceDetail> InvoiceDetails { get; set; }

    public Dictionary<int, string> ParentInvoices { get; set; }

    public string[] DeliveryTypes { get; set; }
}
