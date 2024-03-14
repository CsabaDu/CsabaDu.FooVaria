namespace CsabaDu.FooVaria.Tests.TestHelpers.Fakes.BaseTypes.Types;

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
    // void IExchangeRate.ValidateExchangeRate(decimal exchangeRate, string paramName)
    // void IDefaultMeasureUnit.ValidateMeasureUnit(Enum measureUnit, string paramName)
    // void IMeasurable.ValidateMeasureUnitCode(IMeasurable measurable, string paramName)
    // void IMeasureUnitCode.ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, string paramName)
    #endregion

    #region TestHelpers
    public BaseMeasurementReturns Returns { private get; set; } = new();
    #endregion

    public override Enum GetBaseMeasureUnit() => Returns.GetBaseMeasureUnit;

    public override IFactory GetFactory() => Returns.GetFactory;

    public override string GetName() => Returns.GetName;
}
