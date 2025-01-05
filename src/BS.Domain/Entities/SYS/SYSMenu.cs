namespace BS.Domain.Entities.SYS;

public class SysMenu
{
    [Key]
    public int MenuID { get; set; }

    public int? ParentID { get; set; }

    public byte StatusID { get; set; }

    public string Icon { get; set; }

    public string Code { get; set; }

    public string Title { get; set; }

    public string URL { get; set; }

    public int SortOrder { get; set; }
}