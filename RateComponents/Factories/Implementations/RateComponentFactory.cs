namespace CsabaDu.FooVaria.RateComponents.Factories.Implementations
{
    public abstract class RateComponentFactory : IRateComponentFactory
    {
        #region Constructors
        private protected RateComponentFactory(IMeasurementFactory measurementFactory)
        {
            MeasurementFactory = NullChecked(measurementFactory, nameof(measurementFactory));
        }
        #endregion

        #region Properties
        public IMeasurementFactory MeasurementFactory { get; init; }
        public abstract RateComponentCode RateComponentCode { get; }
        public virtual object DefaultRateComponentQuantity => default(int);
        #endregion

        #region Public methods
        public IRateComponent Create(IRateComponent other)
        {
            var (measurement, quantity) = GetRateComponentParams(other);
            Enum measureUnit = measurement.GetMeasureUnit();

            return Create(measureUnit, quantity);
        }

        #region Abstract methods
        public abstract IRateComponent Create(Enum measureUnit, ValueType quantity);
        #endregion
        #endregion

        #region Protected methods
        #region Static methods
        protected static (IMeasurement Measurement, ValueType Quantity) GetRateComponentParams(IRateComponent rateComponent)
        {
            IMeasurement measurement = NullChecked(rateComponent, nameof(rateComponent)).Measurement;
            ValueType quantity = (ValueType)rateComponent.Quantity;

            return (measurement, quantity);
        }
        #endregion
        #endregion
    }

    public abstract class RateComponentFactory<T> : RateComponentFactory, IRateComponentFactory<T> where T : class, IRateComponent
    {
        public RateComponentFactory(IMeasurementFactory measurementFactory) : base(measurementFactory)
        {
        }

        public T Create(string name, ValueType quantity)
        {
            IMeasurement measurement = MeasurementFactory.Create(name);

            return Create(measurement, quantity);
        }

        public abstract T Create(IMeasurement measurement, ValueType quantity);
        public abstract T? Create(Enum measureUnit, decimal exchangeRate, ValueType quantity, string customName);
        public abstract T? Create(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ValueType quantity);
    }

}
