using System;
using System.Linq;

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


        protected T GetOrCreateRateComponent<T>(IBaseMeasurement baseMeasurement, ValueType quantity)
            where T : class, IRateComponent
        {
            string paramName = nameof(baseMeasurement);

            if (NullChecked(baseMeasurement, paramName) is IMeasurement measurement)
            {
                return GetOrCreateStoredRateComponent<T>(measurement, quantity, QuantityTypeCode);
            }

            throw ArgumentTypeOutOfRangeException(paramName, baseMeasurement);
        }

        protected T GetOrCreateStoredRateComponent<T>(IBaseMeasurement baseMeasurement, ValueType quantity, TypeCode quantityTypeCode)
            where T : class, IRateComponent
        {
            _ = NullChecked(baseMeasurement, nameof(baseMeasurement));

            ValueType convertedQuantity = (ValueType)ConvertQuantity(quantity, quantityTypeCode);

            return (T)CreateBaseMeasure(baseMeasurement, convertedQuantity);
        }

        protected static object ConvertQuantity(ValueType quantity, TypeCode quantityTypeCode)
        {
            string paramName = nameof(quantity);
            object? converted = NullChecked(quantity, paramName).ToQuantity(quantityTypeCode);

            if (converted != null) return converted;

            throw ArgumentTypeOutOfRangeException(paramName, quantity);
        }

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

    }

    public abstract class RateComponentFactory<T> : RateComponentFactory, IRateComponentFactory<T>
        where T : class, IBaseMeasure
    {
        private protected RateComponentFactory(IMeasurementFactory measurementFactory) : base(measurementFactory)
        {
        }


        public abstract T Create(string name, ValueType quantity);
        public abstract T Create(Enum measureUnit, ValueType quantity);
        public abstract T? Create(Enum measureUnit, decimal exchangeRate, ValueType quantity, string customName);
        public abstract T? Create(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate, ValueType quantity);
        public abstract T Create(IBaseMeasure baseMeasure);
        public abstract T CreateNew(T other);
    }
}
