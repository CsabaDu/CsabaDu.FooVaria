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

        #region Abstract properties
        public abstract object DefaultRateComponentQuantity {  get; }
        public abstract RateComponentCode RateComponentCode { get; }
        #endregion
        #endregion

        #region Public methods
        #region Abstract methods
        public abstract IBaseMeasure CreateBaseMeasure(IBaseMeasurement baseMeasurement, ValueType quantity);
        #endregion
        #endregion
    }

    public abstract class RateComponentFactory<T, TNum> : RateComponentFactory, IRateComponentFactory<T, TNum>
        where T : class, IRateComponent, IDefaultBaseMeasure
        where TNum : struct
    {
        #region Constructors
        //static RateComponentFactory()
        //{
        //    DenominatorSet = new();
        //    LimitSet = new();
        //}

        private protected RateComponentFactory(IMeasurementFactory measurementFactory) : base(measurementFactory)
        {
        }
        #endregion

        #region Public methods
        #region Abstract methods
        public abstract T Create(IMeasurement measurement, TNum quantity);
        public abstract T? CreateDefault(MeasureUnitCode measureUnitCode);
        public abstract T CreateNew(T other);

        #endregion
        #endregion

        #region Protected methods
        protected T GetOrCreateRateComponent(IBaseMeasurement baseMeasurement, ValueType quantity)
        {
            string paramName = nameof(baseMeasurement);

            if (NullChecked(baseMeasurement, paramName) is IMeasurement measurement)
            {
                return GetOrCreateStoredRateComponent(measurement, quantity);
            }

            throw ArgumentTypeOutOfRangeException(paramName, baseMeasurement);
        }

        protected T GetOrCreateStoredRateComponent(IMeasurement measurement, ValueType quantity)
        {
            TNum convertedQuantity = ConvertQuantity(quantity);

            return Create(measurement, convertedQuantity);
        }

        #region Static methods
        protected static TNum ConvertQuantity(ValueType quantity)
        {
            string paramName = nameof(quantity);
            object? converted = NullChecked(quantity, paramName).ToQuantity(typeof(TNum));

            return converted is TNum convertedQuantity ?
                convertedQuantity
                : throw ArgumentTypeOutOfRangeException(paramName, quantity);

        }

        protected static T? GetStoredRateComponent(T? other, HashSet<T> rateComponentSet)
        {
            bool exists = rateComponentSet.Contains(NullChecked(other, nameof(other)))
                || rateComponentSet.Add(other!);

            if (!exists) return null;

            return rateComponentSet.TryGetValue(other!, out T? stored) ?
                stored
                : null;
        }
        #endregion
        #endregion
    }
}
