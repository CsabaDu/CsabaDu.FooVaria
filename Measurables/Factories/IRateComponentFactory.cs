namespace CsabaDu.FooVaria.Measurables.Factories;

public interface IRateComponentFactory<out T> where T : class, IMeasurable, IRateComponent
{
    T CreateDefault(MeasureUnitTypeCode measureUnitTypeCode);
}
