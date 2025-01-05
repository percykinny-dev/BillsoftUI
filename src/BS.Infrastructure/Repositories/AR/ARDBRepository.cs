namespace BS.Infrastructure.Repositories.AR;

public partial class ARDBRepository : DBRepositoryBase, IARDBRepository
{
    public ARDBRepository(IConfiguration configuration) : base(configuration)
    {
        
    }

    

    //MISC
    public Task<ARInvoiceDetailVM> CopySODetailsToInvoiceDetails(int companyId, int invoiceId, int salesOrderId)
    {
        throw new NotImplementedException();
    }

    public async Task<ARSharedListsVM> GetSharedListsVM(int companyId, int customerId)
    {
        //var data = new ARSharedListsVM();

        var sql = @"[dbo].[pAR_GetListItems]";
        using (var connection = new SqlConnection(connectionString))
        {
            try
            {
                using (var query = await connection.QueryMultipleAsync(sql,
                    new { CompanyID = companyId, CustomerId = customerId },
                    commandType: System.Data.CommandType.StoredProcedure))
                {
                    var customers = await query.ReadAsync<ARCustomerListVM>();

                    var items = await query.ReadAsync<ARItemListVM>();

                    var uomTypes = await query.ReadAsync<SYSUOMType>();

                    var gstRateTypes = await query.ReadAsync<SYSGSTRateType>();


                    //data.Customers = customers;
                    //data.Items = items;
                    //data.UOMTypes = uomTypes;
                    //data.GSTRateTypes = gstRateTypes;

                    var data = new ARSharedListsVM
                    {
                        Customers = customers,
                        Items = items,
                        UOMTypes = uomTypes,
                        GSTRateTypes = gstRateTypes
                    };


                    return data;

                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }
        
    }
}
