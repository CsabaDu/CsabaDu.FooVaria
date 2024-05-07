namespace CsabaDu.FooVaria.Tests.TestHelpers.Fakes.BaseTypes.BaseRates;

public sealed class BaseRateChild(IRootObject rootObject, string paramName) : BaseRate(rootObject, paramName)
{
    #region Members

    // BaseRate(IRootObject rootObject, string paramName)
    // MeasureUnitCode IBaseRate.GetNumeratorCode()
    // MeasureUnitCode IBaseRate.GetMeasureUnitCode(RateComponentCode rateComponentCode)
    // IBaseRate IBaseRate.GetBaseRate(IQuantifiable numerator, Enum denominator)
    // IBaseRate IBaseRate.GetBaseRate(IQuantifiable numerator, IMeasurable denominator)
    // IBaseRate IBaseRate.GetBaseRate(IQuantifiable numerator, IQuantifiable denominator)
    // void IBaseRate.ValidateRateComponentCode(RateComponentCode rateComponentCode, string paramName)
    // void IBaseQuantifiable.ValidateQuantity(ValueType? quantity, string paramName)
    // void IMeasurable.ValidateMeasureUnitCode(IMeasurable? measurable, string paramName)
    // IFactory ICommonBase.GetFactoryValue()
    // Enum IDefaultMeasureUnit.GetDefaultMeasureUnit()
    // IEnumerable<string> IDefaultMeasureUnit.GetDefaultMeasureUnitNames()
    // void IDefaultMeasureUnit.ValidateMeasureUnit(Enum? measureUnit, string paramName)
    // bool IMeasureUnitCode.HasMeasureUnitCode(MeasureUnitCode measureUnitCode)
    // MeasureUnitCode IMeasureUnitCode.GetMeasureUnitCode()
    // void IMeasureUnitCode.ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, string paramName)
    // TypeCode IQuantityType.GetQuantityTypeCode()
    // Enum IMeasureUnit.GetBaseMeasureUnitValue()
    // Type IMeasureUnit.GetMeasureUnitType()
    // decimal IDefaultQuantity.GetDefaultQuantityValue()
    // bool? ILimitable.FitsIn(ILimiter? limiter)
    // decimal IQuantity<decimal>.GetQuantity()
    // ValueType IQuantity.GetBaseQuantityValue()
    // object IQuantity.GetQuantity(TypeCode quantityTypeCode)
    // decimal IProportional<IBaseRate>.ProportionalTo(IBaseRate? other)
    // bool IExchangeable<IBaseRate>.IsExchangeableTo(IBaseRate? context)
    // bool? IFit<IBaseRate>.FitsIn(IBaseRate? other, LimitMode? limitMode)
    // int IComparable<IBaseRate>.CompareTo(IBaseRate? other)
    // bool IEquatable<IBaseRate>.Equals(IBaseRate? other)
    // MeasureUnitCode IDenominate.GetDenominatorCodeValue()
    // object? IValidRateComponent.GetRateComponent(RateComponentCode rateComponentCode)
    // bool IValidRateComponent.IsValidRateComponent(object? rateComponent, RateComponentCode rateComponentCode)
    // LimitMode? ILimitMode.GetLimitMode()
    // IEnumerable<MeasureUnitCode> IMeasureUnitCodes.GetMeasureUnitCodes()
    // void IMeasureUnitCodes.ValidateMeasureUnitCodes(IBaseQuantifiable? baseQuantifiable, string paramName)

    #endregion

    #region Test helpers
    public BaseRateReturn Return { private get; set; }

    public static BaseRateChild GetBaseRateChild(Enum measureUnit, decimal defaultQuantity, MeasureUnitCode denominatorCode, IBaseRateFactory factory = null)
    {
        DataFields fields = DataFields.Fields;

        return new(fields.RootObject, fields.paramName)
        {
            Return = new()
            {
                GetBaseMeasureUnitValue = measureUnit,
                GetDefaultQuantityValue = defaultQuantity,
                GetDenominatorCodeValue = denominatorCode,
                GetFactoryValue = factory,
            }
        };
    }

    public static BaseRateChild GetBaseRateChild(DataFields fields, IBaseRateFactory factory = null)
    {
        return GetBaseRateChild(fields.measureUnit, fields.defaultQuantity, fields.denominatorCode, factory);
    }

    public static BaseRateChild GetBaseRateChild(IQuantifiable quantifiable, MeasureUnitCode denominatorCode, IBaseRateFactory factory = null)
    {
        return GetBaseRateChild(quantifiable.GetBaseMeasureUnit(), quantifiable.GetDefaultQuantity(), denominatorCode, factory);
    }
    #endregion

    public override MeasureUnitCode GetMeasureUnitCode(RateComponentCode rateComponentCode)
    {
        return (MeasureUnitCode?)GetRateComponent(rateComponentCode)
            ?? throw InvalidRateComponentCodeArgumentException(rateComponentCode);
    }

    public override MeasureUnitCode GetDenominatorCode() => Return.GetDenominatorCodeValue;

    public override IEnumerable<MeasureUnitCode> GetMeasureUnitCodes()
    {
        return [GetNumeratorCode(), GetDenominatorCode()];
    }

    public override MeasureUnitCode GetNumeratorCode() => GetMeasureUnitCode();

    public override object GetRateComponent(RateComponentCode rateComponentCode)
    {
        return rateComponentCode switch
        {
            RateComponentCode.Numerator => GetNumeratorCode(),
            RateComponentCode.Denominator => GetDenominatorCode(),

            _ => null,
        };
    }

    public override decimal GetDefaultQuantity() => Return.GetDefaultQuantityValue;

    public override Enum GetBaseMeasureUnit() => Return.GetBaseMeasureUnitValue;

    public override IFactory GetFactory() => Return.GetFactoryValue;
}
