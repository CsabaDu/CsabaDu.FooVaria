namespace CsabaDu.FooVaria.BaseTypes.BaseQuantifiables.Types.Implementations;

public abstract class BaseQuantifiable(IRootObject rootObject, string paramName) : Measurable(rootObject, paramName), IBaseQuantifiable
{
    #region Constructors
    #region Static constructor
    static BaseQuantifiable()
    {
        QuantityTypeSet =
        [
            //typeof(int),
            //typeof(uint),
            typeof(long),
            typeof(ulong),
            typeof(double),
            typeof(decimal),
        ];

        QuantityTypeCodes = QuantityTypeSet.Select(Type.GetTypeCode);
    }
    #endregion
    #endregion

    #region Properties
    #region Static properties
    public static HashSet<Type> QuantityTypeSet { get; }
    public static IEnumerable<TypeCode> QuantityTypeCodes {  get; }
    #endregion
    #endregion

    #region Public methods
    #region Override methods
    public override bool Equals(object? obj)
    {
        return obj is IBaseQuantifiable other
            && base.Equals(other)
            && GetDefaultQuantity() == other.GetDefaultQuantity();
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(GetMeasureUnitCode(), GetDefaultQuantity());
    }
    #endregion

    #region Virtual methods    
    public virtual bool? FitsIn(ILimiter? limiter)
    {
        if (limiter is null) return true;

        return limiter is IBaseQuantifiable other ?
            DefaultQuantitiesFit(this, other, limiter?.GetLimitMode())
            : null;
    }

    public virtual void ValidateQuantity(ValueType? quantity, string paramName)
    {
        _ = ConvertQuantity(quantity, paramName, GetQuantityTypeCode());
    }
    #endregion

    #region Abstract methods
    public abstract decimal GetDefaultQuantity();
    #endregion

    #region Static methods
    public static object ConvertQuantity(ValueType? quantity, string paramName, TypeCode quantityTypeCode)
    {
        object? exchanged = NullChecked(quantity, paramName).ToQuantity(Defined(quantityTypeCode, nameof(quantityTypeCode)));

        return exchanged ?? throw QuantityArgumentOutOfRangeException(paramName, quantity);
    }

    public static decimal GetDefaultQuantitySquare(IBaseQuantifiable baseQuantifiable)
    {
        decimal quantity = NullChecked(baseQuantifiable, nameof(baseQuantifiable)).GetDefaultQuantity();

        return quantity * quantity;
    }

    public static IEnumerable<TypeCode> GetQuantityTypeCodes()
    {
        return QuantityTypeSet.Select(Type.GetTypeCode);
    }
    #endregion
    #endregion

    #region Protected methods
    #region Static methods
    protected static bool? DefaultQuantitiesFit(IBaseQuantifiable baseQuantifiable, IBaseQuantifiable other, LimitMode? limitMode)
    {
        if (!baseQuantifiable.HasMeasureUnitCode(other.GetMeasureUnitCode())) return null;

        decimal quantity = baseQuantifiable.GetDefaultQuantity();
        decimal otherQuantity = other.GetDefaultQuantity();

        return quantity.FitsIn(otherQuantity, limitMode);
    }

    protected static decimal GetDefaultQuantity(object quantity, decimal exchangeRate)
    {
        return (Convert.ToDecimal(quantity) * exchangeRate)
            .Round(RoundingMode.DoublePrecision);
    }

    protected static object GetQuantity<T, TNum>(T baseQuantifiable, TypeCode quantityTypeCode)
        where T : class, IBaseQuantifiable, IQuantity<TNum>
        where TNum : struct
    {
        ValueType quantity = (ValueType)(object)baseQuantifiable.GetQuantity();

        return quantity.ToQuantity(quantityTypeCode) ?? throw InvalidQuantityTypeCodeEnumArgumentException(quantityTypeCode);
    }

    protected static bool IsValidMeasureUnitCode(IMeasureUnitCodes measureUnitCodes, MeasureUnitCode measureUnitCode)
    {
        return measureUnitCodes.GetMeasureUnitCodes().Contains(measureUnitCode);
    }

    protected static void ValidateMeasureUnitCode(IMeasureUnitCodes measureUnitCodes, MeasureUnitCode measureUnitCode, string paramName)
    {
        if (measureUnitCodes.GetMeasureUnitCodes().Contains(measureUnitCode)) return;

        throw InvalidMeasureUnitCodeEnumArgumentException(measureUnitCode, paramName);
    }

    //protected static void ValidateMeasureUnitCodes(IMeasureUnitCodes measureUnitCodes, IBaseQuantifiable? baseQuantifiable, string paramName)
    //{
    //    if (baseQuantifiable is IMeasureUnitCodes other)
    //    {
    //        foreach (MeasureUnitCode item in other.GetMeasureUnitCodes())
    //        {
    //            validateMeasureUnitCode(item);
    //        }
    //    }
    //    else
    //    {
    //        MeasureUnitCode measureUnitCode = NullChecked(baseQuantifiable, paramName).GetMeasureUnitCode();

    //        validateMeasureUnitCode(measureUnitCode);
    //    }


    //    #region Local methods
    //    void validateMeasureUnitCode(MeasureUnitCode measureUnitCode)
    //    {
    //        ValidateMeasureUnitCode(measureUnitCodes, measureUnitCode, paramName);
    //    }
    //    #endregion
    //}

    protected static void ValidateMeasureUnitCodes(IMeasureUnitCodes measureUnitCodes, IMeasureUnitCodes other, string paramName)
    {
        foreach (MeasureUnitCode item in other.GetMeasureUnitCodes())
        {
            ValidateMeasureUnitCode(measureUnitCodes, item, paramName);
        }
    }

    protected static void ValidatePositiveQuantity(ValueType? quantity, string paramName)
    {
        _ = GetValidPositiveQuantity(quantity, paramName);
    }

    protected static decimal GetValidPositiveQuantity(ValueType? quantity, string paramName)
    {
        decimal converted = (decimal)ConvertQuantity(quantity, paramName, TypeCode.Decimal);

        if (converted > 0) return converted;

        throw QuantityArgumentOutOfRangeException(paramName, quantity);
    }
    #endregion
    #endregion
}
