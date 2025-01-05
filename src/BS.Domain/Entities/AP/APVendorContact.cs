namespace BS.Domain.Entities.AP;

public class APVendorContact
{
    public int ID { get; set; }

    public int VendorID { get; set; }

    public int CompanyID { get; set; }

    public string TypeID { get; set; }

    public byte StatusID { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string WorkTitle { get; set; }

    public string Phone1 { get; set; }

    public string Phone2 { get; set; }

    public string Email1 { get; set; }

    public string Email2 { get; set; }

    public string HComment { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime DateModified { get; set; }
}
