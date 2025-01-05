using System.ComponentModel.DataAnnotations.Schema;

namespace BS.Application.ViewModels.AR;

public class ARCustomerVM
{
    public int ID { get; set; }

    public string Code { get; set; }
    public byte StatusID { get; set; }

    public int CompanyId { get; set; }

    public string Name { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Title { get; set; }

    public string Currency { get; set; }

    public string EmailAddress { get; set; }

    public string WorkPhone { get; set; }

    public string MobilePhone { get; set; }

    public string GSTNo { get; set; }

    public string PanNo { get; set; }

    public bool? SEZCustomer { get; set; }

    public bool? MSMECustomer { get; set; }

    public string BillContactName { get; set; }
    public string BillAddress1 { get; set; }

    public string BillAddress2 { get; set; }

    public string BillCountryCode { get; set; }

    public string BillCity { get; set; }

    public string BillState { get; set; }

    public string BillZipcode { get; set; }

    public bool? IsDefaultBillAddress { get; set; }
    public string ShipContactName { get; set; }
    public string ShipAddress1 { get; set; }

    public string ShipAddress2 { get; set; }

    public string ShipCountryCode { get; set; }

    public string ShipCity { get; set; }

    public string ShipState { get; set; }

    public string ShipZipcode { get; set; }

    public int TermsID { get; set; }

    public bool? IsDefaultShipAddress { get; set; }

    public string UDF1 { get; set; }
    public string UDF2 { get; set; }
    public string UDF3 { get; set; }
    public string UDF4 { get; set; }
    public string UDF5 { get; set; }

    

}

public class ARCustomerDetailVM
{ 
    public ARCustomerVM Customer { get; set; }

    public IEnumerable<ARCustomerAddress> BillingAddresses { get; set; }
    public IEnumerable<ARCustomerAddress> ShippingAddresses { get; set; }
    public IEnumerable<SYSNotes> Notes { get; set; }

}

public class ARCustomerContactVM
{

}

public class ARCustomerContactDetailVM
{

}