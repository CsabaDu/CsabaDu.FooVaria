namespace CsabaDu.FooVaria.Measurables.Types.Implementations;

internal abstract class Measure : BaseMeasure, IMeasure
{
    #region Private constants
    private const int DefaultMeasureQuantity = 0;
    #endregion

    #region Constructors
    private protected Measure(IMeasure measure) : base(measure)
    {
        Quantity = measure.Quantity;
    }

    private protected Measure(IBaseMeasureFactory baseMeasureFactory, ValueType quantity, Enum measureUnit) : base(baseMeasureFactory, quantity, measureUnit)
    {
        Quantity = GetQuantity(quantity);
    }

    private protected Measure(IBaseMeasureFactory baseMeasureFactory, ValueType quantity, IMeasurement measurement) : base(baseMeasureFactory, quantity, measurement)
    {
        Quantity = GetQuantity(quantity);
    }

    private protected Measure(IBaseMeasureFactory baseMeasureFactory, ValueType quantity, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate) : base(baseMeasureFactory, quantity, customName, measureUnitTypeCode, exchangeRate)
    {
        Quantity = GetQuantity(quantity);
    }

    private protected Measure(IBaseMeasureFactory baseMeasureFactory, ValueType quantity, Enum measureUnit, decimal exchangeRate, string customName) : base(baseMeasureFactory, quantity, measureUnit, exchangeRate, customName)
    {
        Quantity = GetQuantity(quantity);
    }
    #endregion

    #region Properties
    public override sealed object Quantity { get; init; }
    public override sealed TypeCode QuantityTypeCode => GetQuantityTypeCode();
    #endregion

    #region Public methods
    public IMeasure Add(IMeasure? other)
    {
        return GetSum(this, other, SummingMode.Add);
    }

    public IMeasure Divide(decimal divisor)
    {
        if (divisor == 0) throw new ArgumentOutOfRangeException(nameof(divisor), divisor, null);

        decimal quantity = decimal.Divide(GetDecimalQuantity(), divisor);

        return GetMeasure(quantity);
    }

    public override sealed bool Equals(IBaseMeasure? other)
    {
        return Equals(this, other);
    }

    public override sealed bool Equals(object? obj)
    {
        return obj is IMeasure measure && Equals(measure);
    }

    public bool? FitsIn(ILimit? limit)
    {
        return FitsIn(limit, limit?.LimitMode);
    }

    public bool? FitsIn(IBaseMeasure? baseMeasure, LimitMode? limitMode)
    {
        if (baseMeasure == null && limitMode == null) return true;

        if (baseMeasure?.IsExchangeableTo(MeasureUnitTypeCode) != true) return null;

        limitMode ??= default;
        IBaseMeasure ceilingBaseMeasure = baseMeasure.Round(RoundingMode.Ceiling);
        baseMeasure = getRoundedBaseMeasure();

        if (baseMeasure == null) return null;

        int comparison = CompareTo(baseMeasure);

        return limitMode switch
        {
            LimitMode.BeEqual => comparison == 0 && ceilingBaseMeasure.Equals(baseMeasure),

            _ => comparison.FitsIn(limitMode),
        };

        #region Local methods
        IBaseMeasure? getRoundedBaseMeasure()
        {
            return limitMode switch
            {
                LimitMode.BeNotLess or
                LimitMode.BeGreater => ceilingBaseMeasure,

                LimitMode.BeNotGreater or
                LimitMode.BeLess or
                LimitMode.BeEqual => baseMeasure!.Round(RoundingMode.Floor),

                _ => null,
            };
        }
        #endregion
    }

    public override IBaseMeasure GetBaseMeasure(ValueType quantity, Enum measureUnit)
    {
        return GetMeasure(quantity, measureUnit);
    }

    public override IBaseMeasure GetBaseMeasure(ValueType quantity, Enum measureUnit, decimal exchangeRate, string customName)
    {
        return GetMeasure(quantity, measureUnit, exchangeRate, customName);
    }

    public override IBaseMeasure GetBaseMeasure(ValueType quantity, IMeasurement? measurement = null)
    {
        return GetMeasure(quantity, measurement);
    }

    public override IBaseMeasure GetBaseMeasure(ValueType quantity, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate)
    {
        return GetMeasure(quantity, measureUnitTypeCode, exchangeRate, customName);
    }

    public override IBaseMeasure GetBaseMeasure(ValueType quantity, string name)
    {
        return GetMeasure(quantity, name);
    }

    public override sealed ValueType GetDefaultRateComponentQuantity()
    {
        return DefaultMeasureQuantity;
    }

    public override sealed int GetHashCode()
    {
        return GetHashCode(this);
    }
    public IMeasure GetMeasure(ValueType quantity, Enum measureUnit)
    {
        return GetMeasureFactory().Create(quantity, measureUnit);
    }

    public IMeasure GetMeasure(ValueType quantity, string name)
    {
        return GetMeasureFactory().Create(quantity, name);
    }

    public IMeasure GetMeasure(ValueType quantity, Enum measureUnit, decimal exchangeRate, string customName)
    {
        return GetMeasureFactory().Create(quantity, measureUnit, exchangeRate, customName);
    }

    public IMeasure GetMeasure(ValueType quantity, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate)
    {
        return GetMeasureFactory().Create(quantity, customName, measureUnitTypeCode, exchangeRate);
    }

    public IMeasure GetMeasure(ValueType quantity, IMeasurement? measurement = null)
    {
        return GetMeasureFactory().Create(quantity, measurement ?? Measurement);
    }

    public IMeasure GetMeasure(IMeasure? other = null)
    {
        return GetMeasureFactory().Create(other ?? this);
    }

    public IMeasure GetMeasure(Enum measureUnit)
    {
        IBaseMeasure excchanged = ExchangeTo(measureUnit) ?? throw InvalidMeasureUnitEnumArgumentException(measureUnit);

        return GetMeasure(excchanged);
    }

    public IMeasure GetMeasure(string name)
    {
        if (Measurement.TryGetMeasureUnit(name, out Enum? measureUnit)) return GetMeasure(measureUnit);

        throw new ArgumentOutOfRangeException(nameof(name), name, null);
    }

    public IMeasureFactory GetMeasureFactory()
    {
        return MeasurableFactory as IMeasureFactory ?? throw new InvalidOperationException(null);
    }

    public override sealed LimitMode? GetLimitMode()
    {
        return base.GetLimitMode();
    }

    public override sealed ValueType GetQuantity(ValueType? quantity = null)
    {
        return base.GetQuantity(quantity);
    }

    public IMeasure Multiply(decimal multiplier)
    {
        decimal quantity = decimal.Multiply(GetDecimalQuantity(), multiplier);

        return GetMeasure(quantity);
    }

    public IMeasure Subtract(IMeasure? other)
    {
        return GetSum(this, other, SummingMode.Subtract);
    }

    #region Abstract methods
    public abstract IMeasure GetMeasure(IBaseMeasure baseMeasure);
    #endregion
    #endregion
}
