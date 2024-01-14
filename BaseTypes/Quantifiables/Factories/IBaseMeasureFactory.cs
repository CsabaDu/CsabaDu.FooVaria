namespace CsabaDu.FooVaria.Quantifiables.Factories
{
    public interface IBaseMeasureFactory : IQuantifiableFactory
    {
        IBaseMeasurementFactory GetBaseMeasurementFactory();

        IBaseMeasure CreateBaseMeasure(Enum measureUnit, ValueType quantity);
        IBaseMeasure CreateBaseMeasure(string name, ValueType quantity);
        IBaseMeasure CreateBaseMeasure(IBaseMeasurement baseMeasurement, ValueType quantity);
        IBaseMeasure? CreateBaseMeasure(Enum measureUnit, decimal exchangeRate, ValueType quantity, string customName);
        IBaseMeasure? CreateBaseMeasure(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate, ValueType quantity);

    }

    //public interface IBaseMeasureFactory<out T, TContext> : IBaseMeasureFactory
    //    where T : class, IBaseMeasure
    //    where TContext : notnull
    //{
    //    T Create(TContext context, decimal quantity);
    //}
}
