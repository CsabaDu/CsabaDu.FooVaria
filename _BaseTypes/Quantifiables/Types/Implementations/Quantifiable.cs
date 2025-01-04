namespace CsabaDu.FooVaria.BaseTypes.Quantifiables.Types.Implementations;

/// <summary>
/// Represents an abstract base class for quantifiable objects.
/// </summary>
/// <param name="rootObject">The root object associated with the quantifiable.</param>
/// <param name="paramName">The parameter name associated with the quantifiable.</param>
public abstract class Quantifiable(IRootObject rootObject, string paramName) : BaseQuantifiable(rootObject, paramName), IQuantifiable
{
    #region Public methods
    #region Override methods
    /// <summary>
    /// Determines if the current quantifiable fits within the specified limiter.
    /// </summary>
    /// <param name="limiter">The limiter to check against.</param>
    /// <returns>True if it fits, null if the limiter is not quantifiable, otherwise false.</returns>
    public override bool? FitsIn(ILimiter? limiter)
    {
        if (limiter is null) return true;

        return limiter is IQuantifiable quantifiable ?
            FitsIn(quantifiable, limiter.GetLimitMode())
            : null;
    }

    #region Sealed methods
    //public override sealed MeasureUnitCode GetMeasureUnitCode()
    //{
    //    return base.GetMeasureUnitCode();
    //}

