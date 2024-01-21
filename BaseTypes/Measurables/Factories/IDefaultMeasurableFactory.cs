namespace CsabaDu.FooVaria.Measurables.Factories;

public interface IDefaultMeasurableFactory : IMeasurableFactory
{
    IMeasurable? CreateDefault(MeasureUnitCode measureUnitCode);
}
