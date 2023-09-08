namespace CsabaDu.FooVaria.Measurables.Types.Implementations;

internal sealed class Denominator : BaseMeasure, IDenominator
{
    #region Constants
    private const decimal DefaultDenominatorQuantity = decimal.One;
    #endregion

    #region Constructors
    internal Denominator(IDenominator denominator) : base(denominator)
    {
        Quantity = denominator.Quantity;
    }

    //internal Denominator(IDenominatorFactory denominatorFactory, ValueType? quantity, Enum measureUnit) : base(denominatorFactory, quantity ?? DefaultDenominatorQuantity, measureUnit)
    //{
    //    Quantity = GetDenominatorQuantity(quantity);
    //}

    internal Denominator(IDenominatorFactory denominatorFactory, ValueType? quantity, IMeasurement measurement) : base(denominatorFactory, quantity ?? DefaultDenominatorQuantity, measurement)
    {
        Quantity = GetDenominatorQuantity(quantity);
    }

    //internal Denominator(IDenominatorFactory denominatorFactory, ValueType? quantity, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate) : base(denominatorFactory, quantity ?? DefaultDenominatorQuantity, customName, measureUnitTypeCode, exchangeRate)
    //{
    //    Quantity = GetDenominatorQuantity(quantity);
    //}

    //internal Denominator(IDenominatorFactory denominatorFactory, ValueType? quantity, Enum measureUnit, decimal exchangeRate, string customName) : base(denominatorFactory, quantity ?? DefaultDenominatorQuantity, measureUnit, exchangeRate, customName)
    //{
    //    Quantity = GetDenominatorQuantity(quantity);
    //}

    //internal Denominator(IDenominatorFactory denominatorFactory, IBaseMeasure baseMeasure) : base(denominatorFactory, baseMeasure)
    //{
    //    Quantity = GetDenominatorQuantity(baseMeasure!.GetQuantity());
    //}

    #endregion

    #region Properties
    public override object Quantity { get; init; }
    public override TypeCode QuantityTypeCode => TypeCode.Decimal;
    #endregion

    #region Public methods
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

    public override IBaseMeasure GetBaseMeasure(ValueType quantity, IMeasurement? measurement = null)
    {
        return GetDenominator(measurement ?? Measurement, quantity);
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
        return DefaultDenominatorQuantity;
    }

    public IDenominator GetDenominator(IMeasurement measurement, ValueType? quantity = null)
    {
        return GetDenominatorFactory().Create(measurement, quantity);
    }

    public IDenominator GetDenominator(IBaseMeasure baseMeasure)
    {
        return GetDenominatorFactory().Create(baseMeasure);
    }

    public IDenominator GetDenominator(IDenominator? other = null)
    {
        return GetDenominatorFactory().Create(other ?? this);
    }

    public IDenominator GetDenominator(Enum measureUnit, ValueType? quantity = null)
    {
        return GetDenominatorFactory().Create(measureUnit, quantity);
    }

    public IDenominator GetDenominator(Enum measureUnit, decimal exchangeRate, string customName, ValueType? quantity = null)
    {
        return GetDenominatorFactory().Create(measureUnit, exchangeRate, customName, quantity);
    }

    public IDenominator GetDenominator(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ValueType? quantity = null)
    {
        return GetDenominatorFactory().Create(customName, measureUnitTypeCode, exchangeRate, quantity);
    }

    public IDenominator GetDenominator(string name, ValueType? quantity = null)
    {
        return GetDenominatorFactory().Create(name, quantity);
    }

    public IDenominatorFactory GetDenominatorFactory()
    {
        return MeasurableFactory as IDenominatorFactory ?? throw new InvalidOperationException(null);
    }

    public override int GetHashCode()
    {
        return GetHashCode(this);
    }

    public override LimitMode? GetLimitMode()
    {
        return null;
    }

    public override ValueType GetQuantity(ValueType? quantity = null)
    {
        quantity = base.GetQuantity(quantity);

        if ((decimal)quantity > 0) return quantity;

        throw QuantityArgumentOutOfRangeException(quantity);
    }
    #endregion

    #region Private methods
    private ValueType GetDenominatorQuantity(ValueType? quantity)
    {
        return GetQuantity(quantity ?? DefaultDenominatorQuantity);
    }
    #endregion
}
