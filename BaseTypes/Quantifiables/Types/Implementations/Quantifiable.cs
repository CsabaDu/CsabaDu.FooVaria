namespace CsabaDu.FooVaria.BaseTypes.Quantifiables.Types.Implementations;

public abstract class Quantifiable : Measurable, IQuantifiable
{
    #region Constants
    private const int QuantityRoundingDecimals = 8;
    #endregion

    #region Constructors
    #region Static constructor
    static Quantifiable()
    {
        QuantityTypeSet = new HashSet<Type>()
        {
            typeof(int),
            typeof(uint),
            typeof(long),
            typeof(ulong),
            typeof(double),
            typeof(decimal),
        };
    }
    #endregion

    protected Quantifiable(IQuantifiable other) : base(other)
    {
    }

    protected Quantifiable(IQuantifiableFactory factory, MeasureUnitCode measureUnitCode) : base(factory, measureUnitCode)
    {
    }

    protected Quantifiable(IQuantifiableFactory factory, Enum measureUnit) : base(factory, measureUnit)
    {
    }

    protected Quantifiable(IQuantifiableFactory factory, IMeasurable measurable) : base(factory, measurable)
    {
    }

    protected Quantifiable(IQuantifiableFactory factory, IQuantifiable quantifiable) : base(factory, quantifiable)
    {
    }

    protected Quantifiable(IQuantifiableFactory factory, MeasureUnitCode measureUnitCode, params IQuantifiable[] quantifiables) : base(factory, measureUnitCode, quantifiables)
    {
    }
    #endregion

    #region Properties
    #region Static properties
    public static ISet<Type> QuantityTypeSet { get; }
    #endregion
    #endregion

    #region Public methods
    #region Override methods
    public override bool Equals(object? obj)
    {
        return obj is IQuantifiable other
            && base.Equals(other)
            && GetDefaultQuantity() == other.GetDefaultQuantity();
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(MeasureUnitCode, GetDefaultQuantity());
    }

    public override void ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, string paramName)
    {
        if (GetMeasureUnitCodes().Contains(measureUnitCode)) return;
    }

    #endregion

    #region Abstract methods
    public abstract decimal GetDefaultQuantity();
    public abstract void ValidateQuantity(ValueType? quantity, string paramName);
    #endregion

    #region Static methods
    public static decimal RoundQuantity(decimal quantity)
    {
        return decimal.Round(quantity, QuantityRoundingDecimals);
    }

    public static double RoundQuantity(double quantity)
    {
        return Math.Round(quantity, QuantityRoundingDecimals);
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
    protected static TNum GetQuantity<TNum>(IQuantity<TNum> quantifiable)
        where TNum : struct
    {
        TypeCode quantityTypeCode = GetQuantityTypeCode(quantifiable);

        return (TNum)quantifiable.GetQuantity(quantityTypeCode);
    }

    protected static TypeCode? GetQuantityTypeCode(ValueType? quantity)
    {
        Type? quantityType = quantity?.GetType();

        return QuantityTypeSet.Any(x => x == quantityType) ?
            Type.GetTypeCode(quantityType)
            : null;
    }

    protected static TypeCode GetQuantityTypeCode<TNum>(IQuantity<TNum> quantifiable)
        where TNum : struct 
    {
        return Type.GetTypeCode(typeof(TNum));

    }

    protected static decimal GetDefaultQuantity(object quantity, decimal exchangeRate)
    {
        decimal defaultQuantity = Convert.ToDecimal(quantity) * exchangeRate;

        return RoundQuantity(defaultQuantity);
    }
    #endregion
    #endregion
}

