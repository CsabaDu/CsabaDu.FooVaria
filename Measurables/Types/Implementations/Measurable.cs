namespace CsabaDu.FooVaria.RateComponents.Types.Implementations;

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
    private protected Measurable(IMeasurableFactory factory, MeasureUnitTypeCode measureUnitTypeCode) : base(factory, measureUnitTypeCode)
    {
    }

    private protected Measurable(IMeasurableFactory factory, Enum measureUnit) : base(factory, measureUnit)
    {
    }

    private protected Measurable(IMeasurableFactory factory, IBaseMeasurable baseMeasurable) : base(factory, baseMeasurable)
    {
    }

    private protected Measurable(IMeasurable other) : base(other)
    {
    }
    #endregion

    #region Public methods
    #region Virtual methods
    public virtual TypeCode GetQuantityTypeCode()
    {
        return MeasureUnitTypeCode.GetQuantityTypeCode();
    }
    #endregion

    #region Override methods
    public override bool Equals(object? obj)
    {
        return obj is IMeasurable other
            && base.Equals(other);
    }

    public override IMeasurableFactory GetFactory()
    {
        return (IMeasurableFactory)Factory;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(typeof(IMeasurable), MeasureUnitTypeCode);
    }

    public override void ValidateMeasureUnit(Enum measureUnit, string paramName)
    {
        MeasureUnitTypes.ValidateMeasureUnit(measureUnit, paramName, MeasureUnitTypeCode);
    }

    #region Sealed methods
    public override sealed void ValidateMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode, string paramName)
    {
        if (measureUnitTypeCode == MeasureUnitTypeCode) return;

        throw InvalidMeasureUnitTypeCodeEnumArgumentException(measureUnitTypeCode, paramName);
    }
    #endregion
    #endregion

    #region Abstract methods
    //public abstract IMeasurable GetDefault();
    //public abstract IMeasurable GetMeasurable(IMeasurable other);
    #endregion
    #endregion

    #region Protected methods
    #region Static methods
    protected static T GetDefault<T>(T measurable) where T : class, IMeasurable, IRateComponent
    {
        return (measurable.GetFactory() as IBaseMeasurableFactory<T>)!.CreateDefault(measurable.MeasureUnitTypeCode);
    }

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

    public abstract TypeCode? GetQuantityTypeCode(object quantity);
    //{
    //    throw new NotImplementedException();
    //}

    public abstract void ValidateQuantityTypeCode(TypeCode quantityTypeCode, string paramName);

    #endregion
    #endregion
}
