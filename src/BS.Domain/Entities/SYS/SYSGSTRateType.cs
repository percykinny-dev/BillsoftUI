namespace BS.Domain.Entities.SYS;

public class SYSGSTRateType
{

    [Key]
    public short GSTRateTypeID { get; set; }

    public string Type { get; set; }

    public string RateDescription { get; set; }

    public decimal CGST_Rate { get; set; }

    public decimal SGST_Rate { get; set; }

    public decimal IGST_Rate { get; set; }

    public decimal UTGST_Rate { get; set; }

    public decimal Cess_Rate { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

        
    }
