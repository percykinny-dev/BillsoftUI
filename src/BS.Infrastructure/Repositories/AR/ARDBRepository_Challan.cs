namespace BS.Infrastructure.Repositories.AR;

public partial class ARDBRepository
{

    //challan specific db calls code goes here
    public Task<ARInvoice> ConvertChallanToInvoice(int companyId, int challanId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteChallan(int companyId, int challanId)
    {
        using (var sqlConnection = new SqlConnection(connectionString))
        {
            var command = sqlConnection.CreateCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = $"[dbo].[procedure_name_goes_here]";

            command.Parameters.Add(new SqlParameter() { ParameterName = "@CompanyID", Value = companyId });
            command.Parameters.Add(new SqlParameter() { ParameterName = "@ChallanID", Value = challanId });

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

    public async Task<ARChallanDetailVM> GetChallanDetailVM(int companyId, int challanId)
    {
        var data = new ARChallanDetailVM();

        var sql = @"[dbo].[pAR_GetChallanDetails]";
        using (var connection = new SqlConnection(connectionString))
        {
            try
            {
                using (var query = await connection.QueryMultipleAsync(sql,
                    new { CompanyID = companyId, ChallanID = challanId }, 
                    commandType: System.Data.CommandType.StoredProcedure))
                {
                    //returns single row
                    var challan = await query.ReadSingleAsync<ARChallanDataVM>();


                    data.Challan = challan;

                    var challanDetail = await query.ReadAsync<ARChallanItemsVM>();
                    data.ChallanItems = challanDetail.ToList();
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
        return data;
    }

    public async Task<(IEnumerable<ARChallanVM>, int)> GetChallansList(int companyId, QueryFilter queryFilter, string[] allowedStatuses)
    {
        IEnumerable<ARChallanVM> challans = null;
        int totalCount = 0;

        var sql = @"[dbo].[pAR_GetChallansList]";
        using (var connection = new SqlConnection(connectionString))
        {
            try
            {
                using (var query = await connection.QueryMultipleAsync(sql,
                    new { CompanyID = companyId, PageNo = queryFilter.PageNumber, PageSize = queryFilter.PageSize },
                    commandType: System.Data.CommandType.StoredProcedure))
                {
                    //returns multiple rows
                    challans = await query.ReadAsync<ARChallanVM>();

                    // Read the second result set (total count)
                    totalCount = await query.ReadSingleAsync<int>();
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
        return (challans, totalCount);
    }

    public async Task<int> SaveChallan(ARChallan challan, IEnumerable<ARChallanDetail> challanItems)
    {
        int insertedChallanID = 0;
        using (var sqlConnection = new SqlConnection(connectionString))
        {
            var command = sqlConnection.CreateCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = $"[dbo].[pAR_InsertChallan]";

           
            command.Parameters.AddWithValue("@ChallanNo", challan.ChallanNo);
            command.Parameters.AddWithValue("@ChallanDate", challan.ChallanDate);
            command.Parameters.AddWithValue("@CompanyID", challan.CompanyID);
            command.Parameters.AddWithValue("@CustomerID", challan.CustomerID);
            command.Parameters.AddWithValue("@BillAddressID", challan.BillAddressID ?? 0);
            command.Parameters.AddWithValue("@ShipAddressID", challan.ShipAddressID ?? 0);
            command.Parameters.AddWithValue("@NetAmount", challan.NetAmount);
            command.Parameters.AddWithValue("@CGSTAmount", challan.CGSTAmount);
            command.Parameters.AddWithValue("@SGSTAmount", challan.SGSTAmount);
            command.Parameters.AddWithValue("@IGSTAmount", challan.IGSTAmount);
            command.Parameters.AddWithValue("@GSTAmount", challan.GSTAmount);
            command.Parameters.AddWithValue("@TotalAmount", challan.TotalAmount);
            command.Parameters.AddWithValue("@IsDeleted", challan.IsDeleted);
            command.Parameters.AddWithValue("@Discount", challan.Discount);
            command.Parameters.AddWithValue("@DiscountAmount", challan.DiscountAmount);
            command.Parameters.AddWithValue("@ContactPerson", challan.ContactPerson ?? "");

            // TVP parameter
            DataTable table = new DataTable();
            table.Columns.Add("ChallanDetailID", typeof(int));
            table.Columns.Add("ChallanID", typeof(int));
            table.Columns.Add("StatusID", typeof(int));
            table.Columns.Add("ItemID", typeof(int));
            table.Columns.Add("HSNCode", typeof(string));
            table.Columns.Add("Quantity", typeof(decimal));
            table.Columns.Add("Uom", typeof(string));
            table.Columns.Add("Rate", typeof(decimal));
            table.Columns.Add("Amount", typeof(decimal));
            table.Columns.Add("Discount", typeof(decimal));
            table.Columns.Add("GSTRateID", typeof(int));
            table.Columns.Add("CGST", typeof(decimal));
            table.Columns.Add("CGSTAmount", typeof(decimal));
            table.Columns.Add("SGST", typeof(decimal));
            table.Columns.Add("SGSTAmount", typeof(decimal));
            table.Columns.Add("IGST", typeof(decimal));
            table.Columns.Add("IGSTAmount", typeof(decimal));
            table.Columns.Add("InvoiceId", typeof(int));
            table.Columns.Add("Total", typeof(decimal));
            
            foreach (var detail in challanItems)
            {
                table.Rows.Add(
                                //detail.ChallanDetailID, 
                                0,
                                0,
                                1,
                                detail.ItemID, 
                                detail.HSNCode,
                                detail.Quantity, 
                                detail.Uom, 
                                detail.Rate, 
                                detail.Amount,
                                0,
                                detail.GSTRateID,
                                detail.CGST,
                                detail.CGSTAmount,
                                detail.SGST,
                                detail.SGSTAmount,
                                detail.IGST, 
                                detail.IGSTAmount,
                                0,
                                detail.Total
                                );
            }

            SqlParameter tvpParam = command.Parameters.AddWithValue("@ChallanDetails", table);
            tvpParam.SqlDbType = SqlDbType.Structured;

            using (command)
            {
                try
                {
                    command.Connection.Open();
                    insertedChallanID = (int) await command.ExecuteScalarAsync();
                    
                }
                catch (Exception ex)
                {
                    return 0;
                }
                finally
                {
                    command?.Connection?.Close();
                }
            }
        }
        return insertedChallanID;
    }
}
