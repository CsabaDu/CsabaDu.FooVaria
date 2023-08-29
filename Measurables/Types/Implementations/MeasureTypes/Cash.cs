namespace CsabaDu.FooVaria.Measurables.Types.Implementations.MeasureTypes;

internal sealed class Cash : Measure, ICash
{
    internal Cash(ICash cash) : base(cash)
    {
    }

    internal Cash(IMeasureFactory measureFactory, ValueType quantity, Enum measureUnit) : base(measureFactory, quantity, measureUnit)
    {
    }

    internal Cash(IMeasureFactory measureFactory, ValueType quantity, IMeasurement measurement) : base(measureFactory, quantity, measurement)
    {
    }

    internal Cash(IMeasureFactory measureFactory, ValueType quantity, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate) : base(measureFactory, quantity, customName, measureUnitTypeCode, exchangeRate)
    {
    }

    internal Cash(IMeasureFactory measureFactory, ValueType quantity, Enum measureUnit, decimal exchangeRate, string customName) : base(measureFactory, quantity, measureUnit, exchangeRate, customName)
    {
    }

    public ICash GetCash(ValueType quantity, string name)
    {
        return (ICash)GetMeasure(quantity, name);
    }

    public ICash GetCash(decimal quantity, Currency currency)
    {
        return (ICash)GetMeasure(quantity, currency);
    }

    public ICash GetCash(decimal quantity, Currency currency, decimal exchangeRate, string customName)
    {
        return (ICash)GetMeasure(quantity, currency, exchangeRate, customName);
    }

    public ICash GetCash(ValueType quantity, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate)
    {
        return (ICash)GetMeasure(quantity, customName, measureUnitTypeCode, exchangeRate);
    }

    public ICash GetCash(ValueType quantity, IMeasurement measurement)
    {
        return (ICash)GetMeasure(quantity, measurement);
    }

    public ICash GetCash(IBaseMeasure baseMeasure)
    {
        return (ICash)GetMeasure(baseMeasure);
    }

    public ICash GetCash(ICash? other = null)
    {
        return (ICash)GetMeasure(other ?? this);
    }

    public ICash GetNextCustomMeasure(decimal quantity, decimal exchangeRate, string customName)
    {
        return (ICash)GetMeasure(quantity, customName, MeasureUnitTypeCode, exchangeRate);
    }
}
