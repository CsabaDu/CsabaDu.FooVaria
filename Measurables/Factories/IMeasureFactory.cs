namespace CsabaDu.FooVaria.Measurables.Factories;

public interface IMeasureFactory : IBaseMeasureFactory, IBaseMeasurableFactory<IMeasure>
{
    IMeasure Create(ValueType quantity, Enum measureUnit);
    IMeasure Create(ValueType quantity, string name);
    IMeasure Create(ValueType quantity, Enum measureUnit, decimal exchangeRate, string customName);
    IMeasure Create(ValueType quantity, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate);
    IMeasure Create(ValueType quantity, IMeasurement measurement);
    //IMeasure Create(IBaseMeasure baseMeasure);
}
