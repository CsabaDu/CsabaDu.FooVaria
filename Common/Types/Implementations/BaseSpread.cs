using CsabaDu.FooVaria.Common.Statics;

namespace CsabaDu.FooVaria.Common.Types.Implementations;

public abstract class BaseSpread : Quantifiable, IBaseSpread
{
    #region Constructors
    protected BaseSpread(IBaseSpread other) : base(other)
    {
    }

    protected BaseSpread(IBaseSpreadFactory factory, IBaseSpread baseSpread) : base(factory, baseSpread)
    {
    }

    //protected BaseSpread(IBaseSpreadFactory factory, IBaseMeasure baseMeasure) : base(factory, baseMeasure)
    //{
    //}

    protected BaseSpread(IBaseSpreadFactory factory, MeasureUnitTypeCode measureUnitTypeCode, params IMeasurable[] measurables) : base(factory, measureUnitTypeCode, measurables)
    {
    }

    protected BaseSpread(IBaseSpreadFactory factory, IBaseMeasure baseMeasure) : base(factory, baseMeasure)
    {
    }

    protected BaseSpread(IBaseSpreadFactory factory, Enum measureUnit) : base(factory, measureUnit)
    {
    }

    #endregion

    #region Public methods
    public int CompareTo(IBaseSpread? other)
    {
        if (other == null) return 1;

        return Compare(this, other) ?? throw ArgumentTypeOutOfRangeException(nameof(other), other);
    }

    public bool Equals(IBaseSpread? other)
    {
        return base.Equals(other);
    }

    public bool? FitsIn(IBaseSpread? baseSpread, LimitMode? limitMode)
    {
        if (baseSpread == null && limitMode == null) return true;

        if (baseSpread == null) return null;

        int? comparison = Compare(this, baseSpread);

        if (comparison == null) return null;

        limitMode ??= LimitMode.BeNotGreater;

        return comparison.Value.FitsIn(limitMode);
    }

    public override sealed decimal GetDefaultQuantity()
    {
        return (GetSpreadMeasure() as IBaseMeasure ?? throw new InvalidOperationException(null)).DefaultQuantity;
    }

    public MeasureUnitTypeCode GetMeasureUnitTypeCode()
    {
        return MeasureUnitTypeCode;
    }

    public double GetQuantity()
    {
        return GetSpreadMeasure().GetQuantity();
    }

    public bool IsExchangeableTo(Enum? context)
    {
        if (context == null) return false;

        if (context is MeasureUnitTypeCode measureUnitTypeCode) return hasMeasureUnitTypeCode();

        if (!IsDefinedMeasureUnit(context)) return false;

        measureUnitTypeCode = MeasureUnitTypes.GetMeasureUnitTypeCode(context);

        return hasMeasureUnitTypeCode();

        #region Local methods
        bool hasMeasureUnitTypeCode()
        {
            return HasMeasureUnitTypeCode(measureUnitTypeCode);
        }
        #endregion
    }

    public decimal ProportionalTo(IBaseSpread other)
    {
        ValidateSpreadMeasure(other, nameof(other));

        return GetDefaultQuantity() / other.GetDefaultQuantity();
    }

    public void ValidateQuantifiable(IQuantifiable? quantifiable, string paramName)
    {
        throw new NotImplementedException();
    }

    public void ValidateSpreadMeasure(ISpreadMeasure? spreadMeasure, string paramName)
    {
        IBaseMeasure baseMeasure = NullChecked(spreadMeasure, paramName).GetSpreadMeasure() as IBaseMeasure ?? throw new InvalidOperationException(null);

        MeasureUnitTypeCode measureUnitTypeCode = baseMeasure.MeasureUnitTypeCode;

        if (measureUnitTypeCode != MeasureUnitTypeCode) throw InvalidMeasureUnitTypeCodeEnumArgumentException(measureUnitTypeCode, paramName);

        decimal quantity = baseMeasure.DefaultQuantity;

        if (quantity > 0) return;

        throw QuantityArgumentOutOfRangeException(paramName, quantity);
    }

    #region Override methods
    public override IBaseSpreadFactory GetFactory()
    {
        return (IBaseSpreadFactory)Factory;
    }

    public override IEnumerable<MeasureUnitTypeCode> GetMeasureUnitTypeCodes()
    {
        yield return MeasureUnitTypeCode.AreaUnit;
        yield return MeasureUnitTypeCode.VolumeUnit;
    }

    #region Sealed methods
    public override sealed TypeCode GetQuantityTypeCode()
    {
        return GetQuantityTypeCode(this);
    }
    #endregion
    #endregion

    #region Abstract methods
    public abstract IBaseSpread? ExchangeTo(Enum measureUnit);
    public abstract IBaseSpread GetBaseSpread(ISpreadMeasure spreadMeasure);
    public abstract ISpreadMeasure GetSpreadMeasure();
    #endregion
    #endregion

    #region Private methods
    #region Static methods
    private static int? Compare(IBaseSpread baseSpread, IBaseSpread? other)
    {
        if (other == null) return null;

        if (baseSpread.MeasureUnitTypeCode != other.MeasureUnitTypeCode) return null;

        return baseSpread.GetDefaultQuantity().CompareTo(other.GetDefaultQuantity());
    }
    #endregion
    #endregion
}
