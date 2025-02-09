
using System.ComponentModel.DataAnnotations.Schema;

namespace BS.Application.ViewModels.AR;

public class ARChallanVM
{

    public int ChallanID { get; set; }
    public string ChallanNo { get; set; }
    public string CustomerName { get; set; }
    public DateTime? ChallanDate { get; set; }
    public decimal NetAmount { get; set; }
    public decimal CGSTAmount { get; set; }
    public decimal SGSTAmount { get; set; }
    public decimal IGSTAmount { get; set; }
    public decimal GSTAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public string Currency { get; set; }

}

public class ARChallanDetailVM
{
    //public ARChallan Challan { get; set; }

    public ARChallanDataVM Challan { get; set; }
    //public List<ARChallanDetail> ChallanItems { get; set; }  
    public List<ARChallanItemsVM> ChallanItems { get; set; }

    public ARSharedListsVM SharedLists { get; set; }
}

public class ARChallanDataVM
{
    public int ChallanID { get; set; }
    public string ChallanNo { get; set; }
    public DateTime? ChallanDate { get; set; }
    public byte StatusID { get; set; }
    public int CompanyID { get; set; }
    public int CustomerID { get; set; }
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
    //public List<ARChallanDetail> ChallanItems { get; set; }

    public string BillContactName { get; set; }

    public string BillAddress1 { get; set; }

    public string BillAddress2 { get; set; }

    public string BillCountryCode { get; set; }

    public string BillCity { get; set; }

    public string BillState { get; set; }

    public string BillZipcode { get; set; }

    public string BillEmailAddress { get; set; }

    public string BillWorkPhone { get; set; }

    public string BillMobilePhone { get; set; }

    public string BillWhatsAppPhone { get; set; }

    public string BillGSTNo { get; set; }

    public string ShipContactName { get; set; }

    public string ShipAddress1 { get; set; }

    public string ShipAddress2 { get; set; }

    public string ShipCountryCode { get; set; }

    public string ShipCity { get; set; }

    public string ShipState { get; set; }

    public string ShipZipcode { get; set; }

    public string ShipEmailAddress { get; set; }

    public string ShipWorkPhone { get; set; }

    public string ShipMobilePhone { get; set; }

    public string ShipWhatsAppPhone { get; set; }

    public string ShipGSTNo { get; set; }

    public string FAYear { get; set; }

    public string Description { get; set; }

    public DateTime? PurchaseOrderDate { get; set; }
    public string PurchaseOrderNo { get; set; }

    public string Currency { get; set; }
}

public class ARChallanItemsVM
{
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
    public decimal CGST { get; set; }
    public decimal CGSTAmount { get; set; }
    public decimal SGST { get; set; }
    public decimal SGSTAmount { get; set; }
    public decimal IGST { get; set; }
    public decimal IGSTAmount { get; set; }
    public decimal Total { get; set; }
    public int? InvoiceID { get; set; }
    public string FAYear { get; set; }
    public decimal Cess { get; set; }
    public decimal CESSAmount { get; set; }
    public decimal BaseRate { get; set; }
    public string ItemSize { get; set; }
    public decimal Box { get; set; }
    public decimal RateDiscount { get; set; }

    public string ItemCode { get; set; }

    public string ItemName { get; set; }

    public string UOMType { get; set; }

    public string GSTType { get; set; }

    public decimal CGST_Rate { get; set; }

    public decimal SGST_Rate { get; set; }

    public decimal IGST_Rate { get; set; }

}