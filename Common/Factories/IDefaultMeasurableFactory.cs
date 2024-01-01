namespace CsabaDu.FooVaria.Common.Factories;

public interface IDefaultMeasurableFactory<out T> : IMeasurableFactory
    where T : class, IMeasurable
{
    T? CreateDefault(MeasureUnitTypeCode measureUnitTypeCode);
}
