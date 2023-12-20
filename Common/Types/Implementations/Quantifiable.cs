namespace CsabaDu.FooVaria.Common.Types.Implementations;

public abstract class Quantifiable : Measurable, IQuantifiable
{
    #region Constructors
    protected Quantifiable(IQuantifiable other) : base(other)
    {
    }

    protected Quantifiable(IQuantifiableFactory factory, MeasureUnitTypeCode measureUnitTypeCode) : base(factory, measureUnitTypeCode)
    {
    }

    protected Quantifiable(IQuantifiableFactory factory, Enum measureUnit) : base(factory, measureUnit)
    {
    }

    protected Quantifiable(IQuantifiableFactory factory, IMeasurable measurable) : base(factory, measurable)
    {
    }

    protected Quantifiable(IQuantifiableFactory factory, MeasureUnitTypeCode measureUnitTypeCode, params IMeasurable[] measurables) : base(factory, measureUnitTypeCode, measurables)
    {
    }
    #endregion

    #region Public methods
    #region Abstract methods
    public abstract decimal GetDefaultQuantity();
    public abstract void ValidateQuantity(ValueType? quantity, string paramName);
    #endregion
    #endregion

    #region Protected methods
    #region Static methods
    protected static TypeCode? GetQuantityTypeCode(ValueType quantity)
    {
        Type? quantityType = quantity?.GetType();

        return GetQuantityTypes().Any(x => x == quantityType) ?
            Type.GetTypeCode(quantityType)
            : null;
    }

    protected static TypeCode GetQuantityTypeCode<TNum>(IQuantity<TNum> quantifiable)
        where TNum : struct 
    {
        return Type.GetTypeCode(typeof(TNum));

    }
    #endregion
    #endregion
}

