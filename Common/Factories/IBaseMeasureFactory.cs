namespace CsabaDu.FooVaria.Common.Factories
{
    public interface IBaseMeasureFactory : IQuantifiableFactory
    {
        IBaseMeasure CreateBaseMeasure(Enum measureUnit, ValueType quantity);
    }

    public interface IBaseMeasureFactory<out T, TContext> : IBaseMeasureFactory
        where T : class, IBaseMeasure
        where TContext : notnull
    {
        T Create(TContext context, decimal quantity);
    }
}
