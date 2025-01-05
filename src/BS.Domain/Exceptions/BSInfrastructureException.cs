namespace BS.Domain.Exceptions;

public class BSInfrastructureException : BillsoftException
{

    public BSInfrastructureException(string message) : base(message)
    {
        base.Source = "BS.Infrastructure";
    }
}
