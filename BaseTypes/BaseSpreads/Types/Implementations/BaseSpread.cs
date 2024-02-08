using CsabaDu.FooVaria.BaseTypes.Quantifiables.Behaviors;

namespace CsabaDu.FooVaria.BaseTypes.BaseSpreads.Types.Implementations;

public abstract class BaseSpread : Quantifiable<IBaseSpread>, IBaseSpread
{
    #region Constructors
    protected BaseSpread(IBaseSpread other) : base(other)
    {
    }

    protected BaseSpread(IBaseSpreadFactory factory, IBaseSpread baseSpread) : base(factory, baseSpread)
    {
    }

    protected BaseSpread(IBaseSpreadFactory factory, MeasureUnitCode measureUnitCode, params IQuantifiable[] quantifiables) : base(factory, measureUnitCode, quantifiables)
    {
    }

    protected BaseSpread(IBaseSpreadFactory factory, IBaseMeasure baseMeasure) : base(factory, baseMeasure)
    {
    }

    protected BaseSpread(IBaseSpreadFactory factory, Enum measureUnit) : base(factory, measureUnit)
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

    public override IBaseSpread? ExchangeTo(Enum? context)
    {
        IBaseMeasure? exchanged = (GetSpreadMeasure() as IBaseMeasure)?.ExchangeTo(context);

        if (exchanged is not ISpreadMeasure spreadMeasure) return null;

        return GetBaseSpread(spreadMeasure);
    }

    public override bool? FitsIn(IBaseSpread? other, LimitMode? limitMode)
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
        return GetQuantity<IBaseSpread, double>(this, quantityTypeCode);
    }
    #endregion

    #region Override methods
    public override sealed decimal GetDefaultQuantity()
    {
        return (GetSpreadMeasure() as IQuantifiable)!.GetDefaultQuantity();
    }

    public override IBaseSpreadFactory GetFactory()
    {
        return (IBaseSpreadFactory)Factory;
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
        if (NullChecked(quantifiable, paramName) is IBaseSpread baseSpread)
        {
            ValidateQuantity(baseSpread.GetQuantity(), paramName);
        }

        throw ArgumentTypeOutOfRangeException(paramName, quantifiable!);
    }
    #region Sealed methods
    #endregion
    #endregion

    #region Abstract methods
    public abstract IBaseSpread GetBaseSpread(ISpreadMeasure spreadMeasure);
    public abstract ISpreadMeasure GetSpreadMeasure();
    #endregion
    #endregion

    #region Private methods
    #region Static methods
    private static int? Compare(IBaseSpread baseSpread, IBaseSpread? other)
    {
        if (other?.HasMeasureUnitCode(baseSpread.MeasureUnitCode) != true) return null;

        return baseSpread.GetDefaultQuantity().CompareTo(other.GetDefaultQuantity());
    }
    #endregion
    #endregion
}
