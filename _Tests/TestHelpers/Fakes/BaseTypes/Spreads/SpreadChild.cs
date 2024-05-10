namespace CsabaDu.FooVaria.Tests.TestHelpers.Fakes.BaseTypes.Spreads;

public class SpreadChild(IRootObject rootObject, string paramName) : Spread(rootObject, paramName)
{
    #region Members

    // Spread(IRootObject rootObject, string paramName)
    // ISpread ISpread.GetSpread(ISpreadMeasure spreadMeasure)
    // IQuantifiable IQuantifiable.GetQuantifiable(MeasureUnitCode measureUnitCode, decimal defaultQuantity)
    // void IBaseQuantifiable.ValidateQuantity(ValueType? quantity, string paramName)
    // void IMeasurable.ValidateMeasureUnitCode(IMeasurable? measurable, string paramName)
    // IFactory ICommonBase.GetFactoryValue()
    // Enum IDefaultMeasureUnit.GetDefaultMeasureUnit()
    // IEnumerable<string> IDefaultMeasureUnit.GetDefaultMeasureUnitNames()
    // void IDefaultMeasureUnit.ValidateMeasureUnit(Enum? measureUnit, string paramName)
    // bool IMeasureUnitCode.HasMeasureUnitCode(MeasureUnitCode measureUnitCode)
    // MeasureUnitCode IMeasureUnitCode.GetMeasureUnitCode()
    // void IMeasureUnitCode.ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, string paramName)
    // Enum IMeasureUnit.GetBaseMeasureUnitValue()
    // TypeCode IQuantityType.GetQuantityTypeCode()
    // decimal IDefaultQuantity.GetDefaultQuantityValue()
    // Type IMeasureUnit.GetMeasureUnitType()
    // bool? ILimitable.FitsIn(ILimiter? limiter)
    // bool ITryExchange<IQuantifiable, Enum>.TryExchangeTo(Enum context, out IQuantifiable? exchanged)
    // decimal IProportional<IQuantifiable>.ProportionalTo(IQuantifiable? other)
    // bool IExchangeable<Enum>.IsExchangeableTo(Enum? context)
    // bool? IFit<IQuantifiable>.FitsIn(IQuantifiable? other, LimitMode? limitMode)
    // int IComparable<IQuantifiable>.CompareTo(IQuantifiable? other)
    // bool IEquatable<IQuantifiable>.Equals(IQuantifiable? other)
    // IQuantifiable IRound<IQuantifiable>.Round(RoundingMode roundingMode)
    // object IRound<IQuantifiable>.GetQuantity(RoundingMode roundingMode)
    // decimal IDecimalQuantity.GetDecimalQuantity()
    // ISpreadMeasure ISpreadMeasure.GetSpreadMeasure()
    // ISpreadMeasure? ISpreadGetSpreadMeasure(IQuantifiable? quantifiable)
    // MeasureUnitCode ISpreadMeasure.GetSpreadMeasureUnitCode()
    // void ISpreadMeasure.ValidateSpreadMeasure(ISpreadMeasure? spreadMeasure, string paramName)
    // double IQuantity<double>.GetQuantity()
    // ValueType IQuantity.GetBaseQuantityValue()
    // object IQuantity.GetQuantity(TypeCode quantityTypeCode)

    #endregion

    #region Test helpers
    public SpreadReturn Return { private get; set; } = new();
    protected ISpreadMeasure SpreadMeasure { get; set; }

    public static SpreadChild GetSpreadChild(Enum measureUnit, ValueType quantity, ISpreadFactory factory = null, RateComponentCode? rateComponentCode = null)
    {
        return new(Fields.RootObject, Fields.paramName)
        {
            Return = GetReturn(factory),
            SpreadMeasure = GetSpreadMeasureBaseMeasureObject(measureUnit, quantity, rateComponentCode),
        };
    }

    public static SpreadChild GetSpreadChild(ISpreadMeasure spreadMeasure, ISpreadFactory factory = null)
    {
        return new(Fields.RootObject, Fields.paramName)
        {
            Return = GetReturn(factory),
            SpreadMeasure = spreadMeasure,
        };
    }

    public static SpreadChild GetSpreadChild(DataFields fields, ISpreadFactory factory = null)
    {
        return GetSpreadChild(fields.measureUnit, fields.quantity, factory);
    }

    public static SpreadChild GetCompleteSpreadChild(DataFields fields, RateComponentCode? rateComponentCode = null)
    {
        return GetSpreadChild(fields.measureUnit, fields.quantity, new SpreadFactoryObject(), rateComponentCode ?? RateComponentCode.Numerator);
    }
    #endregion

    public override IFactory GetFactory() => Return.GetFactoryValue;

    public override ISpreadMeasure GetSpreadMeasure() => SpreadMeasure;

    public override bool TryExchangeTo(Enum context, [NotNullWhen(true)] out IQuantifiable? exchanged)
    {
        return FakeMethods.TryExchange(this, getSpreadChild, context, out exchanged);

        #region Local methods
        IQuantifiable getSpreadChild()
        {
            Enum measureUnit = GetMeasureUnitElements(context, nameof(context)).MeasureUnit;

            return GetSpreadChild(measureUnit, GetBaseQuantity());
        }
        #endregion
    }

    private static SpreadReturn GetReturn(ISpreadFactory factory = null)
    {
        return new()
        {
            GetFactoryValue = factory,
        };
    }
}
