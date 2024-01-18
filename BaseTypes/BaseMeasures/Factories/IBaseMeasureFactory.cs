namespace CsabaDu.FooVaria.BaseMeasures.Factories
{
    public interface IBaseMeasureFactory : IQuantifiableFactory
    {
        RateComponentCode RateComponentCode { get; }

        IBaseMeasure CreateBaseMeasure(IBaseMeasurement baseMeasurement, ValueType quantity);
    }

    public interface IBaseMeasureFactory<T> : IBaseMeasureFactory, IFactory<T>
        where T : class, IBaseMeasure
    {
        T Create(string name, ValueType quantity);
        T Create(Enum measureUnit, ValueType quantity);
        T? Create(Enum measureUnit, decimal exchangeRate, ValueType quantity, string customName);
        T? Create(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate, ValueType quantity);
        T Create(IBaseMeasure baseMeasure);
    }
}