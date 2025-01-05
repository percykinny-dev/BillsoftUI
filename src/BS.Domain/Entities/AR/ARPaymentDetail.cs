namespace BS.Domain.Entities.AR;

public class ARPaymentDetail
{
    [Key]
    public int PaymentDetailID { get; set; }

    public int PaymentID { get; set; }

    public int InvoiceID { get; set; }

    public decimal? Amount { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

}