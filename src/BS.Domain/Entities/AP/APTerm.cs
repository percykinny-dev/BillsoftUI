namespace BS.Domain.Entities.AP;

public class APTerm
{
    public int ID { get; set; }

    public byte StatusID { get; set; }

    public int CompanyID { get; set; }

    public string Code { get; set; }

    public string Title { get; set; }

    public int TermDays { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime DateModified { get; set; }
}
