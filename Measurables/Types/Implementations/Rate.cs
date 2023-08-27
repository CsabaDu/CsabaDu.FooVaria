namespace CsabaDu.FooVaria.Measurables.Types.Implementations
{
    internal abstract class Rate : Measurable, IRate
    {
        private protected Rate(IRate rate) : base(rate)
        {
            Numerator = rate.Numerator;
            Denominator = rate.Denominator;
        }

        private protected Rate(IRateFactory rateFactory, IMeasure numerator, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ValueType? quantity, ILimit? limit) : base(rateFactory, measureUnitTypeCode)
        {
            Numerator = NullChecked(numerator, nameof(numerator));
            Denominator = GetDenominatorFactory(rateFactory).Create(customName, measureUnitTypeCode, exchangeRate, quantity);
        }

        private protected Rate(IRateFactory rateFactory, IMeasure numerator, Enum measureUnit, ValueType? quantity, ILimit? limit) : base(rateFactory, measureUnit)
        {
            Numerator = NullChecked(numerator, nameof(numerator));
            Denominator = GetDenominatorFactory(rateFactory).Create(measureUnit, quantity);
        }

        private protected Rate(IRateFactory rateFactory, IMeasure numerator, Enum measureUnit, decimal exchangeRate, string customName, ValueType? quantity, ILimit? limit) : base(rateFactory, measureUnit)
        {
            Numerator = NullChecked(numerator, nameof(numerator));
            Denominator = GetDenominatorFactory(rateFactory).Create(measureUnit, exchangeRate, customName, quantity);
        }

        private protected Rate(IRateFactory rateFactory, IRate rate, ILimit? limit) : base(rateFactory, rate)
        {
            Numerator = rate.Numerator;
            Denominator = rate.Denominator;
        }

        private protected Rate(IRateFactory rateFactory, IMeasure numerator, IDenominator denominator, ILimit? limit) : base(rateFactory, denominator)
        {
            Numerator = NullChecked(numerator, nameof(numerator));
            Denominator = NullChecked(denominator, nameof(denominator));
        }

        public IBaseMeasure? this[RateComponentCode rateComponentCode] => rateComponentCode switch
        {
            RateComponentCode.Denominator => Denominator,
            RateComponentCode.Numerator => Numerator,
            RateComponentCode.Limit => GetLimit(),

            _ => null,
        };

        public IDenominator Denominator { get; init; }
        public IMeasure Numerator { get; init; }

        public abstract int CompareTo(IRate? other);
        public abstract bool Equals(IRate? other);
        public abstract IRate? ExchangeTo(IDenominator context);

        public override IMeasurable GetDefault()
        {
            throw new NotImplementedException();
        }

        public abstract decimal GetDefaultQuantity();
        public abstract ILimit? GetLimit();

        public override Enum GetMeasureUnit()
        {
            throw new NotImplementedException();
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
        public abstract IBaseMeasure? GetRateComponent(RateComponentCode rateComponentCode);
        public abstract bool IsExchangeableTo(IRate? context);
        public abstract decimal ProportionalTo(IRate comparable);
        public abstract bool TryExchangeTo(IDenominator context, [NotNullWhen(true)] out IRate? exchanged);

        #region Private methods
        private static IDenominatorFactory GetDenominatorFactory(IRateFactory rateFactory)
        {
            return rateFactory.DenominatorFactory;
        }
        #endregion
    }
}
