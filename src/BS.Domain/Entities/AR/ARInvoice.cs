namespace BS.Domain.Entities.AR;

public class ARInvoice
{
    [Key]
    public int InvoiceID { get; set; }

    public int CompanyID { get; set; }

    public string InvoiceNo { get; set; }

    public DateTime InvoiceDate { get; set; }

    public byte StatusID { get; set; }

    public bool? IsReverseCharge { get; set; }

    public int? StateID { get; set; }

    public decimal? NetAmount { get; set; }

    public int? CustomerID { get; set; }

    [ForeignKey(nameof(CustomerID))]
    public ARCustomer Customer { get; set; }

    public int? ShipPartyCode { get; set; }

    public string ShipPartyName { get; set; }

    public string ShipPartyGSTNo { get; set; }

    public string ShipPartyAddress { get; set; }

    public string ShipPartyState { get; set; }

    public int? ShipPartyStateCode { get; set; }

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

    public short? PmtTerms { get; set; }

    public decimal? AmountReceived { get; set; }

    public string PurchaseOrderNo { get; set; }

    public DateTime? PurchaseOrderDate { get; set; }

    public string Department { get; set; }

    public string UDF1 { get; set; }

    public string UDF2 { get; set; }

    public string UDF3 { get; set; }

    public string UDF4 { get; set; }

    public string UDF5 { get; set; }

    public string Location { get; set; }

    public short? GSTRateID { get; set; }

    public string FAYear { get; set; }

    public decimal? Cess { get; set; }

    public decimal? CESSAmtount { get; set; }

    public decimal? CESSDiscount { get; set; }

    public decimal? TransportAmount { get; set; }

    public decimal? TransportPerBox { get; set; }

    public decimal? RateInclusive { get; set; }

    public string DeliveryLocation { get; set; }

    public string ContactPersonNo { get; set; }

    public string InboundNo { get; set; }

    public decimal? ServiceCharge { get; set; }

    public string LUTNo { get; set; }

    public string Eway { get; set; }

    public decimal? AdminCharges { get; set; }

    public string BrCode { get; set; }

    public string BrName { get; set; }

    public string ItemCategory { get; set; }

    public string LRNo { get; set; }

    public string Parcel { get; set; }

    public string InvCC { get; set; }

    public string AdvancePaymentRefNo { get; set; }

    public decimal? AdvancePaymentAmount { get; set; }

    public decimal? TransportGST { get; set; }

    public int? ModifiedbyUserID { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

}
