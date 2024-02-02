namespace CsabaDu.FooVaria.RateComponents.Types.Implementations
{
    internal abstract class RateComponent : BaseMeasure, IRateComponent
    {
        #region Constructors
        private protected RateComponent(IRateComponentFactory factory, IMeasurement measurement) : base(factory, measurement)
        {
            Measurement = GetBaseMeasurementFactory().CreateNew(measurement);
        }
        #endregion

        #region Properties
        public IMeasurement Measurement { get; init; }
        #endregion

        #region Public methods
        public IMeasurable? GetDefault(MeasureUnitCode measureUnitCode)
        {
            return GetFactory().CreateDefault(measureUnitCode);
        }

        #region Override methods
        public override IRateComponentFactory GetFactory()
        {
            return (IRateComponentFactory)Factory;
        }

        public override sealed IMeasurement GetBaseMeasurement()
        {
            return Measurement;
        }

        public override void ValidateQuantity(ValueType? quantity, string paramName) // TODO
        {
            ValidateQuantity(quantity, GetQuantityTypeCode(), paramName);
        }

        #region Sealed methods
        public override sealed IMeasurementFactory GetBaseMeasurementFactory()
        {
            return GetFactory().MeasurementFactory;
        }

        public override sealed void ValidateMeasureUnit(Enum measureUnit, string paramName)
        {
            Measurement.ValidateMeasureUnit(measureUnit, paramName);
        }
        #endregion
        #endregion

        #region Virtual methods
        #endregion

        #region Abstract methods
        public object GetDefaultRateComponentQuantity()
        {
            return GetFactory().DefaultRateComponentQuantity;
        }
        #endregion
        #endregion

        #region Protected methods
        protected TNum GetDefaultRateComponentQuantity<TNum>()
            where TNum : struct
        {
            return (TNum)GetFactory().DefaultRateComponentQuantity;
        }
        #endregion

        #region Private methods
        #region Static methods
        //private static decimal GetDefaultQuantity(IBaseMeasure rateComponent)
        //{
        //    return RoundQuantity(rateComponent.GetDecimalQuantity() * rateComponent.GetExchangeRate());
        //}

        private static TypeCode? GetValidQuantityTypeCodeOrNull(TypeCode quantityTypeCode)
        {
            if (GetQuantityTypeCodes().Contains(quantityTypeCode)) return quantityTypeCode;

            return null;
        }
        #endregion
        #endregion
    }

    internal abstract class RateComponent<TSelf> : RateComponent, IRateComponent<TSelf>
        where TSelf : class, IBaseMeasure
    {
        private protected RateComponent(IRateComponentFactory factory, IMeasurement measurement) : base(factory, measurement)
        {
        }

        public TSelf GetNew(TSelf other)
        {
            return GetFactory().CreateNew(other);
        }

        public override IRateComponentFactory<TSelf> GetFactory()
        {
            return (IRateComponentFactory<TSelf>)Factory;
        }

        public abstract TSelf GetDefault();

        #region Protected methods
        protected decimal GetDefaultQuantity(object quantity)
        {
            return GetDefaultQuantity(quantity, GetExchangeRate());
        }


        protected TSelf GetRateComponent(ValueType quantity)
        {
            return (TSelf)GetBaseMeasure(Measurement, quantity);
        }
        #endregion
    }
}
