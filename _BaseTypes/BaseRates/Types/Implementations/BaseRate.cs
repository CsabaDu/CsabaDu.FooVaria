namespace CsabaDu.FooVaria.BaseTypes.BaseRates.Types.Implementations;

/// <summary>
/// Represents the base rate class which provides common functionality for rate calculations.
/// </summary>
/// <param name="rootObject">The root object.</param>
/// <param name="paramName">The parameter name.</param>
public abstract class BaseRate(IRootObject rootObject, string paramName) : BaseQuantifiable(rootObject, paramName), IBaseRate
{
    #region Public methods
    #region Static methods
    /// <summary>
    /// Gets the rate component codes.
    /// </summary>
    /// <returns>An enumerable collection of rate component codes.</returns>
    public static IEnumerable<RateComponentCode> GetRateComponentCodes()
    {
        return Enum.GetValues<RateComponentCode>();
    }
    #endregion

    #region Override methods
    #region Sealed methods
    /// <summary>
    /// Determines whether the specified object is equal to the current object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
    public override sealed bool Equals(object? obj)
    {
        return obj is IBaseRate baseRate && Equals(baseRate);
    }

    /// <summary>
    /// Determines whether the current object fits within the specified limiter.
    /// </summary>
    /// <param name="limiter">The limiter to check against.</param>
    /// <returns>true if the current object fits within the limiter; otherwise, null.</returns>
    public override sealed bool? FitsIn(ILimiter? limiter)
    {
        if (limiter is null) return true;

        if (limiter is not IBaseRate baseRate) return null;

        return FitsIn(baseRate, limiter?.GetLimitMode());
    }

    /// <summary>
    /// Serves as the default hash function.
    /// </summary>
    /// <returns>A hash code for the current object.</returns>
    public override sealed int GetHashCode()
    {
        return HashCode.Combine(GetNumeratorCode(), GetDefaultQuantity(), GetDenominatorCode());
    }

    /// <summary>
    /// Gets the type code of the quantity.
    /// </summary>
    /// <returns>The type code of the quantity.</returns>
    public override sealed TypeCode GetQuantityTypeCode()
    {
        Type quantityType = GetQuantity().GetType();

        return Type.GetTypeCode(quantityType);
    }

    /// <summary>
    /// Determines whether the specified measure unit code is present.
    /// </summary>
    /// <param name="measureUnitCode">The measure unit code to check.</param>
    /// <returns>true if the measure unit code is present; otherwise, false.</returns>
    public override sealed bool HasMeasureUnitCode(MeasureUnitCode measureUnitCode)
    {
        return GetMeasureUnitCodes().Contains(measureUnitCode);
    }

    /// <summary>
    /// Validates the specified measure unit.
    /// </summary>
    /// <param name="measureUnit">The measure unit to validate.</param>
    /// <param name="paramName">The name of the parameter.</param>
    public override sealed void ValidateMeasureUnit(Enum? measureUnit, string paramName)
    {
        base.ValidateMeasureUnit(measureUnit, paramName);
    }

