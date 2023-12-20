namespace CsabaDu.FooVaria.Common.Factories
{
    public interface IBaseMeasureFactory : IQuantifiableFactory
    {
    }

    public interface IBaseMeasureFactory<out T> : IBaseMeasureFactory
        where T : class, IBaseMeasure
    {
    }
}
