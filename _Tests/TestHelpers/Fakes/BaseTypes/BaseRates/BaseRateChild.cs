using CsabaDu.FooVaria.BaseTypes.BaseQuantifiables.Types;
using CsabaDu.FooVaria.BaseTypes.BaseRates.Behaviors;
using CsabaDu.FooVaria.BaseTypes.BaseRates.Factories;
using CsabaDu.FooVaria.BaseTypes.BaseRates.Types;
using CsabaDu.FooVaria.BaseTypes.Common.Types;
using CsabaDu.FooVaria.BaseTypes.Measurables.Types;

namespace CsabaDu.FooVaria.Tests.TestHelpers.Fakes.BaseTypes.BaseRates;

public class BaseRateChild(IRootObject rootObject, string paramName) : BaseRate(rootObject, paramName)
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
    // IFactory ICommonBase.GetFactory()
    // Enum IDefaultMeasureUnit.GetDefaultMeasureUnit()
    // IEnumerable<string> IDefaultMeasureUnit.GetDefaultMeasureUnitNames()
    // void IDefaultMeasureUnit.ValidateMeasureUnit(Enum? measureUnit, string paramName)
    // bool IMeasureUnitCode.HasMeasureUnitCode(MeasureUnitCode measureUnitCode)
    // MeasureUnitCode IMeasureUnitCode.GetMeasureUnitCode()
    // void IMeasureUnitCode.ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, string paramName)
    // TypeCode IQuantityType.GetQuantityTypeCode()
    // Enum IMeasureUnit.GetBaseMeasureUnit()
    // Type IMeasureUnit.GetMeasureUnitType()
    // decimal IDefaultQuantity.GetDefaultQuantity()
    // bool? ILimitable.FitsIn(ILimiter? limiter)
    // decimal IQuantity<decimal>.GetQuantity()
    // ValueType IQuantity.GetBaseQuantity()
    // object IQuantity.GetQuantity(TypeCode quantityTypeCode)
    // decimal IProportional<IBaseRate>.ProportionalTo(IBaseRate? other)
    // bool IExchangeable<IBaseRate>.IsExchangeableTo(IBaseRate? context)
    // bool? IFit<IBaseRate>.FitsIn(IBaseRate? other, LimitMode? limitMode)
    // int IComparable<IBaseRate>.CompareTo(IBaseRate? other)
    // bool IEquatable<IBaseRate>.Equals(IBaseRate? other)
    // MeasureUnitCode IDenominate.GetDenominatorCode()
    // object? IValidRateComponent.GetRateComponent(RateComponentCode rateComponentCode)
    // bool IValidRateComponent.IsValidRateComponent(object? rateComponent, RateComponentCode rateComponentCode)
    // LimitMode? ILimitMode.GetLimitMode()
    // IEnumerable<MeasureUnitCode> IMeasureUnitCodes.GetMeasureUnitCodes()
    // void IMeasureUnitCodes.ValidateMeasureUnitCodes(IBaseQuantifiable? baseQuantifiable, string paramName)

    #endregion

    #region Test helpers
    public BaseRateReturn Return { private get; set; }
    internal static DataFields Fields = new();

    public static BaseRateChild GetBaseQuantifiableChild(Enum measureUnit, decimal defaultQuantity, IBaseRateFactory factory = null)
    {
        return new(Fields.RootObject, Fields.paramName)
        {
            Return = new()
            {
                GetBaseMeasureUnit = measureUnit,
                GetDefaultQuantity = defaultQuantity,
                GetFactory = factory,
            }
        };
    }
    #endregion

    public override MeasureUnitCode GetMeasureUnitCode(RateComponentCode rateComponentCode)
    {
        return (MeasureUnitCode?)GetRateComponent(rateComponentCode)
            ?? throw InvalidRateComponentCodeArgumentException(rateComponentCode);
    }

    public override sealed MeasureUnitCode GetDenominatorCode() => GetMeasureUnitCode();

    public override IEnumerable<MeasureUnitCode> GetMeasureUnitCodes()
    {
        return [GetNumeratorCode(), GetDenominatorCode()];
    }

    public override MeasureUnitCode GetNumeratorCode() => Return.GetNumeratorCode;

    public override object? GetRateComponent(RateComponentCode rateComponentCode) // logic
    {
        return rateComponentCode switch
        {
            RateComponentCode.Denominator => GetDenominatorCode(),
            RateComponentCode.Numerator => GetNumeratorCode(),

            _ => null,
        };
    }

    public override decimal GetDefaultQuantity() => Return.GetDefaultQuantity;

    public override Enum GetBaseMeasureUnit() => Return.GetBaseMeasureUnit;

    public override IFactory GetFactory() => Return.GetFactory;
}