    /// <summary>
    /// Validates the specified measure unit code.
    /// </summary>
    /// <param name="measureUnitCode">The measure unit code to validate.</param>
    /// <param name="paramName">The name of the parameter.</param>
    public override sealed void ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, string paramName)
    {
        ValidateMeasureUnitCode(this, measureUnitCode, paramName);
    }

    /// <summary>
    /// Validates the specified quantity.
    /// </summary>
    /// <param name="quantity">The quantity to validate.</param>
    /// <param name="paramName">The name of the parameter.</param>
    public override sealed void ValidateQuantity(ValueType? quantity, string paramName)
    {
        base.ValidateQuantity(quantity, paramName);
    }
    #endregion
    #endregion

    #region Virtual methods
    /// <summary>
    /// Gets the limit mode.
    /// </summary>
    /// <returns>The limit mode.</returns>
    public virtual LimitMode? GetLimitMode()
    {
        return null;
    }
    #endregion

    #region Abstract methods
    /// <summary>
    /// Gets the rate component for the specified rate component code.
    /// </summary>
    /// <param name="rateComponentCode">The rate component code.</param>
    /// <returns>The rate component.</returns>
    public abstract object? GetRateComponent(RateComponentCode rateComponentCode);
    #endregion

    /// <summary>
    /// Compares the current object with another object of the same type.
    /// </summary>
    /// <param name="other">The object to compare with the current object.</param>
    /// <returns>A value that indicates the relative order of the objects being compared.</returns>
    public int CompareTo(IBaseRate? other)
    {
        if (other is null) return 1;

        ValidateMeasureUnitCodes(other, nameof(other));

        return GetDefaultQuantity().CompareTo(other.GetDefaultQuantity());
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current object.
    /// </summary>
    /// <param name="other">The object to compare with the current object.</param>
    /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
    public bool Equals(IBaseRate? other)
    {
        return base.Equals(other)
            && other.GetDenominatorCode() == GetDenominatorCode();
    }

    /// <summary>
    /// Determines whether the current object fits within the specified base rate and limit mode.
    /// </summary>
    /// <param name="other">The base rate to check against.</param>
    /// <param name="limitMode">The limit mode to check against.</param>
    /// <returns>true if the current object fits within the base rate and limit mode; otherwise, null.</returns>
    public bool? FitsIn(IBaseRate? other, LimitMode? limitMode)
    {
        if (other is null && !limitMode.HasValue) return true;

        if (!IsExchangeableTo(other)) return null;

        int comparison = CompareTo(other);

        LimitMode limitModeValue = limitMode ?? LimitMode.BeNotGreater;

        if (!limitModeValue.IsDefined()) return null;

        return comparison.FitsIn(limitModeValue);
    }

    /// <summary>
    /// Gets the base quantity.
    /// </summary>
    /// <returns>The base quantity.</returns>
    public ValueType GetBaseQuantity()
    {
        return GetQuantity();
    }

    /// <summary>
    /// Gets the base rate for the specified numerator and denominator.
    /// </summary>
    /// <param name="numerator">The numerator.</param>
    /// <param name="denominator">The denominator.</param>
    /// <returns>The base rate.</returns>
    public IBaseRate GetBaseRate(IQuantifiable numerator, Enum denominator)
    {
        return GetBaseRateFactory().CreateBaseRate(numerator, denominator);
    }

    /// <summary>
    /// Gets the base rate for the specified numerator and measurable denominator.
    /// </summary>
    /// <param name="numerator">The numerator.</param>
    /// <param name="denominator">The measurable denominator.</param>
    /// <returns>The base rate.</returns>
    public IBaseRate GetBaseRate(IQuantifiable numerator, IMeasurable denominator)
    {
        return GetBaseRateFactory().CreateBaseRate(numerator, denominator);
    }

    /// <summary>
    /// Gets the base rate for the specified numerator and quantifiable denominator.
    /// </summary>
    /// <param name="numerator">The numerator.</param>
    /// <param name="denominator">The quantifiable denominator.</param>
    /// <returns>The base rate.</returns>
    public IBaseRate GetBaseRate(IQuantifiable numerator, IQuantifiable denominator)
    {
        return GetBaseRateFactory().CreateBaseRate(numerator, denominator);
    }

    /// <summary>
    /// Gets the denominator code.
    /// </summary>
    /// <returns>The denominator code.</returns>
    public MeasureUnitCode GetDenominatorCode()
    {
        return GetMeasureUnitCode(RateComponentCode.Denominator);
    }

    /// <summary>
    /// Gets the measure unit code for the specified rate component code.
    /// </summary>
    /// <param name="rateComponentCode">The rate component code.</param>
    /// <returns>The measure unit code.</returns>
    public MeasureUnitCode GetMeasureUnitCode(RateComponentCode rateComponentCode)
    {
        return GetMeasureUnitCode(this, rateComponentCode) ?? throw InvalidRateComponentCodeEnumArgumentException(rateComponentCode);
    }

    /// <summary>
    /// Gets the measure unit codes.
    /// </summary>
    /// <returns>An enumerable collection of measure unit codes.</returns>
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

    /// <summary>
    /// Gets the numerator code.
    /// </summary>
    /// <returns>The numerator code.</returns>
    public MeasureUnitCode GetNumeratorCode()
    {
        return GetMeasureUnitCode(RateComponentCode.Numerator);
    }

    /// <summary>
    /// Gets the quantity.
    /// </summary>
    /// <returns>The quantity.</returns>
    public decimal GetQuantity()
    {
        return GetDefaultQuantity();
    }

    /// <summary>
    /// Gets the quantity for the specified type code.
    /// </summary>
    /// <param name="quantityTypeCode">The type code of the quantity.</param>
    /// <returns>The quantity.</returns>
    public object GetQuantity(TypeCode quantityTypeCode)
    {
        object? quantity = GetQuantity().ToQuantity(quantityTypeCode);

        if (quantity is not null) return quantity;

        throw InvalidQuantityTypeCodeEnumArgumentException(quantityTypeCode);
    }

    /// <summary>
    /// Determines whether the current object is exchangeable to the specified base rate.
    /// </summary>
    /// <param name="baseRate">The base rate to check against.</param>
    /// <returns>true if the current object is exchangeable to the base rate; otherwise, false.</returns>
    public bool IsExchangeableTo(IBaseRate? baseRate)
    {
        return baseRate?.GetNumeratorCode() == GetNumeratorCode()
            && baseRate.GetDenominatorCode() == GetDenominatorCode();
    }

    /// <summary>
    /// Determines whether the specified rate component is valid for the specified rate component code.
    /// </summary>
    /// <param name="rateComponent">The rate component to check.</param>
    /// <param name="rateComponentCode">The rate component code.</param>
    /// <returns>true if the rate component is valid; otherwise, false.</returns>
    public bool IsValidRateComponent(object? rateComponent, RateComponentCode rateComponentCode)
    {
        return GetRateComponent(rateComponentCode)?.Equals(rateComponent) == true;
    }

    /// <summary>
    /// Gets the proportional value to the specified base rate.
    /// </summary>
    /// <param name="other">The base rate to compare with.</param>
    /// <returns>The proportional value.</returns>
    public decimal ProportionalTo(IBaseRate? other)
    {
        const string paramName = nameof(other);

        ValidateMeasureUnitCodes(other, paramName);

        decimal defaultQuantity = other!.GetDefaultQuantity();

        if (defaultQuantity == 0) throw QuantityArgumentOutOfRangeException(paramName, defaultQuantity);

        return Math.Abs(GetDefaultQuantity() / defaultQuantity);
    }

    /// <summary>
    /// Validates the measure unit codes for the specified measure unit codes.
    /// </summary>
    /// <param name="measureUnitCodes">The measure unit codes to validate.</param>
    /// <param name="paramName">The name of the parameter.</param>
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

    /// <summary>
    /// Validates the specified rate component code.
    /// </summary>
    /// <param name="rateComponentCode">The rate component code to validate.</param>
    /// <param name="paramName">The name of the parameter.</param>
    public void ValidateRateComponentCode(RateComponentCode rateComponentCode, string paramName)
    {
        object? rateComponent = GetRateComponent(Defined(rateComponentCode, paramName));

        if (rateComponent is not null) return;

        throw InvalidRateComponentCodeEnumArgumentException(rateComponentCode);
    }
    #endregion

    #region Private methods
    #region Static methods
    /// <summary>
    /// Gets the measure unit code for the specified base rate and rate component code.
    /// </summary>
    /// <param name="baseRate">The base rate.</param>
    /// <param name="rateComponentCode">The rate component code.</param>
    /// <returns>The measure unit code.</returns>
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

    /// <summary>
    /// Gets the base rate factory.
    /// </summary>
    /// <returns>The base rate factory.</returns>
    private IBaseRateFactory GetBaseRateFactory()
    {
        return (IBaseRateFactory)GetFactory();
    }
    #endregion
}
