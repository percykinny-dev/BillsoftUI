namespace BS.Domain.Entities.AR;

public class ARCustomer
{
    [Key]
    public int CustomerID { get; set; }

    public byte StatusID { get; set; }

    public int CompanyID { get; set; }

    public string TypeID { get; set; }

    public string Code { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Title { get; set; }

    public string Currency { get; set; }

    public string EmailAddress { get; set; }

    public string WorkPhone { get; set; }

    public string MobilePhone { get; set; }

    public string WhatsAppPhone { get; set; }

    [ForeignKey("PaymentTermID")]
    public ARTerm Term { get; set; }

    public int? PaymentTermID { get; set; }

    public string GSTNo { get; set; }

    public string PanNo { get; set; }

    public bool? SEZCustomer { get; set; }

    public bool? MSMECustomer { get; set; }

    public string BillingAddress1 { get; set; }

    public string BillingAddress2 { get; set; }

    public string BillingCountryCode { get; set; }

    public string BillingCity { get; set; }

    public string BillingState { get; set; }

    public string BillingZipcode { get; set; }

    public string Remarks { get; set; }

    public string UDF1 { get; set; }

    public string UDF2 { get; set; }

    public string UDF3 { get; set; }

    public string UDF4 { get; set; }

    public string UDF5 { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

}