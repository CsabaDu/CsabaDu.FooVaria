namespace CsabaDu.FooVaria.Measurables.Types.Implementations.MeasureTypes;

internal sealed class Cash : Measure, ICash
{
    #region Constructors
    internal Cash(ICash other) : base(other)
    {
    }

    internal Cash(IMeasureFactory measureFactory, ValueType quantity, IMeasurement measurement) : base(measureFactory, quantity, measurement)
    {
    }
    #endregion

    #region Public methods
    public override ICash GetMeasure(IBaseMeasure baseMeasure)
    {
        return GetMeasure(this, baseMeasure);
    }

    public ICash GetCustomMeasure(decimal quantity, Currency measureUnit, decimal exchangeRate, string customName)
    {
        return GetMeasure(this, quantity, measureUnit, exchangeRate, customName);
    }

    public ICash GetMeasure(decimal quantity, Currency measureUnit)
    {
        return GetMeasure(this, quantity, measureUnit);
    }

    public ICash GetMeasure(decimal quantity, string name)
    {
        return GetMeasure(this, quantity, name);
    }

    public ICash GetMeasure(decimal quantity, IMeasurement measurement)
    {
        return GetMeasure(this, quantity, measurement);
    }

    public ICash GetMeasure(ICash other)
    {
        return GetMeasure(other);
    }

    public ICash GetMeasure(decimal quantity)
    {
        return GetMeasure(this, quantity);
    }

    public ICash GetNextCustomMeasure(decimal quantity, string customName, decimal exchangeRate)
    {
        return GetMeasure(this, quantity, customName, exchangeRate);
    }
    #endregion
}
