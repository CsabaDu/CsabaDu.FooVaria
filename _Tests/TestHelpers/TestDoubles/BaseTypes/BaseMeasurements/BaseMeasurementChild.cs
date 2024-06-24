namespace CsabaDu.FooVaria.Tests.TestHelpers.TestDoubles.BaseTypes.BaseMeasurements;

public sealed class BaseMeasurementChild(IRootObject rootObject, string paramName) : BaseMeasurement(rootObject, paramName)
{
    #region Members

    // BaseMeasurement(IRootObject rootObject, string paramName)
    // int IComparable<IBaseMeasurement>.CompareTo(IBaseMeasurement other)
    // bool IEquatable<IBaseMeasurement>.Equals(IBaseMeasurement other)
    // bool BaseMeasurement.Equals(object? obj)
    // IBaseMeasurement IBaseMeasurement.GetBaseMeasurementReturnValue(Enum context)
    // Enum IMeasureUnit.GetBaseMeasureUnitReturnValue()
    // IDictionary<object, decimal> IConstantExchangeRateCollection.GetConstantExchangeRateCollection()
    // Enum IDefaultMeasureUnit.GetDefaultMeasureUnit()
    // IEnumerable<string> IDefaultMeasureUnit.GetDefaultMeasureUnitNames()
    // decimal IExchangeRate.GetExchangeRate()
    // IDictionary<object, decimal> IExchangeRateCollection.GetExchangeRateCollection()
    // IFactory ICommonBase.GetFactoryReturnValue()
    // int BaseMeasurement.GetHashCode()
    // RateComponentCode IMeasureUnitCode.GetMeasureUnitCode()
    // Type IMeasureUnit.GetMeasureUnitType()
    // string INamed.GetNameReturnValue()
    // TypeCode IQuantityType.GetQuantityTypeCode()
    // bool IMeasureUnitCode.HasMeasureUnitCode(RateComponentCode measureUnitCode)
    // bool IExchangeable<Enum>.IsExchangeableTo(Enum context)
    // decimal IProportional<IBaseMeasurement>.ProportionalTo(IBaseMeasurement other)
    // void IExchangeRate.ValidateExchangeRate(decimal decimalQuantity, string paramName)
    // void IDefaultMeasureUnit.ValidateMeasureUnit(Enum measureUnit, string paramName)
    // void IMeasurable.ValidateMeasureUnitCode(IMeasurable measurable, string paramName)
    // void IMeasureUnitCode.ValidateMeasureUnitCode(RateComponentCode measureUnitCode, string paramName)

    #endregion

    #region Test helpers
    public BaseMeasurementReturnValues ReturnValues { private get; set; } = new();

    public static BaseMeasurementChild GetBaseMeasurementChild(Enum measureUnit, IBaseMeasurementFactory factory = null, string measureUnitName = null)
    {
        

        return new(Fields.RootObject, Fields.paramName)
        {
            ReturnValues = new()
            {
                GetBaseMeasureUnitReturnValue = measureUnit,
                GetFactoryReturnValue = factory,
                GetNameReturnValue = measureUnitName,
            }
        };
    }

    public static BaseMeasurementChild GetBaseMeasurementChild(DataFields fields, IBaseMeasurementFactory factory = null, string measureUnitName = null)
    {
        return GetBaseMeasurementChild(fields.measureUnit, factory, measureUnitName);
    }
    #endregion

    public override Enum GetBaseMeasureUnit() => ReturnValues.GetBaseMeasureUnitReturnValue;

    public override IFactory GetFactory() => ReturnValues.GetFactoryReturnValue;

    public override string GetName() => ReturnValues.GetNameReturnValue;
}
