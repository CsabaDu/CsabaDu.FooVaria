namespace CsabaDu.FooVaria.Measurables.Types.Implementations
{
    internal abstract class Measurable : BaseMeasurable, IMeasurable
    {
        #region Constructors
        private protected Measurable(IMeasurableFactory measurableFactory, MeasureUnitTypeCode measureUnitTypeCode) : base(measureUnitTypeCode)
        {
            MeasurableFactory = (IMeasurableFactory)NullChecked(measurableFactory, nameof(measurableFactory));
        }

        private protected Measurable(IMeasurableFactory measurableFactory, Enum measureUnit) : base(measureUnit)
        {
            MeasurableFactory = (IMeasurableFactory)NullChecked(measurableFactory, nameof(measurableFactory));
        }

        private protected Measurable(IMeasurableFactory measurableFactory, IMeasurable measurable) : base(measurable)
        {
            MeasurableFactory = (IMeasurableFactory)NullChecked(measurableFactory, nameof(measurableFactory));
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
        #endregion
    }
}
