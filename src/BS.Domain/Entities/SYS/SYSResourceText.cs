namespace BS.Domain.Entities.SYS;

public class SysResourceText
{
    [Key]
    public int ID { get; set; }

    public string MenuCode { get; set; }

    public string RKey { get; set; }

    public string RValue { get; set; }
}
