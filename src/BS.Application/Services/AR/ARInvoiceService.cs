namespace BS.Application.Services.AR;

public class ARInvoiceService  : IARInvoiceService
{
    readonly IARInvoiceRepository invoiceRepository;
    readonly IARInvoiceDetailRepository invoiceDetailRepository;
    readonly IARDBRepository ardbRepository;

    public ARInvoiceService(
        IARInvoiceRepository invoiceRepository,
        IARInvoiceDetailRepository invoiceDetailRepository,
        IARDBRepository ardbRepository)
    {
        this.invoiceRepository = invoiceRepository;
        this.invoiceDetailRepository = invoiceDetailRepository;
        this.ardbRepository = ardbRepository;
    }

    //INVOICE
    public async Task<IEnumerable<ARInvoiceListVM>> GetInvoices(int companyId, QueryFilter queryFilter, string[] allowedStatuses, string typeId)
    {
        var data = await invoiceRepository.GetInvoices(companyId, queryFilter, allowedStatuses, typeId);
        return data;
    }

    public async Task<IEnumerable<ARInvoiceNew>> GetNewInvoices(int companyId)
    {
        var data = await ardbRepository.GetNewARInvoices(companyId);
        return data;
    }

    public async Task<ARAddInvoiceVM> GetAddInvoiceVM(int companyId)
    {
        return await ardbRepository.GetAddNewInvoiceVM(companyId);
    }

    public async Task<bool> AddInvoice(ARInvoice invoice)
    {
        invoice.DateCreated = invoice.DateModified = DateTime.Now;
        await invoiceRepository.AddAsync(invoice);
        return true;
    }

    public async Task<ResultVM> SaveInvoice(ARInvoice invoice)
    {
        var _ = await invoiceRepository.Get(invoice.InvoiceID);

        if (_ == null || _.CompanyID != invoice.CompanyID)
            throw new BSApplicationException("invalid invoice id");

        //if (_.StatusID != SY_STATUS.New.ToString())
        //    throw new MasException("cannot save invoice as status not equal to new");

        //_.InvoiceCode = invoice.InvoiceCode;
        //_.InvoiceDate = invoice.InvoiceDate;
        //_.TermsID = invoice.TermsID;
        //_.InvoiceParentID = invoice.InvoiceParentID.HasValue ? invoice.InvoiceParentID.Value : null;
        //_.SalesOrderID = invoice.SalesOrderID.HasValue ? invoice.SalesOrderID.Value : null;

        //_.BillTo = invoice.BillTo;
        //_.BTAddress1 = invoice.BTAddress1;
        //_.BTAddress2 = invoice.BTAddress2;
        //_.BTAddress3 = invoice.BTAddress3;
        //_.BTCity = invoice.BTCity;
        //_.BTState = invoice.BTState;
        //_.BTZipCode = invoice.BTZipCode;
        //_.BTCountry = invoice.BTCountry;

        //_.ShipTo = invoice.ShipTo;
        //_.STAddress1 = invoice.STAddress1;
        //_.STAddress2 = invoice.STAddress2;
        //_.STAddress3 = invoice.STAddress3;
        //_.STCity = invoice.STCity;
        //_.STState = invoice.STState;
        //_.STZipCode = invoice.STZipCode;
        //_.STCountry = invoice.STCountry;
        //_.STPhone1 = invoice.STPhone1;

        //_.HComment = invoice.HComment;
        //_.DeliveryTypeID = invoice.DeliveryTypeID;

        //_.DiscountAmount = invoice.DiscountAmount ?? 0.00m;
        //_.FreightAmount = invoice.FreightAmount ?? 0.00m;
        //_.TaxAmount = invoice.TaxAmount ?? 0.00m;

        //if (_.SubtotalAmount == null)
        //    _.SubtotalAmount = 0;

        var _invoiceDetails = await invoiceRepository.GetInvoiceDetails(invoice.CompanyID, invoice.InvoiceID);
        //invoice.SubtotalAmount = _.SubtotalAmount = _invoiceDetails.Sum(s => s.ExtensionAmount) ?? 0.00m;

        //var total = _.SubtotalAmount - invoice.DiscountAmount + invoice.FreightAmount + invoice.TaxAmount;
        //_.TotalAmount = _.BalanceAmount = total;
        _.TotalAmount = 0;

        _.DateModified = DateTime.Now;
        await invoiceRepository.UpdateAsync(_);
        return new ResultVM() { IsSuccess = true, Messages = new string[] { "invoice data updated successfully" } };
    }

    public async Task<ResultVM> DeleteInvoice(int companyId, int invoiceId)
    {
        await ardbRepository.DeleteARInvoice(companyId, invoiceId);
        return new ResultVM() { IsSuccess = true, Messages = new string[] { "invoice deleted successfully" } };
    }

    public async Task<ARInvoiceDetail> GetInvoiceDetail(int companyId, int invoiceId, int invoiceDetailId)
    {
        var data = await invoiceDetailRepository.Get(invoiceDetailId);

        if (data == null || data.CompanyID != companyId || data.InvoiceID != invoiceId)
            throw new BSApplicationException("invalid invoice detail id");

        return data;
    }


    //INVOICE DETAIL
    public async Task<ARInvoiceDetailVM> GetInvoiceDetailVM(int companyId, int invoiceId)
    {
        var data = await ardbRepository.GetInvoiceDetailVM(companyId, invoiceId);
        return data;
    }
    public async Task<ARInvoiceDetailVM> SaveInvoiceDetail(ARInvoiceDetail invoiceDetail)
    {
        var data = await ardbRepository.SaveARInvoiceDetail(invoiceDetail);
        //return new ResultVM() { IsSuccess = true, Messages = new string[] { "invoice detail saved successfully" } };
        return data;
    }

    public async Task<ARInvoiceDetailVM> DeleteInvoiceDetail(int companyId, int invoiceId, int invoiceDetailId)
    {
        return await ardbRepository.DeleteARInvoiceDetail(companyId, invoiceId, invoiceDetailId);
    }
    
}












