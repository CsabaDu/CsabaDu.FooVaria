using CsabaDu.FooVaria.BaseTypes.BaseQuantifiables.Types;
using CsabaDu.FooVaria.BaseTypes.Common.Types;
using CsabaDu.FooVaria.BaseTypes.Measurables.Behaviors;
using CsabaDu.FooVaria.BaseTypes.Measurables.Types;
using CsabaDu.FooVaria.BaseTypes.Quantifiables.Behaviors;
using CsabaDu.FooVaria.BaseTypes.Spreads.Types;
using CsabaDu.FooVaria.BaseTypes.Spreads.Types.Implementations;

namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.BaseMeasurementsTests.Fakes;

internal sealed class SpreadChild(IRootObject rootObject, string paramName) : Spread(rootObject, paramName)
{
    #region Members

    // ISpread ISpread.GetSpread(ISpreadMeasure spreadMeasure)
    // IQuantifiable IQuantifiable.GetQuantifiable(MeasureUnitCode measureUnitCode, decimal defaultQuantity)
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
    // MeasureUnitCode ISpreadMeasure.GetSpreadMeasureUnitCode()
    // void ISpreadMeasure.ValidateSpreadMeasure(ISpreadMeasure? spreadMeasure, string paramName)
    // double IQuantity<double>.GetQuantity()
    // ValueType IQuantity.GetBaseQuantity()
    // object IQuantity.GetQuantity(TypeCode quantityTypeCode)


    #endregion

    #region Test helpers
    public BaseMeasurementReturn Return { private get; set; } = new();
    internal static DataFields Fields = new();

    internal static SpreadChild GetBaseMeasurementChild(Enum measureUnit, IBaseMeasurementFactory factory = null, string measureUnitName = null)
    {
        return new(Fields.RootObject, Fields.paramName)
        {
            Return = new()
            {
                GetBaseMeasureUnit = measureUnit,
                GetFactory = factory,
                GetName = measureUnitName,
            }
        };
    }

    public override ISpread GetSpread(ISpreadMeasure spreadMeasure)
    {
        throw new NotImplementedException();
    }

    public override ISpreadMeasure GetSpreadMeasure()
    {
        throw new NotImplementedException();
    }

    public override bool TryExchangeTo(Enum context, [NotNullWhen(true)] out IQuantifiable? exchanged)
    {
        throw new NotImplementedException();
    }

    public override IFactory GetFactory()
    {
        throw new NotImplementedException();
    }
    #endregion
}
