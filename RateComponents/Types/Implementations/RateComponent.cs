using static CsabaDu.FooVaria.Common.Statics.QuantityTypes;

namespace CsabaDu.FooVaria.RateComponents.Types.Implementations
{
    internal abstract class RateComponent : BaseMeasure, IRateComponent
    {
        #region Constructors
        private protected RateComponent(IRateComponentFactory factory, Enum measureUnit, ValueType quantity) : base(factory, measureUnit)
        {
            Quantity = GetValidQuantity(quantity);
            Measurement = factory.MeasurementFactory.Create(measureUnit);
            DefaultQuantity = GetDefaultQuantity(this);
        }

        private protected RateComponent(IRateComponentFactory factory, IMeasurement measurement, ValueType quantity) : base(factory, measurement)
        {
            Quantity = GetValidQuantity(quantity);
            Measurement = factory.MeasurementFactory.Create(measurement);
            DefaultQuantity = GetDefaultQuantity(this);
        }
        #endregion

        #region Properties
        public object Quantity { get; init; }
        public IMeasurement Measurement { get; init; }
        public override sealed decimal DefaultQuantity { get; init; }
        #endregion

        #region Public methods
        public int CompareTo(IRateComponent? other)
        {
            if (other == null) return 1;

            other.ValidateMeasureUnitTypeCode(MeasureUnitTypeCode, nameof(other));

            return DefaultQuantity.CompareTo(other.DefaultQuantity);
        }

        public IRateComponent? ExchangeTo(Enum measureUnit)
        {
            if (!IsExchangeableTo(measureUnit)) return null;

            decimal exchangeRate = Measurement.GetExchangeRate(measureUnit);

            if (GetRateComponentCode() == RateComponentCode.Limit && DefaultQuantity % exchangeRate > 0) return null;

            decimal quantity = DefaultQuantity / exchangeRate;

            return GetRateComponent(measureUnit, quantity);
        }

        public decimal GetDecimalQuantity()
        {
            return (decimal)GetQuantity(TypeCode.Decimal);
        }

        public decimal GetExchangeRate()
        {
            return Measurement.ExchangeRate;
        }

        public object GetQuantity(RoundingMode roundingMode)
        {
            decimal quantity = roundDecimalQuantity();

            return quantity.ToQuantity(GetQuantityTypeCode()) ?? throw new InvalidOperationException(null);

            #region Local methods
            decimal roundDecimalQuantity()
            {
                quantity = GetDecimalQuantity();

                return roundingMode switch
                {
                    RoundingMode.General => decimal.Round(quantity),
                    RoundingMode.Ceiling => decimal.Ceiling(quantity),
                    RoundingMode.Floor => decimal.Floor(quantity),
                    RoundingMode.Half => getHalfQuantity(),

                    _ => throw InvalidRoundingModeEnumArgumentException(roundingMode),
                };
            }

            decimal getHalfQuantity()
            {
                decimal halfQuantity = decimal.Floor(quantity);
                decimal half = 0.5m;

                if (quantity == halfQuantity) return quantity;

                halfQuantity += half;

                if (quantity <= halfQuantity) return halfQuantity;

                return halfQuantity + half;
            }
            #endregion
        }

        public object GetQuantity(TypeCode quantityTypeCode)
        {
            ValueType quantity = (ValueType)Quantity;

            return quantity.ToQuantity(quantityTypeCode) ?? throw InvalidQuantityTypeCodeEnumArgumentException(quantityTypeCode);
        }

        public virtual TypeCode? GetQuantityTypeCode(object quantity)
        {
            TypeCode quantityTypeCode = Type.GetTypeCode(quantity?.GetType());

            return GetValidQuantityTypeCodeOrNull(quantityTypeCode);
        }

        public IRateComponent GetRateComponent(Enum measureUnit, ValueType quantity)
        {
            return GetFactory().Create(measureUnit, quantity);
        }

        public RateComponentCode GetRateComponentCode()
        {
            return GetFactory().RateComponentCode;
        }

        public bool IsExchangeableTo(Enum? context)
        {
            return Measurement.IsExchangeableTo(context);
        }

        public decimal ProportionalTo(IRateComponent rateComponent)
        {
            MeasureUnitTypeCode measureUnitTypeCode = NullChecked(rateComponent, nameof(rateComponent)).MeasureUnitTypeCode;

            if (IsExchangeableTo(measureUnitTypeCode)) return DefaultQuantity / rateComponent.DefaultQuantity;

            throw InvalidMeasureUnitTypeCodeEnumArgumentException(measureUnitTypeCode, nameof(rateComponent));
        }

        public IRateComponent Round(RoundingMode roundingMode)
        {
            ValueType quantity = (ValueType)GetQuantity(roundingMode);
            Enum measureUnit = Measurement.GetMeasureUnit();

            return GetRateComponent(measureUnit, quantity);
        }

        public bool TryGetQuantity(ValueType quantity, [NotNullWhen(true)] out ValueType? thisTypeQuantity)
        {
            thisTypeQuantity = (ValueType?)quantity.ToQuantity(GetQuantityTypeCode());

            return thisTypeQuantity != null;
        }

        public void ValidateExchangeRate(decimal exchangeRate, string paramName)
        {
            Measurement.ValidateExchangeRate(exchangeRate, paramName);
        }

        public void ValidateQuantity(ValueType? quantity, TypeCode quantityTypeCode, string paramName)
        {
            TypeCode? typeCode = GetQuantityTypeCode(NullChecked(quantity, nameof(quantity)));

            ValidateQuantityTypeCode(quantityTypeCode, paramName);

            if (typeCode == quantityTypeCode) return;

            throw QuantityArgumentOutOfRangeException(quantity);
        }

