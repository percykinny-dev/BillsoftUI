namespace BS.Domain.Entities.AR;

public class ARChallanDetail
{
    [Key]
    public int ChallanDetailID { get; set; }
    public int ChallanID { get; set; }
    public byte StatusID { get; set; }
    public int ItemID { get; set; }
    public string HSNCode { get; set; }
    public decimal Quantity { get; set; }
    public string Uom { get; set; }
    public decimal Rate { get; set; }
    public decimal Amount { get; set; }
    public decimal Discount { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal TaxableValue { get; set; }
    public int GSTRateID { get; set; }

    [NotMapped]
    public int GSTRateTypeID { get; set; }
    public decimal CGST { get; set; }
    public decimal CGSTAmount { get; set; }
    public decimal SGST { get; set; }
    public decimal SGSTAmount { get; set; }
    public decimal IGST { get; set; }
    public decimal IGSTAmount { get; set; }
    public decimal Total { get; set; }

    public int? InvoiceID { get; set; }
    public string UDF1 { get; set; }
    public string UDF2 { get; set; }
    public string UDF3 { get; set; }
    public string UDF4 { get; set; }
    public string UDF5 { get; set; }
    public string Description { get; set; }
    public string FAYear { get; set; }
    public decimal Cess { get; set; }
    public decimal CESSAmount { get; set; }
    public decimal BaseRate { get; set; }
    public string ItemSize { get; set; }
    public decimal Box { get; set; }
    public decimal RateDiscount { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateModified { get; set; }


}