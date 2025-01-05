namespace BS.Infrastructure.Repositories.AR;

public class ARChallanDetailRepository : GenericRepository<ARChallanDetail>, IARChallanDetailRepository
{
    public ARChallanDetailRepository(BillsoftDBContext context) : base(context)
    {
    }

   
}
