namespace BS.Domain.Entities.AR;

public class ARPayment
{
    [Key]
    public int PaymentID { get; set; }

    public string PaymentNo { get; set; }

    public DateTime PaymentDate { get; set; }

    public byte PaymentMethod { get; set; }

    public byte StatusID { get; set; }

    public decimal? AmountReceived { get; set; }

    public int CustomerID { get; set; }

    public string ReferenceNumber { get; set; }

    public DateTime? TransactionDate { get; set; }

    public string Notes { get; set; }

    public string UDF1 { get; set; }

    public string UDF2 { get; set; }

    public string UDF3 { get; set; }

    public string UDF4 { get; set; }

    public string UDF5 { get; set; }

    public int? ModifiedbyUserID { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

}
