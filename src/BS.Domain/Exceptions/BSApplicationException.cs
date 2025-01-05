namespace BS.Domain.Exceptions;

public class BSApplicationException: BillsoftException
{
    
    public BSApplicationException(string message): base(message)
    {
        base.Source = "BS.Application";
    }
}
