using System.ComponentModel;

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

        //private protected Measurable(IMeasurableFactory measurableFactory, params IBaseMeasure[] baseMeasures) : this(measurableFactory)
        //{
        //    _ = NullChecked(baseMeasures, nameof(baseMeasures));

        //    MeasureUnitTypeCode = GetMeasureUnitTypeCode(measurableFactory, baseMeasures);
        //}
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

        public abstract IMeasurable GetDefault();
        #endregion
    }

    internal abstract class Measure : BaseMeasure, IMeasure
    {
        #region Enums
        protected enum SummingMode
        {
            Add,
            Subtract,
        }
        #endregion

        #region Private constants
        private const int DefaultMeasureQuantity = 0;
        #endregion

        private protected Measure(IMeasure measure) : base(measure)
        {
            Quantity = measure.Quantity;
        }

        private protected Measure(IBaseMeasureFactory baseMeasureFactory, ValueType quantity, Enum measureUnit) : base(baseMeasureFactory, quantity, measureUnit)
        {
            Quantity = GetQuantity(quantity);
        }

        private protected Measure(IBaseMeasureFactory baseMeasureFactory, ValueType quantity, IMeasurement measurement) : base(baseMeasureFactory, quantity, measurement)
        {
            Quantity = GetQuantity(quantity);
        }

        private protected Measure(IBaseMeasureFactory baseMeasureFactory, ValueType quantity, MeasureUnitTypeCode measureUnitTypeCode, string customName, decimal exchangeRate) : base(baseMeasureFactory, quantity, measureUnitTypeCode, exchangeRate, customName)
        {
            Quantity = GetQuantity(quantity);
        }

        private protected Measure(IBaseMeasureFactory baseMeasureFactory, ValueType quantity, Enum measureUnit, decimal exchangeRate, string customName) : base(baseMeasureFactory, quantity, measureUnit, exchangeRate, customName)
        {
            Quantity = GetQuantity(quantity);
        }

        public override sealed object Quantity { get; init; }
        public override sealed TypeCode QuantityTypeCode => GetQuantityTypeCode();

        public IMeasure Add(IMeasure? other)
        {
            return GetSum(other, SummingMode.Add);
        }

        public IMeasure Divide(decimal divisor)
        {
            if (divisor == 0) throw new ArgumentOutOfRangeException(nameof(divisor), divisor, null);

            decimal quantity = decimal.Divide(GetDecimalQuantity(), divisor);

            return GetMeasure(quantity);
        }

        public override sealed bool Equals(IBaseMeasure? other)
        {
            return Equals(this, other);
        }

        public override sealed bool Equals(object? obj)
        {
            return obj is IMeasure measure && Equals(measure);
        }

        public bool? FitsIn(ILimit? limit)
        {
            return FitsIn(limit, limit?.LimitMode);
        }

        public bool? FitsIn(IBaseMeasure? baseMeasure, LimitMode? limitMode)
        {
            if (baseMeasure == null && limitMode == null) return true;

            if (baseMeasure?.IsExchangeableTo(MeasureUnitTypeCode) != true) return null;

            limitMode ??= default;
            IBaseMeasure ceilingBaseMeasure = baseMeasure.Round(RoundingMode.Ceiling);
            baseMeasure = getRoundedBaseMeasure();

            if (baseMeasure == null) return null;

            int comparison = CompareTo(baseMeasure);

            return limitMode switch
            {
                LimitMode.BeEqual => comparison == 0 && ceilingBaseMeasure.Equals(baseMeasure),

                _ => comparison.FitsIn(limitMode),
            };

            #region Local methods
            IBaseMeasure? getRoundedBaseMeasure()
            {
                return limitMode switch
                {
                    LimitMode.BeNotLess or
                    LimitMode.BeGreater => ceilingBaseMeasure,

                    LimitMode.BeNotGreater or
                    LimitMode.BeLess or
                    LimitMode.BeEqual => baseMeasure!.Round(RoundingMode.Floor),

                    _ => null,
                };
            }
            #endregion
        }

        public override IBaseMeasure GetBaseMeasure(ValueType quantity, Enum measureUnit)
        {
            return GetMeasure(quantity, measureUnit);
        }

        public override IBaseMeasure GetBaseMeasure(ValueType quantity, Enum measureUnit, decimal exchangeRate, string customName)
        {
            return GetMeasure(quantity, measureUnit, exchangeRate, customName);
        }

        public override IBaseMeasure GetBaseMeasure(ValueType quantity, IMeasurement? measurement = null)
        {
            return GetMeasure(quantity, measurement);
        }

        public override IBaseMeasure GetBaseMeasure(ValueType quantity, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate)
        {
            return GetMeasure(quantity, measureUnitTypeCode, exchangeRate, customName);
        }

        public override IBaseMeasure GetBaseMeasure(ValueType quantity, string name)
        {
            return GetMeasure(quantity, name);
        }

        public override sealed int GetHashCode()
        {
            return GetHashCode(this);
        }

        public override sealed LimitMode? GetLimitMode()
        {
            return base.GetLimitMode();
        }

        public abstract IMeasure GetMeasure(ValueType quantity, Enum measureUnit);
        public abstract IMeasure GetMeasure(ValueType quantity, string name);
        public abstract IMeasure GetMeasure(ValueType quantity, Enum measureUnit, decimal exchangeRate, string customName);
        public abstract IMeasure GetMeasure(ValueType quantity, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate);
        public abstract IMeasure GetMeasure(ValueType quantity, IMeasurement? measurement = null);
        public abstract IMeasure GetMeasure(IBaseMeasure baseMeasure);
        public abstract IMeasure GetMeasure(IMeasure? other = null);

        public IMeasureFactory GetMeasureFactory()
        {
            return MeasurableFactory as IMeasureFactory ?? throw new InvalidOperationException(null);
        }

        public override sealed ValueType GetQuantity(ValueType? quantity = null)
        {
            return base.GetQuantity(quantity);
        }

        public IMeasure Multiply(decimal multiplier)
        {
            decimal quantity = decimal.Multiply(GetDecimalQuantity(), multiplier);

            return GetMeasure(quantity);
        }

        public IMeasure Subtract(IMeasure? other)
        {
            return GetSum(other, SummingMode.Subtract);
        }

        //public bool TryGetValidMeasureQuantity(ValueType quantity, Enum measureUnit, out ValueType? measureQuantity)
        //{
        //    throw new NotImplementedException();
        //}

        private IMeasure GetSum(IMeasure? other, SummingMode summingMode)
        {
            if (other == null) return GetMeasure();

            if (!IsExchangeableTo(other.MeasureUnitTypeCode)) throw new ArgumentOutOfRangeException(nameof(other), other.MeasureUnitTypeCode, null);

            decimal quantity = getSumDefaultQuantity() / GetExchangeRate();

            return GetMeasure(quantity);

            #region Local methods
            decimal getSumDefaultQuantity()
            {
                decimal thisQuantity = DefaultQuantity;
                decimal otherQuantity = other!.DefaultQuantity;

                return summingMode switch
                {
                    SummingMode.Add => decimal.Add(thisQuantity, otherQuantity),
                    SummingMode.Subtract => decimal.Subtract(thisQuantity, otherQuantity),

                    _ => throw new InvalidEnumArgumentException(nameof(summingMode), (int)summingMode, typeof(SummingMode)),
                };
            }
            #endregion
        }

        public abstract IMeasure GetMeasure(Enum measureUnit);
        public abstract IMeasure GetMeasure(string name);

        public override bool TryGetBaseMeasure(ValueType quantity, Enum measureUnit, decimal exchangeRate, string? customName, [NotNullWhen(true)] out IBaseMeasure? baseMeasure)
        {
            throw new NotImplementedException();
        }

        public override ValueType GetDefaultRateComponentQuantity()
        {
            return DefaultMeasureQuantity;
        }

        public override IMeasurable GetDefault()
        {
            Enum measureUnit = GetDefaultMeasureUnit();
            ValueType quantity = GetDefaultRateComponentQuantity();

            return GetMeasure(quantity, measureUnit);
        }
    }
}
