namespace BS.Application.ViewModels.AR;

public class ARSharedListsVM
{
    public IEnumerable<ARCustomerListVM> Customers { get; set; }

    public IEnumerable<ARItemListVM> Items { get; set; }

    public IEnumerable<SYSUOMType> UOMTypes { get; set; }

    public IEnumerable<SYSGSTRateType> GSTRateTypes { get; set; }

}

public class ARCustomerListVM
{
    public int CustomerID { get; set; }

    public int CompanyID { get; set; }

    public string Code { get; set; }

    public string Title { get; set; }

}

public class ARItemListVM
{
    public int ItemID { get; set; }

    public string ItemCode { get; set; }    

    public string ItemName { get; set; }    

    public string HSNNo { get; set; }   

}


