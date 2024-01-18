namespace CsabaDu.FooVaria.RateComponents.Types.Implementations
{
    internal abstract class RateComponent : BaseMeasure, IBaseMeasure
    {
        #region Constructors
        private protected RateComponent(IRateComponentFactory factory, IMeasurement measurement) : base(factory, measurement)
        {
            Measurement = factory.MeasurementFactory.CreateNew(measurement);
        }
        #endregion

        #region Properties
        public IMeasurement Measurement { get; init; }
        #endregion

        #region Public methods
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

        public override sealed RateComponentCode GetRateComponentCode()
        {
            return GetFactory().RateComponentCode;
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

    internal abstract class RateComponent<TSelf, TNum> : RateComponent, IRateComponent<TSelf, TNum>
        where TSelf : class, IBaseMeasure, IDefaultBaseMeasure
        where TNum : struct
    {
        #region Constructors
        //private protected RateComponent(IRateComponentFactory factory, Enum measureUnit, ValueType quantity) : base(factory, measureUnit, quantity)
        //{
        //}

        private protected RateComponent(IRateComponentFactory<TSelf, TNum> factory, IMeasurement measurement) : base(factory, measurement)
        {
        }
        #endregion

        #region Public metthods
        public TSelf GetBaseMeasure(IBaseMeasure baseMeasure)
        {
            if (NullChecked(baseMeasure, nameof(baseMeasure)) is TSelf other) return GetNew(other);

            IBaseMeasurement baseMeasurement = baseMeasure.GetBaseMeasurement();
            ValueType quantity = (ValueType)baseMeasure.Quantity;

            return (TSelf)GetBaseMeasure(baseMeasurement, quantity);
        }

        public TSelf GetBaseMeasure(TNum quantity)
        {
            return GetRateComponent(Measurement, quantity);
        }

        public TSelf GetDefault()
        {
            return GetDefault(MeasureUnitCode)!;
        }

        public TSelf? GetDefault(MeasureUnitCode measureUnitCode)
        {
            return GetFactory().CreateDefault(measureUnitCode);
        }

        public object GetDefaultRateComponentQuantity()
        {
            return GetFactory().DefaultRateComponentQuantity;
        }

        public TNum GetQuantity()
        {
            return (TNum)GetQuantity(GetQuantityTypeCode());
        }

        public TSelf GetRateComponent(IMeasurement measurement, TNum quantity)
        {
            return GetFactory().Create(measurement, quantity);
        }

        #region Override methods
        public override IRateComponentFactory<TSelf, TNum> GetFactory()
        {
            return (IRateComponentFactory<TSelf, TNum>)Factory;
        }

        public override sealed TypeCode GetQuantityTypeCode()
        {
            return Quantifiable.GetQuantityTypeCode(this);
        }
        #endregion
        #endregion

        #region Protected methods
        protected decimal GetDefaultQuantity(object quantity)
        {
            return GetDefaultQuantity(quantity, GetExchangeRate());
        }

        public TSelf GetBaseMeasure(Enum measureUnit, ValueType quantity)
        {
            throw new NotImplementedException();
        }

        public TSelf GetBaseMeasure(string name, ValueType quantity)
        {
            throw new NotImplementedException();
        }

        public TSelf? GetBaseMeasure(Enum measureUnit, decimal exchangeRate, ValueType quantity, string customName)
        {
            throw new NotImplementedException();
        }

        public TSelf? GetBaseMeasure(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate, ValueType quantity)
        {
            throw new NotImplementedException();
        }

        public TSelf GetNew(TSelf other)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
