using BS.Domain.Entities.AR;

namespace BS.Domain.Entities.SYS;


public class SYSNotes
{
    [Key]
    public int NoteID { get; set; }
    public int EntityID { get; set; }
    public string EntityType { get; set; }
    public string EventType { get; set; }
    public string EventDescription { get; set; }

    public int CreatedByUserID { get; set; }

    public SYSUser User { get; set; }
    
    public DateTime CreatedDate { get; set; }
}

