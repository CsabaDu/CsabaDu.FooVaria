namespace CsabaDu.FooVaria.Quantifiables.Factories
{
    public interface IBaseMeasureFactory : IQuantifiableFactory
    {
        IBaseMeasurementFactory GetBaseMeasurementFactory();

        IBaseMeasure CreateBaseMeasure(Enum measureUnit, ValueType quantity);
    }

    //public interface IBaseMeasureFactory<out T, TContext> : IBaseMeasureFactory
    //    where T : class, IBaseMeasure
    //    where TContext : notnull
    //{
    //    T Create(TContext context, decimal quantity);
    //}
}
