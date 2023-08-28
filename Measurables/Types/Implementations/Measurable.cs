namespace CsabaDu.FooVaria.Measurables.Types.Implementations;

internal abstract class Measurable : BaseMeasurable, IMeasurable
{
    #region Constructors
    private protected Measurable(IMeasurableFactory measurableFactory, MeasureUnitTypeCode measureUnitTypeCode) : base(measureUnitTypeCode)
    {
        MeasurableFactory = NullChecked(measurableFactory, nameof(measurableFactory));
    }

    private protected Measurable(IMeasurableFactory measurableFactory, Enum measureUnit) : base(measureUnit)
    {
        MeasurableFactory = NullChecked(measurableFactory, nameof(measurableFactory));
    }

    private protected Measurable(IMeasurableFactory measurableFactory, IMeasurable measurable) : base(measurable)
    {
        MeasurableFactory = NullChecked(measurableFactory, nameof(measurableFactory));
    }

    private protected Measurable(IMeasurable measurable) : base(measurable)
    {
        MeasurableFactory = measurable.MeasurableFactory;
    }
    #endregion

    #region Properties
    public IMeasurableFactory MeasurableFactory { get; init; }
    #endregion

    #region Public methods
    public override bool Equals(object? obj)
    {
        return obj is IMeasurable other
            && MeasureUnitTypeCode == other.MeasureUnitTypeCode
            && MeasurableFactory.Equals(other.MeasurableFactory);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(MeasureUnitTypeCode, MeasurableFactory);
    }

    public IMeasurable GetMeasurable(IMeasurable? measurable = null)
    {
        return MeasurableFactory.Create(measurable ?? this);
    }

    public virtual IMeasurable GetMeasurable(IMeasurableFactory measurableFactory, IMeasurable measurable)
    {
        return NullChecked(measurableFactory, nameof(measurableFactory)).Create(measurable);
    }

    public virtual TypeCode GetQuantityTypeCode(MeasureUnitTypeCode? measureUnitTypeCode = null)
    {
        TypeCode? quantityTypeCode = (measureUnitTypeCode ?? MeasureUnitTypeCode).GetQuantityTypeCode();

        return quantityTypeCode ?? throw InvalidMeasureUnitTypeCodeEnumArgumentException(measureUnitTypeCode!.Value);
    }

    #region Abstract methods
    public abstract IMeasurable GetDefault();
    #endregion
    #endregion
}
