namespace BS.Application.ViewModels.AR;

public class ARInvoiceNew
{
    [Display(Name = "Invoice ID")]
    public int InvoiceID { get; set; }

    public string Type { get; set; }

    public string Status { get; set; }

    //public int ProfileID { get; set; }

    [Display(Name = "Code")]
    public string InvoiceCode { get; set; }

    [Display(Name = "Invoice Date")]
    public DateTime InvoiceDate { get; set; }

    [Display(Name = "Invoice Due Date")]
    public DateTime InvoiceDueDate { get; set; }

    [Display(Name = "Sales Order ID")]
    public int SalesOrderID { get; set; }

    [Display(Name = "Customer Code")]
    public string CustomerCode { get; set; }

    [Display(Name = "Customer Title")]
    public string CustomerTitle { get; set; }

    [Display(Name = "Bill To")]
    public string BillTo { get; set; }

    [Display(Name = "Ship To")]
    public string ShipTo { get; set; }

    [Display(Name = "Terms")]
    public string Terms { get; set; }

    [Display(Name = "Subtotal Amount")]
    public decimal SubtotalAmount { get; set; }

    [Display(Name = "Discount Amount")]
    public decimal DiscountAmount { get; set; }

    [Display(Name = "Freight Amount")]
    public decimal FreightAmount { get; set; }

    [Display(Name = "Tax Amount")]
    public decimal TaxAmount { get; set; }

    [Display(Name = "Total Amount")]
    public decimal TotalAmount { get; set; }
}
