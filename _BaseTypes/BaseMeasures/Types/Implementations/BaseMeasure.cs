namespace CsabaDu.FooVaria.BaseTypes.BaseMeasures.Types.Implementations;

/// <summary>
/// Represents an abstract base class for measures.
/// </summary>
/// <param name="rootObject">The root object associated with the measure.</param>
/// <param name="paramName">The parameter name associated with the measure.</param>
public abstract class BaseMeasure(IRootObject rootObject, string paramName) : Quantifiable(rootObject, paramName), IBaseMeasure
{
    #region Public methods
    #region Static methods
    /// <summary>
    /// Validates the quantity and its type code.
    /// </summary>
    /// <param name="quantity">The quantity to validate.</param>
    /// <param name="quantityTypeCode">The type code of the quantity.</param>
    /// <param name="paramName">The parameter name associated with the quantity.</param>
    /// <exception cref="ArgumentTypeOutOfRangeException">Thrown when the quantity type code does not match the quantity type.</exception>
    public static void ValidateQuantity(ValueType? quantity, TypeCode quantityTypeCode, string paramName)
    {
        Type quantityType = NullChecked(quantity, paramName).GetType();

        ValidateQuantityTypeCode(quantityTypeCode, nameof(quantityTypeCode));

        if (quantityTypeCode == Type.GetTypeCode(quantityType)) return;

        throw ArgumentTypeOutOfRangeException(paramName, quantity!);
    }

    /// <summary>
    /// Validates the quantity type code.
    /// </summary>
    /// <param name="quantityTypeCode">The type code to validate.</param>
    /// <param name="paramName">The parameter name associated with the type code.</param>
    /// <exception cref="InvalidQuantityTypeCodeEnumArgumentException">Thrown when the quantity type code is invalid.</exception>
    public static void ValidateQuantityTypeCode(TypeCode quantityTypeCode, string paramName)
    {
        if (GetQuantityTypeCode(quantityTypeCode) > TypeCode.Object) return;

        throw InvalidQuantityTypeCodeEnumArgumentException(quantityTypeCode, paramName);
    }
    #endregion

    #region Override methods
    #region Sealed methods
    /// <summary>
    /// Determines if the measure fits within the specified limiter.
    /// </summary>
    /// <param name="limiter">The limiter to check against.</param>
    /// <returns>True if it fits, null if the limiter is not quantifiable, otherwise false.</returns>
    public override sealed bool? FitsIn(ILimiter? limiter)
    {
        return base.FitsIn(limiter);
    }

    /// <summary>
    /// Gets the base measure unit.
    /// </summary>
    /// <returns>The base measure unit as an Enum.</returns>
    public override sealed Enum GetBaseMeasureUnit()
    {
        IBaseMeasurement baseMeasurement = GetBaseMeasurement();

        return baseMeasurement.GetBaseMeasureUnit();
    }

    /// <summary>
    /// Gets the default quantity.
    /// </summary>
    /// <returns>The default quantity as a decimal.</returns>
    public override sealed decimal GetDefaultQuantity()
    {
        object quantity = GetBaseQuantity();
        decimal exchangeRate = GetExchangeRate();

        return GetDefaultQuantity(quantity, exchangeRate);
    }

    /// <summary>
    /// Determines if the measure is exchangeable to the specified context.
    /// </summary>
    /// <param name="context">The context to check against.</param>
    /// <returns>True if exchangeable, otherwise false.</returns>
    public override sealed bool IsExchangeableTo(Enum? context)
    {
        if (!base.IsExchangeableTo(context)) return false;

        return IsValidMeasureUnit(getMeasureUnit());

        #region Local methods
        Enum getMeasureUnit()
        {
            MeasureUnitElements measureUnitElements = GetMeasureUnitElements(context, nameof(context));

            return measureUnitElements.MeasureUnit;
        }
        #endregion
    }

    /// <summary>
    /// Rounds the measure to the specified rounding mode.
    /// </summary>
    /// <param name="roundingMode">The rounding mode to use.</param>
    /// <returns>The rounded measure.</returns>
    public override sealed IBaseMeasure Round(RoundingMode roundingMode)
    {
        ValueType quantity = (ValueType)GetQuantity(roundingMode);
        IBaseMeasurement baseMeasurement = GetBaseMeasurement();

        return GetBaseMeasure(baseMeasurement, quantity);
    }

    /// <summary>
    /// Validates the measure unit.
    /// </summary>
    /// <param name="measureUnit">The measure unit to validate.</param>
    /// <param name="paramName">The parameter name associated with the measure unit.</param>
    public override sealed void ValidateMeasureUnit(Enum? measureUnit, string paramName)
    {
        GetBaseMeasurement().ValidateMeasureUnit(measureUnit, paramName);
    }
    #endregion
    #endregion

    #region Abstract methods
    /// <summary>
    /// Gets the base measurement.
    /// </summary>
    /// <returns>The base measurement.</returns>
    public abstract IBaseMeasurement GetBaseMeasurement();

