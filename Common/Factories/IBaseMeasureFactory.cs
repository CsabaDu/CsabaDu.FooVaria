namespace CsabaDu.FooVaria.Common.Factories
{
    public interface IBaseMeasureFactory : IQuantifiableFactory
    {
    }

    public interface IBaseMeasureFactory<out T, TContext> : IBaseMeasureFactory
        where T : class, IBaseMeasure
        where TContext : notnull
    {
        T CreateBaseMeasure(TContext context, decimal quantity);
    }
}
