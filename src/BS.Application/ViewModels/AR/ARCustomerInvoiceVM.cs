namespace BS.Application.ViewModels.AR;

public class ARCustomerInvoiceVM
{
    [Display(Name = "Customer Code")]
    public string CustomerCode { get; set; }

    [Display(Name = "Customer Title")]
    public string CustomerTitle { get; set; }

    [Display(Name = "Invoice")]
    public int InvoiceID { get; set; }

    [Display(Name = "TypeID")]
    public string TypeID { get; set; }

    [Display(Name = "StatusID")]
    public string StatusID { get; set; }

    [Display(Name = "Invoice Code")]
    public string InvoiceCode { get; set; }

    [Display(Name = "Invoice Date")]
    public DateTime InvoiceDate { get; set; }

    [Display(Name = "Invoice Due Date")]
    public DateTime InvoiceDueDate { get; set; }

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

    [Display(Name = "Balance Amount")]
    public decimal BalanceAmount { get; set; }
}

