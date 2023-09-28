namespace CsabaDu.FooVaria.Measurables.Types.Implementations;

internal sealed class Denominator : BaseMeasure, IDenominator
{
    #region Constructors
    internal Denominator(IDenominator other) : base(other)
    {
    }

    internal Denominator(IDenominatorFactory factory, ValueType quantity, IMeasurement measurement) : base(factory, quantity, measurement)
    {
    }

    internal Denominator(IDenominatorFactory factory, IMeasurement measurement) : base(factory, DefaultDenominatorQuantity, measurement)
    {
    }

    internal Denominator(IDenominatorFactory factory, MeasureUnitTypeCode measureUnitTypeCode) : base(factory, measureUnitTypeCode)
    {
    }
    #endregion

    #region Public methods
    public IDenominator GetDenominator(IMeasurement measurement, ValueType quantity)
    {
        return GetFactory().Create(measurement, quantity);
    }

    public IDenominator GetDenominator(IMeasurement measurement)
    {
        return GetDenominator(measurement, DefaultDenominatorQuantity);
    }

    public IDenominator GetDenominator(IBaseMeasure baseMeasure)
    {
        return (IDenominator)GetFactory().Create(baseMeasure);
    }

    public IDenominator GetDenominator(IDenominator other)
    {
        return GetFactory().Create(other);
    }

    public IDenominator GetDenominator(Enum measureUnit, ValueType quantity)
    {
        return GetFactory().Create(measureUnit, quantity);
    }

    public IDenominator GetDenominator(Enum measureUnit)
    {
        return GetDenominator(measureUnit, DefaultDenominatorQuantity);
    }

    public IDenominator GetDenominator(Enum measureUnit, decimal exchangeRate, string customName, ValueType quantity)
    {
        return GetFactory().Create(measureUnit, exchangeRate, customName, quantity);
    }

    public IDenominator GetDenominator(Enum measureUnit, decimal exchangeRate, string customName)
    {
        return GetDenominator(measureUnit, exchangeRate, customName, DefaultDenominatorQuantity);
    }

    public IDenominator GetDenominator(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ValueType quantity)
    {
        return GetFactory().Create(customName, measureUnitTypeCode, exchangeRate, quantity);
    }

    public IDenominator GetDenominator(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate)
    {
        return GetDenominator(customName, measureUnitTypeCode, exchangeRate, DefaultDenominatorQuantity);
    }

    public IDenominator GetDenominator(string name, ValueType quantity)
    {
        return GetFactory().Create(name, quantity);
    }

    public IDenominator GetDenominator(string name)
    {
        return GetDenominator(name, DefaultDenominatorQuantity);
    }

    public IDenominator GetDenominator(IBaseMeasure baseMeasure, ValueType quantity)
    {
        return GetFactory().Create(baseMeasure, quantity);
    }

    #region Override methods
    public override bool Equals(IBaseMeasure? other)
    {
        return Equals(this, other);
    }

    public override bool Equals(object? obj)
    {
        return obj is IDenominator denominator && Equals(denominator);
    }

    public override IBaseMeasure GetBaseMeasure(ValueType quantity, Enum measureUnit)
    {
        return GetDenominator(measureUnit, quantity);
    }

    //public override IBaseMeasure GetBaseMeasure(ValueType quantity, Enum measureUnit, decimal exchangeRate, string customName)
    //{
    //    return GetDenominator(measureUnit, exchangeRate, customName, quantity);
    //}

    //public override IBaseMeasure GetBaseMeasure(ValueType quantity, IMeasurement measureUnitTypeCode)
    //{
    //    return GetDenominator(measureUnitTypeCode, quantity);
    //}

    //public override IBaseMeasure GetBaseMeasure(ValueType quantity, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate)
    //{
    //    return GetDenominator(measureUnitTypeCode, exchangeRate, customName, quantity);
    //}

    //public override IBaseMeasure GetBaseMeasure(ValueType quantity, string name)
    //{
    //    return GetDenominator(name, quantity);
    //}

    public override IDenominatorFactory GetFactory()
    {
        return (IDenominatorFactory)Factory;
    }

    public override int GetHashCode()
    {
        return GetHashCode(this);
    }

    public override LimitMode? GetLimitMode()
    {
        return null;
    }

    public override TypeCode GetQuantityTypeCode()
    {
        return TypeCode.Decimal;
    }

    public IDenominator GetDefaultRateComponent()
    {
        return (IDenominator)GetDefault();
    }
    #endregion
    #endregion
}
