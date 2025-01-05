namespace BS.Infrastructure.Repositories.AR;

public class ARChallanRepository : GenericRepository<ARChallan>, IARChallanRepository
{
    public ARChallanRepository(BillsoftDBContext context) : base(context)
    {
    }

    public async Task<IEnumerable<ARChallanVM>> GetChallans(int companyId, QueryFilter queryFilter, byte[] allowedStatuses)
    {
        var query = context.ARChallans
            .Where(s => s.CompanyID == companyId && allowedStatuses.Contains(s.StatusID))
            .AsQueryable();

        if (queryFilter == null)
        {
            var r1 = query.Select(i => new ARChallanVM
            {
                ChallanID = i.ChallanID,
                ChallanNo = i.ChallanNo
            });

            return await Task.FromResult(r1.ToList());
        }


        if (!string.IsNullOrEmpty(queryFilter.SearchText))
        {
            query = query.Where(s => s.ChallanNo.Contains(queryFilter.SearchText)
            || s.Department.Contains(queryFilter.SearchText));
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
        {
            query = query.OrderByDescending(EvaluateOrderBy("DateModified"));
        }

        if (queryFilter.PageNumber > 0)
        {
            var skipRecords = (queryFilter.PageNumber - 1) * queryFilter.PageSize;
            query = query
                .Skip(skipRecords)
                .Take(queryFilter.PageSize);
        }

        var result = query.Select(i => new ARChallanVM
        {
            ChallanID = i.ChallanID,
            ChallanNo = i.ChallanNo
        });

        return await Task.FromResult(result.ToList());
    }

    public async Task<bool> CheckChalanCodeAlreadyExists(ARChallan challan)
    {
        if (challan.ChallanID == 0)
            return await context.ARChallans.FirstOrDefaultAsync(s => s.CompanyID == challan.CompanyID
            && s.ChallanNo == challan.ChallanNo) != null;
        else
            return await context.ARChallans.FirstOrDefaultAsync(s => s.CompanyID == challan.CompanyID
            && s.ChallanNo == challan.ChallanNo && s.CompanyID != challan.CompanyID) != null;
    }

    public override async Task DeleteAsync(ARChallan entity)
    {
        entity.StatusID = (byte)SYSStatus.Deleted;
        await base.UpdateAsync(entity);
    }

    public async Task<bool> Delete(int companyId, int challanId)
    {
        var challan = await context.ARChallans.FindAsync(challanId);
        if (challan == null || challan.CompanyID != companyId)
            throw new BSInfrastructureException("invalid challan");

        await DeleteAsync(challan);
        return true;
    }

    #region private methods
    private Expression<Func<ARChallan, object>> EvaluateOrderBy(string propertyName)
    {
        switch (propertyName)
        {
            case "ChallanNo":
                return s => s.ChallanNo;

            case "Department":
                return s => s.Department;

            case "DeliveryPerson":
                return s => s.DeliveryPerson;

            case "DateModified":
            default:
                return s => s.DateModified;
        }
    }

    #endregion
}
