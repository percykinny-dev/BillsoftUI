namespace BS.Application.Exceptions;

public class BillsoftException : Exception
{
    public BillsoftException(string message) : base(message)
    {
        this.Source = "MAS";
    }
}
