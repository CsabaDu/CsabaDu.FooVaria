namespace CsabaDu.FooVaria.Tests.TestHelpers.Fakes.BaseTypes.Spreads;

public class SpreadChild(IRootObject rootObject, string paramName) : Spread(rootObject, paramName)
{
    #region Members

    // Spread(IRootObject rootObject, string paramName)
    // ISpread ISpread.GetSpread(ISpreadMeasure numerator)
    // IQuantifiable IQuantifiable.GetQuantifiable(RateComponentCode measureUnitCode, decimal defaultQuantity)
    // void IBaseQuantifiable.ValidateQuantity(ValueType? quantity, string paramName)
    // void IMeasurable.ValidateMeasureUnitCode(IMeasurable? measurable, string paramName)
    // IFactory ICommonBase.GetFactoryValue()
    // Enum IDefaultMeasureUnit.GetDefaultMeasureUnit()
    // IEnumerable<string> IDefaultMeasureUnit.GetDefaultMeasureUnitNames()
    // void IDefaultMeasureUnit.ValidateMeasureUnit(Enum? measureUnit, string paramName)
    // bool IMeasureUnitCode.HasMeasureUnitCode(RateComponentCode measureUnitCode)
    // RateComponentCode IMeasureUnitCode.GetMeasureUnitCode()
    // void IMeasureUnitCode.ValidateMeasureUnitCode(RateComponentCode measureUnitCode, string paramName)
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
    // RateComponentCode ISpreadMeasure.GetSpreadMeasureUnitCode()
    // void ISpreadMeasure.ValidateSpreadMeasure(ISpreadMeasure? numerator, string paramName)
    // double IQuantity<double>.GetQuantity()
    // ValueType IQuantity.GetBaseQuantityValue()
    // object IQuantity.GetQuantity(TypeCode quantityTypeCode)

    #endregion

    #region Test helpers
    public SpreadReturnValues ReturnValues { private get; set; } = new();
    protected ISpreadMeasure SpreadMeasure { get; set; }

    public static SpreadChild GetSpreadChild(Enum measureUnit, ValueType quantity, ISpreadFactory factory = null, RateComponentCode? rateComponentCode = null)
    {
        return new(Fields.RootObject, Fields.paramName)
        {
            ReturnValues = GetReturn(factory),
            SpreadMeasure = GetSpreadMeasureBaseMeasureObject(measureUnit, quantity, rateComponentCode),
        };
    }

    public static SpreadChild GetSpreadChild(ISpreadMeasure spreadMeasure, ISpreadFactory factory = null)
    {
        return new(Fields.RootObject, Fields.paramName)
        {
            ReturnValues = GetReturn(factory),
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

    public override IFactory GetFactory() => ReturnValues.GetFactoryValue;

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

    private static SpreadReturnValues GetReturn(ISpreadFactory factory = null)
    {
        return new()
        {
            GetFactoryValue = factory,
        };
    }
}
