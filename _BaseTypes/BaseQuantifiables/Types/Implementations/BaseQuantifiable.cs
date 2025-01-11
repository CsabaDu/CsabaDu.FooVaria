namespace CsabaDu.FooVaria.BaseTypes.BaseQuantifiables.Types.Implementations;

public abstract class BaseQuantifiable(IRootObject rootObject, string paramName) : Measurable(rootObject, paramName), IBaseQuantifiable
{
    #region Constructors
    #region Static constructor
    /// <summary>
    /// Initializes static members of the <see cref="BaseQuantifiable"/> class.
    /// </summary>
    static BaseQuantifiable()
    {
        QuantityTypes =
        [
            typeof(long),
            typeof(ulong),
            typeof(double),
            typeof(decimal),
        ];

        QuantityTypeCodes = GetQuantityTypeCodes();
    }
    #endregion
    #endregion

    #region Properties
    #region Static properties
    /// <summary>
    /// Gets the set of quantity types.
    /// </summary>
    public static IEnumerable<Type> QuantityTypes { get; }

    /// <summary>
    /// Gets the collection of quantity type codes.
    /// </summary>
    public static IEnumerable<TypeCode> QuantityTypeCodes { get; }
    #endregion
    #endregion

    #region Public methods
    #region Override methods
    /// <summary>
    /// Determines whether the specified object is equal to the current object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
    public override bool Equals(object? obj)
    {
        return obj is IBaseQuantifiable other
            && base.Equals(other)
            && GetDefaultQuantity() == other.GetDefaultQuantity();
    }

