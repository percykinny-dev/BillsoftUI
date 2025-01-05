namespace BS.Domain.Entities.ERP;

public class ERPPurchaseOrder
{
    [Key]
    public int PurchaseOrderID { get; set; }

    public string PONumber { get; set; }

    public DateTime PurchaseOrderDate { get; set; }

    public byte StatusID { get; set; }

    public bool? IsReverseCharge { get; set; }

    public decimal? NetAmount { get; set; }

    public int? VendorID { get; set; }

    public short? GSTRateID { get; set; }

    public decimal? CGSTAmount { get; set; }

    public decimal? SGSTAmount { get; set; }

    public decimal? IGSTAmount { get; set; }

    public decimal? GSTAmount { get; set; }

    public decimal? TotalAmount { get; set; }

    public decimal? Discount { get; set; }

    public decimal? DiscountAmount { get; set; }

    public decimal? Cess { get; set; }

    public decimal? CESSAmtount { get; set; }

    public decimal? CESSDiscount { get; set; }

    public bool? IsDeleted { get; set; }

    public string Department { get; set; }

    public byte? PrintOption { get; set; }

    public string Description { get; set; }

    public short? PaymentTermID { get; set; }

    public string UDF1 { get; set; }

    public string UDF2 { get; set; }

    public string UDF3 { get; set; }

    public string UDF4 { get; set; }

    public string UDF5 { get; set; }

    public int? ModifiedbyUserID { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

}