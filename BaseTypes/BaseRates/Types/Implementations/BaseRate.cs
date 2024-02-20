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

        #region Properties
        #region Abstract properties
        public abstract object? this[RateComponentCode rateComponentCode] { get; }
        #endregion
        #endregion

        #region Public methods
        public int CompareTo(IBaseRate? other)
        {
            if (other == null) return 1;

            ValidateMeasureUnitCodes(other);

            return GetDefaultQuantity().CompareTo(other.GetDefaultQuantity());
        }

        public IBaseRate ConvertToLimitable(ILimiter limiter)
        {
            string paramName = nameof(limiter);

            if (NullChecked(limiter, paramName) is IBaseRate baseRate) return GetBaseRate(baseRate);

            throw ArgumentTypeOutOfRangeException(paramName, limiter);
        }

        public bool Equals(IBaseRate? other)
        {
            return base.Equals(other)
                && other.GetNumeratorCode() == GetNumeratorCode();
        }

        public bool? FitsIn(ILimiter? limiter)
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

        public IBaseRate GetBaseRate(IBaseRate baseRate)
        {
            return GetFactory().CreateBaseRate(baseRate);
        }

        public IBaseRate GetBaseRate(IBaseMeasure numerator, IBaseMeasurement denominatorMeasurement)
        {
            return GetFactory().CreateBaseRate(numerator, denominatorMeasurement);
        }

        public IBaseRate GetBaseRate(IBaseMeasure numerator, Enum denominatorMeasureUnit)
        {
            return GetFactory().CreateBaseRate(numerator, denominatorMeasureUnit);
        }

        public IBaseRate GetBaseRate(IBaseMeasure numerator, MeasureUnitCode denominatorCode)
        {
            return GetFactory().CreateBaseRate(numerator, denominatorCode);
        }

        public IBaseRate GetBaseRate(params IBaseMeasure[] baseMeasures)
        {
            return GetFactory().CreateBaseRate(baseMeasures);
        }

        public MeasureUnitCode GetMeasureUnitCode(RateComponentCode rateComponentCode)
        {
            return rateComponentCode switch
            {
                RateComponentCode.Numerator => GetNumeratorCode(),
                RateComponentCode.Denominator => GetDenominatorCode(),

                _ => throw InvalidRateComponentCodeArgumentException(rateComponentCode)
            };
        }

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
            return baseRate != null
                && baseRate.HasMeasureUnitCode(GetMeasureUnitCode())
                && baseRate.GetNumeratorCode() == GetNumeratorCode();
        }

        public decimal ProportionalTo(IBaseRate? other)
        {
            decimal defaultQuantity = NullChecked(other, nameof(other)).GetDefaultQuantity();

            ValidateMeasureUnitCodes(other!);

            return Math.Abs(GetDefaultQuantity() / defaultQuantity);
        }

        #region Override methods
        public override IBaseRateFactory GetFactory()
        {
            return (IBaseRateFactory)Factory;
        }

        public override sealed void ValidateQuantity(IBaseQuantifiable? baseQuantifiable, string paramName)
        {
            base.ValidateQuantity(baseQuantifiable, paramName);
        }

        public override IEnumerable<MeasureUnitCode> GetMeasureUnitCodes()
        {
            yield return GetNumeratorCode();
            yield return GetDenominatorCode();
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
        public abstract IBaseRate GetBaseRate(MeasureUnitCode numeratorCode, decimal defaultQuantity, MeasureUnitCode denominatorCode);
        public abstract MeasureUnitCode GetDenominatorCode();
        public abstract MeasureUnitCode GetNumeratorCode();

        #endregion

        #region Static methods
        public static IEnumerable<RateComponentCode> GetRateComponentCodes()
        {
            return Enum.GetValues<RateComponentCode>();
        }
        #endregion
        #endregion

        #region Protected methods
        protected void ValidateMeasureUnitCodes(IBaseRate other)
        {
            if (IsExchangeableTo(other)) return;

            MeasureUnitCode measureUnitCode  = GetMeasureUnitCode();

            measureUnitCode = other.HasMeasureUnitCode(measureUnitCode) ?
                GetNumeratorCode()
                : measureUnitCode;

            throw InvalidMeasureUnitCodeEnumArgumentException(measureUnitCode, nameof(other));
        }
        #endregion
    }
}
