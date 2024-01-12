namespace CsabaDu.FooVaria.RateComponents.Types.Implementations.MeasureTypes;

internal sealed class Cash : Measure<ICash, decimal, Currency>, ICash
{
    #region Constructors
    internal Cash(IMeasureFactory factory, Currency currency, ValueType quantity) : base(factory, currency, quantity)
    {
    }
    #endregion

    #region Public methods
    public ICash? GetCustomMeasure(Currency currency, decimal exchangeRate, decimal quantity, string customName)
    {
        return GetRateComponent(currency, exchangeRate, quantity, customName);
    }

    public ICash? GetNextCustomMeasure(string customName, decimal exchangeRate, decimal quantity)
    {
        return GetRateComponent(customName, MeasureUnitCode, exchangeRate, quantity);
    }
    #endregion
}
