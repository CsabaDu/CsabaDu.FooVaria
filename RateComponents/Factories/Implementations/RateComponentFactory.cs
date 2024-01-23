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
        public TypeCode QuantityTypeCode => Type.GetTypeCode(DefaultRateComponentQuantity.GetType());

        #region Abstract properties
        public abstract object DefaultRateComponentQuantity {  get; }
        public abstract RateComponentCode RateComponentCode { get; }
        #endregion
        #endregion

        #region Public methods
        #region Abstract methods
        public abstract IBaseMeasure CreateBaseMeasure(IBaseMeasurement baseMeasurement, ValueType quantity);
        public abstract IMeasurable? CreateDefault(MeasureUnitCode measureUnitCode);
        #endregion
        #endregion

        #region Protected methods
        protected object ConvertQuantity(ValueType quantity)
        {
            string paramName = nameof(quantity);
            Type quantityType = NullChecked(quantity, paramName).GetType();

            if (Type.GetTypeCode(quantityType) == QuantityTypeCode) return quantity;

            object? converted = NullChecked(quantity, paramName).ToQuantity(QuantityTypeCode);

            if (converted != null) return converted;

            throw ArgumentTypeOutOfRangeException(paramName, quantity);
        }

        #region Static methods
        protected static T? GetStoredRateComponent<T>(T? other, HashSet<T> rateComponentSet)
            where T : class, IRateComponent
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

    public abstract class RateComponentFactory<T> : RateComponentFactory, IRateComponentFactory<T>
        where T : class, IBaseMeasure
    {
        #region Constructors
        private protected RateComponentFactory(IMeasurementFactory measurementFactory) : base(measurementFactory)
        {
        }
        #endregion

        #region Public methods
        #region Abstract methods
        public abstract T Create(string name, ValueType quantity);
        public abstract T Create(Enum measureUnit, ValueType quantity);
        public abstract T? Create(Enum measureUnit, decimal exchangeRate, ValueType quantity, string customName);
        public abstract T? Create(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate, ValueType quantity);
        public abstract T Create(IBaseMeasure baseMeasure);
        public abstract T CreateNew(T other);
        #endregion
        #endregion
    }
}
