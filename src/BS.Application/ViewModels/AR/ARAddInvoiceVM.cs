namespace BS.Application.ViewModels.AR;

public  class ARAddInvoiceVM
{
    public IEnumerable<ARCustomer> Customers { get; set; }

    public IEnumerable<ARInvoiceType> InvoiceTypes { get; set; }
}