        public void ValidateQuantityTypeCode(TypeCode quantityTypeCode, string paramName)
        {
            if (GetValidQuantityTypeCodeOrNull(quantityTypeCode) != null) return;

            throw InvalidQuantityTypeCodeEnumArgumentException(quantityTypeCode, paramName);
        }

        #region Override methods
        public override IRateComponentFactory GetFactory()
        {
            return (IRateComponentFactory)Factory;
        }

        public override void Validate(IRootObject? rootObject, string paramName)
        {
            Validate(this, rootObject, validateBaseMeasure, paramName);

            #region Local methods
            void validateBaseMeasure()
            {
                _ = GetValidBaseMeasure(this, rootObject!, paramName);
            }
            #endregion
        }

        public override void ValidateQuantity(ValueType? quantity, string paramName) // TODO
        {
            if (GetValidQuantityOrNull(this, NullChecked(quantity, paramName)) != null) return;

            throw QuantityArgumentOutOfRangeException(paramName, quantity);
        }

        #region Sealed methods
        public override sealed bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public override sealed int GetHashCode()
        {
            return HashCode.Combine(DefaultQuantity, MeasureUnitTypeCode);
        }

        public override sealed void ValidateMeasureUnit(Enum measureUnit, string paramName)
        {
            Measurement.ValidateMeasureUnit(measureUnit, paramName);
        }
        #endregion
        #endregion

        #region Virtual methods
        public virtual bool Equals(IRateComponent? other)
        {
            return DefaultQuantity == other?.DefaultQuantity
                && MeasureUnitTypeCode == other?.MeasureUnitTypeCode;
        }
        #endregion

        #region Abstract methods
        public abstract LimitMode? GetLimitMode();
        #endregion
        #endregion

        #region Protected methods
        protected TNum GetDefaultRateComponentQuantity<TNum>() where TNum : struct
        {
            return (TNum)GetFactory().DefaultRateComponentQuantity;
        }

        protected IRateComponent GetRateComponent(IRateComponent rateComponent, IRateComponentFactory factory)
        {
            if (rateComponent.IsExchangeableTo(MeasureUnitTypeCode)) return factory.Create(rateComponent);

            throw InvalidMeasureUnitTypeCodeEnumArgumentException(rateComponent.MeasureUnitTypeCode, nameof(rateComponent));
        }

        protected object GetValidQuantity(ValueType? quantity)
        {
            _ = NullChecked(quantity, nameof(quantity));

            return GetValidQuantityOrNull(this, quantity) ?? throw QuantityArgumentOutOfRangeException(quantity);
        }

        #region Static methods
        protected static T GetValidBaseMeasure<T>(T commonBase, IRootObject other, string paramName) where T : class, IRateComponent
        {
            T baseMeasure = GetValidMeasurable(commonBase, other, paramName);
            object quantity = baseMeasure.Quantity;

            if (GetValidQuantityOrNull(commonBase, baseMeasure.Quantity) != null) return baseMeasure;

            throw QuantityArgumentOutOfRangeException(paramName, (ValueType)quantity);
        }

        protected static object? GetValidQuantityOrNull(IRateComponent rateComponent, object? quantity)
        {
            quantity = ((ValueType?)quantity)?.ToQuantity(rateComponent.GetQuantityTypeCode());

            return rateComponent.GetRateComponentCode() switch
            {
                RateComponentCode.Denominator => getValidDenominatorQuantity(),
                RateComponentCode.Numerator or
                RateComponentCode.Limit => quantity,

                _ => throw new InvalidOperationException(null),
            };

            #region Local methods
            object? getValidDenominatorQuantity()
            {
                if (quantity == null || (decimal)quantity <= 0) return null;

                return quantity;
            }
            #endregion
        }

        protected static void ValidateBaseMeasure<T>(T commonBase, IRootObject other, string paramName) where T : class, IRateComponent
        {
            T baseMeasure = GetValidMeasurable(commonBase, other, paramName);
            RateComponentCode rateComponentCode = commonBase.GetRateComponentCode();
            RateComponentCode otherRateComponentCode = baseMeasure.GetRateComponentCode();

            _ = GetValidBaseMeasurable(baseMeasure, rateComponentCode, otherRateComponentCode, paramName);
        }
        #endregion
        #endregion

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
        #endregion
        #endregion
    }

    internal abstract class RateComponent<TSelf> : RateComponent, IRateComponent<TSelf> where TSelf : class, IRateComponent
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

        public virtual TSelf? GetRateComponent(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ValueType quantity)
        {
            return GetFactory().Create(customName, measureUnitTypeCode, exchangeRate, quantity);
        }

        public bool TryGetRateComponent(Enum measureUnit, decimal exchangeRate, ValueType quantity, string customName, [NotNullWhen(true)] out IRateComponent? rateComponent)
        {
            rateComponent = GetRateComponent(measureUnit, exchangeRate, quantity, customName);

            return rateComponent != null;
        }

        public bool TryGetRateComponent(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ValueType quantity, [NotNullWhen(true)] out IRateComponent? rateComponent)
        {
            rateComponent = GetRateComponent(customName, measureUnitTypeCode, exchangeRate, quantity);

            return rateComponent != null;
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

    internal abstract class RateComponent<TSelf, TNum> : RateComponent<TSelf>, IRateComponent<TSelf, TNum> where TSelf : class, IRateComponent, IDefaultRateComponent where TNum : struct
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
            return GetDefault(MeasureUnitTypeCode)!;
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
        public abstract TSelf? GetDefault(MeasureUnitTypeCode measureUnitTypeCode);
        #endregion
        #endregion
    }
}