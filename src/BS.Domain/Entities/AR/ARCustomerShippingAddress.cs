namespace BS.Domain.Entities.AR;

public class ARCustomerShippingAddress
{
    [Key]
    public int ShipAddressID { get; set; }

    public int CustomerID { get; set; }

    public byte StatusID { get; set; }

    public string ShipCode { get; set; }

    public string ShippingContactName { get; set; }

    public string ShippingAddress1 { get; set; }

    public string ShippingAddress2 { get; set; }

    public string ShippingCountryCode { get; set; }

    public string ShippingCity { get; set; }

    public string ShippingState { get; set; }

    public string ShippingZipcode { get; set; }

    public string EmailAddress { get; set; }

    public string WorkPhone { get; set; }

    public string MobilePhone { get; set; }

    public string WhatsAppPhone { get; set; }

    public string GSTNo { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

}
