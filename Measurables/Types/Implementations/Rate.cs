namespace CsabaDu.FooVaria.Measurables.Types.Implementations
{
    internal abstract class Rate : Measurable, IRate
    {
        #region Constructors
        private protected Rate(IRate rate) : base(rate)
        {
            Numerator = rate.Numerator;
            Denominator = rate.Denominator;
        }

        private protected Rate(IRateFactory rateFactory, IMeasure numerator, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ValueType? quantity) : base(rateFactory, measureUnitTypeCode)
        {
            Numerator = NullChecked(numerator, nameof(numerator));
            Denominator = GetDenominatorFactory(rateFactory).Create(customName, measureUnitTypeCode, exchangeRate, quantity);
        }

        private protected Rate(IRateFactory rateFactory, IMeasure numerator, Enum measureUnit, ValueType? quantity) : base(rateFactory, measureUnit)
        {
            Numerator = NullChecked(numerator, nameof(numerator));
            Denominator = GetDenominatorFactory(rateFactory).Create(measureUnit, quantity);
        }

        private protected Rate(IRateFactory rateFactory, IMeasure numerator, Enum measureUnit, decimal exchangeRate, string customName, ValueType? quantity) : base(rateFactory, measureUnit)
        {
            Numerator = NullChecked(numerator, nameof(numerator));
            Denominator = GetDenominatorFactory(rateFactory).Create(measureUnit, exchangeRate, customName, quantity);
        }

        private protected Rate(IRateFactory rateFactory, IRate rate) : base(rateFactory, rate)
        {
            Numerator = rate.Numerator;
            Denominator = rate.Denominator;
        }

        private protected Rate(IRateFactory rateFactory, IMeasure numerator, IDenominator denominator) : base(rateFactory, denominator)
        {
            Numerator = NullChecked(numerator, nameof(numerator));
            Denominator = NullChecked(denominator, nameof(denominator));
        }
        #endregion

        #region Properties
        public IBaseMeasure? this[RateComponentCode rateComponentCode] => rateComponentCode switch
        {
            RateComponentCode.Denominator => Denominator,
            RateComponentCode.Numerator => Numerator,
            RateComponentCode.Limit => GetLimit(),

            _ => null,
        };
        public IDenominator Denominator { get; init; }
        public IMeasure Numerator { get; init; }
        #endregion

        #region Public methods
        public int CompareTo(IRate? other)
        {
            if (other == null) return 1;

            if (!IsExchangeableTo(other)) throw new ArgumentOutOfRangeException(nameof(other));

            return GetDefaultQuantity().CompareTo(GetDefaultQuantity(other));
        }

        public bool Equals(IRate? other)
        {
            return other is IRate rate
                && Numerator.Equals(rate.Numerator)
                && Denominator.Equals(rate.Denominator);
        }

        public override bool Equals(object? obj)
        {
            return obj is IRate other
                && Equals(other);
        }

        public IRate? ExchangeTo(IDenominator denominator)
        {
            if (!denominator.IsExchangeableTo(MeasureUnitTypeCode)) return null;

            throw new NotImplementedException();
        }

        public override IMeasurable GetDefault()
        {
            IMeasure numerator = (IMeasure)Numerator.GetDefault();
            IDenominator denominator = (IDenominator)Denominator.GetDefault();

            return GetRate(numerator, denominator);
        }

        public decimal GetDefaultQuantity(IRate? rate = null)
        {
            rate ??= this;
            decimal numeratorQuantity = rate.Numerator.DefaultQuantity;
            decimal denominatorQuantity = rate.Denominator.DefaultQuantity;

            return numeratorQuantity / denominatorQuantity;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Numerator, Denominator);
        }

        public virtual ILimit? GetLimit()
        {
            return null;
        }

        public override sealed Enum GetMeasureUnit()
        {
            return Denominator.GetMeasureUnit();
        }

        public override sealed TypeCode GetQuantityTypeCode(MeasureUnitTypeCode? measureUnitTypeCode = null)
        {
            return base.GetQuantityTypeCode(measureUnitTypeCode ?? Numerator.MeasureUnitTypeCode);
        }

        public abstract IRate GetRate(IMeasure numerator, string customName, ValueType? quantity = null, ILimit? limit = null);
        public abstract IRate GetRate(IMeasure numerator, Enum measureUnit, ValueType? quantity = null, ILimit? limit = null);
        public abstract IRate GetRate(IMeasure numerator, Enum measureUnit, decimal exchangeRate, string customName, ValueType? quantity = null, ILimit? limit = null);
        public abstract IRate GetRate(IMeasure numerator, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ValueType? quantity = null, ILimit? limit = null);
        public abstract IRate GetRate(IMeasure numerator, IMeasurement measurement, ValueType? quantity = null, ILimit? limit = null);
        public abstract IRate GetRate(IMeasure numerator, IDenominator? denominator = null, ILimit? limit = null);
        public abstract IRate GetRate(IRate? other = null);

        public IBaseMeasure? GetRateComponent(RateComponentCode rateComponentCode)
        {
            return this[rateComponentCode];
        }

        public bool IsExchangeableTo(IRate? other)
        {
            return other != null
                && Numerator.IsExchangeableTo(other.Numerator.MeasureUnitTypeCode)
                && Denominator.IsExchangeableTo(other.Denominator.MeasureUnitTypeCode);
        }

        public decimal ProportionalTo(IRate rate)
        {
            decimal defaultQuantity = GetDefaultQuantity(NullChecked(rate, nameof(rate)));

            return GetDefaultQuantity() / defaultQuantity;
        }

        public bool TryExchangeTo(IDenominator denominator, [NotNullWhen(true)] out IRate? exchanged)
        {
            exchanged = ExchangeTo(denominator);

            return exchanged != null;
        }
        #region Abstract methods
        #endregion
        #endregion

        #region Private methods
        private static IDenominatorFactory GetDenominatorFactory(IRateFactory rateFactory)
        {
            return rateFactory.DenominatorFactory;
        }
        #endregion
    }
}
