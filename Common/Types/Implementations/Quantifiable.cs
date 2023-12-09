namespace CsabaDu.FooVaria.Common.Types.Implementations;

public abstract class Quantifiable : Measurable, IQuantifiable
{
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

    public virtual TypeCode GetQuantityTypeCode()
    {
        return MeasureUnitTypeCode.GetQuantityTypeCode();
    }

    public abstract decimal GetDefaultQuantity();
    public abstract void ValidateQuantity(ValueType? quantity, string paramName);

    protected static TypeCode? GetQuantityTypeCode(ValueType quantity)
    {
        Type? quantityType = quantity?.GetType();

        return GetQuantityTypes().Any(x => x == quantityType) ?
            Type.GetTypeCode(quantityType)
            : null;
    }

    protected static TypeCode GetQuantityTypeCode<TNum>(IQuantity<TNum> quantifiable) where TNum : struct 
    {
        return Type.GetTypeCode(typeof(TNum));

    }

}

