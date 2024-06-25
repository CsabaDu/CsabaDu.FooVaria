namespace CsabaDu.FooVaria.BaseTypes.BaseRates.Types.Implementations;

public abstract class BaseRate(IRootObject rootObject, string paramName) : BaseQuantifiable(rootObject, paramName), IBaseRate
{
    #region Public methods
    #region Static methods
    public static IEnumerable<RateComponentCode> GetRateComponentCodes()
    {
        return Enum.GetValues<RateComponentCode>();
    }
    #endregion

    #region Override methods
    #region Sealed methods
    public override sealed bool Equals(object? obj)
    {
        return obj is IBaseRate baseRate && Equals(baseRate);
    }

    public override sealed bool? FitsIn(ILimiter? limiter)
    {
        if (limiter is null) return true;

        if (limiter is not IBaseRate baseRate) return null;

        return FitsIn(baseRate, limiter?.GetLimitMode());
    }

    public override sealed int GetHashCode()
    {
        return HashCode.Combine(GetNumeratorCode(), GetDefaultQuantity(), GetDenominatorCode());
    }

    public override sealed TypeCode GetQuantityTypeCode()
    {
        Type quantityType = GetQuantity().GetType();

        return Type.GetTypeCode(quantityType);
    }

    public override sealed bool HasMeasureUnitCode(MeasureUnitCode measureUnitCode)
    {
        return GetMeasureUnitCodes().Contains(measureUnitCode);
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
        base.ValidateQuantity(quantity, paramName);
    }
    #endregion
    #endregion

    #region Virtual methods
    public virtual LimitMode? GetLimitMode()
    {
        return null;
    }
    #endregion

    #region Abstract methods
    public abstract object? GetRateComponent(RateComponentCode rateComponentCode);
    #endregion

    public int CompareTo(IBaseRate? other)
    {
        if (other is null) return 1;

        ValidateMeasureUnitCodes(other, nameof(other));

        return GetDefaultQuantity().CompareTo(other.GetDefaultQuantity());
    }

    public bool Equals(IBaseRate? other)
    {
        return base.Equals(other)
            && other.GetDenominatorCode() == GetDenominatorCode();
    }

    public bool? FitsIn(IBaseRate? other, LimitMode? limitMode)
    {
        if (other is null && !limitMode.HasValue) return true;

        if (!IsExchangeableTo(other)) return null;

        int comparison = CompareTo(other);

        LimitMode limitModeValue = limitMode ?? LimitMode.BeNotGreater;

        if (!limitModeValue.IsDefined()) return null;

        return comparison.FitsIn(limitModeValue);
    }

    public ValueType GetBaseQuantity()
    {
        return GetQuantity();
    }

    public IBaseRate GetBaseRate(IQuantifiable numerator, Enum denominator)
    {
        return GetBaseRateFactory().CreateBaseRate(numerator, denominator);
    }

    public IBaseRate GetBaseRate(IQuantifiable numerator, IMeasurable denominator)
    {
        return GetBaseRateFactory().CreateBaseRate(numerator, denominator);
    }

    public IBaseRate GetBaseRate(IQuantifiable numerator, IQuantifiable denominator)
    {
        return GetBaseRateFactory().CreateBaseRate(numerator, denominator);
    }

    public MeasureUnitCode GetDenominatorCode()
    {
        return GetMeasureUnitCode(RateComponentCode.Denominator);
    }

    public MeasureUnitCode GetMeasureUnitCode(RateComponentCode rateComponentCode)
    {
        return GetMeasureUnitCode(this, rateComponentCode) ?? throw InvalidRateComponentCodeEnumArgumentException(rateComponentCode);
    }

    public IEnumerable<MeasureUnitCode> GetMeasureUnitCodes()
    {
        foreach (RateComponentCode item in GetRateComponentCodes())
        {
            MeasureUnitCode? measureUnitCode = GetMeasureUnitCode(this, item);

            if (measureUnitCode.HasValue)
            {
                yield return measureUnitCode.Value;
            }
        }
    }

    public MeasureUnitCode GetNumeratorCode()
    {
        return GetMeasureUnitCode(RateComponentCode.Numerator);
    }

    public decimal GetQuantity()
    {
        return GetDefaultQuantity();
    }

    public object GetQuantity(TypeCode quantityTypeCode)
    {
        object? quantity = GetQuantity().ToQuantity(quantityTypeCode);

        if (quantity is not null) return quantity;

        throw InvalidQuantityTypeCodeEnumArgumentException(quantityTypeCode);
    }

    public bool IsExchangeableTo(IBaseRate? baseRate)
    {
        return baseRate?.GetNumeratorCode() == GetNumeratorCode()
            && baseRate.GetDenominatorCode() == GetDenominatorCode();
    }

    public bool IsValidRateComponent(object? rateComponent, RateComponentCode rateComponentCode)
    {
        return GetRateComponent(rateComponentCode)?.Equals(rateComponent) == true;
    }

    public decimal ProportionalTo(IBaseRate? other)
    {
        const string paramName = nameof(other);

        ValidateMeasureUnitCodes(other, paramName);

        decimal defaultQuantity = other!.GetDefaultQuantity();

        if (defaultQuantity == 0) throw QuantityArgumentOutOfRangeException(paramName, defaultQuantity);

        return Math.Abs(GetDefaultQuantity() / defaultQuantity);
    }

    public void ValidateMeasureUnitCodes(IMeasureUnitCodes? measureUnitCodes, string paramName)
    {
        if (NullChecked(measureUnitCodes, paramName) is not IBaseRate baseRate)
        {
            throw ArgumentTypeOutOfRangeException(paramName, measureUnitCodes!);
        }

        IEnumerable<MeasureUnitCode> thisMeasureUnitCodes = GetMeasureUnitCodes();
        IEnumerable<MeasureUnitCode> otherMeasureUnitCodes = baseRate.GetMeasureUnitCodes();

        for (int i = 0; i < thisMeasureUnitCodes.Count(); i++)
        {
            if (otherMeasureUnitCodes.Count() <= i) return;

            MeasureUnitCode measureUnitCode = otherMeasureUnitCodes.ElementAt(i);

            if (measureUnitCode != thisMeasureUnitCodes.ElementAt(i))
            {
                throw InvalidMeasureUnitCodeEnumArgumentException(measureUnitCode, paramName);
            }
        }
    }

    public void ValidateRateComponentCode(RateComponentCode rateComponentCode, string paramName)
    {
        object? rateComponent = GetRateComponent(Defined(rateComponentCode, paramName));

        if (rateComponent is not null) return;

        throw InvalidRateComponentCodeEnumArgumentException(rateComponentCode);
    }
    #endregion

    #region Private methods
    #region Static methods
    private static MeasureUnitCode? GetMeasureUnitCode(BaseRate baseRate, RateComponentCode rateComponentCode)
    {
        object? rateComponent = baseRate.GetRateComponent(rateComponentCode);

        if (rateComponent is MeasureUnitCode measureUnitCode)
        {
            return measureUnitCode;
        }

        if (rateComponent is IMeasurable measurable)
        {
            return measurable.GetMeasureUnitCode();
        }

        return null;
    }
    #endregion

    private IBaseRateFactory GetBaseRateFactory()
    {
        return (IBaseRateFactory)Factory;
    }
    #endregion
}
