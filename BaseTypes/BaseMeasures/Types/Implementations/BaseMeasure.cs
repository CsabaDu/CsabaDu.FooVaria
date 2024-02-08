namespace CsabaDu.FooVaria.BaseTypes.BaseMeasures.Types.Implementations
{
    public abstract class BaseMeasure : Quantifiable<IBaseMeasure>, IBaseMeasure
    {
        #region Constructors
        protected BaseMeasure(IBaseMeasureFactory factory, Enum measureUnit) : base(factory, measureUnit)
        {
        }

        protected BaseMeasure(IBaseMeasureFactory factory, IBaseMeasurement baseMeasurement) : base(factory, baseMeasurement)
        {
        }
        #endregion

        #region Properties
        #region Abstract properties
        public abstract object Quantity { get; init; }
        #endregion
        #endregion

        #region Public methods
        public bool Equals(IBaseMeasure? x, IBaseMeasure? y)
        {
            if (x == null && y == null) return true;

            return x != null
                && y != null
                && x.GetLimitMode() == y.GetLimitMode()
                && x.GetRateComponentCode() == y.GetRateComponentCode()
                && x.Equals(y);
        }


        public bool? FitsIn(ILimiter? limiter)
        {
            if (limiter is not IBaseMeasure baseMeasure) return null;

            LimitMode limitMode = limiter.LimitMode;

            return FitsIn(baseMeasure, limitMode);
        }

        public IBaseMeasure GetBaseMeasure(ValueType quantity)
        {
            Enum measureUnit = GetMeasureUnit();
            IBaseMeasurementFactory factory = GetBaseMeasurementFactory();
            IBaseMeasurement baseMeasurement = factory.CreateBaseMeasurement(measureUnit)!;

            return GetBaseMeasure(baseMeasurement, quantity);
        }

        public IBaseMeasure GetBaseMeasure(IBaseMeasurement baseMeasurement, ValueType quantity)
        {
            return GetFactory().CreateBaseMeasure(baseMeasurement, quantity);
        }

        public decimal GetDecimalQuantity()
        {
            return (decimal)GetQuantity(TypeCode.Decimal);
        }

        public decimal GetExchangeRate()
        {
            IBaseMeasurement baseMeasurement = GetBaseMeasurement();

            return baseMeasurement.GetExchangeRate();
        }

        public int GetHashCode([DisallowNull] IBaseMeasure other)
        {
            return HashCode.Combine(other.GetLimitMode(), other.GetRateComponentCode(), other.GetHashCode());
        }

        public object GetQuantity(RoundingMode roundingMode)
        {
            return GetDecimalQuantity().Round(roundingMode);
        }

        public object GetQuantity(TypeCode quantityTypeCode)
        {
            ValueType quantity = (ValueType)Quantity;

            return quantity.ToQuantity(quantityTypeCode) ?? throw InvalidQuantityTypeCodeEnumArgumentException(quantityTypeCode);
        }

        public TypeCode? GetQuantityTypeCode(object quantity)
        {
            TypeCode quantityTypeCode = Type.GetTypeCode(quantity?.GetType());

            return GetValidQuantityTypeCodeOrNull(quantityTypeCode);
        }

        public RateComponentCode GetRateComponentCode()
        {
            return GetFactory().RateComponentCode;
        }

        public IBaseMeasure Round(RoundingMode roundingMode)
        {
            ValueType quantity = (ValueType)GetQuantity(roundingMode);
            IBaseMeasurement baseMeasurement = GetBaseMeasurement();

            return GetBaseMeasure(baseMeasurement, quantity);
        }

        public void ValidateExchangeRate(decimal exchangeRate, string paramName)
        {
            IBaseMeasurement baseMeasurement = GetBaseMeasurement();

            baseMeasurement.ValidateExchangeRate(exchangeRate, paramName);
        }

        #region Override methods
        public override IBaseMeasureFactory GetFactory()
        {
            return (IBaseMeasureFactory)Factory;
        }

        public override Enum GetMeasureUnit()
        {
            IBaseMeasurement baseMeasurement = GetBaseMeasurement();

            return baseMeasurement.GetMeasureUnit();
        }

        #region Sealed methods
        public override sealed IBaseMeasure? ExchangeTo(Enum? context)
        {
            if (!IsExchangeableTo(context)) return null;

            if (!IsValidMeasureUnit(context))
            {
                if (context is not MeasureUnitCode measureUnitCode) return null;

                context = GetDefaultMeasureUnit(measureUnitCode);
            }

            IBaseMeasurementFactory factory = GetBaseMeasurementFactory();
            IBaseMeasurement baseMeasurement = factory.CreateBaseMeasurement(context!)!;
            decimal quantity = GetDefaultQuantity();
            quantity /= BaseMeasurement.GetExchangeRate(context!);

            return GetBaseMeasure(baseMeasurement, quantity);
        }

        public override sealed bool? FitsIn(IBaseMeasure? other, LimitMode? limitMode)
        {
            bool limitModeHasValue = limitMode.HasValue;

            if (isbaseMeasureNull() && !limitModeHasValue) return true;

            if (other?.HasMeasureUnitCode(MeasureUnitCode) != true) return null;

            if (!limitModeHasValue) return CompareTo(other) <= 0;

            _ = Defined(limitMode!.Value, nameof(limitMode));

            IBaseMeasure ceilingBaseMeasure = other.Round(RoundingMode.Ceiling);
            other = limitMode switch
            {
                LimitMode.BeNotLess or
                LimitMode.BeGreater => ceilingBaseMeasure,

                LimitMode.BeNotGreater or
                LimitMode.BeLess or
                LimitMode.BeEqual => other!.Round(RoundingMode.Floor),

                _ => null,
            };

            if (isbaseMeasureNull()) return null;

            int comparison = CompareTo(other);

            if (limitMode == LimitMode.BeEqual) return areEqual();

            return comparison.FitsIn(limitMode);

            #region Local methods
            bool areEqual()
            {
                return comparison == 0 && ceilingBaseMeasure.Equals(other);
            }

            bool isbaseMeasureNull()
            {
                return other == null;
            }
            #endregion
        }

        public override sealed void ValidateQuantity(IQuantifiable? quantifiable, string paramName)
        {
            if (NullChecked(quantifiable, paramName) is IBaseMeasure baseMeasure)
            {
                ValueType quantity = (ValueType)baseMeasure.Quantity;

                ValidateQuantity(quantity, paramName);
            }

            throw ArgumentTypeOutOfRangeException(paramName, quantifiable!);
        }

        #endregion
        #endregion

        #region Virtual methods
        public virtual LimitMode? GetLimitMode()
        {
            return null;
        }
        #endregion

        #region Abstract methods
        public abstract IBaseMeasurement GetBaseMeasurement();
        public abstract IBaseMeasurementFactory GetBaseMeasurementFactory();
        #endregion

        #region Static methods
        public static void ValidateQuantity(ValueType? quantity, TypeCode quantityTypeCode, string paramName)
        {
            Type quantityType = NullChecked(quantity, paramName).GetType();

            ValidateQuantityTypeCode(quantityTypeCode, nameof(quantityTypeCode));

            if (quantityTypeCode == Type.GetTypeCode(quantityType)) return;

            throw ArgumentTypeOutOfRangeException(paramName, quantity!);
        }

        public static void ValidateQuantityTypeCode(TypeCode quantityTypeCode, string paramName)
        {
            if (GetValidQuantityTypeCodeOrNull(quantityTypeCode) != null) return;

            throw InvalidQuantityTypeCodeEnumArgumentException(quantityTypeCode, paramName);
        }
        #endregion
        #endregion

        #region Protected methods
        #region Static methods
        protected static object? GetValidQuantityOrNull(IBaseMeasure baseMeasure, object? quantity)
        {
            quantity = ((ValueType?)quantity)?.ToQuantity(baseMeasure.GetQuantityTypeCode());

            return baseMeasure.GetRateComponentCode() switch
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

        protected static TSelf ConvertToLimitable<TSelf>(IBaseMeasure<TSelf> limitable, ILimiter limiter)
            where TSelf : class, IBaseMeasure
        {
            string paramName = nameof(limiter);

            if (NullChecked(limiter, paramName) is IBaseMeasure baseMeasure) return limitable.GetBaseMeasure(baseMeasure);

            throw ArgumentTypeOutOfRangeException(paramName, limiter);
        }
        #endregion
        #endregion

        #region Private methods
        #region Static methods
        private static TypeCode? GetValidQuantityTypeCodeOrNull(TypeCode quantityTypeCode)
        {
            if (GetQuantityTypeCodes().Contains(quantityTypeCode)) return quantityTypeCode;

            return null;
        }
        #endregion
        #endregion
    }

    public abstract class BaseMeasure<TSeff> : BaseMeasure, IBaseMeasure<TSeff>
        where TSeff : class, IBaseMeasure
    {
        #region Constructors
        protected BaseMeasure(IBaseMeasureFactory<TSeff> factory, Enum measureUnit) : base(factory, measureUnit)
        {
        }
        #endregion

        #region Public methods
        public TSeff GetBaseMeasure(Enum measureUnit, ValueType quantity)
        {
            return GetFactory().Create(measureUnit, quantity);
        }

        public TSeff GetBaseMeasure(string name, ValueType quantity)
        {
            return GetFactory().Create(name, quantity);
        }

        public TSeff? GetBaseMeasure(Enum measureUnit, decimal exchangeRate, ValueType quantity, string customName)
        {
            return GetFactory().Create(measureUnit, exchangeRate, quantity, customName);
        }

        public TSeff? GetBaseMeasure(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate, ValueType quantity)
        {
            return GetFactory().Create(customName, measureUnitCode, exchangeRate, quantity);
        }

        public TSeff GetBaseMeasure(IBaseMeasure baseMeasure)
        {
            return GetFactory().Create(baseMeasure);
        }

        #region Override methods
        public override IBaseMeasureFactory<TSeff> GetFactory()
        {
            return (IBaseMeasureFactory<TSeff>)Factory;
        }

        public TSeff ConvertToLimitable(ILimiter limiter)
        {
            return ConvertToLimitable(this, limiter);
        }
        #endregion
        #endregion
    }
}
