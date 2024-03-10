namespace CsabaDu.FooVaria.Tests.TestSupport.Fakes.BaseTypes.Types;

internal sealed class MeasurableChild(IRootObject rootObject, string paramName) : Measurable(rootObject, paramName)
{
    #region Members
    // MeasurableChild MeasurableChild(IRootObject rootObject, string paramName)
    // Enum IMeasureUnit.GetBaseMeasureUnit()
    // Enum IDefaultMeasureUnit.GetDefaultMeasureUnit()
    // IEnumerable<string> IDefaultMeasureUnit.GetDefaultMeasureUnitNames()
    // IFactory ICommonBase.GetFactory()
    // MeasureUnitCode IMeasureUnitCode.GetMeasureUnitCode()
    // Type IMeasureUnit.GetMeasureUnitType()
    // TypeCode IQuantityType.GetQuantityTypeCode()
    // bool IMeasureUnitCode.HasMeasureUnitCode(MeasureUnitCode measureUnitCode)
    // void IDefaultMeasureUnit.ValidateMeasureUnit(Enum measureUnit, string paramName)
    // void IMeasurable.ValidateMeasureUnitCode(IMeasurable measurable, string paramName)
    // void IMeasureUnitCode.ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, string paramName)
    #endregion

    #region TestHelpers
    internal MeasurableReturns Returns { private get; set; }
    #endregion

    public override Enum GetBaseMeasureUnit() => Returns.GetBaseMeasureUnit;

    public override IFactory GetFactory() => Returns.GetFactory;
}
