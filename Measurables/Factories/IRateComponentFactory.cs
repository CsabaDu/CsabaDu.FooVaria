namespace CsabaDu.FooVaria.Measurables.Factories;

public interface IRateComponentFactory<out T> where T : class, IMeasurable
{
    T CreateDefault(MeasureUnitTypeCode measureUnitTypeCode);
}
