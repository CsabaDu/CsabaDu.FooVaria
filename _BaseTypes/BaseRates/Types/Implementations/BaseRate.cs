namespace CsabaDu.FooVaria.BaseTypes.BaseRates.Types.Implementations
{
    public abstract class BaseRate : BaseQuantifiable, IBaseRate
    {
        #region Constructors
        protected BaseRate(IBaseRate other) : base(other)
        {
        }

        protected BaseRate(IBaseRateFactory factory) : base(factory)
        {
        }
        #endregion

        #region Public methods
        public int CompareTo(IBaseRate? other)
        {
            if (other == null) return 1;

            ValidateMeasureUnitCodes(other, nameof(other));

            return GetDefaultQuantity().CompareTo(other.GetDefaultQuantity());
        }

        public bool Equals(IBaseRate? other)
        {
            return base.Equals(other)
                && other.GetNumeratorCode() == GetNumeratorCode();
        }

        public override sealed bool? FitsIn(ILimiter? limiter)
        {
            if (limiter is not IBaseRate baseRate) return null;

            return FitsIn(baseRate, limiter?.GetLimitMode());
        }

        public bool? FitsIn(IBaseRate? other, LimitMode? limitMode)
        {
            if (other == null && !limitMode.HasValue) return true;

            if (!IsExchangeableTo(other)) return null;

            int comparison = CompareTo(other);

            LimitMode limitModeValue = limitMode ?? LimitMode.BeNotGreater;

            if (!limitModeValue.IsDefined()) return null;

            return comparison.FitsIn(limitModeValue);
        }

        public ValueType GetBaseQuantity()
        {
            return GetQuantity();
        }

        public IBaseRate GetBaseRate(IQuantifiable numerator, Enum denominator)
        {
            return GetFactory().CreateBaseRate(numerator, denominator);
        }

        public IBaseRate GetBaseRate(IQuantifiable numerator, IMeasurable denominator)
        {
            return GetFactory().CreateBaseRate(numerator, denominator);
        }

        public IBaseRate GetBaseRate(IQuantifiable numerator, IQuantifiable denominator)
        {
            return GetFactory().CreateBaseRate(numerator, denominator);
        }

        public abstract MeasureUnitCode GetMeasureUnitCode(RateComponentCode rateComponentCode);

        public decimal GetQuantity()
        {
            return GetDefaultQuantity();
        }

        public object GetQuantity(TypeCode quantityTypeCode)
        {
            object? quantity = GetQuantity().ToQuantity(Defined(quantityTypeCode, nameof(quantityTypeCode)));

            if (quantity != null) return quantity;

            throw new InvalidOperationException(null);
        }

        public bool IsExchangeableTo(IBaseRate? baseRate)
        {
            return baseRate?.HasMeasureUnitCode(GetMeasureUnitCode()) == true
                && baseRate.GetNumeratorCode() == GetNumeratorCode();
        }

        public bool IsValidMeasureUnitCode(MeasureUnitCode measureUnitCode)
        {
            return IsValidMeasureUnitCode(this, measureUnitCode);
        }

        public bool IsValidRateComponent(object? rateComponent, RateComponentCode rateComponentCode)
        {
            return GetRateComponent(rateComponentCode)?.Equals(rateComponent) == true;
        }

        public decimal ProportionalTo(IBaseRate? other)
        {
            const string paramName = nameof(other);

            decimal defaultQuantity = NullChecked(other, paramName).GetDefaultQuantity();

            ValidateMeasureUnitCodes(other, paramName);

            return Math.Abs(GetDefaultQuantity() / defaultQuantity);
        }

        public void ValidateMeasureUnitCodes(IBaseQuantifiable? baseQuantifiable, string paramName)
        {
            ValidateMeasureUnitCodes(this, baseQuantifiable, paramName);
        }

        public void ValidateRateComponentCode(RateComponentCode rateComponentCode, string paramName)
        {
            object? rateComponent = GetRateComponent(Defined(rateComponentCode, paramName));

            if (rateComponent != null) return;

            throw InvalidRateComponentCodeArgumentException(rateComponentCode);
        }

        #region Override methods
        public override IBaseRateFactory GetFactory()
        {
            return (IBaseRateFactory)Factory;
        }

        #region Sealed methods
        public override sealed bool Equals(object? obj)
        {
            return obj is IBaseRate baseRate && Equals(baseRate);
        }

        public override sealed int GetHashCode()
        {
            return HashCode.Combine(GetDenominatorCode(), GetNumeratorCode(), GetDefaultQuantity());
        }

        public override sealed MeasureUnitCode GetMeasureUnitCode()
        {
            return GetDenominatorCode();
        }

        public override sealed TypeCode GetQuantityTypeCode()
        {
            Type quantityType = GetQuantity().GetType();

            return Type.GetTypeCode(quantityType);
        }

        public override sealed void ValidateMeasureUnit(Enum? measureUnit, string paramName)
        {
            base.ValidateMeasureUnit(measureUnit, paramName);
        }

        public override sealed void ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, string paramName)
        {
            ValidateMeasureUnitCode(this, measureUnitCode, paramName);
        }

        public override sealed void ValidateQuantity(IBaseQuantifiable? baseQuantifiable, string paramName)
        {
            base.ValidateQuantity(baseQuantifiable, paramName);
        }

        public override sealed void ValidateQuantity(ValueType? quantity, string paramName)
        {
            ValidatePositiveQuantity(quantity, paramName);
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
        //public abstract IBaseRate GetBaseRate(MeasureUnitCode numeratorCode, decimal defaultQuantity, MeasureUnitCode denominatorCode);
        public abstract MeasureUnitCode GetDenominatorCode();
        public abstract IEnumerable<MeasureUnitCode> GetMeasureUnitCodes();
        public abstract MeasureUnitCode GetNumeratorCode();
        public abstract object? GetRateComponent(RateComponentCode rateComponentCode);

        #endregion

        #region Static methods
        public static IEnumerable<RateComponentCode> GetRateComponentCodes()
        {
            return Enum.GetValues<RateComponentCode>();
        }
        #endregion
        #endregion
    }
}
