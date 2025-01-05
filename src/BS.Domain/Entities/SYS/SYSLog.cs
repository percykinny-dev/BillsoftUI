namespace BS.Domain.Entities.SYS;

public class SysLog
{
    [Key]
    public int LogID { get; set; }

    public string LogType { get; set; }

    public string Category { get; set; }

    public string Message { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }
}