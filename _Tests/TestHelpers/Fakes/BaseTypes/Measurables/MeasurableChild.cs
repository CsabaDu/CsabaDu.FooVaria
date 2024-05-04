namespace CsabaDu.FooVaria.Tests.TestHelpers.Fakes.BaseTypes.Measurables;

public sealed class MeasurableChild(IRootObject rootObject, string paramName) : Measurable(rootObject, paramName)
{
    #region Members

    // Measurable(IRootObject rootObject, string paramName)
    // bool Measurable.Equals(object? obj)
    // Enum IMeasureUnit.GetBaseMeasureUnitValue()
    // Enum IDefaultMeasureUnit.GetDefaultMeasureUnit()
    // IEnumerable<string> IDefaultMeasureUnit.GetDefaultMeasureUnitNames()
    // IFactory ICommonBase.GetFactoryValue()
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
        DataFields fields = DataFields.Fields;

        return new(fields.RootObject, fields.paramName)
        {
            Return = new()
            {
                GetBaseMeasureUnitValue = measureUnit,
                GetFactoryValue = factory,
            }
        };
    }

    public static MeasurableChild GetMeasurableChild(DataFields fields, IMeasurableFactory factory = null)
    {
        return GetMeasurableChild(fields.measureUnit, factory);
    }
    #endregion

    public override Enum GetBaseMeasureUnit() => Return.GetBaseMeasureUnitValue;

    public override IFactory GetFactory() => Return.GetFactoryValue;
}
