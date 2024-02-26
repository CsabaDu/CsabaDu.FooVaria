namespace CsabaDu.FooVaria.BaseTypes.BaseQuantifiables.Types.Implementations;

public abstract class BaseQuantifiable : Measurable, IBaseQuantifiable
{
    #region Constructors
    #region Static constructor
    static BaseQuantifiable()
    {
        QuantityTypeSet =
        [
            typeof(int),
            typeof(uint),
            typeof(long),
            typeof(ulong),
            typeof(double),
            typeof(decimal),
        ];
    }
    #endregion

    protected BaseQuantifiable(IBaseQuantifiable other) : base(other)
    {
    }

    protected BaseQuantifiable(IBaseQuantifiableFactory factory) : base(factory)
    {
    }
    #endregion

    #region Properties
    #region Static properties
    public static HashSet<Type> QuantityTypeSet { get; }
    #endregion
    #endregion

    #region Public methods
    public bool IsValidMeasureUnitCode(MeasureUnitCode measureUnitCode)
    {
        return GetMeasureUnitCodes().Contains(measureUnitCode);
    }

    #region Override methods
    public override bool Equals(object? obj)
    {
        return base.Equals(obj)
            && obj is IBaseQuantifiable other
            && GetDefaultQuantity() == other.GetDefaultQuantity();
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(GetMeasureUnitCode(), GetDefaultQuantity());
    }

    public override IBaseQuantifiableFactory GetFactory()
    {
        return (IBaseQuantifiableFactory)Factory;
    }
    #endregion

    #region Virtual methods    
    public virtual IEnumerable<MeasureUnitCode> GetMeasureUnitCodes()
    {
        yield return GetMeasureUnitCode();
    }

    public virtual void ValidateQuantity(ValueType? quantity, string paramName)
    {
        _ = ConvertQuantity(quantity, paramName, GetQuantityTypeCode());
    }

    public virtual void ValidateQuantity(IBaseQuantifiable? baseQuantifiable, string paramName)
    {
        decimal quantity = TypeChecked(baseQuantifiable, paramName, GetType())!.GetDefaultQuantity();

        ValidatePositiveQuantity(quantity, paramName);
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
        foreach (Type item in QuantityTypeSet)
        {
            yield return Type.GetTypeCode(item);
        }
    }
    #endregion
    #endregion

    #region Protected methods
    #region Static methods
    protected static decimal GetDefaultQuantity(object quantity, decimal exchangeRate)
    {
        decimal defaultQuantity = Convert.ToDecimal(quantity) * exchangeRate;

        return defaultQuantity.Round(RoundingMode.DoublePrecision);
    }

    protected static object GetQuantity<T, TNum>(T baseQuantifiable, TypeCode quantityTypeCode)
        where T : class, IBaseQuantifiable, IQuantity<TNum>
        where TNum : struct
    {
        ValueType quantity = (ValueType)(object)baseQuantifiable.GetQuantity();

        return quantity.ToQuantity(quantityTypeCode) ?? throw InvalidQuantityTypeCodeEnumArgumentException(quantityTypeCode);
    }

    protected static void ValidateMeasureUnitCode(IBaseQuantifiable baseQuantifiable, MeasureUnitCode measureUnitCode, string paramName)
    {
        if (baseQuantifiable.GetMeasureUnitCodes().Contains(measureUnitCode)) return;

        throw InvalidMeasureUnitCodeEnumArgumentException(measureUnitCode, paramName);
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
