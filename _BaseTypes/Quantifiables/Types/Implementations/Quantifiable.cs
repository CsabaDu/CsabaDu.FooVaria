namespace CsabaDu.FooVaria.BaseTypes.Quantifiables.Types.Implementations;

public abstract class Quantifiable(IRootObject rootObject, string paramName) : BaseQuantifiable(rootObject, paramName), IQuantifiable
{
    #region Public methods
    #region Override methods
    public override bool? FitsIn(ILimiter? limiter)
    {
        if (limiter is null) return true;

        return limiter is IQuantifiable quantifiable ?
            FitsIn(quantifiable, limiter.GetLimitMode())
            : null;
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
    public virtual bool IsExchangeableTo(Enum? context)
    {
        if (context is null) return false;

        if (context is not MeasureUnitCode measureUnitCode)
        {
            if (!IsDefinedMeasureUnit(context)) return false;

            measureUnitCode = GetMeasureUnitCode(context);
        }

        return HasMeasureUnitCode(measureUnitCode);
    }
    #endregion

    #region Abstract methods
    public abstract ValueType GetBaseQuantity();
    public abstract IQuantifiable Round(RoundingMode roundingMode);
    public abstract bool TryExchangeTo(Enum context, [NotNullWhen(true)] out IQuantifiable? exchanged);
    #endregion

    public int CompareTo(IQuantifiable? other)
    {
        if (other is null) return 1;

        ValidateMeasureUnitCode(other.GetMeasureUnitCode(), nameof(other));

        return GetDefaultQuantity().CompareTo(other.GetDefaultQuantity());
    }

    public bool Equals(IQuantifiable? other)
    {
        return base.Equals(other);
    }

    public bool? FitsIn(IQuantifiable? other, LimitMode? limitMode)
    {
        if (other is null) return limitMode.HasValue ? null : true;

        return (limitMode ??= LimitMode.BeNotGreater).IsDefined() ?
            DefaultQuantitiesFit(this, other, limitMode)
            : null;
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
        if (!Enum.IsDefined(typeof(RoundingMode), roundingMode)) throw InvalidRoundingModeEnumArgumentException(roundingMode);

        ValueType quantity = GetBaseQuantity();

        return quantity.Round(roundingMode)!;
    }

    public object GetQuantity(TypeCode quantityTypeCode)
    {
        ValueType quantity = GetBaseQuantity();

        return quantity.ToQuantity(quantityTypeCode) ?? throw InvalidQuantityTypeCodeEnumArgumentException(quantityTypeCode);
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

    #region Protected methods
    #region Static methods
    protected static bool Equals<T>(T? x, T? y)
        where T : class, IBaseShapeComponents
    {
        return x is null == y is null
            && (y is null
            || x!.HasMeasureUnitCode(y.GetMeasureUnitCode())
            && x!.GetBaseShapeComponents().SequenceEqual(y.GetBaseShapeComponents()));
    }

    protected static int GetHashCode<T>(T baseShapeComponent)
            where T : class, IBaseShapeComponents, IEqualityComparer<T>
    {
        HashCode hashCode = new();

        hashCode.Add(baseShapeComponent.GetMeasureUnitCode());

        foreach (IBaseShapeComponents item in baseShapeComponent.GetBaseShapeComponents())
        {
            hashCode.Add(item);
        }

        return hashCode.ToHashCode();
    }
    #endregion
    #endregion
}
