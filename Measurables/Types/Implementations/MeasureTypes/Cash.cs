﻿namespace CsabaDu.FooVaria.Measurables.Types.Implementations.MeasureTypes;

internal sealed class Cash : Measure, ICash
{
    #region Constructors
    internal Cash(ICash cash) : base(cash)
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

    public ICash GetMeasure(decimal quantity, IMeasurement? measurement = null)
    {
        return GetMeasure(this, quantity, measurement);
    }

    public ICash GetMeasure(ICash? other = null)
    {
        return GetMeasure(this, other as Cash);
    }

    public ICash GetNextCustomMeasure(decimal quantity, string customName, decimal exchangeRate)
    {
        return GetMeasure(this, quantity, customName, exchangeRate);
    }

    public decimal GetQuantity()
    {
        return (decimal)Quantity;
    }
    #endregion
}
