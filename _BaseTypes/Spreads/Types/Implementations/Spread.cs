namespace CsabaDu.FooVaria.BaseTypes.Spreads.Types.Implementations;

public abstract class Spread(IRootObject rootObject, string paramName) : Quantifiable(rootObject, paramName), ISpread
{
    #region Public methods
    public override sealed ValueType GetBaseQuantity()
    {
        return GetQuantity();
    }

    public void ValidateSpreadMeasure(ISpreadMeasure? spreadMeasure, string paramName)
    {
        IBaseMeasure? baseMeasure = NullChecked(spreadMeasure, paramName).GetSpreadMeasure() as IBaseMeasure;

        MeasureUnitCode measureUnitCode = baseMeasure?.GetMeasureUnitCode() ?? throw new InvalidOperationException(null);

        if (!HasMeasureUnitCode(measureUnitCode)) throw InvalidMeasureUnitCodeEnumArgumentException(measureUnitCode, paramName);

        decimal quantity = baseMeasure.GetDefaultQuantity();

        if (quantity > 0) return;

        throw QuantityArgumentOutOfRangeException(paramName, quantity);
    }

    #region Virtual methods
    //public override ISpread? ExchangeTo(Enum? context)
    //{
    //    IQuantifiable? exchanged = (GetSpreadMeasure() as IQuantifiable)?.ExchangeTo(context);

    //    if (exchanged is not ISpreadMeasure spreadMeasure) return null;

    //    return GetSpread(spreadMeasure);
    //}

    public virtual MeasureUnitCode GetSpreadMeasureUnitCode()
    {
        return GetMeasureUnitCode();
    }

    public virtual double GetQuantity()
    {
        return GetSpreadMeasure().GetQuantity();
    }
    #endregion

    #region Override methods
    public override Enum GetBaseMeasureUnit()
    {
        return (GetSpreadMeasure() as IMeasurable)!.GetBaseMeasureUnit();
    }

    #region Sealed methods
    public override sealed ISpread Round(RoundingMode roundingMode)
    {
        IBaseMeasure baseMeasure = (IBaseMeasure)GetSpreadMeasure();
        ISpreadMeasure spreadMeasure = (ISpreadMeasure)baseMeasure.Round(roundingMode);

        return GetSpread(spreadMeasure);
    }

    public override sealed decimal GetDefaultQuantity()
    {
        return (GetSpreadMeasure() as IBaseQuantifiable)!.GetDefaultQuantity();
    }

    public override sealed TypeCode GetQuantityTypeCode()
    {
        return base.GetQuantityTypeCode();
    }
    #endregion
    #endregion

    #region Abstract methods
    public abstract ISpread GetSpread(ISpreadMeasure spreadMeasure);
    public abstract ISpreadMeasure GetSpreadMeasure();
    #endregion
    #endregion
}
