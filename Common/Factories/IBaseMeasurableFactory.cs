namespace CsabaDu.FooVaria.Common.Factories
{
    public interface IBaseMeasurableFactory : IFactory
    {
    }

    public interface IBaseMeasurableFactory<out T> : IBaseMeasurableFactory where T : class, IBaseMeasurable
    {
        T CreateDefault(MeasureUnitTypeCode measureUnitTypeCode);
    }
}
