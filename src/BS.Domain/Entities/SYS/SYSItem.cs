namespace BS.Domain.Entities.SYS;

public class SYSItem
{
    [Key]
    public int ItemID { get; set; }

    public string ItemCode { get; set; }

    public string ItemName { get; set; }

    public int? StatusID { get; set; }

    public string HSNNo { get; set; }

    public decimal? Rate { get; set; }

    public string Uom { get; set; }

    public decimal? CGST { get; set; }

    public decimal? SGST { get; set; }

    public decimal? IGST { get; set; }

    public string UDF1 { get; set; }

    public string UDF2 { get; set; }

    public string UDF3 { get; set; }

    public string UDF4 { get; set; }

    public string UDF5 { get; set; }

    public string DesignPL { get; set; }

    public string SizePL { get; set; }

    public string ItemSize { get; set; }

    public decimal? Rolls { get; set; }

    public decimal? Cess { get; set; }

    public decimal? BaseRateDVM { get; set; }

    public decimal? BottleQtyDVM { get; set; }

    public decimal? StockQty { get; set; }

    public int? CategoryID { get; set; }

    public int? ModifiedbyUserID { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

}
