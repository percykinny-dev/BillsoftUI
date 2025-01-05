namespace BS.Domain.Common;

public static class USPhoneUtil
{
    public static string FormatPhoneNumber(string phone)
    {
        Regex regex = new Regex(@"[^\d]");
        phone = regex.Replace(phone, "");
        phone = Regex.Replace(phone, @"(\d{3})(\d{3})(\d{4})", "$1-$2-$3");
        return phone;
    }
}
