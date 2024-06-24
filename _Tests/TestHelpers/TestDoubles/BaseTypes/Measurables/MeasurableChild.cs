namespace CsabaDu.FooVaria.Tests.TestHelpers.TestDoubles.BaseTypes.Measurables;

public sealed class MeasurableChild(IRootObject rootObject, string paramName) : Measurable(rootObject, paramName)
{
    #region Members

    // Measurable(IRootObject rootObject, string paramName)
    // bool Measurable.Equals(object? obj)
    // Enum IMeasureUnit.GetBaseMeasureUnitReturnValue()
    // Enum IDefaultMeasureUnit.GetDefaultMeasureUnit()
    // IEnumerable<string> IDefaultMeasureUnit.GetDefaultMeasureUnitNames()
    // IFactory ICommonBase.GetFactoryReturnValue()
    // int Measurable.GetHashCode()
    // RateComponentCode IMeasureUnitCode.GetMeasureUnitCode()
    // Type IMeasureUnit.GetMeasureUnitType()
    // TypeCode IQuantityType.GetQuantityTypeCode()
    // bool IMeasureUnitCode.HasMeasureUnitCode(RateComponentCode measureUnitCode)
    // void IDefaultMeasureUnit.ValidateMeasureUnit(Enum measureUnit, string paramName)
    // void IMeasurable.ValidateMeasureUnitCode(IMeasurable measurable, string paramName)
    // void IMeasureUnitCode.ValidateMeasureUnitCode(RateComponentCode measureUnitCode, string paramName)

    #endregion

    #region Test helpers
    public MeasurableReturnValues ReturnValues { private get; set; }

    public static MeasurableChild GetMeasurableChild(Enum measureUnit, IMeasurableFactory factory = null)
    {
        

        return new(Fields.RootObject, Fields.paramName)
        {
            ReturnValues = new()
            {
                GetBaseMeasureUnitReturnValue = measureUnit,
                GetFactoryReturnValue = factory,
            }
        };
    }

    public static MeasurableChild GetMeasurableChild(DataFields fields, IMeasurableFactory factory = null)
    {
        return GetMeasurableChild(fields.measureUnit, factory);
    }
    #endregion

    public override Enum GetBaseMeasureUnit() => ReturnValues.GetBaseMeasureUnitReturnValue;

    public override IFactory GetFactory() => ReturnValues.GetFactoryReturnValue;
}
