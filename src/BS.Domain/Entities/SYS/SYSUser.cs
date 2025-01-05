namespace BS.Domain.Entities.SYS;

public class SYSUser
{
    [Key]
    public int UserID { get; set; }

    public string UserName { get; set; }

    public string UserPassword { get; set; }

    public int StatusID { get; set; }

    public bool? AdminUser { get; set; }

    public DateTime? PasswordChangedDate { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string EmailAddress { get; set; }

    public string Phone1 { get; set; }

    public DateTime? LastLoggedInDate { get; set; }

    public string UDF1 { get; set; }

    public string UDF2 { get; set; }

    public string UDF3 { get; set; }

    public string UDF4 { get; set; }

    public string UDF5 { get; set; }

    public int? CreatedbyUserID { get; set; }

    public int? ModifiedbyUserID { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

}