namespace BS.Application.ViewModels.AR;

public class ARDepositNew
{
    [Display(Name = "Deposit ID")]
    public int DepositID { get; set; }

    [Display(Name = "Deposit Type")]
    public string DepositType { get; set; }

    [Display(Name = "Bank Code")]
    public string BankCode { get; set; }

    [Display(Name = "Deposit Date")]
    public string DepositDate { get; set; }

    [Display(Name = "Deposit Reference")]
    public string DepositReference { get; set; }

    [Display(Name = "Deposit Total")]
    public decimal DepositTotal { get; set; }

    [Display(Name = "Detail Type")]
    public string DetailType { get; set; }

    [Display(Name = "Detail Title")]
    public string DetailTitle { get; set; }

    [Display(Name = "Customer")]
    public string Customer { get; set; }

    [Display(Name = "Invoice ID")]
    public string InvoiceID { get; set; }

    [Display(Name = "Account Key")]
    public string AccountKey { get; set; }

    [Display(Name = "Deposit Receipt No")]
    public string DepositReceiptNo { get; set; }

    [Display(Name = "Deposit Amount")]
    public decimal DepositAmount { get; set; }

    [Display(Name = "Deposit Discount Amount")]
    public decimal DepositDiscountAmount { get; set; }

    [Display(Name = "Comment")]
    public string DComment { get; set; }
}
