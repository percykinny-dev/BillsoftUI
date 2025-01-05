namespace BS.Domain.Entities.AR;

public class ARQuotation
{
    [Key]
    public int QuotationID { get; set; }

    public int CompanyID { get; set; }

    public string QuotationNo { get; set; }

    public DateTime QuotationDate { get; set; }

    public string Subject { get; set; }

    public byte StatusID { get; set; }

    public bool? IsReverseCharge { get; set; }

    public int? StateID { get; set; }

    public decimal? NetAmount { get; set; }

    public int? CustomerID { get; set; }

    public int? ShipAddressID { get; set; }

    public decimal? CGSTAmount { get; set; }

    public decimal? SGSTAmount { get; set; }

    public decimal? IGSTAmount { get; set; }

    public decimal? GSTAmount { get; set; }

    public decimal? TotalAmount { get; set; }

    public bool? IsDeleted { get; set; }

    public decimal? Discount { get; set; }

    public decimal? DiscountAmount { get; set; }

    public string ContactPerson { get; set; }

    public string Description { get; set; }

    public short? PaymentTermID { get; set; }

    public decimal? AmountReceived { get; set; }

    public string PurchaseOrderNo { get; set; }

    public DateTime? PurchaseOrderDate { get; set; }

    public short? GSTRateID { get; set; }

    public decimal? RateInclusive { get; set; }

    public string Delivery { get; set; }

    public string Freight { get; set; }

    public string Cost { get; set; }

    public int? ModifiedbyUserID { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

}
