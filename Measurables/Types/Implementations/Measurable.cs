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
        public IMeasurable GetDefaultMeasurable(IMeasurable? measurable = null)
        {
            return MeasurableFactory.CreateDefault(measurable ?? this);
        }

        public IMeasurable GetMeasurable(IMeasurable measurable)
        {
            return MeasurableFactory.Create(measurable);
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
        #endregion
    }

    internal abstract class Measure : BaseMeasure, IMeasure
    {
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

        private protected Measure(IBaseMeasureFactory baseMeasureFactory, ValueType quantity, MeasureUnitTypeCode customMeasureUnitTypeCode, decimal exchangeRate, string? customName) : base(baseMeasureFactory, quantity, customMeasureUnitTypeCode, exchangeRate, customName)
        {
            Quantity = GetQuantity(quantity);
        }

        private protected Measure(IBaseMeasureFactory baseMeasureFactory, ValueType quantity, Enum measureUnit, decimal exchangeRate, string? customName) : base(baseMeasureFactory, quantity, measureUnit, exchangeRate, customName)
        {
            Quantity = GetQuantity(quantity);
        }

        public override sealed object Quantity { get; init; }
        public override sealed TypeCode QuantityTypeCode => GetQuantityTypeCode();

        public IMeasure Add(IMeasure? other)
        {
            throw new NotImplementedException();
        }

        public IMeasure Divide(decimal divisor)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(IBaseMeasure? other)
        {
            return Equals(this, other);
        }

        public override bool Equals(object? obj)
        {
            return obj is IMeasure measure && Equals(measure);
        }

        public bool? FitsIn(ILimit? limit)
        {
            return FitsIn(limit, limit?.LimitMode);
        }

        public bool? FitsIn(IBaseMeasure? baseMeasure, LimitMode? limitMode)
        {
            throw new NotImplementedException();
        }

        public override IBaseMeasure GetBaseMeasure(ValueType quantity, Enum measureUnit)
        {
            throw new NotImplementedException();
        }

        public override IBaseMeasure GetBaseMeasure(ValueType quantity, Enum measureUnit, decimal exchangeRate, string? customName = null)
        {
            throw new NotImplementedException();
        }

        public override IBaseMeasure GetBaseMeasure(ValueType quantity, IMeasurement? measurement = null)
        {
            throw new NotImplementedException();
        }

        public override IBaseMeasure GetBaseMeasure(ValueType quantity, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, string? customName = null)
        {
            throw new NotImplementedException();
        }

        public override IBaseMeasure GetBaseMeasure(ValueType quantity, string name)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            return GetHashCode(this);
        }

        public IMeasure GetMeasure(ValueType quantity, Enum measureUnit, decimal? exchangeRate = null)
        {
            throw new NotImplementedException();
        }

        public IMeasure GetMeasure(ValueType quantity, IMeasurement? measurement = null)
        {
            throw new NotImplementedException();
        }

        public IMeasure GetMeasure(IBaseMeasure baseMeasure)
        {
            throw new NotImplementedException();
        }

        public IMeasure GetMeasure(IMeasure? other = null)
        {
            throw new NotImplementedException();
        }

        public IMeasure GetMeasure(Enum measureUnit)
        {
            throw new NotImplementedException();
        }

        public override sealed ValueType GetQuantity(ValueType? quantity = null)
        {
            return base.GetQuantity(quantity);
        }

        public IMeasure Multiply(decimal multiplier)
        {
            throw new NotImplementedException();
        }

        public IMeasure Subtract(IMeasure? other)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValidMeasureQuantity(ValueType quantity, Enum measureUnit, out ValueType? measureQuantity)
        {
            throw new NotImplementedException();
        }
    }
}
