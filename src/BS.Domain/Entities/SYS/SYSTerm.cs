namespace BS.Domain.Entities.SYS;

public class SYSTerm
{
    [Key]
    public int TermID { get; set; }

    public string Term { get; set; }

    public string TermDescription { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

}
