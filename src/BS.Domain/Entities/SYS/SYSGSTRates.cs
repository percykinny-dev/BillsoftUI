namespace BS.Domain.Entities.SYS;

public class SYSGSTRates
{
    [Key]
    public short GSTRateID { get; set; }

    public int ItemID { get; set; }

    public short GSTRateTypeID { get; set; }

    public DateTime? EffectiveFromDate { get; set; }

    public DateTime? EffectiveToDate { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

}