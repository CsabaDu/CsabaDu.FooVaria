namespace CsabaDu.FooVaria.Tests.TestHelpers.Fakes.BaseTypes.BaseMeasurements;

public sealed class BaseMeasurementChild(IRootObject rootObject, string paramName) : BaseMeasurement(rootObject, paramName)
{
    #region Members

    // BaseMeasurement(IRootObject rootObject, string paramName)
    // int IComparable<IBaseMeasurement>.CompareTo(IBaseMeasurement other)
    // bool IEquatable<IBaseMeasurement>.Equals(IBaseMeasurement other)
    // bool BaseMeasurement.Equals(object? obj)
    // IBaseMeasurement IBaseMeasurement.GetBaseMeasurementValue(Enum context)
    // Enum IMeasureUnit.GetBaseMeasureUnitValue()
    // IDictionary<object, decimal> IConstantExchangeRateCollection.GetConstantExchangeRateCollection()
    // Enum IDefaultMeasureUnit.GetDefaultMeasureUnit()
    // IEnumerable<string> IDefaultMeasureUnit.GetDefaultMeasureUnitNames()
    // decimal IExchangeRate.GetExchangeRate()
    // IDictionary<object, decimal> IExchangeRateCollection.GetExchangeRateCollection()
    // IFactory ICommonBase.GetFactoryValue()
    // int BaseMeasurement.GetHashCode()
    // MeasureUnitCode IMeasureUnitCode.GetMeasureUnitCode()
    // Type IMeasureUnit.GetMeasureUnitType()
    // string INamed.GetNameValue()
    // TypeCode IQuantityType.GetQuantityTypeCode()
    // bool IMeasureUnitCode.HasMeasureUnitCode(MeasureUnitCode measureUnitCode)
    // bool IExchangeable<Enum>.IsExchangeableTo(Enum context)
    // decimal IProportional<IBaseMeasurement>.ProportionalTo(IBaseMeasurement other)
    // void IExchangeRate.ValidateExchangeRate(decimal decimalQuantity, string paramName)
    // void IDefaultMeasureUnit.ValidateMeasureUnit(Enum measureUnit, string paramName)
    // void IMeasurable.ValidateMeasureUnitCode(IMeasurable measurable, string paramName)
    // void IMeasureUnitCode.ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, string paramName)

    #endregion

    #region Test helpers
    public BaseMeasurementReturn Return { private get; set; } = new();
    //internal static DataFields Fields => DataFields.Fields;

    public static BaseMeasurementChild GetBaseMeasurementChild(Enum measureUnit, IBaseMeasurementFactory factory = null, string measureUnitName = null)
    {
        DataFields fields = DataFields.Fields;

        return new(fields.RootObject, fields.paramName)
        {
            Return = new()
            {
                GetBaseMeasureUnitValue = measureUnit,
                GetFactoryValue = factory,
                GetNameValue = measureUnitName,
            }
        };
    }

    public static BaseMeasurementChild GetBaseMeasurementChild(DataFields fields, IBaseMeasurementFactory factory = null, string measureUnitName = null)
    {
        return GetBaseMeasurementChild(fields.measureUnit, factory, measureUnitName);
    }
    #endregion

    public override Enum GetBaseMeasureUnit() => Return.GetBaseMeasureUnitValue;

    public override IFactory GetFactory() => Return.GetFactoryValue;

    public override string GetName() => Return.GetNameValue;
}
