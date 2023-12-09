namespace CsabaDu.FooVaria.RateComponents.Factories
{
    public interface IRateComponentFactory : IBaseMeasureFactory<IRateComponent>
    {
        IMeasurementFactory MeasurementFactory { get; init; }
        RateComponentCode RateComponentCode { get; }
        int DefaultRateComponentQuantity { get; }

        IRateComponent Create(Enum measureUnit, ValueType quantity);
    }

    public interface IRateComponentFactory<out T> : IRateComponentFactory where T : class, IRateComponent
    {
        //T Create(ValueType quantity);
        T Create(string name, ValueType quantity);
        T Create(IMeasurement measurement, ValueType quantity);
        T? Create(Enum measureUnit, ValueType quantity, decimal exchangeRate, string customName);
        T? Create(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ValueType quantity);
    }
}
