namespace CsabaDu.FooVaria.BaseTypes.Measurables.Factories;

public interface IDefaultMeasurableFactory : IMeasurableFactory
{
    IMeasurable? CreateDefault(MeasureUnitCode measureUnitCode);
}
