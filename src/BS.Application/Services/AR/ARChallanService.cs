using BS.Domain.Entities.AR;

namespace BS.Application.Services.AR;

public class ARChallanService : IARChallanService
{
    readonly IARChallanRepository challanRepository;
    readonly IARChallanDetailRepository challanDetailRepository;
    readonly IARDBRepository arDBRepository;

    public ARChallanService(
        IARChallanRepository challanRepository,
        IARChallanDetailRepository challanDetailRepository,
        IARDBRepository arDBRepository)
    {
        Guard.AgainstNull(challanRepository, nameof(challanRepository));
        Guard.AgainstNull(challanDetailRepository, nameof(challanDetailRepository));
        Guard.AgainstNull(arDBRepository, nameof(arDBRepository));

        this.challanRepository = challanRepository;
        this.challanDetailRepository = challanDetailRepository;
        this.arDBRepository = arDBRepository;
    }

    //challans
    public async Task<IEnumerable<ARChallanVM>> GetChallansList(int companyId, QueryFilter queryFilter, string[] allowedStatuses)
    {
        var (challans, totalCount) = await arDBRepository.GetChallansList(companyId, queryFilter, allowedStatuses);
        queryFilter.RecordCount = totalCount;
        return challans;
    }
    public async Task<ARSharedListsVM> GetSharedListsVM(int companyId, int customerId)
    {
        var sharedLists = await arDBRepository.GetSharedListsVM(companyId, customerId);
        return sharedLists;
    }

    public async Task<ARChallanDetailVM> GetChallanDetailVM(int companyId, int challanId)
    {
        var data = await arDBRepository.GetChallanDetailVM(companyId, challanId);
        return data;
    }

    public async Task<ARChallan> GetChallan(int companyId, int challanId)
    {
        var data = await challanRepository.Get(challanId);
        if(data == null || data.CompanyID != companyId) { throw new BSApplicationException("invalid challan id"); }
        return data;
    }

    public async Task<ResultVM> SaveChallan(ARChallan challan, IEnumerable<ARChallanDetail> challanItems)
    {
        int challanId = await arDBRepository.SaveChallan(challan, challanItems);
        challan.ChallanID = challanId;
        if (challanId > 0) {
            return new ResultVM() { IsSuccess = true, Messages = new string[] { $"new challan {challanId} added successfully" } };
        }
        else
        {
            return new ResultVM() { IsSuccess = false, Messages = new string[] { $"some error occured when saving the challan" } };
        }
        
    }

    public async Task<ResultVM> SaveChallan(ARChallan challan)
    {
        if (challan.ChallanID == 0)
        {
            await challanRepository.AddAsync(challan);
            return new ResultVM() { IsSuccess = true, Messages = new string[] { "new challan added successfully" } };
        }

        await challanRepository.UpdateAsync(challan);
        return new ResultVM() { IsSuccess = true, Messages = new string[] { "challan updated successfully" } };
    }

    public async Task<ResultVM> DeleteChallan(int companyId, int challanId)
    {
        await arDBRepository.DeleteChallan(companyId, challanId);
        return new ResultVM() { IsSuccess = true, Messages = new string[] { "challan deleted successfully" } };
    }

    public async Task<ARInvoice> ConvertChallanToInvoice(int companyId, int challanId)
    {
        var data = await arDBRepository.ConvertChallanToInvoice(companyId, challanId);
        return data;
    }

    //challan details
    public async Task<ARChallanDetail> GetChallanDetail(int companyId, int challanId, int challanDetailId)
    {
        var data = await challanDetailRepository.Get(challanDetailId);
        if (data == null || data.ChallanID != challanId) throw new BSApplicationException("invalid challan detail id");

        return data;
    }

    public async Task<ResultVM> SaveChallanDetail(ARChallanDetail challanDetail)
    {
        if (challanDetail.ChallanDetailID == 0)
        {
            await challanDetailRepository.AddAsync(challanDetail);
            return new ResultVM() { IsSuccess = true, Messages = new string[] { "new challan detail added successfully" } };
        }

        await challanDetailRepository.UpdateAsync(challanDetail);
        return new ResultVM() { IsSuccess = true, Messages = new string[] { "challan detail updated successfully" } };
    }

    public async Task<ResultVM> DeleteChallanDetail(int companyId, int challanId, int challanDetailId)
    {
        var data = await challanDetailRepository.Get(challanDetailId);
        if (data == null || data.ChallanID != challanId) throw new BSApplicationException("invalid challan detail id");

        await challanDetailRepository.DeleteAsync(data);
        return new ResultVM() { IsSuccess = true, Messages = new string[] { "challan detail deleted successfully" } };
    }
}
