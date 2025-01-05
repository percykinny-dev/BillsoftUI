namespace BS.Infrastructure.Repositories.AR;

partial class ARDBRepository
{
    //CUSTOMER
    public Task<ARCustomerDetailVM> GetCustomerDetailVM(int companyId, int customerId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ARCustomerInvoiceVM>> GetCustomerInvoices(int companyId, int customerId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateCustomerDefaultAddress(int customerId, int customerAddressId)
    {
        using (var sqlConnection = new SqlConnection(connectionString))
        {
            var command = sqlConnection.CreateCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = $"[dbo].[pAR_UpdateDefaultCustomerAddress]";

            command.Parameters.Add(new SqlParameter() { ParameterName = "@CustomerID", Value = customerId });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@AddressID", Value = customerAddressId });
         
            using (command)
            {
                try
                {
                    command.Connection.Open();
                    await command.ExecuteNonQueryAsync();
                }
                catch (Exception ex)
                {
                    return false;
                }
                finally
                {
                    command?.Connection?.Close();
                }
            }
        }
        return true;
    }
}
