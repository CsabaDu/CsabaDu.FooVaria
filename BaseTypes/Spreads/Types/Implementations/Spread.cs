namespace CsabaDu.FooVaria.BaseTypes.Spreads.Types.Implementations;

public abstract class Spread : Quantifiable<ISpread>, ISpread
{
    #region Constructors
    protected Spread(ISpread other) : base(other)
    {
    }

    protected Spread(ISpreadFactory factory, ISpread spread) : base(factory, spread)
    {
    }

    protected Spread(ISpreadFactory factory, MeasureUnitCode measureUnitCode, params IQuantifiable[] quantifiables) : base(factory, measureUnitCode, quantifiables)
    {
    }

    protected Spread(ISpreadFactory factory, IBaseMeasure baseMeasure) : base(factory, baseMeasure)
    {
    }

    protected Spread(ISpreadFactory factory, Enum measureUnit) : base(factory, measureUnit)
    {
    }
    #endregion

    #region Properties
    public decimal DefaultQuantity { get; init; }
    #endregion

    #region Public methods
    public void ValidateSpreadMeasure(ISpreadMeasure? spreadMeasure, string paramName)
    {
        IBaseMeasure? baseMeasure = NullChecked(spreadMeasure, paramName).GetSpreadMeasure() as IBaseMeasure;

        MeasureUnitCode measureUnitCode = baseMeasure?.MeasureUnitCode ?? throw new InvalidOperationException(null);

        if (!HasMeasureUnitCode(measureUnitCode)) throw InvalidMeasureUnitCodeEnumArgumentException(measureUnitCode, paramName);

        decimal quantity = baseMeasure.GetDefaultQuantity();

        ValidateQuantity(quantity, paramName);
    }

    #region Virtual methods

    public override ISpread? ExchangeTo(Enum? context)
    {
        IBaseMeasure? exchanged = (GetSpreadMeasure() as IBaseMeasure)?.ExchangeTo(context);

        if (exchanged is not ISpreadMeasure spreadMeasure) return null;

        return GetSpread(spreadMeasure);
    }

    public override bool? FitsIn(ISpread? other, LimitMode? limitMode)
    {
        if (other == null && limitMode == null) return true;

        if (other == null) return null;

        int? comparison = Compare(this, other);

        if (comparison == null) return null;

        return comparison.Value.FitsIn(limitMode ?? LimitMode.BeNotGreater);
    }

    public virtual MeasureUnitCode GetSpreadMeasureUnitCode()
    {
        return MeasureUnitCode;
    }

    public virtual double GetQuantity()
    {
        return GetSpreadMeasure().GetQuantity();
    }

    public object GetQuantity(TypeCode quantityTypeCode)
    {
        return GetQuantity<ISpread, double>(this, quantityTypeCode);
    }
    #endregion

    #region Override methods
    public override sealed decimal GetDefaultQuantity()
    {
        return (GetSpreadMeasure() as IQuantifiable)!.GetDefaultQuantity();
    }

    public override ISpreadFactory GetFactory()
    {
        return (ISpreadFactory)Factory;
    }

    public override Enum GetMeasureUnit()
    {
        return (GetSpreadMeasure() as IMeasurable)!.GetMeasureUnit();
    }

    public override IEnumerable<MeasureUnitCode> GetMeasureUnitCodes()
    {
        yield return MeasureUnitCode.AreaUnit;
        yield return MeasureUnitCode.VolumeUnit;
    }

    public override sealed void ValidateQuantity(IQuantifiable? quantifiable, string paramName)
    {
        if (NullChecked(quantifiable, paramName) is ISpread spread)
        {
            ValidateQuantity(spread.GetQuantity(), paramName);
        }

        throw ArgumentTypeOutOfRangeException(paramName, quantifiable!);
    }
    #region Sealed methods
    #endregion
    #endregion

    #region Abstract methods
    public abstract ISpread GetSpread(ISpreadMeasure spreadMeasure);
    public abstract ISpreadMeasure GetSpreadMeasure();
    #endregion
    #endregion

    #region Private methods
    #region Static methods
    private static int? Compare(ISpread spread, ISpread? other)
    {
        if (other?.HasMeasureUnitCode(spread.MeasureUnitCode) != true) return null;

        return spread.GetDefaultQuantity().CompareTo(other.GetDefaultQuantity());
    }
    #endregion
    #endregion
}
