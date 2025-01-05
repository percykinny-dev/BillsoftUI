namespace BS.Application.Contracts.AR;

public interface IARChallanService
{
    //challans
    public Task<IEnumerable<ARChallanVM>> GetChallansList(int companyId, QueryFilter queryFilter, string[] allowedStatuses);

    public Task<ARChallanDetailVM> GetChallanDetailVM(int companyId, int challanId);

    public Task<ARChallan> GetChallan(int companyId, int challanId);

    public Task<ResultVM> SaveChallan(ARChallan challan);

    public Task<ResultVM> DeleteChallan(int companyId, int challanId);

    public Task<ARInvoice> ConvertChallanToInvoice(int companyId, int challanId);

    public Task<ResultVM> SaveChallan(ARChallan challan, IEnumerable<ARChallanDetail> challanItems);

    //challan details
    public Task<ARChallanDetail> GetChallanDetail(int companyId, int challanId, int challanDetailId);

    public Task<ResultVM> SaveChallanDetail(ARChallanDetail challanDetail);

    public Task<ResultVM> DeleteChallanDetail(int companyId, int challanId, int challanDetailId);

    public Task<ARSharedListsVM> GetSharedListsVM(int companyId, int customerId);
}
