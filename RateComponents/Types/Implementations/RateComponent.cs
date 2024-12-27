namespace CsabaDu.FooVaria.RateComponents.Types.Implementations
{
    /// <summary>
    /// Represents an abstract base class for rate components.
    /// </summary>
    internal abstract class RateComponent : BaseMeasure, IRateComponent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="RateComponent"/> class.
        /// </summary>
        /// <param name="factory">The factory used to create rate components.</param>
        private protected RateComponent(IRateComponentFactory factory) : base(factory, nameof(factory))
        {
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Gets the default measurable object for the specified measure unit code.
        /// </summary>
        /// <param name="measureUnitCode">The measure unit code.</param>
        /// <returns>The default measurable object.</returns>
        public IMeasurable? GetDefault(MeasureUnitCode measureUnitCode)
        {
            return GetRateComponentFactory().CreateDefault(measureUnitCode);
        }

        /// <summary>
        /// Gets the default rate component quantity.
        /// </summary>
        /// <returns>The default rate component quantity.</returns>
        public object GetDefaultRateComponentQuantity()
        {
            return GetRateComponentFactory().DefaultRateComponentQuantity;
        }

        #region Override methods
        #region Sealed methods
        /// <summary>
        /// Gets the base measurement factory.
        /// </summary>
        /// <returns>The base measurement factory.</returns>
        public override sealed IMeasurementFactory GetBaseMeasurementFactory()
        {
            return GetRateComponentFactory().MeasurementFactory;
        }

        /// <summary>
        /// Gets the type code of the quantity.
        /// </summary>
        /// <returns>The type code of the quantity.</returns>
        public override sealed TypeCode GetQuantityTypeCode()
        {
            Type quantityType = GetBaseQuantity().GetType();
            return Type.GetTypeCode(quantityType);
        }
        #endregion
        #endregion
        #endregion

        #region Protected methods
        /// <summary>
        /// Gets the default rate component quantity of the specified type.
        /// </summary>
        /// <typeparam name="TNum">The type of the quantity.</typeparam>
        /// <returns>The default rate component quantity.</returns>
        protected TNum GetDefaultRateComponentQuantity<TNum>()
            where TNum : struct
        {
            return (TNum)GetRateComponentFactory().DefaultRateComponentQuantity;
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Gets the rate component factory.
        /// </summary>
        /// <returns>The rate component factory.</returns>
        private IRateComponentFactory GetRateComponentFactory()
        {
            return (IRateComponentFactory)GetFactory();
        }
        #endregion
    }

    /// <summary>
    /// Represents an abstract base class for rate components with a specific type.
    /// </summary>
    /// <typeparam name="TSelf">The type of the rate component.</typeparam>
    internal abstract class RateComponent<TSelf> : RateComponent, IRateComponent<TSelf>
        where TSelf : class, IRateComponent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="RateComponent{TSelf}"/> class.
        /// </summary>
        /// <param name="factory">The factory used to create rate components.</param>
        /// <param name="measurement">The measurement associated with the rate component.</param>
        private protected RateComponent(IRateComponentFactory<TSelf> factory, IMeasurement measurement) : base(factory)
        {
            Measurement = NullChecked(measurement, nameof(measurement));
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the measurement associated with the rate component.
        /// </summary>
        public IMeasurement Measurement { get; init; }
        #endregion

        #region Public methods
        /// <summary>
        /// Gets the default rate component.
        /// </summary>
        /// <returns>The default rate component.</returns>
        public TSelf GetDefault()
        {
            return (TSelf)GetDefault(GetMeasureUnitCode())!;
        }

        #region Override methods
        #region Sealed methods
        /// <summary>
        /// Gets the base measurement.
        /// </summary>
        /// <returns>The base measurement.</returns>
        public override sealed IMeasurement GetBaseMeasurement()
        {
            return Measurement;
        }
        #endregion
        #endregion
        #endregion

        #region Protected methods
        /// <summary>
        /// Gets the default quantity for the specified quantity and exchange rate.
        /// </summary>
        /// <param name="quantity">The quantity.</param>
        /// <returns>The default quantity.</returns>
        protected decimal GetDefaultQuantity(object quantity)
        {
            return GetDefaultQuantity(quantity, GetExchangeRate());
        }

        /// <summary>
        /// Gets the rate component with the specified quantity.
        /// </summary>
        /// <param name="quantity">The quantity.</param>
        /// <returns>The rate component.</returns>
        protected TSelf GetRateComponent(ValueType quantity)
        {
            return (TSelf)GetBaseMeasure(Measurement, quantity);
        }

        /// <summary>
        /// Tries to exchange the rate component to the specified context.
        /// </summary>
        /// <param name="context">The context to exchange to.</param>
        /// <param name="exchanged">The exchanged quantifiable object.</param>
        /// <returns>True if the exchange was successful, otherwise false.</returns>
        public override sealed bool TryExchangeTo(Enum context, [NotNullWhen(true)] out IQuantifiable? exchanged)
        {
            exchanged = null;

            if (!IsExchangeableTo(context)) return false;

            MeasureUnitElements measureUnitElements = GetMeasureUnitElements(context, nameof(context));
            exchanged = ExchangeTo(measureUnitElements.MeasureUnit);

            return exchanged is not null;
        }

        /// <summary>
        /// Exchanges the rate component to the specified measure unit.
        /// </summary>
        /// <param name="measureUnit">The measure unit to exchange to.</param>
        /// <returns>The exchanged rate component.</returns>
        public TSelf? ExchangeTo(Enum measureUnit)
        {
            if (!IsExchangeableTo(measureUnit)) return null;

            return (TSelf)GetBaseMeasure(measureUnit);
        }
        #endregion
    }
}
