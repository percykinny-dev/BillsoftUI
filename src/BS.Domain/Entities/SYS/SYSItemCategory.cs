namespace BS.Domain.Entities.SYS;

public class SYSItemCategory
{
    [Key]
    public int CategoryID { get; set; }

    public string CategoryName { get; set; }

    public string CategoryDescription { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

}
