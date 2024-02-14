namespace CsabaDu.FooVaria.BaseTypes.BaseRates.Types.Implementations;

public abstract class BaseRate : Quantifiable, IBaseRate
{
    #region Constructors
    protected BaseRate(IBaseRate other) : base(other)
    {
    }

    protected BaseRate(IBaseRateFactory factory, MeasureUnitCode denominatorMeasureUnitCode) : base(factory, denominatorMeasureUnitCode)
    {
    }

    protected BaseRate(IBaseRateFactory factory, Enum denominatorMeasureUnit) : base(factory, denominatorMeasureUnit)
    {
    }

    protected BaseRate(IBaseRateFactory factory, IBaseMeasurement denominatorMeasurement) : base(factory, denominatorMeasurement)
    {
    }

    protected BaseRate(IBaseRateFactory factory, IBaseRate baseRate) : base(factory, baseRate)
    {
    }

    protected BaseRate(IBaseRateFactory factory, IQuantifiable denominator) : base(factory, denominator)
    {
    }
    #endregion

    #region Properties
    public virtual object? this[RateComponentCode rateComponentCode] => rateComponentCode switch
    {
        RateComponentCode.Numerator => GetNumeratorMeasureUnitCode(),
        RateComponentCode.Denominator => GetDenominatorMeasureUnitCode(),

        _ => null,
    };
    #endregion

    #region Public methods
    public int CompareTo(IBaseRate? other)
    {
        if (other == null) return 1;

        ValidateMeasureUnitCodes(other);

        return GetDefaultQuantity().CompareTo(other.GetDefaultQuantity());
    }

    public IBaseRate ConvertToLimitable(ILimiter limiter)
    {
        string paramName = nameof(limiter);

        if (NullChecked(limiter, paramName) is IBaseRate baseRate) return GetBaseRate(baseRate);

        throw ArgumentTypeOutOfRangeException(paramName, limiter);
    }

    public bool Equals(IBaseRate? other)
    {
        return base.Equals(other)
            && other.GetNumeratorMeasureUnitCode() == GetNumeratorMeasureUnitCode();
    }

    public bool? FitsIn(ILimiter? limiter)
    {
        if (limiter is not IBaseRate baseRate) return null;

        return FitsIn(baseRate, limiter!.LimitMode);
    }

    public bool? FitsIn(IBaseRate? other, LimitMode? limitMode)
    {
        if (other == null && !limitMode.HasValue) return true;

        if (!IsExchangeableTo(other)) return null;

        int comparison = CompareTo(other);

        LimitMode limitModeValue = limitMode ?? LimitMode.BeNotGreater;

        if (!limitModeValue.IsDefined()) return null;

        return comparison.FitsIn(limitModeValue);
    }

    public IBaseRate GetBaseRate(IBaseRate baseRate)
    {
        return GetFactory().CreateBaseRate(baseRate);
    }

    //public IBaseRate GetBaseRate(IBaseMeasure numerator, IBaseMeasure denominator)
    //{
    //    return GetFactory().CreateBaseRate(numerator, denominator);
    //}

    public IBaseRate GetBaseRate(IBaseMeasure numerator, IBaseMeasurement denominatorMeasurement)
    {
        return GetFactory().CreateBaseRate(numerator, denominatorMeasurement);
    }

    public IBaseRate GetBaseRate(IBaseMeasure numerator, Enum denominatorMeasureUnit)
    {
        return GetFactory().CreateBaseRate(numerator, denominatorMeasureUnit);
    }

    public IBaseRate GetBaseRate(IBaseMeasure numerator, MeasureUnitCode denominatorMeasureUnitCode)
    {
        return GetFactory().CreateBaseRate(numerator, denominatorMeasureUnitCode);
    }

    public IBaseRate GetBaseRate(params IBaseMeasure[] baseMeasures)
    {
        return GetFactory().CreateBaseRate(baseMeasures);
    }

    public MeasureUnitCode GetDenominatorMeasureUnitCode()
    {
        return MeasureUnitCode;
    }

