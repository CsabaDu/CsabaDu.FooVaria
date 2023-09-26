namespace CsabaDu.FooVaria.Measurables.Types.Implementations;

internal sealed class Denominator : BaseMeasure, IDenominator
{
    #region Constants
    private const decimal DefaultRateComponentQuantity = decimal.One;
    #endregion

    #region Constructors
    internal Denominator(IDenominator other) : base(other)
    {
    }

    internal Denominator(IDenominatorFactory factory, ValueType quantity, IMeasurement measurement) : base(factory, quantity, measurement)
    {
    }

    internal Denominator(IDenominatorFactory factory, IMeasurement measurement) : base(factory, DefaultRateComponentQuantity, measurement)
    {
    }
    #endregion

    #region Public methods
    public IDenominator GetDenominator(IMeasurement measurement, ValueType quantity)
    {
        return GetDenominatorFactory().Create(measurement, quantity);
    }

    public IDenominator GetDenominator(IMeasurement measurement)
    {
        return GetDenominator(measurement, DefaultRateComponentQuantity);
    }

    public IDenominator GetDenominator(IBaseMeasure baseMeasure)
    {
        return GetDenominatorFactory().Create(baseMeasure, DefaultRateComponentQuantity);
    }

    public IDenominator GetDenominator(IDenominator other)
    {
        return GetDenominatorFactory().Create(other);
    }

    public IDenominator GetDenominator(Enum measureUnit, ValueType quantity)
    {
        return GetDenominatorFactory().Create(measureUnit, quantity);
    }

    public IDenominator GetDenominator(Enum measureUnit)
    {
        return GetDenominator(measureUnit, DefaultRateComponentQuantity);
    }

    public IDenominator GetDenominator(Enum measureUnit, decimal exchangeRate, string customName, ValueType quantity)
    {
        return GetDenominatorFactory().Create(measureUnit, exchangeRate, customName, quantity);
    }

    public IDenominator GetDenominator(Enum measureUnit, decimal exchangeRate, string customName)
    {
        return GetDenominator(measureUnit, exchangeRate, customName, DefaultRateComponentQuantity);
    }

    public IDenominator GetDenominator(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ValueType quantity)
    {
        return GetDenominatorFactory().Create(customName, measureUnitTypeCode, exchangeRate, quantity);
    }

    public IDenominator GetDenominator(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate)
    {
        return GetDenominator(customName, measureUnitTypeCode, exchangeRate, DefaultRateComponentQuantity);
    }

    public IDenominator GetDenominator(string name, ValueType quantity)
    {
        return GetDenominatorFactory().Create(name, quantity);
    }

    public IDenominator GetDenominator(string name)
    {
        return GetDenominator(name, DefaultRateComponentQuantity);
    }

    public IDenominator GetDenominator(IBaseMeasure baseMeasure, ValueType quantity)
    {
        return GetDenominatorFactory().Create(baseMeasure, quantity);
    }

    public IDenominatorFactory GetDenominatorFactory()
    {
        return MeasurableFactory as IDenominatorFactory ?? throw new InvalidOperationException(null);
    }

    #region Overriden methods
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

    public override IBaseMeasure GetBaseMeasure(ValueType quantity, Enum measureUnit, decimal exchangeRate, string customName)
    {
        return GetDenominator(measureUnit, exchangeRate, customName, quantity);
    }

    public override IBaseMeasure GetBaseMeasure(ValueType quantity, IMeasurement measurement)
    {
        return GetDenominator(measurement, quantity);
    }

    public override IBaseMeasure GetBaseMeasure(ValueType quantity, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate)
    {
        return GetDenominator(measureUnitTypeCode, exchangeRate, customName, quantity);
    }

    public override IBaseMeasure GetBaseMeasure(ValueType quantity, string name)
    {
        return GetDenominator(name, quantity);
    }

    public override ValueType GetDefaultRateComponentQuantity()
    {
        return DefaultRateComponentQuantity;
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
    #endregion
    #endregion
}
