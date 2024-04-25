﻿namespace CsabaDu.FooVaria.Tests.TestHelpers.Fakes.BaseTypes.Measurables;

public sealed class MeasurableChild(IRootObject rootObject, string paramName) : Measurable(rootObject, paramName)
{
    #region Members

    // Measurable(IRootObject rootObject, string paramName)
    // bool Measurable.Equals(object? obj)
    // Enum IMeasureUnit.GetBaseMeasureUnit()
    // Enum IDefaultMeasureUnit.GetDefaultMeasureUnit()
    // IEnumerable<string> IDefaultMeasureUnit.GetDefaultMeasureUnitNames()
    // IFactory ICommonBase.GetFactory()
    // int Measurable.GetHashCode()
    // MeasureUnitCode IMeasureUnitCode.GetMeasureUnitCode()
    // Type IMeasureUnit.GetMeasureUnitType()
    // TypeCode IQuantityType.GetQuantityTypeCode()
    // bool IMeasureUnitCode.HasMeasureUnitCode(MeasureUnitCode measureUnitCode)
    // void IDefaultMeasureUnit.ValidateMeasureUnit(Enum measureUnit, string paramName)
    // void IMeasurable.ValidateMeasureUnitCode(IMeasurable measurable, string paramName)
    // void IMeasureUnitCode.ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, string paramName)

    #endregion

    #region Test helpers
    public MeasurableReturn Return { private get; set; }

    public static MeasurableChild GetMeasurableChild(Enum measureUnit, IMeasurableFactory factory = null)
    {
        DataFields fields = new();

        return new(fields.RootObject, fields.paramName)
        {
            Return = new()
            {
                GetBaseMeasureUnit = measureUnit,
                GetFactory = factory,
            }
        };
    }

    public static MeasurableChild GetMeasurableChild(DataFields fields, IMeasurableFactory factory = null)
    {
        return GetMeasurableChild(fields.measureUnit, factory);
    }
    #endregion

    public override Enum GetBaseMeasureUnit() => Return.GetBaseMeasureUnit;

    public override IFactory GetFactory() => Return.GetFactory;
}
