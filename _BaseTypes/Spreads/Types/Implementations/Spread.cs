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
        double quantity = GetQuantity().Round(roundingMode);
        Enum measureUnit = GetBaseMeasureUnit();
        ISpreadFactory factory = GetSpreadFactory();
        ISpreadMeasure spreadMeasure = factory.CreateSpreadMeasure(measureUnit, quantity)!;

        return GetSpread(spreadMeasure);
    }
    #endregion
    #endregion

    #region Virtual methods
    public virtual double GetQuantity()
    {
        return GetSpreadMeasure().GetQuantity();
    }
    #endregion

    #region Abstract methods
    public abstract ISpreadMeasure GetSpreadMeasure();
    #endregion

    public ISpread GetSpread(ISpreadMeasure spreadMeasure)
    {
        ValidateSpreadMeasure(spreadMeasure, nameof(spreadMeasure));

        ISpreadFactory factory = GetSpreadFactory();

        return factory.CreateSpread(NullChecked(spreadMeasure, nameof(spreadMeasure)));
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
    #endregion

    #region Private methods
    private ISpreadFactory GetSpreadFactory()
    {
        return (ISpreadFactory)GetFactory();
    }
    #endregion
}
