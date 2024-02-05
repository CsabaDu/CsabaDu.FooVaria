namespace CsabaDu.FooVaria.BaseTypes.BaseSpreads.Types.Implementations;

public abstract class BaseSpread : Quantifiable, IBaseSpread
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
    public virtual int CompareTo(IBaseSpread? other)
    {
        if (other == null) return 1;

        return Compare(this, other) ?? throw ArgumentTypeOutOfRangeException(nameof(other), other);
    }

    public virtual bool Equals(IBaseSpread? other)
    {
        return base.Equals(other);
    }

    public virtual bool? FitsIn(IBaseSpread? baseSpread, LimitMode? limitMode)
    {
        if (baseSpread == null && limitMode == null) return true;

        if (baseSpread == null) return null;

        int? comparison = Compare(this, baseSpread);

        if (comparison == null) return null;

        limitMode ??= LimitMode.BeNotGreater;

        return comparison.Value.FitsIn(limitMode);
    }

    public override decimal GetDefaultQuantity()
    {
        return (GetSpreadMeasure() as IBaseMeasure ?? throw new InvalidOperationException(null)).GetDefaultQuantity();
    }

    public override Enum GetMeasureUnit()
    {
        return (GetSpreadMeasure() as IMeasurable)!.GetMeasureUnit();
    }

    public virtual MeasureUnitCode GetMeasureUnitCode()
    {
        return MeasureUnitCode;
    }

    public virtual double GetQuantity()
    {
        return GetSpreadMeasure().GetQuantity();
    }

    public virtual bool IsExchangeableTo(Enum? context)
    {
        if (context == null) return false;

        if (context is MeasureUnitCode measureUnitCode) return hasMeasureUnitCode();

        if (!IsDefinedMeasureUnit(context)) return false;

        measureUnitCode = GetMeasureUnitCode(context);

        return hasMeasureUnitCode();

        #region Local methods
        bool hasMeasureUnitCode()
        {
            return HasMeasureUnitCode(measureUnitCode);
        }
        #endregion
    }

    public virtual decimal ProportionalTo(IBaseSpread other)
    {
        ValidateSpreadMeasure(other, nameof(other));

        return GetDefaultQuantity() / other.GetDefaultQuantity();
    }

    public void ValidateSpreadMeasure(ISpreadMeasure? spreadMeasure, string paramName)
    {
        IBaseMeasure? baseMeasure = NullChecked(spreadMeasure, paramName).GetSpreadMeasure() as IBaseMeasure;

        MeasureUnitCode measureUnitCode = baseMeasure?.MeasureUnitCode ?? throw new InvalidOperationException(null);

        if (!HasMeasureUnitCode(measureUnitCode)) throw InvalidMeasureUnitCodeEnumArgumentException(measureUnitCode, paramName);

        decimal quantity = baseMeasure.GetDefaultQuantity();

        ValidateQuantity(quantity, paramName);
    }

    #region Override methods
    public override IBaseSpreadFactory GetFactory()
    {
        return (IBaseSpreadFactory)Factory;
    }

    public override IEnumerable<MeasureUnitCode> GetMeasureUnitCodes()
    {
        yield return MeasureUnitCode.AreaUnit;
        yield return MeasureUnitCode.VolumeUnit;
    }

    #region Sealed methods
    public override sealed TypeCode GetQuantityTypeCode()
    {
        return GetQuantityTypeCode(this);
    }
    #endregion
    #endregion

    #region Abstract methods
    public virtual IBaseSpread? ExchangeTo(Enum measureUnit)
    {
        IBaseMeasure? exchanged = (GetSpreadMeasure() as IBaseMeasure)?.ExchangeTo(measureUnit);

        if (exchanged is not ISpreadMeasure spreadMeasure) return null;

        return GetBaseSpread(spreadMeasure);
    }
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

    public object GetQuantity(TypeCode quantityTypeCode)
    {
        return GetQuantity<IBaseSpread, double>(this, quantityTypeCode);
    }
    #endregion
    #endregion
}
