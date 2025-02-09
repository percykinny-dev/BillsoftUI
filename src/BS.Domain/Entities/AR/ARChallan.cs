namespace BS.Domain.Entities.AR;

public class ARChallan
{
    [Key]
    public int ChallanID { get; set; }
    public string ChallanNo { get; set; }
    public DateTime? ChallanDate { get; set; }
    public byte StatusID { get; set; }
    public int CompanyID { get; set; }
    public int CustomerID { get; set; }
    public string Currency { get; set; }
    public int? BillAddressID { get; set; }
    public int? ShipAddressID { get; set; }
    public decimal NetAmount { get; set; }
    public decimal CGSTAmount { get; set; }
    public decimal SGSTAmount { get; set; }
    public decimal IGSTAmount { get; set; }
    public decimal GSTAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public bool IsDeleted { get; set; }
    public decimal Discount { get; set; }
    public decimal DiscountAmount { get; set; }
    public string ContactPerson { get; set; }
    public string Description { get; set; }
    public short PaymentTermID { get; set; }
    public decimal AmountReceived { get; set; }
    public string PurchaseOrderNo { get; set; }
    public DateTime? PurchaseOrderDate { get; set; }
    public string Department { get; set; }
    public short GSTRateID { get; set; }
    public string FAYear { get; set; }
    public string DeliveryPerson { get; set; }
    public bool ItemLessChallan { get; set; }
    public string UDF1 { get; set; }
    public string UDF2 { get; set; }
    public string UDF3 { get; set; }
    public string UDF4 { get; set; }
    public string UDF5 { get; set; }
    public DateTime? DateCreated { get; set; }
    public DateTime? DateModified { get; set; }

}
