namespace BS.Application.ViewModels.AR;

public class ARChallanVM
{
    public int ChallanID { get; set; }

    public string ChallanNo { get; set; }

    public string CustomerName { get; set; }

    public DateTime? ChallanDate { get; set; }

    public decimal NetAmount { get; set; }
    public decimal CGSTAmount { get; set; }
    public decimal SGSTAmount { get; set; }
    public decimal IGSTAmount { get; set; }
    public decimal GSTAmount { get; set; }
    public decimal TotalAmount { get; set; }


}

public class ARChallanDetailVM
{
    public ARChallan Challan { get; set; }

    public List<ARChallanDetail> ChallanItems { get; set; }  

    public ARSharedListsVM SharedLists { get; set; }
}

public class ARChallanDataVM
{
    public ARChallan Challan { get; set; }
    public List<ARChallanDetail> ChallanItems { get; set; }
}