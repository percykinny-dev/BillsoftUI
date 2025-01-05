namespace BS.Domain.Entities.AR;

public class ARCustomerRateCard
{
    [Key]
    public int RateCardID { get; set; }

    public int CustomerID { get; set; }

    public int ItemID { get; set; }

    public decimal Rate { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

}
