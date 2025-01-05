namespace BS.Domain.Entities.AP;

public class APInvoice
{
    public int ID { get; set; }

    public string TypeID { get; set; }

    public byte StatusID { get; set; }

    public int CompanyID { get; set; }

    public string InvoiceNo { get; set; }

    public DateTime? InvoiceDate { get; set; }

    public DateTime? InvoiceDueDate { get; set; }

    public int? PurchaseOrderID { get; set; }

    [ForeignKey("VendorID")]
    public APVendor Vendor { get; set; }

    public int VendorID { get; set; }

    public string BillTo { get; set; }

    public string BTAddress1 { get; set; }

    public string BTAddress2 { get; set; }

    public string BTAddress3 { get; set; }

    public string BTCity { get; set; }

    public string BTState { get; set; }

    public string BTZipCode { get; set; }

    public string BTCountry { get; set; }

    public string ShipTo { get; set; }

    public string STAddress1 { get; set; }

    public string STAddress2 { get; set; }

    public string STAddress3 { get; set; }

    public string STCity { get; set; }

    public string STState { get; set; }

    public string STZipCode { get; set; }

    public string STCountry { get; set; }

    public string STPhone1 { get; set; }

    public int? TermsID { get; set; }

    public int? InvoiceParentID { get; set; }

    public string Comment { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateModified { get; set; }
}