    /// <summary>
    /// Validates the measure unit code.
    /// </summary>
    /// <param name="measureUnitCode">The measure unit code to validate.</param>
    /// <param name="paramName">The parameter name associated with the measure unit code.</param>
    public override sealed void ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, string paramName)
    {
        base.ValidateMeasureUnitCode(measureUnitCode, paramName);
    }
    #endregion
    #endregion

    #region Virtual methods
    /// <summary>
    /// Determines if the current quantifiable is exchangeable to the specified context.
    /// </summary>
    /// <param name="context">The context to check against.</param>
    /// <returns>True if exchangeable, otherwise false.</returns>
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
    /// <summary>
    /// Gets the base quantity of the quantifiable.
    /// </summary>
    /// <returns>The base quantity as a ValueType.</returns>
    public abstract ValueType GetBaseQuantity();

    /// <summary>
    /// Rounds the quantifiable to the specified rounding mode.
    /// </summary>
    /// <param name="roundingMode">The rounding mode to use.</param>
    /// <returns>The rounded quantifiable.</returns>
    public abstract IQuantifiable Round(RoundingMode roundingMode);

    /// <summary>
    /// Tries to exchange the quantifiable to the specified context.
    /// </summary>
    /// <param name="context">The context to exchange to.</param>
    /// <param name="exchanged">The exchanged quantifiable if successful.</param>
    /// <returns>True if the exchange was successful, otherwise false.</returns>
    public abstract bool TryExchangeTo(Enum context, [NotNullWhen(true)] out IQuantifiable? exchanged);
    #endregion

    /// <summary>
    /// Compares the current quantifiable to another quantifiable.
    /// </summary>
    /// <param name="other">The other quantifiable to compare to.</param>
    /// <returns>An integer indicating the relative order of the quantifiables.</returns>
    public int CompareTo(IQuantifiable? other)
    {
        if (other is null) return 1;

        ValidateMeasureUnitCode(other.GetMeasureUnitCode(), nameof(other));

        return GetDefaultQuantity().CompareTo(other.GetDefaultQuantity());
    }

    /// <summary>
    /// Determines if the current quantifiable is equal to another quantifiable.
    /// </summary>
    /// <param name="other">The other quantifiable to compare to.</param>
    /// <returns>True if equal, otherwise false.</returns>
    public bool Equals(IQuantifiable? other)
    {
        return base.Equals(other);
    }

    /// <summary>
    /// Determines if the current quantifiable fits within another quantifiable based on the specified limit mode.
    /// </summary>
    /// <param name="other">The other quantifiable to check against.</param>
    /// <param name="limitMode">The limit mode to use.</param>
    /// <returns>True if it fits, null if the limit mode is not defined, otherwise false.</returns>
    public bool? FitsIn(IQuantifiable? other, LimitMode? limitMode)
    {
        if (other is null) return limitMode.HasValue ? null : true;

        return (limitMode ??= LimitMode.BeNotGreater).IsDefined() ?
            DefaultQuantitiesFit(this, other, limitMode)
            : null;
    }

    /// <summary>
    /// Gets the decimal quantity of the quantifiable.
    /// </summary>
    /// <returns>The decimal quantity.</returns>
    public decimal GetDecimalQuantity()
    {
        return (decimal)GetQuantity(TypeCode.Decimal);
    }

    /// <summary>
    /// Gets a new quantifiable with the specified measure unit code and default quantity.
    /// </summary>
    /// <param name="measureUnitCode">The measure unit code to use.</param>
    /// <param name="defaultQuantity">The default quantity to use.</param>
    /// <returns>The new quantifiable.</returns>
    public IQuantifiable GetQuantifiable(MeasureUnitCode measureUnitCode, decimal defaultQuantity)
    {
        IQuantifiableFactory factory = (IQuantifiableFactory)GetFactory();

        return factory.CreateQuantifiable(measureUnitCode, defaultQuantity);
    }

    /// <summary>
    /// Gets the quantity of the quantifiable based on the specified rounding mode.
    /// </summary>
    /// <param name="roundingMode">The rounding mode to use.</param>
    /// <returns>The quantity as an object.</returns>
    public object GetQuantity(RoundingMode roundingMode)
    {
        if (!Enum.IsDefined(typeof(RoundingMode), roundingMode)) throw InvalidRoundingModeEnumArgumentException(roundingMode);

        ValueType quantity = GetBaseQuantity();

        return quantity.Round(roundingMode)!;
    }

    /// <summary>
    /// Gets the quantity of the quantifiable based on the specified type code.
    /// </summary>
    /// <param name="quantityTypeCode">The type code to use.</param>
    /// <returns>The quantity as an object.</returns>
    public object GetQuantity(TypeCode quantityTypeCode)
    {
        ValueType quantity = GetBaseQuantity();

        return quantity.ToQuantity(quantityTypeCode) ?? throw InvalidQuantityTypeCodeEnumArgumentException(quantityTypeCode);
    }

    /// <summary>
    /// Calculates the proportion of the current quantifiable to another quantifiable.
    /// </summary>
    /// <param name="other">The other quantifiable to compare to.</param>
    /// <returns>The proportion as a decimal.</returns>
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
    /// <summary>
    /// Determines if two base shape components are equal.
    /// </summary>
    /// <typeparam name="T">The type of the base shape component.</typeparam>
    /// <param name="x">The first base shape component.</param>
    /// <param name="y">The second base shape component.</param>
    /// <returns>True if equal, otherwise false.</returns>
    protected static bool Equals<T>(T? x, T? y)
        where T : class, IBaseShapeComponent
    {
        return x is null == y is null
            && (y is null || x!.HasMeasureUnitCode(y.GetMeasureUnitCode())
            && x!.GetBaseShapeComponents().SequenceEqual(y.GetBaseShapeComponents()));
    }

    /// <summary>
    /// Gets the hash code for a base shape component.
    /// </summary>
    /// <typeparam name="T">The type of the base shape component.</typeparam>
    /// <param name="baseShapeComponent">The base shape component.</param>
    /// <returns>The hash code.</returns>
    protected static int GetHashCode<T>(T baseShapeComponent)
            where T : class, IBaseShapeComponent, IEqualityComparer<T>
    {
        HashCode hashCode = new();

        hashCode.Add(baseShapeComponent.GetMeasureUnitCode());

        foreach (IBaseShapeComponent item in baseShapeComponent.GetBaseShapeComponents())
        {
            hashCode.Add(item);
        }

        return hashCode.ToHashCode();
    }
    #endregion
    #endregion
}
