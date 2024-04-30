namespace CsabaDu.FooVaria.Tests.TestHelpers.Fakes.BaseTypes.BaseMeasures;

public class BaseMeasureChild(IRootObject rootObject, string paramName) : BaseMeasure(rootObject, paramName)
{
    #region Members

    // BaseMeasure(IRootObject rootObject, string paramName)
    // int IComparable<IQuantifiable>.CompareTo(IQuantifiable? other)
    // bool IEquatable<IQuantifiable>.Equals(IQuantifiable? other)
    // bool IEqualityComparer<IBaseMeasure>.Equals(IBaseMeasure? x, IBaseMeasure? y)
    // bool? ILimitable.FitsIn(ILimiter? _limiter)
    // bool? IFit<IQuantifiable>.FitsIn(IQuantifiable? other, LimitMode? limitMode)
    // IBaseMeasure IBaseMeasure.GetBaseMeasure(ValueType quantity)
    // IBaseMeasure IBaseMeasure.GetBaseMeasure(IBaseMeasurement baseMeasurement, ValueType quantity)
    // IBaseMeasurement IBaseMeasure.GetBaseMeasurement()
    // IBaseMeasurementFactory IBaseMeasure.GetBaseMeasurementFactory()
    // Enum IMeasureUnit.GetBaseMeasureUnit()
    // ValueType IQuantity.GetBaseQuantity()
    // decimal IDecimalQuantity.GetDecimalQuantity()
    // Enum IDefaultMeasureUnit.GetDefaultMeasureUnit()
    // IEnumerable<string> IDefaultMeasureUnit.GetDefaultMeasureUnitNames()
    // decimal IDefaultQuantity.GetDefaultQuantity()
    // decimal IExchangeRate.GetExchangeRate()
    // IFactory ICommonBase.GetFactory()
    // int IEqualityComparer<IBaseMeasure>.GetHashCode(IBaseMeasure obj)
    // LimitMode? ILimitMode.GetLimitMode()
    // MeasureUnitCode IMeasureUnitCode.GetMeasureUnitCode()
    // Type IMeasureUnit.GetMeasureUnitType()
    // IQuantifiable IQuantifiable.GetQuantifiable(MeasureUnitCode measureUnitCode, decimal defaultQuantity)
    // object IRound<IQuantifiable>.GetQuantity(RoundingMode roundingMode)
    // object IQuantity.GetQuantity(TypeCode quantityTypeCode)
    // TypeCode IQuantityType.GetQuantityTypeCode()
    // TypeCode? IQuantityTypeCode.GetQuantityTypeCode(object quantity)
    // RateComponentCode IRateComponentCode.GetRateComponentCode()
    // bool IMeasureUnitCode.HasMeasureUnitCode(MeasureUnitCode measureUnitCode)
    // bool IExchangeable<Enum>.IsExchangeableTo(Enum? context)
    // decimal IProportional<IQuantifiable>.ProportionalTo(IQuantifiable? other)
    // IQuantifiable IRound<IQuantifiable>.Round(RoundingMode roundingMode)
    // bool ITryExchange<IQuantifiable, Enum>.TryExchangeTo(Enum context, out IQuantifiable? exchanged)
    // void IExchangeRate.ValidateExchangeRate(decimal exchangeRate, string paramName)
    // void IDefaultMeasureUnit.ValidateMeasureUnit(Enum? measureUnit, string paramName)
    // void IMeasurable.ValidateMeasureUnitCode(IMeasurable? measurable, string paramName)
    // void IMeasureUnitCode.ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, string paramName)
    // void IBaseQuantifiable.ValidateQuantity(ValueType? quantity, string paramName)

    #endregion

    #region Test helpers
    internal static DataFields Fields = new();
    public BaseMeasureReturn Return { protected get; set; }
    private LimiterObject LimiterObject {  get; set; }

    public static BaseMeasureChild GetBaseMeasureChild(Enum measureUnit, ValueType quantity, RateComponentCode? rateComponentCode = null, LimitMode? limitMode = null)
    {
        IBaseMeasurement baseMeasurement = BaseMeasurementFactory.CreateBaseMeasurement(measureUnit);

        return new(Fields.RootObject, Fields.paramName)
        {
            Return = new()
            {
                GetBaseMeasurement = baseMeasurement,
                GetBaseQuantity = quantity,
                GetFactory = rateComponentCode.HasValue ?
                    GetBaseMeasureFactoryObject(rateComponentCode.Value)
                    : null,
            },
            LimiterObject = new()
            {
                LimitMode = limitMode,
            },
        };
    }

    public static BaseMeasureChild GetBaseMeasureChild(DataFields fields)
    {
        return GetBaseMeasureChild(fields.measureUnit, fields.quantity);
    }

    public static BaseMeasureChild GetCompleteBaseMeasureChild(DataFields fields)
    {
        return GetBaseMeasureChild(fields.measureUnit, fields.quantity, fields.rateComponentCode, fields.limitMode);
    }
    #endregion

    public override sealed IBaseMeasurement GetBaseMeasurement() => Return.GetBaseMeasurement;

    public override sealed IBaseMeasurementFactory GetBaseMeasurementFactory() => BaseMeasurementFactory;

    public override sealed ValueType GetBaseQuantity() => Return.GetBaseQuantity;

    public override sealed IFactory GetFactory() => Return.GetFactory;

    public override sealed LimitMode? GetLimitMode() => LimiterObject.GetLimitMode();

    public override sealed bool TryExchangeTo(Enum context, [NotNullWhen(true)] out IQuantifiable exchanged)
    {
        return FakeMethods.TryExchange(this, getBaseMeasureChild, context, out exchanged);

        #region Local methods
        IQuantifiable getBaseMeasureChild()
        {
            Enum measureUnit = GetMeasureUnitElements(context, nameof(context)).MeasureUnit;

            return GetBaseMeasureChild(measureUnit, GetBaseQuantity());
        }
        #endregion
    }
}
