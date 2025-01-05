namespace BS.Domain.Entities.AR;

public class ARCustomerAddress
{
    [Key]
    public int AddressID { get; set; }

    public int CustomerID { get; set; }

    public byte AddressType { get; set; }

    public byte StatusID { get; set; }

    public bool? IsDefault { get; set; }

    public string ShipCode { get; set; }

    public string ContactName { get; set; }

    public string Address1 { get; set; }

    public string Address2 { get; set; }

    public string CountryCode { get; set; }

    public string City { get; set; }

    public string State { get; set; }

    public string Zipcode { get; set; }

    public string EmailAddress { get; set; }

    public string WorkPhone { get; set; }

    public string MobilePhone { get; set; }

    public string WhatsAppPhone { get; set; }

    public string GSTNo { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

}
