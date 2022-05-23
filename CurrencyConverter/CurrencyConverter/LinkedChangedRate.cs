namespace CurrencyConverter;

public sealed class LinkedChangedRate
{
    public LinkedChangedRate(ChangeRate current, LinkedChangedRate? parent)
    {
        Current = current;
        Parent = parent;
    }

    public ChangeRate Current { get; }
    public LinkedChangedRate? Parent { get; }
}