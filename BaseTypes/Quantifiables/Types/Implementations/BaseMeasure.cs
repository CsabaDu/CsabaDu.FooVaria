namespace CsabaDu.FooVaria.Quantifiables.Types.Implementations;

public abstract class BaseMeasure : Quantifiable, IBaseMeasure
{
    #region Constructors
    protected BaseMeasure(IBaseMeasureFactory factory, MeasureUnitCode measureUnitCode) : base(factory, measureUnitCode)
    {
    }

    protected BaseMeasure(IBaseMeasureFactory factory, Enum measureUnit) : base(factory, measureUnit)
    {
    }

    protected BaseMeasure(IBaseMeasureFactory factory, IBaseMeasurement baseMeasurement) : base(factory, baseMeasurement)
    {
    }

    protected BaseMeasure(IBaseMeasureFactory factory, IBaseMeasure baseMeasure) : base(factory, baseMeasure)
    {
    }

    protected BaseMeasure(IBaseMeasure other) : base(other)
    {
    }
    #endregion

    #region Properties
    #region Abstract properties
    public abstract decimal DefaultQuantity { get; init; }
    #endregion
    #endregion

    #region Public methods
    public IBaseMeasure GetBaseMeasure(Enum measureUnit, ValueType quantity)
    {
        return GetFactory().CreateBaseMeasure(measureUnit, quantity);
    }

    public void ValidateQuantifiable(IQuantifiable? quantifiable, string paramName)
    {
        ValidateQuantity(NullChecked(quantifiable, paramName).GetDefaultQuantity(), paramName);
    }

    #region Override methods
    public override IBaseMeasureFactory GetFactory()
    {
        return (IBaseMeasureFactory)Factory;
    }

    #region Sealed methods
    public override sealed decimal GetDefaultQuantity()
    {
        return DefaultQuantity;
    }
    #endregion
    #endregion
    #endregion
}
