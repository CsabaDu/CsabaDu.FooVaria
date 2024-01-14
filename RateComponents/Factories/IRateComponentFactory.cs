namespace CsabaDu.FooVaria.RateComponents.Factories
{
    public interface IRateComponentFactory : IBaseMeasureFactory/*<IRateComponent, Enum>*/, IFactory<IRateComponent>
    {
        //RateComponentCode RateComponentCode { get; }    
        object DefaultRateComponentQuantity { get; }
        IMeasurementFactory MeasurementFactory { get; }

        IRateComponent Create(IMeasurement measurement, ValueType quantity);
    //}

    //public interface IRateComponentFactory<T> : IRateComponentFactory
    //    where T : class, IRateComponent
    //{
    //    T Create(string name, ValueType quantity);
    //    T Create(IMeasurement measurement, ValueType quantity);
    //    T? Create(Enum measureUnit, decimal exchangeRate, ValueType quantity, string customName);
    //    T? Create(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate, ValueType quantity);
    }
}
