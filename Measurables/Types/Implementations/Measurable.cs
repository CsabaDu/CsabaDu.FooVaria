namespace CsabaDu.FooVaria.Measurables.Types.Implementations
{
    internal abstract class Measurable : MeasureUnit, IMeasurable
    {
        private protected Measurable(IMeasurableFactory measurableFactory, MeasureUnitTypeCode measureUnitTypeCode) : base(measureUnitTypeCode)
        {
            _ = NullChecked(measurableFactory, nameof(measurableFactory));

            MeasurableFactory = measurableFactory;
        }
        private protected Measurable(IMeasurableFactory measurableFactory, Enum measureUnit) : base(measureUnit)
        {
            _ = NullChecked(measurableFactory, nameof(measurableFactory));

            MeasurableFactory = measurableFactory;
        }

        //private protected Measurable(IMeasurableFactory measurableFactory, params IBaseMeasure[] baseMeasures) : this(measurableFactory)
        //{
        //    _ = NullChecked(baseMeasures, nameof(baseMeasures));

        //    MeasureUnitTypeCode = GetMeasureUnitTypeCode(measurableFactory, baseMeasures);
        //}

        private protected Measurable(IMeasurableFactory measurableFactory, IMeasurable measurable) : base(measurable)
        {
            _ = NullChecked(measurableFactory, nameof(measurableFactory));

            MeasurableFactory = measurableFactory;
        }

        private protected Measurable(IMeasurable measurable) : base(measurable)
        {
            MeasurableFactory = measurable.MeasurableFactory;
        }

        public IMeasurableFactory MeasurableFactory { get; init; }

        public IMeasurable GetDefaultMeasurable(IMeasurable? measurable = null)
        {
            throw new NotImplementedException();
        }

        public IMeasurable GetMeasurable(IMeasurable measurable)
        {
            return MeasurableFactory.Create(measurable);
        }
        public virtual IMeasurable GetMeasurable(IMeasurableFactory measurableFactory, IMeasurable measurable)
        {
            _ = NullChecked(measurableFactory, nameof(measurableFactory));

            return measurableFactory.Create(measurable);
        }

        public virtual TypeCode GetQuantityTypeCode(MeasureUnitTypeCode? measureUnitTypeCode = null)
        {
            measureUnitTypeCode ??= MeasureUnitTypeCode;

            return measureUnitTypeCode switch
            {
                MeasureUnitTypeCode.AreaUnit or
                MeasureUnitTypeCode.DistanceUnit or
                MeasureUnitTypeCode.ExtentUnit or
                MeasureUnitTypeCode.TimePeriodUnit or
                MeasureUnitTypeCode.VolumeUnit or
                MeasureUnitTypeCode.WeightUnit => TypeCode.Double,
                MeasureUnitTypeCode.Currency => TypeCode.Decimal,
                MeasureUnitTypeCode.Pieces => TypeCode.Int64,

               _ => throw InvalidMeasureUnitTypeCodeEnumArgumentException((MeasureUnitTypeCode)measureUnitTypeCode),
            };
        }
    }

    internal abstract class BaseMeasure : Measurable, IBaseMeasure
    {
        private protected BaseMeasure(IMeasurable measurable) : base(measurable)
        {
        }

        private protected BaseMeasure(IBaseMeasureFactory baseMeasureFactory, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate) : base(baseMeasureFactory, measureUnitTypeCode)
        {
        }

        private protected BaseMeasure(IBaseMeasureFactory baseMeasureFactory, Enum measureUnit) : base(baseMeasureFactory, measureUnit)
        {
        }

        private protected BaseMeasure(IBaseMeasureFactory baseMeasureFactory, IMeasurable measurable) : base(baseMeasureFactory, measurable)
        {
        }

        public IMeasurement Measurement { get; init; }
        public virtual TypeCode QuantityTypeCode => GetQuantityTypeCode(MeasureUnitTypeCode);
        public decimal DecimalQuantity => (decimal)GetQuantity(TypeCode.Decimal);
        public abstract object Quantity { get; init; }
        public abstract RateComponentCode RateComponentCode { get; init; }

        public abstract ValueType? ExchangeTo(decimal exchangeRate);
        public abstract IBaseMeasure? ExchangeTo(Enum measureUnit);
        public abstract IBaseMeasure GetBaseMeasure(ValueType quantity, Enum measureUnit, decimal? exchangeRate = null);
        public abstract IBaseMeasure GetBaseMeasure(ValueType quantity, IMeasurement? measurement = null);
        public abstract IBaseMeasure GetBaseMeasure(IBaseMeasure? other = null);
        public abstract decimal GetDefaultQuantity(IBaseMeasure? baseMeasure = null);
        public abstract decimal GetExchangeRate();
        public abstract LimitMode? GetLimitMode();

        public override Enum GetMeasureUnit()
        {
            return Measurement.GetMeasureUnit();
        }

        public abstract ValueType GetQuantity();
        public abstract ValueType GetQuantity(RoundingMode roundingMode);
        public abstract ValueType GetQuantity(TypeCode quantityTypeCode);
        public abstract IBaseMeasure Round(RoundingMode roundingMode);
        public abstract bool TryExchangeTo(decimal exchangeRate, [NotNullWhen(true)] out ValueType? exchanged);
        public abstract bool TryExchangeTo(Enum measureUnit, [NotNullWhen(true)] out IBaseMeasure? exchanged);
        public abstract void ValidateQuantity(ValueType quantity, TypeCode quantityTypeCode = TypeCode.Object);
        public abstract void ValidateQuantityTypeCode(TypeCode quantityTypeCode);
    }
}
