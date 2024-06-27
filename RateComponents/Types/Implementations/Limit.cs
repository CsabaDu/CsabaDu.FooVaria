﻿namespace CsabaDu.FooVaria.RateComponents.Types.Implementations;

internal sealed class Limit(ILimitFactory factory, IMeasurement measurement, ulong quantity, LimitMode limitMode) : RateComponent<ILimit>(factory, measurement), ILimit
{
    #region Properties
    public LimitMode LimitMode { get; init; } = Defined(limitMode, nameof(limitMode));
    public ulong Quantity { get; init; } = quantity;
    //public ILimitFactory Factory { get; init; } = factory;
    #endregion

    #region Public methods
    public bool Equals(ILimit? x, ILimit? y)
    {
        return base.Equals(x, y);
    }

    public ILimit GetBaseMeasure(ulong quantity)
    {
        return GetRateComponent(quantity);
    }

    public int GetHashCode([DisallowNull] ILimit limit)
    {
        return base.GetHashCode(limit);
    }

    public ILimit? GetLimit(Enum measureUnit, decimal exchangeRate, ValueType quantity, string customName, LimitMode limitMode)
    {
        return GetFactory().Create(measureUnit, exchangeRate, quantity, customName, limitMode);
    }

    public ILimit GetLimit(IMeasurement measurement, ulong quantity, LimitMode limitMode)
    {
        return GetFactory().Create(measurement, quantity, limitMode);
    }

    public ILimit GetLimit(IBaseMeasure baseMeasure, LimitMode limitMode)
    {
        return GetFactory().Create(baseMeasure, limitMode);
    }

    public ILimit GetLimit(ulong quantity)
    {
        return GetFactory().Create(this, quantity);
    }

    public ILimit GetLimit(string name, ValueType quantity, LimitMode limitMode)
    {
        return GetFactory().Create(name, quantity, limitMode);
    }

    public ILimit GetLimit(Enum measureUnit, ValueType quantity, LimitMode limitMode)
    {
        return GetFactory().Create(measureUnit, quantity, limitMode);
    }

    public ILimit? GetLimit(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate, ValueType quantity, LimitMode limitMode)
    {
        return GetFactory().Create(customName, measureUnitCode, exchangeRate, quantity, limitMode);
    }

    public ILimit GetNew(ILimit other)
    {
        return GetFactory().CreateNew(other);
    }

    public ILimit GetNew()
    {
        return GetNew(this);
    }

    public ulong GetQuantity()
    {
        return Quantity;
    }

    public bool? Includes(IBaseMeasure? limitable)
    {
        return limitable is null ?
            false
            : limitable?.FitsIn(this);
    }

    #region Override methods
    public override ValueType GetBaseQuantity()
    {
        return Quantity;
    }

    public ILimitFactory GetFactory()
    {
        return (ILimitFactory)Factory;
    }

    public override LimitMode? GetLimitMode()
    {
        return LimitMode;
    }
    #endregion
    #endregion
}
