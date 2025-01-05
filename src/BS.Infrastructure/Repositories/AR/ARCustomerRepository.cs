namespace BS.Infrastructure.Repositories.AR;

public class ARCustomerRepository : GenericRepository<ARCustomer>, IARCustomerRepository
{
    public ARCustomerRepository(BillsoftDBContext context): base(context)
    {
    }

    public async Task<IEnumerable<ARCustomerVM>> GetCustomers(int companyId, QueryFilter queryFilter, string[] allowedStatuses)
    {
        var query = context.ARCustomers
            .Where(s => s.CompanyID == companyId ) // && allowedStatuses.Contains(s.Title))
            .AsQueryable();

        if (queryFilter == null)
        {
            var r1 = query.Select(i => new ARCustomerVM
            {
                ID = i.CustomerID,
                Name = i.FirstName + ' ' + i.LastName,
                Code = i.Code,
                Title = i.Title,
                Currency = i.Currency,
                GSTNo = i.GSTNo, 
                PanNo = i.PanNo,
                WorkPhone = i.WorkPhone,
                MobilePhone = i.MobilePhone,
                EmailAddress = i.EmailAddress,
                BillAddress1 = i.BillingAddress1,
                BillAddress2 = i.BillingAddress2,
                BillCity = i.BillingCity,
                BillState = i.BillingState,
                SEZCustomer = i.SEZCustomer,
                MSMECustomer = i.MSMECustomer
               
            });

            return await Task.FromResult(r1.ToList());
        }


        if (!string.IsNullOrEmpty(queryFilter.SearchText))
        {
            query = query.Where(s => s.Title.Contains(queryFilter.SearchText)
            || s.Title.Contains(queryFilter.SearchText));
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

        var result = query.Select(i => new ARCustomerVM
        {
            ID = i.CustomerID,
            Name = i.FirstName + ' ' + i.LastName,
            Code = i.Code,
            Title = i.Title,
            GSTNo = i.GSTNo,
            PanNo = i.PanNo,
            WorkPhone = i.WorkPhone,
            MobilePhone = i.MobilePhone,
            EmailAddress = i.EmailAddress,
            BillAddress1 = i.BillingAddress1,
            BillAddress2 = i.BillingAddress2,
            BillCity = i.BillingCity,
            BillState = i.BillingState,
          });

        return await Task.FromResult(result.ToList());
    }

    public override async Task<ARCustomer> Get(int id)
    {
        return await context.ARCustomers.FindAsync(id);
    }

    public async Task<ARCustomerVM> GetCustomerDetails(int companyId,int customerId)
    {
        var customer = await context.ARCustomers.FindAsync(customerId);

        if (customer == null || customer.CompanyID != companyId)
            throw new BSInfrastructureException("invalid customer");

        //if (customer == null)
        //{
        //    return null;
        //}

        var billingAddress = await context.ARCustomerAddresses
            .Where(s => s.CustomerID == customerId && s.AddressType == 1 && s.IsDefault == true)
            .FirstOrDefaultAsync();

        var shippingAddress = await context.ARCustomerAddresses
            .Where(s => s.CustomerID == customerId && s.AddressType == 2 && s.IsDefault == true)
            .FirstOrDefaultAsync();

        var customerVM = new ARCustomerVM
        {
            ID = customer.CustomerID,
            CompanyId = customer.CompanyID,
            Code = customer.Code,
            Name = customer.FirstName + " " + customer.LastName,
            Title = customer.Title,
            EmailAddress = customer.EmailAddress,
            GSTNo = customer.GSTNo,
            PanNo = customer.PanNo,
            WorkPhone = customer.WorkPhone,
            MobilePhone = customer.MobilePhone,
            SEZCustomer = customer.SEZCustomer,
            MSMECustomer = customer.MSMECustomer,
            BillContactName = billingAddress?.ContactName,
            BillAddress1 = billingAddress?.Address1,
            BillAddress2 = billingAddress?.Address2,
            BillCity = billingAddress?.City,
            BillState = billingAddress?.State,
            BillZipcode = billingAddress?.Zipcode,
            ShipContactName = shippingAddress?.ContactName,
            ShipAddress1 = shippingAddress?.Address1,
            ShipAddress2 = shippingAddress?.Address2,
            ShipCity = shippingAddress?.City,
            ShipState = shippingAddress?.State,
            ShipZipcode = shippingAddress?.Zipcode
        };

        return customerVM;
    }


    public async Task<IEnumerable<SYSNotes>> GetCustomerNotes(int customerId, int? topRecordCount)
    {
        var query = context.SysNotes
            .Where(s => s.EntityID == customerId && s.EntityType == "Customer") // && allowedStatuses.Contains(s.Title))
            .OrderByDescending(s=> s.CreatedDate)
            .Take(topRecordCount?? 100)
            .Include(s => s.User)
            .AsQueryable();

     
            var r1 = query.Select(i => new SYSNotes
            {
                NoteID = i.NoteID,
                EntityID = i.EntityID,
                EntityType = i.EntityType,
                EventType = i.EventType,
                EventDescription = i.EventDescription,
                CreatedDate = i.CreatedDate,
                User = i.User
            });

            return await Task.FromResult(r1.ToList());
     
    }

    public async Task<bool> CheckCustomerCodeAlreadyExists(ARCustomer customer)
    {
        if (customer.CustomerID == 0)
            return await context.ARCustomers.FirstOrDefaultAsync(s => s.CompanyID == customer.CompanyID 
            && s.Code == customer.Code) != null;
        else
            return await context.ARCustomers.FirstOrDefaultAsync(s => s.CompanyID == customer.CompanyID 
            && s.Code == customer.Code && s.CustomerID != customer.CustomerID) != null;

    }

    public override async Task DeleteAsync(ARCustomer entity)
    {
        entity.StatusID = (byte)SYSStatus.Deleted;
        await base.UpdateAsync(entity);
    }

    public async  Task<bool> Delete(int companyId, int customerId)
    {
        var customer = await context.ARCustomers.FindAsync(customerId);
        if (customer == null || customer.CompanyID != companyId)
            throw new BSInfrastructureException("invalid customer");

        await DeleteAsync(customer);
        return true;
    }

    public void Insert(ARCustomer entity)
    {
        context.ARCustomers.Add(entity);
        context.SaveChanges();
    }

    #region private methods
    private Expression<Func<ARCustomer, object>> EvaluateOrderBy(string propertyName)
    {
        switch (propertyName)
        {
            case "Code":
                return s => s.Title;

            case "TypeId":
                return s => s.TypeID;

            case "Title":
                return s => s.Title;

            case "DateModified":
            default:
                return s => s.DateModified;
        }
    }

    #endregion
}
