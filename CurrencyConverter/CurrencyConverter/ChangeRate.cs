namespace CurrencyConverter;

public readonly struct ChangeRate:IEquatable<ChangeRate>
{
    public string From { get; }
    public string To { get; }
    public decimal Rate { get; }

    public ChangeRate(string from, string to, decimal rate)
    {
        From = from;
        To = to;
        Rate = rate;
    }

    public ChangeRate GetReverseChangeRate()
    {
        return new ChangeRate(To, From, Math.Round(1 / Rate, CurrencyHelper.DecimalPrecision));
    }

    public override string ToString()
    {
        return $"{From};{To};{Rate}";
    }

    public bool Equals(ChangeRate other)
    {
        return From == other.From && To == other.To && Rate == other.Rate;
    }

    public override bool Equals(object? obj)
    {
        return obj is ChangeRate other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(From, To, Rate);
    }

    public static bool operator ==(ChangeRate a, ChangeRate b) => a.Equals(b);
    public static bool operator !=(ChangeRate a, ChangeRate b) => !a.Equals(b);
}