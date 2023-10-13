namespace CsabaDu.FooVaria.Measurables.Types.Implementations.MeasureTypes;

internal sealed class Cash : Measure, ICash
{
    #region Constructors
    internal Cash(IMeasureFactory factory, ValueType quantity, Currency currency) : base(factory, quantity, currency)
    {
    }
    #endregion

    #region Public methods
    public ICash GetCustomMeasure(decimal quantity, Currency measureUnit, decimal exchangeRate, string customName)
    {
        return GetMeasure(this, quantity, measureUnit, exchangeRate, customName);
    }

    public ICash GetDefaultRateComponent()
    {
        return GetDefault(this);
    }

    public decimal GetDefaultRateComponentQuantity()
    {
        return GetDefaultRateComponentQuantity<decimal>();
    }

    public override ICash GetMeasure(IBaseMeasure baseMeasure)
    {
        return GetMeasure(this, baseMeasure);
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

    //public override void GetValidBaseMeasurable(ICommonBase? other)
    //{
    //    GetValidBaseMeasurable(this, other);
    //}
    #endregion
}