    public decimal GetQuantity()
    {
        return GetDefaultQuantity();
    }

    public object GetQuantity(TypeCode quantityTypeCode)
    {
        object? quantity = GetQuantity().ToQuantity(Defined(quantityTypeCode, nameof(quantityTypeCode)));

        if (quantity != null) return quantity;

        throw new InvalidOperationException(null);
    }

    public bool IsExchangeableTo(IBaseRate? baseRate)
    {
        return baseRate != null
            && baseRate.HasMeasureUnitCode(MeasureUnitCode)
            && baseRate.GetNumeratorMeasureUnitCode() == GetNumeratorMeasureUnitCode();
    }

    public decimal ProportionalTo(IBaseRate? other)
    {
        decimal defaultQuantity = NullChecked(other, nameof(other)).GetDefaultQuantity();

        ValidateMeasureUnitCodes(other!);

        return Math.Abs(GetDefaultQuantity() / defaultQuantity);
    }

    #region Override methods
    public override IBaseRateFactory GetFactory()
    {
        return (IBaseRateFactory)Factory;
    }

    public override sealed void ValidateQuantity(IQuantifiable? quantifiable, string paramName)
    {
        base.ValidateQuantity(quantifiable, paramName);
    }

    public override IEnumerable<MeasureUnitCode> GetMeasureUnitCodes()
    {
        yield return GetNumeratorMeasureUnitCode();
        yield return MeasureUnitCode;
    }

    #region Sealed methods
    public override sealed bool Equals(object? obj)
    {
        return obj is IBaseRate baseRate && Equals(baseRate);
    }

    public override sealed int GetHashCode()
    {
        return HashCode.Combine(MeasureUnitCode, GetNumeratorMeasureUnitCode(), GetDefaultQuantity());
    }

    public override sealed TypeCode GetQuantityTypeCode()
    {
        return GetQuantityTypeCode(this);
    }

    public override sealed void ValidateMeasureUnit(Enum? measureUnit, string paramName)
    {
        base.ValidateMeasureUnit(measureUnit, paramName);
    }

    public override sealed void ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, string paramName)
    {
        ValidateMeasureUnitCode(this, measureUnitCode, paramName);
    }

    public override sealed void ValidateQuantity(ValueType? quantity, string paramName)
    {
        ValidateQuantity(this, quantity, paramName);
    }
    #endregion
    #endregion

    #region Virtual methods
    public virtual MeasureUnitCode GetMeasureUnitCode(RateComponentCode rateComponentCode)
    {
        if (this[rateComponentCode] is MeasureUnitCode measureUnitCode) return measureUnitCode;

        throw InvalidRateComponentCodeArgumentException(rateComponentCode);
    }
    #endregion

    #region Abstract methods
    public abstract IBaseRate GetBaseRate(MeasureUnitCode numeratorMeasureUnitCode, decimal defaultQuantity, MeasureUnitCode denominatorMeasureUnitCode);
    public abstract MeasureUnitCode GetNumeratorMeasureUnitCode();

    #endregion

    #region Static methods
    public static IEnumerable<RateComponentCode> GetRateComponentCodes()
    {
        return Enum.GetValues<RateComponentCode>();
    }
    #endregion
    #endregion

    #region Protected methods
    protected void ValidateMeasureUnitCodes(IBaseRate other)
    {
        string paramName = nameof(other);

        foreach (RateComponentCode item in Enum.GetValues<RateComponentCode>())
        {
            if (this[item] is MeasureUnitCode measureUnitCode)
            {
                if (other[item] is not MeasureUnitCode otherMeasureUnitCode)
                {
                    throw ArgumentTypeOutOfRangeException(paramName, other);
                }

                if (measureUnitCode != otherMeasureUnitCode)
                {
                    throw InvalidMeasureUnitCodeEnumArgumentException(otherMeasureUnitCode, paramName);
                }
            }
        }
    }
    #endregion
}
