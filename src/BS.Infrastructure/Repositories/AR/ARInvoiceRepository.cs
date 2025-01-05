namespace BS.Infrastructure.Repositories.AR;

public class ARInvoiceRepository : GenericRepository<ARInvoice>, IARInvoiceRepository
{
    public ARInvoiceRepository(BillsoftDBContext context) : base(context)
    {
    }

    public async Task<IEnumerable<ARInvoiceDetail>> GetInvoiceDetails(int companyId, int invoiceId)
    {
        return await context.ARInvoiceDetails
            .Where(s => /*s.ProfileID == profileId && */s.InvoiceID == invoiceId)
            .ToListAsync();
    }

    public async Task<IEnumerable<ARInvoiceListVM>> GetInvoices(int companyId, QueryFilter queryFilter, string[] allowedStatuses, string typeId)
    {
        if (allowedStatuses.Length == 0)
            allowedStatuses = new string[] {SYSStatus.New.ToString(), SYSStatus.Active.ToString(),
                SYSStatus.Archive.ToString(), SYSStatus.Inactive.ToString()};

        var query = context.ARInvoices
            //.Where(s => s.companyId == companyId
            //    && s.TypeID == (string.IsNullOrEmpty(typeId) ? s.TypeID : typeId)
            //    && allowedStatuses.Contains(s.StatusID))
            .Include(s => s.Customer)
            .AsQueryable();

        if (!string.IsNullOrEmpty(queryFilter.SearchText))
        {
            query = query.Where(s => s.InvoiceNo.Contains(queryFilter.SearchText)
            || s.Customer.Title.Contains(queryFilter.SearchText)
            || s.Customer.Code.Contains(queryFilter.SearchText)
            /*|| s.StatusID.Contains(queryFilter.SearchText)*/);
        }

        queryFilter.RecordCount = query.Count();

        if (!string.IsNullOrEmpty(queryFilter.SortColumn))
        {
            if (queryFilter.SortDirection == SortDirection.ASC)
                query = query.OrderBy(EvaluateOrderBy(queryFilter.SortColumn));
            else
                query = query.OrderByDescending(EvaluateOrderBy(queryFilter.SortColumn));
        }
        else
            query = query.OrderBy(EvaluateOrderBy(string.Empty));

        if (queryFilter.PageNumber > 0)
        {
            var skipRecords = (queryFilter.PageNumber - 1) * queryFilter.PageSize;
            query = query
                .Skip(skipRecords)
                .Take(queryFilter.PageSize);
        }

        var result = query.Select(i => new ARInvoiceListVM
        {
            ID = i.InvoiceID,
            Code = i.InvoiceNo,
            //Type = i.TypeID,
            //Status = i.StatusID,
            InvoiceDate = i.InvoiceDate,
            //DueDate = i.InvoiceDueDate,
            Customer = i.Customer.Title,
            TotalAmount = i.TotalAmount,
            //BalanceAmount = i.BalanceAmount
        });

        return await Task.FromResult(result.ToList());
    }

    public async Task<IEnumerable<ARInvoiceListVM>> GetInvoicesByCustomer(int companyId, int customerId)
    {
        var query = context.ARInvoices
            .Where(s => s.CustomerID == customerId)
            .AsQueryable();

        var result = query.Select(i => new ARInvoiceListVM
        {
            ID = i.InvoiceID,
            Code = i.InvoiceNo,
            //Type = i.TypeID,
            //Status = i.StatusID,
            InvoiceDate = i.InvoiceDate,
            //DueDate = i.InvoiceDueDate,
            Customer = i.Customer.Code,
            TotalAmount = i.TotalAmount,
            //BalanceAmount = i.BalanceAmount
        });

        return await Task.FromResult(result.ToList());
    }

    public async Task<IEnumerable<ARInvoiceType>> GetInvoiceTypes()
    {
        //return await context.ARInvoiceTypes.ToListAsync();
        throw new NotImplementedException();

    }

    public Dictionary<int, string> GetParentInvoices(int companyId, int customerId)
    {
        throw new NotImplementedException();
    }

    #region private methods
    private Expression<Func<ARInvoice, object>> EvaluateOrderBy(string propertyName)
    {
        switch (propertyName.ToLowerInvariant())
        {
            //case "type":
            //    return s => s.TypeID;

            case "status":
                return s => s.StatusID;

            //case "invoiceduedate":
            //    return s => s.InvoiceDueDate;

            case "date":
                return s => s.InvoiceDate;

            case "customer":
                return s => s.Customer.Title;

            //case "balance":
            //    return s => s.BalanceAmount;

            case "amount":
                return s => s.TotalAmount;

            case "datemodified":
                return s => s.DateCreated;

            case "invoiceno":
            default:
                return s => s.InvoiceNo;
        }
    }


    #endregion
}
