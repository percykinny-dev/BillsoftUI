namespace BS.Infrastructure.DBContext;

public partial class BillsoftDBContext : DbContext
{
    public BillsoftDBContext(DbContextOptions<BillsoftDBContext> options)
        : base(options)
    {
    }

    #region system sys db tables

    //public virtual DbSet<SysUser> SysUsers { get; set; }

    //public virtual DbSet<SysMenu> SysMenus { get; set; }

    //public virtual DbSet<SysLog> SysLogs { get; set; }

    public virtual DbSet<SysResourceText> SysResourceTexts { get; set; }

    public virtual DbSet<SYSNotes> SysNotes { get; set; }

    public virtual DbSet<SYSUOMType> SysUOMTypes { get; set; }

    public virtual DbSet<SYSGSTRateType> SysGSTRateTypes { get; set; }


    #endregion

    #region account receivables ar db tables

    public virtual DbSet<ARCustomer> ARCustomers { get; set; }

    public virtual DbSet<ARCustomerAddress> ARCustomerAddresses { get; set; }
    
    public virtual DbSet<ARCustomerContact> ARCustomerContacts { get; set; }

    public virtual DbSet<ARPayment> ARPayments { get; set; }

    public virtual DbSet<ARPaymentDetail> ARPaymentDetails { get; set; }

    public virtual DbSet<ARInvoice> ARInvoices { get; set; }

    //public virtual DbSet<ARInvoiceType> ARInvoiceTypes { get; set; }

    public virtual DbSet<ARInvoiceDetail> ARInvoiceDetails { get; set; }

    public virtual DbSet<ARTerm> ARTerms { get; set; }

    public virtual DbSet<ARChallan> ARChallans { get; set; }

    public virtual DbSet<ARChallanDetail> ARChallanDetails { get; set; }

    public virtual DbSet<ARProformaInvoice> ARProformaInvoices { get; set; }

    public virtual DbSet<ARProformaInvoiceDetail> ARProformaInvoiceDetails { get; set; }

    public virtual DbSet<ARQuotation> ARQuotations { get; set; }

    public virtual DbSet<ARQuotationDetail> ARQuotationDetails { get; set; }

    #endregion

    #region account payable ap db tables

    public virtual DbSet<APVendor> APVendors { get; set; }

    //public virtual DbSet<APVendorContact> APVendorContacts { get; set; }

    //public virtual DbSet<APPayment> APPayments { get; set; }

    //public virtual DbSet<APPaymentDetail> APPaymentDetails { get; set; }

    //public virtual DbSet<APInvoice> APInvoices { get; set; }

    //public virtual DbSet<APInvoiceDetail> APInvoiceDetails { get; set; }

    public virtual DbSet<APTerm> APTerms { get; set; }

    #endregion
}
