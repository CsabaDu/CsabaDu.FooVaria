namespace CsabaDu.FooVaria.RateComponents.Types.Implementations
{
    internal abstract class RateComponent : BaseMeasure, IRateComponent
    {
        #region Constructors
        private protected RateComponent(IRateComponentFactory factory, IMeasurement measurement) : base(factory)
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

        #region Sealed methods
        public override sealed IMeasurementFactory GetBaseMeasurementFactory()
        {
            return GetFactory().MeasurementFactory;
        }

        public override sealed void ValidateMeasureUnit(Enum? measureUnit, string paramName)
        {
            Measurement.ValidateMeasureUnit(measureUnit!, paramName);
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
    }

    internal abstract class RateComponent<TSelf>(IRateComponentFactory factory, IMeasurement measurement) : RateComponent(factory, measurement), IRateComponent<TSelf>
        where TSelf : class, IBaseMeasure
    {
        #region Public methods
        #region Abstract methods
        public abstract TSelf GetDefault();
        #endregion
        #endregion

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
