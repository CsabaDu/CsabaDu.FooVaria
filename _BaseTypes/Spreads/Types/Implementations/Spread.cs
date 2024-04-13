namespace CsabaDu.FooVaria.BaseTypes.Spreads.Types.Implementations;

public abstract class Spread(IRootObject rootObject, string paramName) : Quantifiable(rootObject, paramName), ISpread
{
    #region Public methods
    #region Override methods
    public override Enum GetBaseMeasureUnit()
    {
        ISpreadMeasure spreadMeasure = GetSpreadMeasure();

        return spreadMeasure.GetBaseMeasureUnit();
    }

    #region Sealed methods
    public override sealed ValueType GetBaseQuantity()
    {
        return GetQuantity();
    }

    public override sealed decimal GetDefaultQuantity()
    {
        return (GetSpreadMeasure() as IBaseQuantifiable)!.GetDefaultQuantity();
    }

    public override sealed TypeCode GetQuantityTypeCode()
    {
        return base.GetQuantityTypeCode();
    }

    public override sealed bool IsExchangeableTo(Enum? context)
    {
        return (GetSpreadMeasure() as IBaseMeasure)!.IsExchangeableTo(context);
    }

    public override sealed ISpread Round(RoundingMode roundingMode)
    {
        IBaseMeasure baseMeasure = (IBaseMeasure)GetSpreadMeasure();
        ISpreadMeasure spreadMeasure = (ISpreadMeasure)baseMeasure.Round(roundingMode);

        return GetSpread(spreadMeasure);
    }
    #endregion
    #endregion

    #region Virtual methods
    //public virtual MeasureUnitCode GetSpreadMeasureUnitCode()
    //{
    //    return GetMeasureUnitCode();
    //}

    public virtual double GetQuantity()
    {
        return GetSpreadMeasure().GetQuantity();
    }
    #endregion

    #region Abstract methods
    public abstract ISpread GetSpread(ISpreadMeasure spreadMeasure);
    public abstract ISpreadMeasure GetSpreadMeasure();
    #endregion

    public void ValidateSpreadMeasure(ISpreadMeasure? spreadMeasure, string paramName)
    {
        IBaseMeasure? baseMeasure = NullChecked(spreadMeasure, paramName).GetSpreadMeasure() as IBaseMeasure;

        MeasureUnitCode measureUnitCode = baseMeasure?.GetMeasureUnitCode() ?? throw new InvalidOperationException(null);

        if (!HasMeasureUnitCode(measureUnitCode)) throw InvalidMeasureUnitCodeEnumArgumentException(measureUnitCode, paramName);

        decimal quantity = baseMeasure.GetDefaultQuantity();

        if (quantity > 0) return;

        throw QuantityArgumentOutOfRangeException(paramName, quantity);
    }
    #endregion
}
