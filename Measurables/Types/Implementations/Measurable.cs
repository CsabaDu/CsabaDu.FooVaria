namespace CsabaDu.FooVaria.Measurables.Types.Implementations;

internal abstract class Measurable : BaseMeasurable, IMeasurable
{
    #region Enums
    protected enum SummingMode
    {
        Add,
        Subtract,
    }
    #endregion

    #region Constructors
    private protected Measurable(IMeasurableFactory measurableFactory, MeasureUnitTypeCode measureUnitTypeCode) : base(measureUnitTypeCode)
    {
        MeasurableFactory = NullChecked(measurableFactory, nameof(measurableFactory));
    }

    private protected Measurable(IMeasurableFactory factory, Enum measureUnit) : base(measureUnit)
    {
        MeasurableFactory = NullChecked(factory, nameof(factory));
    }

    private protected Measurable(IMeasurableFactory measurableFactory, IMeasurable measurable) : base(measurable)
    {
        MeasurableFactory = NullChecked(measurableFactory, nameof(measurableFactory));
    }

    private protected Measurable(IMeasurable other) : base(other)
    {
        MeasurableFactory = other.MeasurableFactory;
    }
    #endregion

    #region Properties
    public IMeasurableFactory MeasurableFactory { get; init; }
    #endregion

    #region Public methods
    public IMeasurable GetMeasurable(IMeasurable measurable)
    {
        return MeasurableFactory.Create(measurable);
    }

    public IMeasurable GetMeasurable(IMeasurableFactory measurableFactory, IMeasurable measurable)
    {
        return NullChecked(measurableFactory, nameof(measurableFactory)).Create(measurable);
    }

    #region Virtual methods
    public virtual TypeCode GetQuantityTypeCode()
    {
        return MeasureUnitTypeCode.GetQuantityTypeCode();
    }
    #endregion

    #region Overriden methods
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

    public override sealed IEnumerable<MeasureUnitTypeCode> GetMeasureUnitTypeCodes()
    {
        return base.GetMeasureUnitTypeCodes();
    }

    public override sealed void ValidateMeasureUnit(Enum measureUnit)
    {
        base.ValidateMeasureUnit(measureUnit);
    }

    public override sealed void ValidateMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode)
    {
        if (measureUnitTypeCode == MeasureUnitTypeCode) return;

        throw InvalidMeasureUnitTypeCodeEnumArgumentException(measureUnitTypeCode);
    }
    #endregion

    #region Abstract methods
    public abstract IMeasurable GetDefault();
    #endregion
    #endregion

    #region Protected methods
    protected static IMeasure GetSum(IMeasure measure, IMeasure? other, SummingMode summingMode)
    {
        if (other == null) return measure.GetMeasure(measure);

        if (other.IsExchangeableTo(measure.MeasureUnitTypeCode))
        {
            decimal quantity = getDefaultQuantitySum() / measure.GetExchangeRate();

            return measure.GetMeasure(quantity);
        }

        throw new ArgumentOutOfRangeException(nameof(other), other.MeasureUnitTypeCode, null);

        #region Local methods
        decimal getDefaultQuantitySum()
        {
            decimal thisQuantity = measure.DefaultQuantity;
            decimal otherQuantity = other!.DefaultQuantity;

            return summingMode switch
            {
                SummingMode.Add => decimal.Add(thisQuantity, otherQuantity),
                SummingMode.Subtract => decimal.Subtract(thisQuantity, otherQuantity),

                _ => throw new InvalidOperationException(null),
            };
        }
        #endregion
    }
    #endregion
}
