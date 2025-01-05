namespace BS.Domain.Exceptions;

public class BillsoftException : Exception
{
    public BillsoftException(string message) : base(message)
    {
        this.Source = $"BS.{this.Source}";
    }
}
