namespace BS.Infrastructure.Repositories.AR;

public class ARInvoiceDetailRepository : GenericRepository<ARInvoiceDetail>, IARInvoiceDetailRepository
{
    public ARInvoiceDetailRepository(BillsoftDBContext context) : base(context)
    {
    }
}
