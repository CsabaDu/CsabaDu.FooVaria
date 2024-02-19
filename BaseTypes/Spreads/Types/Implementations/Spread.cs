﻿
namespace CsabaDu.FooVaria.BaseTypes.Spreads.Types.Implementations;

public abstract class Spread : Quantifiable, ISpread
{
    #region Constructors
    protected Spread(ISpread other) : base(other)
    {
    }

    protected Spread(ISpreadFactory factory) : base(factory)
    {
    }

    //protected Spread(ISpreadFactory factory, ISpread spread) : base(factory, spread)
    //{
    //}

    //protected Spread(ISpreadFactory factory, MeasureUnitCode measureUnitCode, params IShapeComponent[] shapeComponents) : base(factory, measureUnitCode, shapeComponents)
    //{
    //}

    //protected Spread(ISpreadFactory factory, IBaseMeasure baseMeasure) : base(factory, baseMeasure)
    //{
    //}

    //protected Spread(ISpreadFactory factory, Enum measureUnit) : base(factory, measureUnit)
    //{
    //}
    #endregion

    //#region Properties
    //public decimal DefaultQuantity { get; init; }
    //#endregion

    #region Public methods
    public void ValidateSpreadMeasure(ISpreadMeasure? spreadMeasure, string paramName)
    {
        IBaseMeasure? baseMeasure = NullChecked(spreadMeasure, paramName).GetSpreadMeasure() as IBaseMeasure;

        MeasureUnitCode measureUnitCode = baseMeasure?.GetMeasureUnitCode() ?? throw new InvalidOperationException(null);

        if (!HasMeasureUnitCode(measureUnitCode)) throw InvalidMeasureUnitCodeEnumArgumentException(measureUnitCode, paramName);

        decimal quantity = baseMeasure.GetDefaultQuantity();

        ValidateQuantity(quantity, paramName);
    }

    #region Virtual methods

    public override ISpread? ExchangeTo(Enum? context)
    {
        IQuantifiable? exchanged = (GetSpreadMeasure() as IQuantifiable)?.ExchangeTo(context);

        if (exchanged is not ISpreadMeasure spreadMeasure) return null;

        return GetSpread(spreadMeasure);
    }

    public override bool? FitsIn(IQuantifiable? other, LimitMode? limitMode)
    {
        if (other == null && limitMode == null) return true;

        if (other == null) return null;

        if (!other.HasMeasureUnitCode(GetMeasureUnitCode())) return null;

        int? comparison = GetDefaultQuantity().CompareTo(other.GetDefaultQuantity());

        return comparison.HasValue ?
            comparison.Value.FitsIn(limitMode ?? LimitMode.BeNotGreater)
            : null;
    }

    public virtual MeasureUnitCode GetSpreadMeasureUnitCode()
    {
        return GetMeasureUnitCode();
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
        return (GetSpreadMeasure() as IBaseQuantifiable)!.GetDefaultQuantity();
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

    #region Sealed methods
    public override sealed ISpread Round(RoundingMode roundingMode)
    {
        IBaseMeasure baseMeasure = (IBaseMeasure)GetSpreadMeasure();

        ISpreadMeasure spreadMeasure = (ISpreadMeasure)baseMeasure.Round(roundingMode);

        return GetSpread(spreadMeasure);
    }
    public override sealed object GetQuantity(RoundingMode roundingMode)
    {
        IBaseMeasure spreadMeasure = (IBaseMeasure)GetSpreadMeasure();

        return spreadMeasure.GetQuantity(roundingMode);
    }

    public override sealed void ValidateQuantity(IBaseQuantifiable? baseQuantifiable, string paramName)
    {
        if (NullChecked(baseQuantifiable, paramName) is ISpread spread)
        {
            ValidateQuantity(spread.GetQuantity(), paramName);
        }

        throw ArgumentTypeOutOfRangeException(paramName, baseQuantifiable!);
    }
    #endregion
    #endregion

    #region Abstract methods
    public abstract ISpread GetSpread(ISpreadMeasure spreadMeasure);
    public abstract ISpreadMeasure GetSpreadMeasure();
    #endregion
    #endregion

    #region Private methods
    #region Static methods
    private static int? Compare(ISpread spread, IQuantifiable? other)
    {
        if (other?.HasMeasureUnitCode(spread.GetMeasureUnitCode()) != true) return null;

        return spread.GetDefaultQuantity().CompareTo(other.GetDefaultQuantity());
    }
    #endregion
    #endregion
}
