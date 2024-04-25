﻿namespace CsabaDu.FooVaria.Tests.TestHelpers.Fakes.BaseTypes.BaseMeasurements;

public sealed class BaseMeasurementChild(IRootObject rootObject, string paramName) : BaseMeasurement(rootObject, paramName)
{
    #region Members

    // BaseMeasurement(IRootObject rootObject, string paramName)
    // int IComparable<IBaseMeasurement>.CompareTo(IBaseMeasurement other)
    // bool IEquatable<IBaseMeasurement>.Equals(IBaseMeasurement other)
    // bool BaseMeasurement.Equals(object? obj)
    // IBaseMeasurement IBaseMeasurement.GetBaseMeasurement(Enum context)
    // Enum IMeasureUnit.GetBaseMeasureUnit()
    // IDictionary<object, decimal> IConstantExchangeRateCollection.GetConstantExchangeRateCollection()
    // Enum IDefaultMeasureUnit.GetDefaultMeasureUnit()
    // IEnumerable<string> IDefaultMeasureUnit.GetDefaultMeasureUnitNames()
    // decimal IExchangeRate.GetExchangeRate()
    // IDictionary<object, decimal> IExchangeRateCollection.GetExchangeRateCollection()
    // IFactory ICommonBase.GetFactory()
    // int BaseMeasurement.GetHashCode()
    // MeasureUnitCode IMeasureUnitCode.GetMeasureUnitCode()
    // Type IMeasureUnit.GetMeasureUnitType()
    // string INamed.GetName()
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
    internal static DataFields Fields = new();

    public static BaseMeasurementChild GetBaseMeasurementChild(Enum measureUnit, IBaseMeasurementFactory factory = null, string measureUnitName = null)
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

    public static BaseMeasurementChild GetBaseMeasurementChild(DataFields fields)
    {
        return GetBaseMeasurementChild(fields.measureUnit);
    }
    #endregion

    public override Enum GetBaseMeasureUnit() => Return.GetBaseMeasureUnit;

    public override IFactory GetFactory() => Return.GetFactory;

    public override string GetName() => Return.GetName;
}
