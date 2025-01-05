namespace BS.Infrastructure.DBContext;

public partial class BillsoftDBContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        BuildSysDomainEntities(modelBuilder);

        BuildARDomainEntities(modelBuilder);

        BuildAPDomainEntities(modelBuilder);
    }

    private static void BuildSysDomainEntities(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SYSUser>(entity =>
        {
            entity.ToTable("SYS_User");
        });

        modelBuilder.Entity<SYSCompany>(entity =>
        {
            entity.ToTable("SYS_Company");
        });

        modelBuilder.Entity<SYSGSTRates>(entity =>
        {
            entity.ToTable("SYS_GSTRates");
        });

        modelBuilder.Entity<SYSGSTRateType>(entity =>
        {
            entity.ToTable("SYS_GSTRateType");
        });

        modelBuilder.Entity<SYSItem>(entity =>
        {
            entity.ToTable("SYS_Item");
        });

        modelBuilder.Entity<SYSItemCategory>(entity =>
        {
            entity.ToTable("SYS_ItemCategory");
        });

        modelBuilder.Entity<SYSState>(entity =>
        {
            entity.ToTable("SYS_State");
        });

        modelBuilder.Entity<SYSTerm>(entity =>
        {
            entity.ToTable("SYS_Term");
        });

        modelBuilder.Entity<SYSUOMType>(entity =>
        {
            entity.ToTable("SYS_UOMType");
        });

        //modelBuilder.Entity<SysMenu>(entity =>
        //{
        //    entity.ToTable("SYSMenu");
        //});

        //modelBuilder.Entity<SysLog>(entity =>
        //{
        //    entity.ToTable("SYSLog");
        //});

        //modelBuilder.Entity<SysResourceText>(entity =>
        //{
        //    entity.ToTable("SYSResourceText");
        //});

        modelBuilder.Entity<SYSNotes>(entity =>
        {
            entity.ToTable("SYS_Notes")
            .HasOne(s => s.User)
            .WithMany()
            .HasForeignKey(s => s.CreatedByUserID);
         });
    }

    private static void BuildARDomainEntities(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ARChallan>(entity =>
        {
            entity.ToTable("AR_Challan");
        });

        modelBuilder.Entity<ARChallanDetail>(entity =>
        {
            entity.ToTable("AR_ChallanDetail");
        });

        modelBuilder.Entity<ARCustomer>(entity =>
        {
            entity.ToTable("AR_Customer");
            
            entity
            .Property(s => s.StatusID)
            .HasDefaultValue(true);

            entity
            .Property(s => s.DateCreated)
            .HasComputedColumnSql();

            entity
            .Property(s => s.DateModified)
            .HasComputedColumnSql();
        });

        modelBuilder.Entity<ARCustomerAddress>(entity =>
        {
            entity.ToTable("AR_CustomerAddress");

            entity
           .Property(s => s.StatusID)
           .HasDefaultValue(true);

            entity
            .Property(s => s.DateCreated)
            .HasComputedColumnSql();

            entity
            .Property(s => s.DateModified)
            .HasComputedColumnSql();
        });

        modelBuilder.Entity<ARCustomerContact>(entity =>
        {
            entity.ToTable("AR_CustomerContact");
        });

        modelBuilder.Entity<ARCustomerRateCard>(entity =>
        {
            entity.ToTable("AR_CustomerRateCard");
        });

        modelBuilder.Entity<ARCustomerShippingAddress>(entity =>
        {
            entity.ToTable("AR_CustomerShippingAddress");
        });

        //table needs to be added in the database
        //modelBuilder.Entity<ARInvoiceType>(entity =>
        //{
        //    entity.ToTable("AR_InvoiceType");
        //});

        modelBuilder.Entity<ARInvoice>(entity =>
        {
            entity.ToTable("AR_Invoice");
        });

        modelBuilder.Entity<ARInvoiceDetail>(entity =>
        {
            entity.ToTable("AR_InvoiceDetail");
        });

        modelBuilder.Entity<ARPayment>(entity =>
        {
            entity.ToTable("AR_Payment");
        });

        modelBuilder.Entity<ARPaymentDetail>(entity =>
        {
            entity.ToTable("ARPaymentDetail");
        });

        modelBuilder.Entity<ARProformaInvoice>(entity =>
        {
            entity.ToTable("AR_ProformaInvoice");
        });

        modelBuilder.Entity<ARProformaInvoiceDetail>(entity =>
        {
            entity.ToTable("AR_ProformaInvoiceDetail");
        });

        modelBuilder.Entity<ARQuotation>(entity =>
        {
            entity.ToTable("AR_Quotation");
        });

        modelBuilder.Entity<ARQuotationDetail>(entity =>
        {
            entity.ToTable("AR_QuotationDetail");
        });

        modelBuilder.Entity<ARTerm>(entity =>
        {
            entity.ToTable("AR_Term");
        });
    }

    private static void BuildAPDomainEntities(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<APVendor>(entity =>
        {
            entity.ToTable("AP_Vendor")
            .HasKey(x => x.VendorID);

        });

        /*
        modelBuilder.Entity<APVendorContact>(entity =>
        {
            entity.ToTable("AP_VendorContact");
        });

        modelBuilder.Entity<APInvoice>(entity =>
        {
            entity.ToTable("AP_Invoice");
        });

        modelBuilder.Entity<APInvoiceDetail>(entity =>
        {
            entity.ToTable("AP_InvoiceDetail");
        });

        modelBuilder.Entity<APPayment>(entity =>
        {
            entity.ToTable("AP_Payment");
        });

        modelBuilder.Entity<APPaymentDetail>(entity =>
        {
            entity.ToTable("AP_PaymentDetail");
        });

        modelBuilder.Entity<APTerm>(entity =>
        {
            entity.ToTable("AP_Term");
        });
        */
    }
}