    /// <summary>
    /// Gets the base measurement factory.
    /// </summary>
    /// <returns>The base measurement factory.</returns>
    public abstract IBaseMeasurementFactory GetBaseMeasurementFactory();

    /// <summary>
    /// Gets the limit mode.
    /// </summary>
    /// <returns>The limit mode.</returns>
    public abstract LimitMode? GetLimitMode();
    #endregion

    /// <summary>
    /// Determines if two base measures are equal.
    /// </summary>
    /// <param name="x">The first base measure.</param>
    /// <param name="y">The second base measure.</param>
    /// <returns>True if equal, otherwise false.</returns>
    public bool Equals(IBaseMeasure? x, IBaseMeasure? y)
    {
        return x is null == y is null
            && x?.GetRateComponentCode() == y?.GetRateComponentCode()
            && x?.GetLimitMode() == y?.GetLimitMode()
            && x?.Equals(y) != false;
    }

    /// <summary>
    /// Gets the base measure with the specified quantity.
    /// </summary>
    /// <param name="quantity">The quantity to use.</param>
    /// <returns>The base measure.</returns>
    public IBaseMeasure GetBaseMeasure(ValueType quantity)
    {
        IBaseMeasurement baseMeasurement = GetBaseMeasurement();

        return GetBaseMeasure(baseMeasurement, quantity);
    }

    /// <summary>
    /// Gets the base measure with the specified base measurement and quantity.
    /// </summary>
    /// <param name="baseMeasurement">The base measurement to use.</param>
    /// <param name="quantity">The quantity to use.</param>
    /// <returns>The base measure.</returns>
    public IBaseMeasure GetBaseMeasure(IBaseMeasurement baseMeasurement, ValueType quantity)
    {
        _ = NullChecked(baseMeasurement, nameof(baseMeasurement));
        ValidateQuantity(quantity, nameof(quantity));

        IBaseMeasureFactory factory = GetBaseMeasureFactory();

        return factory.CreateBaseMeasure(baseMeasurement, quantity);
    }

    /// <summary>
    /// Gets the exchange rate.
    /// </summary>
    /// <returns>The exchange rate as a decimal.</returns>
    public decimal GetExchangeRate()
    {
        IBaseMeasurement baseMeasurement = GetBaseMeasurement();

        return baseMeasurement.GetExchangeRate();
    }

    /// <summary>
    /// Gets the hash code for the specified base measure.
    /// </summary>
    /// <param name="other">The base measure to get the hash code for.</param>
    /// <returns>The hash code.</returns>
    public int GetHashCode([DisallowNull] IBaseMeasure other)
    {
        return HashCode.Combine(other.GetRateComponentCode(), other.GetLimitMode(), other.GetHashCode());
    }

    /// <summary>
    /// Gets the quantity type code for the specified quantity.
    /// </summary>
    /// <param name="quantity">The quantity to get the type code for.</param>
    /// <returns>The quantity type code.</returns>
    public TypeCode GetQuantityTypeCode(object? quantity)
    {
        TypeCode quantityTypeCode = Type.GetTypeCode(quantity?.GetType());

        return GetQuantityTypeCode(quantityTypeCode);
    }

    /// <summary>
    /// Gets the rate component code.
    /// </summary>
    /// <returns>The rate component code.</returns>
    public RateComponentCode GetRateComponentCode()
    {
        IBaseMeasureFactory factory = GetBaseMeasureFactory();

        return factory.RateComponentCode;
    }

    /// <summary>
    /// Validates the exchange rate.
    /// </summary>
    /// <param name="exchangeRate">The exchange rate to validate.</param>
    /// <param name="paramName">The parameter name associated with the exchange rate.</param>
    public void ValidateExchangeRate(decimal exchangeRate, string paramName)
    {
        IBaseMeasurement baseMeasurement = GetBaseMeasurement();

        baseMeasurement.ValidateExchangeRate(exchangeRate, paramName);
    }
    #endregion

    #region Private methods
    /// <summary>
    /// Gets the base measure factory.
    /// </summary>
    /// <returns>The base measure factory.</returns>
    private IBaseMeasureFactory GetBaseMeasureFactory()
    {
        return (IBaseMeasureFactory)GetFactory();
    }

    #region Static methods
    /// <summary>
    /// Gets the quantity type code.
    /// </summary>
    /// <param name="quantityTypeCode">The type code to get.</param>
    /// <returns>The quantity type code.</returns>
    private static TypeCode GetQuantityTypeCode(TypeCode quantityTypeCode)
    {
        if (quantityTypeCode == TypeCode.Empty || QuantityTypeCodes.Contains(quantityTypeCode))
        {
            return quantityTypeCode;
        }

        if (quantityTypeCode.IsDefined()) return TypeCode.Object;

        throw InvalidQuantityTypeCodeEnumArgumentException(quantityTypeCode);
    }
    #endregion
    #endregion
}
