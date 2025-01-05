namespace BS.Application.Services.AP;

public class APVendorService : IAPVendorService
{
    readonly IAPVendorRepository vendorRepository;

    public APVendorService()
    {
        
    }

    public APVendorService(IAPVendorRepository vendorRepository)
    {
        this.vendorRepository = vendorRepository;
    }
}
