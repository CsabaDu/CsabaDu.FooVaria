namespace CsabaDu.FooVaria.Tests.TestHelpers.Fakes.BaseTypes.BaseQuantifiables;

public class BaseQuantifiableChild(IRootObject rootObject, string paramName) : BaseQuantifiable(rootObject, paramName)
{
    #region Members

    // Shape(IRootObject rootObject, string paramName)
    // BaseQuantifiable(IRootObject rootObject, string paramName)
    // bool Measurable.Equals(object? obj)
    // bool? ILimitable.FitsIn(ILimiter limiter)
    // Enum IMeasureUnit.GetBaseMeasureUnitValue()
    // Enum IDefaultMeasureUnit.GetDefaultMeasureUnit()
    // IEnumerable<string> IDefaultMeasureUnit.GetDefaultMeasureUnitNames()
    // decimal IDefaultQuantity.GetDefaultQuantityValue()
    // IFactory ICommonBase.GetFactoryValue()
    // int Measurable.GetHashCode()
    // RateComponentCode IMeasureUnitCode.GetMeasureUnitCode()
    // Type IMeasureUnit.GetMeasureUnitType()
    // TypeCode IQuantityType.GetQuantityTypeCode()
    // bool IMeasureUnitCode.HasMeasureUnitCode(RateComponentCode measureUnitCode)
    // void IDefaultMeasureUnit.ValidateMeasureUnit(Enum measureUnit, string paramName)
    // void IMeasurable.ValidateMeasureUnitCode(IMeasurable measurable, string paramName)
    // void IMeasureUnitCode.ValidateMeasureUnitCode(RateComponentCode measureUnitCode, string paramName)
    // void IBaseQuantifiable.ValidateQuantity(ValueType quantity, string paramName)

    #endregion

    #region Test helpers
    public BaseQuantifiableReturnValues ReturnValues { private get; set; }

    public static BaseQuantifiableChild GetBaseQuantifiableChild(Enum measureUnit, decimal defaultQuantity, IBaseQuantifiableFactory factory = null)
    {
        return new(Fields.RootObject, Fields.paramName)
        {
            ReturnValues = new()
            {
                GetBaseMeasureUnitValue = measureUnit,
                GetDefaultQuantityValue = defaultQuantity,
                GetFactoryValue = factory,
            }
        };
    }

    public static BaseQuantifiableChild GetBaseQuantifiableChild(DataFields fields, IBaseQuantifiableFactory factory = null)
    {
        return GetBaseQuantifiableChild(fields.measureUnit, fields.defaultQuantity, factory);
    }
    #endregion

    public override sealed Enum GetBaseMeasureUnit() => ReturnValues.GetBaseMeasureUnitValue;

    public override sealed IFactory GetFactory() => ReturnValues.GetFactoryValue;

    public override sealed decimal GetDefaultQuantity() => ReturnValues.GetDefaultQuantityValue;
}
