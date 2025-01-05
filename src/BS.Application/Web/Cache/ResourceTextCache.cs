namespace BS.Application.Web.Cache;

public class ResourceTextCache
{
    private readonly ISysLogRepository logRepository;
    private readonly IAppCache lazyCache;
    private readonly ISysResourceTextRepository resourceTextRepository;

    private string cached_resourcetext_by_companyid = "resource_text_for_company_{0}";

    public ResourceTextCache(
        ISysLogRepository logRepository,
        IAppCache lazyCache,
        ISysResourceTextRepository resourceTextRepository)
    {
        this.logRepository = logRepository;
        this.lazyCache = lazyCache;
        this.resourceTextRepository = resourceTextRepository;
    }


    public async Task<IEnumerable<SysResourceText>> GetResourcesForProfile(int loggedinUserId, string menuCode)
    {
        try
        {
            //var cacheKey = string.Format(cached_resourcetext_by_companyid, CompanyID);
            //var companyResources =
            //    await lazyCache.GetOrAddAsync(cacheKey, async () => await GetResourceTextFromDB(CompanyID), DateTimeOffset.Now.AddMinutes(5), ExpirationMode.ImmediateEviction);
            //return companyResources.Where(s => s.MenuCode == menuCode || s.MenuCode == string.Empty);
            return null;
        }
        catch (Exception ex)
        {
            var _ = new SysLog()
            {
                CreatedOn = DateTime.Now,
                CreatedBy = loggedinUserId,
                Message = ex.Message,
                Category = "ResourceTextCache",
            };
            //logRepository.Log(_);
            throw;
        }
    }

    #region private methods

    private async Task<IEnumerable<SysResourceText>> GetResourceTextFromDB(int CompanyID)
    {
        //var resources = await resourceTextRepository.GetResourceTexts(CompanyID);
        return null;
    }
    #endregion
}
