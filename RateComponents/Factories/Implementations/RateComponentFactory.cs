﻿namespace CsabaDu.FooVaria.RateComponents.Factories.Implementations
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
        public virtual object DefaultRateComponentQuantity => default(int);
        public abstract RateComponentCode RateComponentCode { get; }
        #endregion

        #region Public methods
        public IRateComponent Create(Enum measureUnit, decimal defaultQuantity)
        {
            if (NullChecked(measureUnit, nameof(measureUnit)) is not MeasureUnitCode measureUnitCode) return Create(measureUnit, defaultQuantity);

            return Create(measureUnitCode.GetDefaultMeasureUnit(), defaultQuantity);
        }

        public IRateComponent CreateNew(IRateComponent other)
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

        protected static T GetStored<T>([DisallowNull] T rateComponent, HashSet<T> set)
        {
            bool exists = set.Contains(rateComponent) || set.Add(rateComponent);

            if (exists
                && set.TryGetValue(rateComponent, out T? stored)
                && stored != null)
            {
                return stored;
            }

            throw new InvalidOperationException(null);
        }

        public IBaseMeasure CreateBaseMeasure(Enum measureUnit, ValueType quantity)
        {
            return Create(measureUnit, quantity);
        }
        #endregion
        #endregion
    }

    public abstract class RateComponentFactory<T> : RateComponentFactory, IRateComponentFactory<T>
        where T : class, IRateComponent
    {
        #region Constructors
        private protected RateComponentFactory(IMeasurementFactory measurementFactory) : base(measurementFactory)
        {
        }
        #endregion

        #region Public methods
        public T Create(string name, ValueType quantity)
        {
            IMeasurement measurement = MeasurementFactory.Create(name);

            return Create(measurement, quantity);
        }

        #region Abstract methods
        public abstract T Create(IMeasurement measurement, ValueType quantity);
        public abstract T? Create(Enum measureUnit, decimal exchangeRate, ValueType quantity, string customName);
        public abstract T? Create(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate, ValueType quantity);
        #endregion
        #endregion
    }
}
