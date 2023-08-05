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
}
