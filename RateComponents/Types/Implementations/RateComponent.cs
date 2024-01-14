namespace CsabaDu.FooVaria.RateComponents.Types.Implementations
{
    internal abstract class RateComponent : BaseMeasure, IRateComponent
    {
        #region Constructors
        private protected RateComponent(IRateComponentFactory factory, Enum measureUnit, ValueType quantity) : base(factory, measureUnit)
        {
            Quantity = GetValidQuantity(quantity);
            Measurement = factory.MeasurementFactory.Create(measureUnit);
        }

        private protected RateComponent(IRateComponentFactory factory, IMeasurement measurement, ValueType quantity) : base(factory, measurement)
        {
            Quantity = GetValidQuantity(quantity);
            Measurement = factory.MeasurementFactory.CreateNew(measurement);
        }
        #endregion

        #region Properties
        public override sealed object Quantity { get; init; }
        public IMeasurement Measurement { get; init; }
        #endregion

        #region Public methods
        public override sealed TypeCode? GetQuantityTypeCode(object quantity)
        {
            TypeCode quantityTypeCode = Type.GetTypeCode(quantity?.GetType());

            return GetValidQuantityTypeCodeOrNull(quantityTypeCode);
        }

        public override void ValidateQuantity(ValueType? quantity, TypeCode quantityTypeCode, string paramName)
        {
            TypeCode? typeCode = GetQuantityTypeCode(NullChecked(quantity, nameof(quantity)));

            ValidateQuantityTypeCode(quantityTypeCode, paramName);

            if (typeCode == quantityTypeCode) return;

            throw QuantityArgumentOutOfRangeException(quantity);
        }

        #region Override methods
        public override IRateComponentFactory GetFactory()
        {
            return (IRateComponentFactory)Factory;
        }

        public override void ValidateQuantity(ValueType? quantity, string paramName) // TODO
        {
            if (GetValidQuantityOrNull(this, NullChecked(quantity, paramName)) != null) return;

            throw QuantityArgumentOutOfRangeException(paramName, quantity);
        }

        #region Sealed methods
        public override sealed void ValidateMeasureUnit(Enum measureUnit, string paramName)
        {
            Measurement.ValidateMeasureUnit(measureUnit, paramName);
        }
        #endregion
        #endregion

        #region Virtual methods
        #endregion

        #region Abstract methods
        //public abstract LimitMode? GetLimitMode();
        #endregion
        #endregion

        #region Protected methods
        protected TNum GetDefaultRateComponentQuantity<TNum>()
            where TNum : struct
        {
            return (TNum)GetFactory().DefaultRateComponentQuantity;
        }

        //protected IRateComponent GetRateComponent(IRateComponent rateComponent, IRateComponentFactory factory)
        //{
        //    if (rateComponent.IsExchangeableTo(MeasureUnitCode)) return factory.CreateNew(rateComponent);

        //    throw InvalidMeasureUnitCodeEnumArgumentException(rateComponent.MeasureUnitCode, nameof(rateComponent));
        //}
        #endregion
        //#endregion

        #region Private methods
        #region Static methods
        private static decimal GetDefaultQuantity(IRateComponent rateComponent)
        {
            return RoundQuantity(rateComponent.GetDecimalQuantity() * rateComponent.GetExchangeRate());
        }

        private static TypeCode? GetValidQuantityTypeCodeOrNull(TypeCode quantityTypeCode)
        {
            if (GetQuantityTypeCodes().Contains(quantityTypeCode)) return quantityTypeCode;

            return null;
        }

        public abstract IRateComponent GetRateComponent(IMeasurement measurement, ValueType quantity);
        #endregion
        #endregion
    }

    internal abstract class RateComponent<TSelf> : RateComponent, IRateComponent<TSelf>
        where TSelf : class, IRateComponent
    {
        #region Constructors
        private protected RateComponent(IRateComponentFactory factory, Enum measureUnit, ValueType quantity) : base(factory, measureUnit, quantity)
        {
        }

        private protected RateComponent(IRateComponentFactory factory, IMeasurement measurement, ValueType quantity) : base(factory, measurement, quantity)
        {
        }
        #endregion

        #region Public methods
        public override IRateComponentFactory<TSelf> GetFactory()
        {
            return (IRateComponentFactory<TSelf>)Factory;
        }

        public virtual TSelf GetRateComponent(ValueType quantity)
        {
            return GetFactory().Create(Measurement, quantity);
        }

        public virtual TSelf GetRateComponent(string name, ValueType quantity)
        {
            return GetFactory().Create(name, quantity);
        }

        public virtual TSelf GetRateComponent(IMeasurement measurement, ValueType quantity)
        {
            return GetFactory().Create(measurement, quantity);
        }

        public virtual TSelf? GetRateComponent(Enum measureUnit, decimal exchangeRate, ValueType quantity, string customName)
        {
            return GetFactory().Create(measureUnit, exchangeRate, quantity, customName);
        }

        public virtual TSelf? GetRateComponent(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate, ValueType quantity)
        {
            return GetFactory().Create(customName, measureUnitCode, exchangeRate, quantity);
        }

        #region Override methods
        #region Sealed methods
        public override sealed LimitMode? GetLimitMode()
        {
            return null;
        }
        #endregion
        #endregion
        #endregion
    }

    internal abstract class RateComponent<TSelf, TNum> : RateComponent<TSelf>, IRateComponent<TSelf, TNum>
        where TSelf : class, IRateComponent, IDefaultBaseMeasure
        where TNum : struct
    {
        #region Constructors
        private protected RateComponent(IRateComponentFactory factory, Enum measureUnit, ValueType quantity) : base(factory, measureUnit, quantity)
        {
        }

        private protected RateComponent(IRateComponentFactory factory, IMeasurement measurement, ValueType quantity) : base(factory, measurement, quantity)
        {
        }
        #endregion

        #region Public metthods
        public TSelf GetDefault()
        {
            return GetDefault(MeasureUnitCode)!;
        }

        public TNum GetDefaultRateComponentQuantity()
        {
            return GetDefaultRateComponentQuantity<TNum>();
        }

        public TSelf GetNew(TSelf other)
        {
            return GetRateComponent(other);
        }

        public TNum GetQuantity()
        {
            return (TNum)Quantity;
        }

        public TSelf GetRateComponent(TNum quantity)
        {
            return GetFactory().Create(Measurement, quantity);
        }

        public override sealed TypeCode? GetQuantityTypeCode(object quantity)
        {
            if (quantity is IQuantity<TNum> rateComponent) return Quantifiable.GetQuantityTypeCode(rateComponent);

            return base.GetQuantityTypeCode(quantity);
        }

        public override sealed TypeCode GetQuantityTypeCode()
        {
            return Quantifiable.GetQuantityTypeCode(this);
        }

        #region Abstract methods
        public abstract TSelf GetRateComponent(IRateComponent rateComponent);
        public abstract TSelf? GetDefault(MeasureUnitCode measureUnitCode);
        #endregion
        #endregion
    }
}