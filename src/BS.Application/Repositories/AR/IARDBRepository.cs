namespace BS.Application.Repositories.AR;

public interface IARDBRepository
{
    //AR CUSTOMERS

    Task<bool> UpdateCustomerDefaultAddress(int customerId, int customerAddressId);

    Task<ARCustomerDetailVM> GetCustomerDetailVM(int companyId, int customerId);

    Task<IEnumerable<ARCustomerInvoiceVM>> GetCustomerInvoices(int companyId, int customerId);


    //AR INVOICES
    Task<ARAddInvoiceVM> GetAddNewInvoiceVM(int companyId);

    Task<IEnumerable<ARInvoiceNew>> GetNewARInvoices(int companyId);

    Task<ARInvoiceDetailVM> GetInvoiceDetailVM(int companyId, int invoiceId);

    Task<bool> DeleteARInvoice(int companyId, int invoiceId);

    Task<ARInvoiceDetailVM> SaveARInvoiceDetail(ARInvoiceDetail invoiceDetail);

    Task<ARInvoiceDetailVM> DeleteARInvoiceDetail(int companyId, int invoiceId, int invoiceDetailId);

    Task<ARInvoiceDetailVM> CopySODetailsToInvoiceDetails(int companyId, int invoiceId, int salesOrderId);

    //AR DEPOSITS
    Task<IEnumerable<ARDepositNew>> GetNewARDeposits(int companyId);

    Task<int> AddNewDeposit(ARDeposit arDeposit);

    Task<ARDepositDetailVM> GetDepositDetailVM(int companyId, int depositId);

    bool SaveDepositDetail(ARDepositDetail depositDetail);

    bool DeleteDepositDetail(int companyId, int depositId, int depositDetailId);
    

    //CHALLANS
    Task<(IEnumerable<ARChallanVM>, int)> GetChallansList(int companyId, ChallanQueryFilter queryFilter, string[] allowedStatuses);

    Task<ARChallanDetailVM> GetChallanDetailVM(int companyId, int challanId);

    Task<bool> DeleteChallan(int companyId, int challanId);

    Task<ARInvoice> ConvertChallanToInvoice(int companyId, int challanId);

    Task<ARSharedListsVM> GetSharedListsVM(int companyId, int customerId);

    Task<int> SaveChallan(ARChallan challan, IEnumerable<ARChallanDetail> challanDetails);
}
