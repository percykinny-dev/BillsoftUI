namespace BS.Infrastructure.Repositories.AR;

public class ARQuotationRepository : GenericRepository<ARQuotation>, IARQuotationRepository
{
    public ARQuotationRepository(BillsoftDBContext context) : base(context)
    {
    }

    public async Task<IEnumerable<ARQuotation>> GetQuotations(int companyId, QueryFilter queryFilter, byte[] allowedStatuses)
    {
        var query = context.ARQuotations
            .Where(s => s.CompanyID == companyId && allowedStatuses.Contains(s.StatusID))
            .AsQueryable();

        if (queryFilter == null)
        {
            var r1 = query.Select(i => new ARQuotation
            {
                QuotationID = i.QuotationID,
                StatusID = i.StatusID,
                Subject = i.Subject,
                ContactPerson = i.ContactPerson
            });

            return await Task.FromResult(r1.ToList());
        }


        if (!string.IsNullOrEmpty(queryFilter.SearchText))
        {
            query = query.Where(s => s.Subject.Contains(queryFilter.SearchText)
            || s.ContactPerson.Contains(queryFilter.SearchText));
        }

        queryFilter.RecordCount = query.Count();

        if (!string.IsNullOrEmpty(queryFilter.SortColumn))
        {
            if (queryFilter.SortDirection == SortDirection.ASC)
                query = query.OrderBy(EvaluateOrderBy(queryFilter.SortColumn));
            else
                query = query.OrderByDescending(EvaluateOrderBy(queryFilter.SortColumn));
        }

        if (queryFilter.PageNumber > 0)
        {
            var skipRecords = (queryFilter.PageNumber - 1) * queryFilter.PageSize;
            query = query
                .Skip(skipRecords)
                .Take(queryFilter.PageSize);
        }

        var result = query.Select(i => new ARQuotation
        {
            QuotationID = i.QuotationID,
            StatusID = i.StatusID,
            Subject = i.Subject,
            ContactPerson = i.ContactPerson
        });

        return await Task.FromResult(result.ToList());
    }

    public async Task<ARQuotation> GetQuotation(int companyId, int quotationId)
    {
        var quotation = await context.ARQuotations.FindAsync(quotationId);
        if (quotation == null || quotation.CompanyID != companyId)
            throw new BSInfrastructureException("invalid quotation id");

        return quotation;
    }

    public async Task<bool> Delete(int companyId, int quotationId)
    {
        var q = await context.ARQuotations.FindAsync(quotationId);
        if (q == null || q.CompanyID != companyId)
            throw new BSInfrastructureException("invalid quotation id");
        await DeleteAsync(q);
        return true;
    }

    public async override Task DeleteAsync(ARQuotation entity)
    {
        entity.StatusID = (byte)SYSStatus.Deleted;
        await context.SaveChangesAsync();
    }

    #region private methods
    private Expression<Func<ARQuotation, object>> EvaluateOrderBy(string propertyName)
    {
        switch (propertyName)
        {
            case "ContactPerson":
                return s => s.ContactPerson;

            case "DateModified":
            default:
                return s => s.DateModified;
        }
    }

    #endregion
}
