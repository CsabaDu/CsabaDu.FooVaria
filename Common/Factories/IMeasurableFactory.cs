namespace CsabaDu.FooVaria.Common.Factories
{
    public interface IMeasurableFactory : IFactory
    {
    }

    public interface IMeasurableFactory<out T> : IMeasurableFactory where T : class, IMeasurable
    {
        T CreateDefault(MeasureUnitTypeCode measureUnitTypeCode);
    }
}
