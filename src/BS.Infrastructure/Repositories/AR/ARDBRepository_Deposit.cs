namespace BS.Infrastructure.Repositories.AR;

public partial class ARDBRepository
{
    //DEPOSIT
    public async Task<int> AddNewDeposit(ARDeposit arDeposit)
    {
        int depositID = 0;
        using (var sqlConnection = new SqlConnection(connectionString))
        {
            var command = sqlConnection.CreateCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "[dbo].[pAR_AddNewDeposit]";

            command.Parameters.Add(new SqlParameter() { ParameterName = "@CompanyID", Value = arDeposit.CompanyID });
            //command.Parameters.Add(new SqlParameter() { ParameterName = "@UserIDCreated", Value = arDeposit.UserIDCreated });
            //command.Parameters.Add(new SqlParameter() { ParameterName = "@TypeID", Value = arDeposit.TypeID });
            //command.Parameters.Add(new SqlParameter() { ParameterName = "@BankID", Value = arDeposit.BankID });
            //command.Parameters.Add(new SqlParameter() { ParameterName = "@Code", Value = arDeposit.Code ?? string.Empty });
            //command.Parameters.Add(new SqlParameter() { ParameterName = "@DepositDate", Value = arDeposit.DepositDate });
            //command.Parameters.Add(new SqlParameter() { ParameterName = "@DepositReference", Value = arDeposit.DepositReference });

            using (command)
            {
                try
                {
                    command.Connection.Open();
                    depositID = (int)await command.ExecuteScalarAsync();
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 50000 && ex.Class == 16)
                        throw new BSInfrastructureException(ex.Message);

                    throw;
                }
                finally
                {
                    command?.Connection?.Close();
                }
            }
        }
        return depositID;
    }

    public bool DeleteDepositDetail(int companyId, int depositId, int depositDetailId)
    {
        throw new NotImplementedException();
    }

    public Task<ARDepositDetailVM> GetDepositDetailVM(int companyId, int depositId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ARDepositNew>> GetNewARDeposits(int companyId)
    {
        throw new NotImplementedException();
    }

    public bool SaveDepositDetail(ARDepositDetail depositDetail)
    {
        throw new NotImplementedException();
    }
}
