﻿namespace CsabaDu.FooVaria.Measurables.Types.Implementations;

internal sealed class Denominator : BaseMeasure, IDenominator
{
    #region Private constants
    private const decimal DefaultDenominatorQuantity = decimal.One;
    #endregion

    #region Constructors
    public Denominator(IDenominator denominator) : base(denominator)
    {
        Quantity = denominator.Quantity;
    }

    public Denominator(IDenominatorFactory denominatorFactory, ValueType? quantity, Enum measureUnit) : base(denominatorFactory, quantity ?? DefaultDenominatorQuantity, measureUnit)
    {
        Quantity = GetDenominatorQuantity(quantity);
    }

    public Denominator(IDenominatorFactory denominatorFactory, ValueType? quantity, IMeasurement measurement) : base(denominatorFactory, quantity ?? DefaultDenominatorQuantity, measurement)
    {
        Quantity = GetDenominatorQuantity(quantity);
    }

    public Denominator(IDenominatorFactory denominatorFactory, ValueType? quantity, MeasureUnitTypeCode customMeasureUnitTypeCode, decimal exchangeRate, string? customName) : base(denominatorFactory, quantity ?? DefaultDenominatorQuantity, customMeasureUnitTypeCode, exchangeRate, customName)
    {
        Quantity = GetDenominatorQuantity(quantity);
    }

    public Denominator(IDenominatorFactory denominatorFactory, ValueType? quantity, Enum measureUnit, decimal exchangeRate, string? customName) : base(denominatorFactory, quantity ?? DefaultDenominatorQuantity, measureUnit, exchangeRate, customName)
    {
        Quantity = GetDenominatorQuantity(quantity);
    }
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

    public override IBaseMeasure GetBaseMeasure(ValueType quantity, Enum measureUnit, decimal exchangeRate, string? customName = null)
    {
        return GetDenominator(measureUnit, exchangeRate, customName, quantity);
    }

    public override IBaseMeasure GetBaseMeasure(ValueType quantity, IMeasurement? measurement = null)
    {
        return GetDenominator(measurement ?? Measurement, quantity);
    }

    public override IBaseMeasure GetBaseMeasure(ValueType quantity, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, string? customName = null)
    {
        return GetDenominator(measureUnitTypeCode, exchangeRate, customName, quantity);
    }

    public override IBaseMeasure GetBaseMeasure(ValueType quantity, string name)
    {
        return GetDenominator(name, quantity);
    }

    public override IMeasurable GetDefault()
    {
        throw new NotImplementedException();
    }

    public override ValueType GetDefaultRateComponentQuantity()
    {
        throw new NotImplementedException();
    }

    public IDenominator GetDenominator(IMeasurement measurement, ValueType? quantity = null)
    {
        return GetDenominatorFactory().Create(measurement, quantity);
    }

    public IDenominator GetDenominator(IDenominator? other = null)
    {
        return GetDenominatorFactory().Create(other ?? this);
    }

    public IDenominator GetDenominator(Enum measureUnit, ValueType? quantity = null)
    {
        return GetDenominatorFactory().Create(measureUnit, quantity);
    }

    public IDenominator GetDenominator(Enum measureUnit, decimal exchangeRate, string? customName = null, ValueType? quantity = null)
    {
        return GetDenominatorFactory().Create(measureUnit, exchangeRate, customName, quantity);
    }

    public IDenominator GetDenominator(MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, string? customName = null, ValueType? quantity = null)
    {
        return GetDenominatorFactory().Create(measureUnitTypeCode, exchangeRate, customName, quantity);
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

    public override ValueType GetQuantity(ValueType? quantity = null)
    {
        quantity = base.GetQuantity(quantity);

        if ((decimal)quantity <= 0) throw QuantityArgumentOutOfRangeException(quantity);

        return quantity;
    }

    public override bool TryGetBaseMeasure(ValueType quantity, Enum measureUnit, decimal exchangeRate, string? customName, [NotNullWhen(true)] out IBaseMeasure? baseMeasure)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Private methods
    private ValueType GetDenominatorQuantity(ValueType? quantity)
    {
        return GetQuantity(quantity ?? DefaultDenominatorQuantity);
    }
    #endregion
}
