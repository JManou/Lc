namespace CurrencyConverter;

public sealed class CurrencyConverter: ICurrencyConverter
{
    private readonly ILookup<string, ChangeRate> _changeRates;

    public CurrencyConverter(IEnumerable<ChangeRate> changeRates)
    {
        IEnumerable<ChangeRate> Consolidate(ChangeRate rate)
        {
            yield return rate;
            yield return rate.GetReverseChangeRate();
        }

        _changeRates = changeRates
            .SelectMany(Consolidate)
            .ToLookup(x => x.From);
    }
    
    public ulong Convert(string from, string to, decimal amount)
    {
        var changeRate = GetChangeRate(from, to);
        return System.Convert.ToUInt64(Convert(changeRate, amount));
    }

    private static decimal Convert(LinkedChangedRate rate, decimal amount)
    {
        if (rate.Parent == default)
        {
            return Math.Round(amount * rate.Current.Rate, CurrencyHelper.DecimalPrecision);
        }

        var temp = Convert(rate.Parent, amount);
        return Math.Round(temp * rate.Current.Rate, CurrencyHelper.DecimalPrecision);
    }
    
    private LinkedChangedRate GetChangeRate(string from, string to)
    {
        var changes = _changeRates[from].Select(x => new LinkedChangedRate(x, null)).ToList();
        var encountered = new HashSet<string>(_changeRates.Count);
        encountered.Add(from);
        var index = 0;
        while (index < changes.Count)
        {
            var changeRate = changes[index++];

            if (changeRate.Current.To == to)
            {
                return changeRate;
            }

            if (!encountered.Add(changeRate.Current.To))
            {
                continue;
            }
            
            var nextItems = _changeRates[changeRate.Current.To].Select(x => new LinkedChangedRate(x, changeRate));
            changes.AddRange(nextItems);
        }

        throw new Exception("Conversion not possible.");
    }
}
