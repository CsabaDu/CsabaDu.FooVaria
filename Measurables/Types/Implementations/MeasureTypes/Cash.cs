namespace CsabaDu.FooVaria.Measurables.Types.Implementations.MeasureTypes;

internal sealed class Cash : Measure, ICash
{
    internal Cash(ICash cash) : base(cash)
    {
    }

    internal Cash(IMeasureFactory measureFactory, decimal quantity, Enum measureUnit) : base(measureFactory, quantity, measureUnit)
    {
    }

    internal Cash(IMeasureFactory measureFactory, decimal quantity, IMeasurement measurement) : base(measureFactory, quantity, measurement)
    {
    }

    internal Cash(IMeasureFactory measureFactory, decimal quantity, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate) : base(measureFactory, quantity, customName, measureUnitTypeCode, exchangeRate)
    {
    }

    internal Cash(IMeasureFactory measureFactory, decimal quantity, Enum measureUnit, decimal exchangeRate, string customName) : base(measureFactory, quantity, measureUnit, exchangeRate, customName)
    {
    }

    public override ICash GetMeasure(IBaseMeasure baseMeasure)
    {
        ValidateBaseMeasure(baseMeasure);

        return (ICash)base.GetMeasure(baseMeasure);
    }

    public ICash GetCustomMeasure(decimal quantity, Currency currency, decimal exchangeRate, string customName)
    {
        return (ICash)GetMeasure(quantity, currency, exchangeRate, customName);
    }

    public ICash GetMeasure(decimal quantity, Currency measureUnit)
    {
        return (ICash)base.GetMeasure(quantity, measureUnit);
    }

    public ICash GetMeasure(decimal quantity, string name)
    {
        return (ICash)base.GetMeasure(quantity, name);
    }

    public ICash GetMeasure(decimal quantity, IMeasurement? measurement = null)
    {
        return (ICash)base.GetMeasure(quantity, measurement);
    }

    public ICash GetMeasure(ICash? other = null)
    {
        return (ICash)base.GetMeasure(other);
    }

    public override Enum GetMeasureUnit()
    {
        return (Currency)Measurement.MeasureUnit;
    }

    public ICash GetNextCustomMeasure(decimal quantity, string customName, decimal exchangeRate)
    {
        return (ICash)GetMeasure(quantity, customName, /*MeasureUnitTypeCode, */exchangeRate);
    }

    public decimal GetQuantity()
    {
        return (decimal)Quantity;
    }
}

    //public ICash GetCash(IBaseMeasure baseMeasure)
    //{
    //    return (ICash)GetMeasure(baseMeasure);
    //}
    //public ICash GetCash(ICash? other = null)
    //{
    //    return (ICash)GetMeasure(other ?? this);
    //}
    //public ICash GetCash(ValueType quantity, string name)
    //{
    //    return (ICash)GetMeasure(quantity, name);
    //}

    //public ICash GetCash(decimal quantity, Currency currency)
    //{
    //    return (ICash)GetMeasure(quantity, currency);
    //}

    //public ICash GetCash(ValueType quantity, IMeasurement? measurement = null)
    //{
    //    return (ICash)GetMeasure(quantity, measurement);
    //}
