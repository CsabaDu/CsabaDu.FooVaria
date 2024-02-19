namespace CsabaDu.FooVaria.Measures.Types.Implementations;

internal sealed class Cash(IMeasureFactory factory, Currency currency, decimal quantity)
    : Measure<ICash, decimal, Currency>(factory, currency, quantity), ICash
{
    #region Public methods
    public ICash? GetCustomMeasure(Currency currency, decimal exchangeRate, decimal quantity, string customName)
    {
        return (ICash?)GetBaseMeasure(currency, exchangeRate, quantity, customName);
    }

    public ICash? GetNextCustomMeasure(string customName, decimal exchangeRate, decimal quantity)
    {
        return (ICash?)GetBaseMeasure(customName, GetMeasureUnitCode(), exchangeRate, quantity);
    }
    #endregion
}
