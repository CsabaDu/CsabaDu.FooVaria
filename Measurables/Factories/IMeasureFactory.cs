namespace CsabaDu.FooVaria.Measurables.Factories;

public interface IMeasureFactory : IBaseMeasureFactory
{
    IMeasure Create(ValueType quantity, Enum measureUnit);
    IMeasure Create(ValueType quantity, string name);
    IMeasure Create(ValueType quantity, Enum measureUnit, decimal exchangeRate, string customName);
    IMeasure Create(ValueType quantity, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate);
    IMeasure Create(ValueType quantity, IMeasurement measurement);
    IMeasure Create(IBaseMeasure baseMeasure);
    //IMeasure Create(IMeasure measure);
}
