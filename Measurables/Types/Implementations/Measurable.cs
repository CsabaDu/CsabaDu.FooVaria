namespace CsabaDu.FooVaria.Measurables.Types.Implementations
{
    internal abstract class Measurable : BaseMeasurable, IMeasurable
    {
        #region Constructors

        private protected Measurable(IMeasurableFactory measurableFactory, MeasureUnitTypeCode measureUnitTypeCode) : base(measureUnitTypeCode)
        {
            MeasurableFactory = NullChecked(measurableFactory, nameof(measurableFactory));
        }

        private protected Measurable(IMeasurableFactory measurableFactory, Enum measureUnit) : base(measureUnit)
        {
            MeasurableFactory = NullChecked(measurableFactory, nameof(measurableFactory));
        }

        private protected Measurable(IMeasurableFactory measurableFactory, IMeasurable measurable) : base(measurable)
        {
            MeasurableFactory = NullChecked(measurableFactory, nameof(measurableFactory));
        }

        private protected Measurable(IMeasurable measurable) : base(measurable)
        {
            MeasurableFactory = measurable.MeasurableFactory;
        }

        private protected Measurable(IMeasurableFactory measurableFactory, params IQuantifiable[] quantifiables) : base(quantifiables)
        {
            MeasurableFactory = NullChecked(measurableFactory, nameof(measurableFactory));
        }
        #endregion

        #region Properties
        public IMeasurableFactory MeasurableFactory { get; init; }
        #endregion

        #region Public methods
        public override bool Equals(object? obj)
        {
            return obj is IMeasurable other
                && MeasureUnitTypeCode == other.MeasureUnitTypeCode
                && MeasurableFactory.Equals(other.MeasurableFactory);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(MeasureUnitTypeCode, MeasurableFactory);
        }

        public IMeasurable GetMeasurable(IMeasurable? measurable = null)
        {
            return MeasurableFactory.Create(measurable ?? this);
        }

        public virtual IMeasurable GetMeasurable(IMeasurableFactory measurableFactory, IMeasurable measurable)
        {
            return NullChecked(measurableFactory, nameof(measurableFactory)).Create(measurable);
        }

        public TypeCode GetQuantityTypeCode(MeasureUnitTypeCode? measureUnitTypeCode = null)
        {
            TypeCode? quantityTypeCode = (measureUnitTypeCode ?? MeasureUnitTypeCode).GetQuantityTypeCode();

            return quantityTypeCode ?? throw InvalidMeasureUnitTypeCodeEnumArgumentException(measureUnitTypeCode!.Value);
        }

        #region Abstract methods
        public abstract IMeasurable GetDefault();
        #endregion
        #endregion

    }

    internal abstract class Rate : Measurable, IRate
    {
        private protected Rate(IMeasurable measurable) : base(measurable)
        {
        }

        private protected Rate(IMeasurableFactory measurableFactory, MeasureUnitTypeCode measureUnitTypeCode) : base(measurableFactory, measureUnitTypeCode)
        {
        }

        private protected Rate(IMeasurableFactory measurableFactory, Enum measureUnit) : base(measurableFactory, measureUnit)
        {
        }

        private protected Rate(IMeasurableFactory measurableFactory, IMeasurable measurable) : base(measurableFactory, measurable)
        {
        }

        private protected Rate(IMeasurableFactory measurableFactory, params IQuantifiable[] quantifiables) : base(measurableFactory, quantifiables)
        {
        }

        public abstract IBaseMeasure? this[RateComponentCode rateComponentCode] { get; }

        public abstract IDenominator Denominator { get; init; }
        public abstract IMeasure Numerator { get; init; }

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

        public abstract IRate GetRate(IMeasure numerator, Enum measureUnit, decimal? exchangeRate = null, ValueType? quantity = null, ILimit? limit = null);
        public abstract IRate GetRate(IMeasure numerator, IMeasurement measurement, ValueType? quantity = null, ILimit? limit = null);
        public abstract IRate GetRate(IMeasure numerator, IDenominator? denominator = null, ILimit? limit = null);
        public abstract IRate GetRate(IRate? other = null);
        public abstract IBaseMeasure? GetRateComponent(RateComponentCode rateComponentCode);
        public abstract bool IsExchangeableTo(IRate? context);
        public abstract decimal ProportionalTo(IRate comparable);
        public abstract bool TryExchangeTo(IDenominator context, [NotNullWhen(true)] out IRate? exchanged);
    }
}
