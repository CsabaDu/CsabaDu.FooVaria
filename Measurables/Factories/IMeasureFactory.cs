namespace CsabaDu.FooVaria.Measurables.Factories;

public interface IMeasureFactory : IBaseMeasureFactory
{
    IMeasure Create(ValueType quantity, Enum measureUnit);
    IMeasure Create(ValueType quantity, string name);
    IMeasure Create(ValueType quantity, Enum measureUnit, decimal exchangeRate, string? customName = null);
    IMeasure Create(ValueType quantity, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, string? customName = null);
    IMeasure Create(ValueType quantity, IMeasurement measurement);
    IMeasure Create(IMeasure measure);
}

