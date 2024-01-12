namespace CsabaDu.FooVaria.Measurables.Factories;

public interface IDefaultMeasurableFactory<out T> : IMeasurableFactory
    where T : class, IMeasurable
{
    T? CreateDefault(MeasureUnitCode measureUnitCode);
}
