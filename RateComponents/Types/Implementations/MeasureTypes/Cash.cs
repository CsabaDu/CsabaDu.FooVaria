namespace CsabaDu.FooVaria.RateComponents.Types.Implementations.MeasureTypes;

internal sealed class Cash : Measure<ICash, decimal, Currency>, ICash
{
    #region Constructors
    internal Cash(IMeasureFactory factory, ValueType quantity, Currency currency) : base(factory, quantity, currency)
    {
    }
    #endregion

    #region Public methods
    public ICash GetCustomMeasure(decimal quantity, Currency measureUnit, decimal exchangeRate, string customName)
    {
        return GetMeasure(quantity, measureUnit, exchangeRate, customName);
    }

    public ICash GetNextCustomMeasure(decimal quantity, string customName, decimal exchangeRate)
    {
        return GetMeasure(quantity, customName, exchangeRate);
    }
    #endregion
}

    //public ICash GetDefault()
    //{
    //    return GetDefault(this);
    //}

    //public decimal GetDefaultRateComponentQuantity()
    //{
    //    return GetDefaultRateComponentQuantity<decimal>();
    //}

    //public override ICash GetMeasure(IRateComponent baseMeasure)
    //{
    //    return GetMeasure(this, baseMeasure);
    //}

    //public ICash GetMeasure(decimal quantity, Currency measureUnit)
    //{
    //    return GetMeasure(this, quantity, measureUnit);
    //}

    //public ICash GetMeasure(decimal quantity, string name)
    //{
    //    return GetMeasure(this, quantity, name);
    //}

    //public ICash GetMeasure(decimal quantity, IMeasurement measurement)
    //{
    //    return GetMeasure(this, quantity, measurement);
    //}

    //public ICash GetMeasure(ICash other)
    //{
    //    return GetMeasure(other);
    //}

    //public ICash GetMeasure(decimal quantity)
    //{
    //    return GetMeasure(this, quantity);
    //}

    //public Currency GetMeasureUnit()
    //{
    //    return GetMeasureUnit<Currency>(this);
    //}