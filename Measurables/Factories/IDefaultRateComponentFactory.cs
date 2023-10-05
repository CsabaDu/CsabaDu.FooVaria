namespace CsabaDu.FooVaria.Measurables.Factories
{
    public interface IDefaultRateComponentFactory
    {
    }

    public interface IDefaultRateComponentFactory<out T> : IDefaultRateComponentFactory where T : class, IMeasurable, IRateComponentType
    {
        T CreateDefault(MeasureUnitTypeCode measureUnitTypeCode);
    }
}

