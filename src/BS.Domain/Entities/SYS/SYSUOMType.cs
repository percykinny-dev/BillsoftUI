namespace BS.Domain.Entities.SYS;

public class SYSUOMType
{

    [Key]
    public short UOMTypeID { get; set; }
    public string UOMType { get; set; }
    public short SortOrder { get; set; }
    public DateTime? DateCreated { get; set; }
    public DateTime? DateModified { get; set; }

}
