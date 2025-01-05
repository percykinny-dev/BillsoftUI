namespace BS.Domain.Entities.SYS;

public class SYSCompany
{
    [Key]
    public int CompanyID { get; set; }

    public string TypeID { get; set; }

    public byte StatusID { get; set; }

    public string Code { get; set; }

    public string BillSoftLicense { get; set; }

    public int? PrimaryUserID { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int? AdminCompany { get; set; }

    public int? ManagerID { get; set; }

    public string CompanyKey { get; set; }

    public string CompanyName { get; set; }

    public string Address1 { get; set; }

    public string Address2 { get; set; }

    public string Address3 { get; set; }

    public string City { get; set; }

    public string State { get; set; }

    public string ZipCode { get; set; }

    public string CountryCode { get; set; }

    public string Phone1 { get; set; }

    public string Phone2 { get; set; }

    public string DashboardHTML { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

}
