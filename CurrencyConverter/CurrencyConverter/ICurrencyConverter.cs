namespace CurrencyConverter;

public interface ICurrencyConverter
{
    ulong Convert(string from, string to, decimal amount);
}