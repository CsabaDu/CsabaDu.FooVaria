namespace CsabaDu.FooVaria.Measurables.Factories;

public interface IDefaultRateComponentFactory<out T> where T : class, IMeasurable, IRateComponentType
{
    T CreateDefault(MeasureUnitTypeCode measureUnitTypeCode);
}

