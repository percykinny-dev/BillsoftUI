namespace BS.Domain.Entities.SYS;

public class SYSState
{
    [Key]
    public int StateID { get; set; }

    public string StateName { get; set; }

    public string StateCode { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

}
