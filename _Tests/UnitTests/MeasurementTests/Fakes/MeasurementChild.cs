namespace CsabaDu.FooVaria.Tests.UnitTests.MeasurementTests.Fakes;

internal sealed class MeasurementChild(IMeasurementFactory factory, Enum measureUnit) : Measurement(factory, measureUnit)
{
    #region Members

    // IMeasurementFactory IMeasurement.Factory { get; init; }
    // object IMeasurement.MeasureUnit { get; init; }

    // int IComparable<IBaseMeasurement>.CompareTo(IBaseMeasurement? other)
    // bool IEquatable<IBaseMeasurement>.Equals(IBaseMeasurement? other)
    // IBaseMeasurement? IBaseMeasurement.GetBaseMeasurement(Enum context)
    // Enum IMeasureUnit.GetBaseMeasureUnit()
    // IDictionary<object, decimal> IConstantExchangeRateCollection.GetConstantExchangeRateCollection()
    // string? ICustomNameCollection.GetCustomName()
    // IDictionary<object, string> ICustomNameCollection.GetCustomNameCollection()
    // IMeasurement IDefaultMeasurable<IMeasurement>.GetDefault()
    // IMeasurable? IDefaultMeasurable.GetDefault(MeasureUnitCode measureUnitCode)
    // Enum IDefaultMeasureUnit.GetDefaultMeasureUnit()
    // IEnumerable<string> IDefaultMeasureUnit.GetDefaultMeasureUnitNames()
    // string IMeasureUnitCollection.GetDefaultName()
    // decimal IExchangeRate.GetExchangeRate()
    // IDictionary<object, decimal> IExchangeRateCollection.GetExchangeRateCollection()
    // IFactory ICommonBase.GetFactory()
    // IMeasurement IMeasurement.GetMeasurement(Enum measureUnit)
    // IMeasurement IMeasurement.GetMeasurement(IMeasurement other)
    // IMeasurement? IMeasurement.GetMeasurement(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate)
    // IMeasurement? IMeasurement.GetMeasurement(Enum measureUnit, decimal exchangeRate, string customName)
    // IMeasurement IMeasurement.GetMeasurement(string name)
    // MeasureUnitCode IMeasureUnitCode.GetMeasureUnitCode()
    // Type IMeasureUnit.GetMeasureUnitType()
    // string INamed.GetName()
    // TypeCode IQuantityType.GetQuantityTypeCode()
    // bool IMeasureUnitCode.HasMeasureUnitCode(MeasureUnitCode measureUnitCode)
    // bool IExchangeable<Enum>.IsExchangeableTo(Enum? context)
    // decimal IProportional<IBaseMeasurement>.ProportionalTo(IBaseMeasurement? other)
    // void ICustomNameCollection.SetCustomName(string customName)
    // void ICustomNameCollection.SetOrReplaceCustomName(string customName)
    // bool IMeasurement.TryGetMeasurement(decimal exchangeRate, out IMeasurement? measurement)
    // bool ICustomNameCollection.TrySetCustomName(string? customName)
    // void IExchangeRate.ValidateExchangeRate(decimal exchangeRate, string paramName)
    // void IDefaultMeasureUnit.ValidateMeasureUnit(Enum? measureUnit, string paramName)
    // void IMeasurable.ValidateMeasureUnitCode(IMeasurable? measurable, string paramName)
    // void IMeasureUnitCode.ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, string paramName)

    #endregion
}
