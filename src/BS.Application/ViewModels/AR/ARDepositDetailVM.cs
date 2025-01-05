namespace BS.Application.ViewModels.AR;

public class ARDepositDetailVM
{
    public int DepositID { get; set; }

    public ARDeposit Deposit { get; set; }

    public IEnumerable<ARDepositDetail> DepositDetails { get; set; }

    public IEnumerable<ARDepositType> DepositTypes { get; set; }

    public IEnumerable<ARCustomer> Customers { get; set; }
}