    /// <summary>
    /// Serves as the default hash function.
    /// </summary>
    /// <returns>A hash code for the current object.</returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(GetMeasureUnitCode(), GetDefaultQuantity());
    }
    #endregion

    #region Virtual methods    
    /// <summary>
    /// Determines whether the current object fits within the specified limiter.
    /// </summary>
    /// <param name="limiter">The limiter to check against.</param>
    /// <returns>true if the current object fits within the limiter; otherwise, null.</returns>
    public virtual bool? FitsIn(ILimiter? limiter)
    {
        if (limiter is null) return true;

        return limiter is IBaseQuantifiable other ?
            DefaultQuantitiesFit(this, other, limiter?.GetLimitMode())
            : null;
    }

    /// <summary>
    /// Validates the specified quantity.
    /// </summary>
    /// <param name="quantity">The quantity to validate.</param>
    /// <param name="paramName">The name of the parameter.</param>
    public virtual void ValidateQuantity(ValueType? quantity, string paramName)
    {
        _ = ConvertQuantity(quantity, paramName, GetQuantityTypeCode());
    }
    #endregion

    #region Abstract methods
    /// <summary>
    /// Gets the default quantity.
    /// </summary>
    /// <returns>The default quantity.</returns>
    public abstract decimal GetDefaultQuantity();
    #endregion

    #region Static methods
    /// <summary>
    /// Converts the specified quantity to the specified type code.
    /// </summary>
    /// <param name="quantity">The quantity to convert.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <param name="quantityTypeCode">The type code to convert to.</param>
    /// <returns>The converted quantity.</returns>
    public static object ConvertQuantity(ValueType? quantity, string paramName, TypeCode quantityTypeCode)
    {
        object? exchanged = NullChecked(quantity, paramName).ToQuantity(Defined(quantityTypeCode, nameof(quantityTypeCode)));

        return exchanged ?? throw QuantityArgumentOutOfRangeException(paramName, quantity);
    }

    /// <summary>
    /// Gets the square of the default quantity.
    /// </summary>
    /// <param name="baseQuantifiable">The base quantifiable object.</param>
    /// <returns>The square of the default quantity.</returns>
    public static decimal GetDefaultQuantitySquare(IBaseQuantifiable baseQuantifiable)
    {
        decimal quantity = NullChecked(baseQuantifiable, nameof(baseQuantifiable)).GetDefaultQuantity();

        return quantity * quantity;
    }

    /// <summary>
    /// Gets the collection of quantity type codes.
    /// </summary>
    /// <returns>An enumerable collection of quantity type codes.</returns>
    public static IEnumerable<TypeCode> GetQuantityTypeCodes()
    {
        return QuantityTypes.Select(Type.GetTypeCode);
    }
    #endregion
    #endregion

    #region Protected methods
    #region Static methods
    /// <summary>
    /// Determines whether the default quantities fit within the specified limit mode.
    /// </summary>
    /// <param name="baseQuantifiable">The base quantifiable object.</param>
    /// <param name="other">The other base quantifiable object.</param>
    /// <param name="limitMode">The limit mode to check against.</param>
    /// <returns>true if the default quantities fit within the limit mode; otherwise, null.</returns>
    protected static bool? DefaultQuantitiesFit(IBaseQuantifiable baseQuantifiable, IBaseQuantifiable other, LimitMode? limitMode)
    {
        if (!baseQuantifiable.HasMeasureUnitCode(other.GetMeasureUnitCode())) return null;

        decimal quantity = baseQuantifiable.GetDefaultQuantity();
        decimal otherQuantity = other.GetDefaultQuantity();

        return quantity.FitsIn(otherQuantity, limitMode);
    }

    /// <summary>
    /// Gets the default quantity for the specified quantity and exchange rate.
    /// </summary>
    /// <param name="quantity">The quantity.</param>
    /// <param name="exchangeRate">The exchange rate.</param>
    /// <returns>The default quantity.</returns>
    protected static decimal GetDefaultQuantity(object quantity, decimal exchangeRate)
    {
        return (Convert.ToDecimal(quantity) * exchangeRate)
            .Round(RoundingMode.DoublePrecision);
    }

    ///// <summary>
    ///// Gets the quantity for the specified base quantifiable object and type code.
    ///// </summary>
    ///// <typeparam name="T">The type of the base quantifiable object.</typeparam>
    ///// <typeparam name="TNum">The type of the quantity.</typeparam>
    ///// <param name="baseQuantifiable">The base quantifiable object.</param>
    ///// <param name="quantityTypeCode">The type code of the quantity.</param>
    ///// <returns>The quantity.</returns>
    //protected static object GetQuantity<T, TNum>(T baseQuantifiable, TypeCode quantityTypeCode)
    //    where T : class, IBaseQuantifiable, IQuantity<TNum>
    //    where TNum : struct
    //{
    //    ValueType quantity = (ValueType)(object)baseQuantifiable.GetQuantity();

    //    return quantity.ToQuantity(quantityTypeCode) ?? throw InvalidQuantityTypeCodeEnumArgumentException(quantityTypeCode);
    //}

    ///// <summary>
    ///// Determines whether the specified measure unit code is valid.
    ///// </summary>
    ///// <param name="measureUnitCodes">The measure unit codes.</param>
    ///// <param name="measureUnitCode">The measure unit code to check.</param>
    ///// <returns>true if the measure unit code is valid; otherwise, false.</returns>
    //protected static bool IsValidMeasureUnitCode(IMeasureUnitCodes measureUnitCodes, MeasureUnitCode measureUnitCode)
    //{
    //    return measureUnitCodes.GetMeasureUnitCodes().Contains(measureUnitCode);
    //}

    /// <summary>
    /// Validates the specified measure unit code.
    /// </summary>
    /// <param name="measureUnitCodes">The measure unit codes.</param>
    /// <param name="measureUnitCode">The measure unit code to validate.</param>
    /// <param name="paramName">The name of the parameter.</param>
    protected static void ValidateMeasureUnitCode(IMeasureUnitCodes measureUnitCodes, MeasureUnitCode measureUnitCode, string paramName)
    {
        if (measureUnitCodes.GetMeasureUnitCodes().Contains(measureUnitCode)) return;

        throw InvalidMeasureUnitCodeEnumArgumentException(measureUnitCode, paramName);
    }

    /// <summary>
    /// Validates the measure unit codes for the specified other measure unit codes.
    /// </summary>
    /// <param name="measureUnitCodes">The measure unit codes.</param>
    /// <param name="other">The other measure unit codes.</param>
    /// <param name="paramName">The name of the parameter.</param>
    protected static void ValidateMeasureUnitCodes(IMeasureUnitCodes measureUnitCodes, IMeasureUnitCodes other, string paramName)
    {
        foreach (MeasureUnitCode item in other.GetMeasureUnitCodes())
        {
            ValidateMeasureUnitCode(measureUnitCodes, item, paramName);
        }
    }

    /// <summary>
    /// Validates that the specified quantity is positive.
    /// </summary>
    /// <param name="quantity">The quantity to validate.</param>
    /// <param name="paramName">The name of the parameter.</param>
    protected static void ValidatePositiveQuantity(ValueType? quantity, string paramName)
    {
        _ = GetValidPositiveQuantity(quantity, paramName);
    }

    /// <summary>
    /// Gets the valid positive quantity for the specified quantity.
    /// </summary>
    /// <param name="quantity">The quantity to validate.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <returns>The valid positive quantity.</returns>
    protected static decimal GetValidPositiveQuantity(ValueType? quantity, string paramName)
    {
        decimal converted = (decimal)ConvertQuantity(quantity, paramName, TypeCode.Decimal);

        if (converted > 0) return converted;

        throw QuantityArgumentOutOfRangeException(paramName, quantity);
    }
    #endregion
    #endregion
}
