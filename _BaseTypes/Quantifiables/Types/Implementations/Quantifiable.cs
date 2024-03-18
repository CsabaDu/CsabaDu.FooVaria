namespace CsabaDu.FooVaria.BaseTypes.Quantifiables.Types.Implementations;

public abstract class Quantifiable(IRootObject rootObject, string paramName) : BaseQuantifiable(rootObject, paramName), IQuantifiable
{
    #region Public methods
    public virtual int CompareTo(IQuantifiable? other)
    {
        if (other == null) return 1;

        ValidateMeasureUnitCode(other.GetMeasureUnitCode(), nameof(other));

        return GetDefaultQuantity().CompareTo(other.GetDefaultQuantity());
    }

    public virtual bool Equals(IQuantifiable? other)
    {
        return base.Equals(other);
    }

    public override bool? FitsIn(ILimiter? limiter)
    {
        if (limiter is not IQuantifiable) return null;

        return base.FitsIn(limiter);

        //LimitMode? limitMode = limiter.GetLimitMode();

        //return FitsIn(quantifiable, limitMode);
    }

    public virtual bool? FitsIn(IQuantifiable? other, LimitMode? limitMode)
    {
        if (other == null && !limitMode.HasValue) return true;

        if (other?.HasMeasureUnitCode(GetMeasureUnitCode()) != true) return null;

        decimal defaultQuantity = GetDefaultQuantity();
        decimal otherQuantity = other.GetDefaultQuantity();
        limitMode ??= LimitMode.BeNotGreater;

        return defaultQuantity.FitsIn(otherQuantity, limitMode);

        //bool limitModeHasValue = limitMode.HasValue;
        //if (!limitModeHasValue) return CompareTo(other) <= 0;

        //_ = Defined(limitMode!.Value, nameof(limitMode));

        //IQuantifiable ceilingBaseMeasure = other.Round(RoundingMode.Ceiling);
        //other = limitMode switch
        //{
        //    LimitMode.BeNotLess or
        //    LimitMode.BeGreater => ceilingBaseMeasure,

        //    LimitMode.BeNotGreater or
        //    LimitMode.BeLess or
        //    LimitMode.BeEqual => other.Round(RoundingMode.Floor),

        //    _ => null,
        //};

        //if (other == null) return null;

        //int comparison = CompareTo(other);

        //if (limitMode == LimitMode.BeEqual) return comparison == 0 && ceilingBaseMeasure.Equals(other);

        //return comparison.FitsIn(limitMode);
    }

    public decimal GetDecimalQuantity()
    {
        return (decimal)GetQuantity(TypeCode.Decimal);
    }

    public IQuantifiable GetQuantifiable(MeasureUnitCode measureUnitCode, decimal defaultQuantity)
    {
        IQuantifiableFactory factory = (IQuantifiableFactory)GetFactory();

        return factory.CreateQuantifiable(measureUnitCode, defaultQuantity);
    }

    public object GetQuantity(RoundingMode roundingMode)
    {
        ValueType baseQuantity = GetBaseQuantity();

        return baseQuantity switch
        {
            double quantity => quantity.Round(roundingMode),
            decimal quantity => quantity.Round(roundingMode),

            _ => baseQuantity,
        };
    }

    public object GetQuantity(TypeCode quantityTypeCode)
    {
        return GetBaseQuantity().ToQuantity(quantityTypeCode) ?? throw InvalidQuantityTypeCodeEnumArgumentException(quantityTypeCode);
    }

    public bool IsExchangeableTo(Enum? context)
    {
        if (context == null) return false;

        if (context is not MeasureUnitCode measureUnitCode)
        {
            if (!IsDefinedMeasureUnit(context)) return false;

            measureUnitCode = GetDefinedMeasureUnitCode(context);
        }

        return HasMeasureUnitCode(measureUnitCode);
    }

    public decimal ProportionalTo(IQuantifiable? other)
    {
        const string paramName = nameof(other);

        ValidateMeasureUnitCode(other, paramName);

        decimal defaultQuantity = other!.GetDefaultQuantity();

        if (defaultQuantity != 0) return Math.Abs(GetDefaultQuantity() / defaultQuantity);

        throw QuantityArgumentOutOfRangeException(paramName, defaultQuantity);
    }

    public bool TryExchangeTo(Enum? context, [NotNullWhen(true)] out IQuantifiable? exchanged)
    {
        exchanged = ExchangeTo(context);

        return exchanged != null;
    }

    //public void ValidateQuantifiable(IBaseQuantifiable? baseQuantifiable,[DisallowNull] string paramName)
    //{
    //    ValidateMeasureUnitCode(baseQuantifiable, paramName);
    //    ValidateQuantity(baseQuantifiable, paramName);
    //}

    #region Override methods
    #region Sealed methods
    public override sealed MeasureUnitCode GetMeasureUnitCode()
    {
        return base.GetMeasureUnitCode();
    }

    public override sealed void ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, string paramName)
    {
        base.ValidateMeasureUnitCode(measureUnitCode, paramName);
    }
    #endregion
    #endregion

    #region Abstract methods
    public abstract IQuantifiable? ExchangeTo(Enum? context);
    public abstract ValueType GetBaseQuantity();
    public abstract IQuantifiable Round(RoundingMode roundingMode);
    #endregion
    #endregion
}
