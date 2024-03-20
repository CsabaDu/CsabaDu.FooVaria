namespace CsabaDu.FooVaria.BaseTypes.Quantifiables.Types.Implementations;

public abstract class Quantifiable(IRootObject rootObject, string paramName) : BaseQuantifiable(rootObject, paramName), IQuantifiable
{
    #region Public methods
    #region Override methods
    public override bool? FitsIn(ILimiter? limiter)
    {
        if (limiter == null) return true;

        if (limiter is not IQuantifiable) return null;

        return base.FitsIn(limiter);
    }

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

    #region Virtual methods
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

    public virtual bool? FitsIn(IQuantifiable? other, LimitMode? limitMode)
    {
        if (other == null && !limitMode.HasValue) return true;

        if (other?.HasMeasureUnitCode(GetMeasureUnitCode()) != true) return null;

        limitMode ??= LimitMode.BeNotGreater;

        if (!Enum.IsDefined(limitMode.Value)) return null;

        decimal defaultQuantity = GetDefaultQuantity();
        decimal otherQuantity = other.GetDefaultQuantity();

        return defaultQuantity.FitsIn(otherQuantity, limitMode);
    }
    #endregion

    #region Abstract methods
    public abstract ValueType GetBaseQuantity();
    public abstract bool TryExchangeTo(Enum context, [NotNullWhen(true)] out IQuantifiable? exchanged);
    #endregion

    //public abstract IQuantifiable? ExchangeTo(Enum? context);
    //public abstract IQuantifiable Round(RoundingMode roundingMode);

    //public object GetQuantity(RoundingMode roundingMode)
    //{
    //    ValueType quantity = GetBaseQuantity();

    //    return quantity.Round(roundingMode)!;
    //}

    public decimal GetDecimalQuantity()
    {
        return (decimal)GetQuantity(TypeCode.Decimal);
    }

    public IQuantifiable GetQuantifiable(MeasureUnitCode measureUnitCode, decimal defaultQuantity)
    {
        IQuantifiableFactory factory = (IQuantifiableFactory)GetFactory();

        return factory.CreateQuantifiable(measureUnitCode, defaultQuantity);
    }

    public object GetQuantity(TypeCode quantityTypeCode)
    {
        ValueType quantity = GetBaseQuantity();

        return quantity.ToQuantity(quantityTypeCode) ?? throw InvalidQuantityTypeCodeEnumArgumentException(quantityTypeCode);
    }

    public virtual bool IsExchangeableTo(Enum? context) // To override with valid measureUnit
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
    #endregion
}
