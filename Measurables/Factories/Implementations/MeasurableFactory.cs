using CsabaDu.FooVaria.Measurables.Types.Implementations;
using CsabaDu.FooVaria.Measurables.Types.Implementations.MeasureTypes;

namespace CsabaDu.FooVaria.Measurables.Factories.Implementations
{
    public abstract class MeasurableFactory : IMeasurableFactory
    {
        #region Static Properties
        protected static IDictionary<Enum, IMeasurement> Measurements
        => Measurement.GetValidMeasureUnits().ToDictionary
        (
            x => x,
            x => new Measurement(new MeasurementFactory(), x) as IMeasurement
        );
        #endregion

        #region Public methods
        public IMeasurable Create(IMeasurable measurable)
        {
            return CreateMeasurable(measurable);
        }
        #endregion

        #region Protected methods
        protected static IBaseMeasure CreateBaseMeasure(IBaseMeasure other)
        {
            return NullChecked(other, nameof(other)) switch
            {
                Denominator denominator => CreateDenominator(denominator),
                Measure measure => CreateMeasure(measure),
                Limit limit => CreateLimit(limit),

                _ => throw new InvalidOperationException(null),
            };
        }

        protected static IDenominator CreateDenominator(IDenominator other)
        {
            return new Denominator(other);
        }

        protected static IFlatRate CreateFlatRate(IFlatRate other)
        {
            return new FlatRate(other);
        }

        protected static ILimit CreateLimit(ILimit other)
        {
            return new Limit(other);
        }

        protected static ILimitedRate CreateLimitedRate(ILimitedRate other)
        {
            return new LimitedRate(other);
        }

        protected static IMeasure CreateMeasure(IMeasure other)
        {
            return NullChecked(other, nameof(other)) switch
            {
                Area area => new Area(area),
                Cash cash => new Cash(cash),
                Distance distance => new Distance(distance),
                Extent extent => new Extent(extent),
                TimePeriod timePeriod => new TimePeriod(timePeriod),
                PieceCount pieceCount => new PieceCount(pieceCount),
                Volume volume => new Volume(volume),
                Weight weight => new Weight(weight),

                _ => throw new InvalidOperationException(null),
            };
        }

        protected static IRate CreateRate(IRate other)
        {
            return NullChecked(other, nameof(other)) switch
            {
                FlatRate flatRate => CreateFlatRate(flatRate),
                LimitedRate limitedRate => CreateLimitedRate(limitedRate),

                _ => throw new InvalidOperationException(null),
            };
        }

        protected static IMeasurement GetMeasurement(IMeasurement other)
        {
            Enum measureUnit = NullChecked(other, nameof(other)).GetMeasureUnit();

            return GetMeasurement(measureUnit);
        }

        protected static IMeasurement GetMeasurement(Enum measureUnit)
        {
            return Measurements[NullChecked(measureUnit, nameof(measureUnit))];
        }
        #endregion

        #region Private methods
        private static IMeasurable CreateMeasurable(IMeasurable other)
        {
            return NullChecked(other, nameof(other)) switch
            {
                Measurement measurement => GetMeasurement(measurement),
                BaseMeasure baseMeasure => CreateBaseMeasure(baseMeasure),
                Rate rate => CreateRate(rate),

                _ => throw new InvalidOperationException(null),
            };
        }
        #endregion
    }

    public abstract class BaseMeasureFactory : MeasurableFactory, IBaseMeasureFactory
    {
        private protected BaseMeasureFactory(IMeasurementFactory measurementFactory, RateComponentCode rateComponentCode)
        {
            MeasurementFactory = NullChecked(measurementFactory, nameof(measurementFactory));
            RateComponentCode = GetValidRateComponentCode(rateComponentCode);
        }

        public IMeasurementFactory MeasurementFactory { get; init; }
        public RateComponentCode RateComponentCode { get; init; }

        public abstract IBaseMeasure Create(IBaseMeasureFactory baseMeasureFactory, IBaseMeasure baseMeasure);
        public abstract IBaseMeasure CreateDefault(RateComponentCode rateComponentCode, MeasureUnitTypeCode measureUnitTypeCode);

        public RateComponentCode GetValidRateComponentCode(RateComponentCode rateComponentCode)
        {
            return MeasurementFactory.GetValidRateComponentCode(rateComponentCode);
        }


        #region Protected methods
        protected static ILimit CreateLimit(ILimitFactory limitFactory, IBaseMeasure baseMeasure)
        {
            if (baseMeasure is ILimit limit) return CreateLimit(limit);

            return new Limit(limitFactory, baseMeasure, null);
        }
        #endregion

        #region Private methods
        //private static IBaseMeasure? CreateBaseMeasure(IRate rate, RateComponentCode? rateComponentCode)
        //{
        //    IBaseMeasure? baseMeasure = GetBaseMeasure(rate, rateComponentCode ?? default);

        //    return baseMeasure == null ? null : CreateBaseMeasure(baseMeasure);
        //}

        private static IDenominator CreateDenominator(IDenominatorFactory denominatorFactory, IBaseMeasure baseMeasure)
        {
            if (baseMeasure is IDenominator denominator) return CreateDenominator(denominator);

            return new Denominator(denominatorFactory, baseMeasure);
        }

        private static IMeasure CreateMeasure(IMeasureFactory measureFactory, IBaseMeasure baseMeasure)
        {
            if (baseMeasure is IMeasure measure) return CreateMeasure(measure);

            return baseMeasure?.MeasureUnitTypeCode switch
            {
                null => throw new ArgumentNullException(nameof(baseMeasure)),

                MeasureUnitTypeCode.AreaUnit => new Area(measureFactory, baseMeasure),
                MeasureUnitTypeCode.Currency => new Cash(measureFactory, baseMeasure),
                MeasureUnitTypeCode.DistanceUnit => new Distance(measureFactory, baseMeasure),
                MeasureUnitTypeCode.ExtentUnit => new Extent(measureFactory, baseMeasure),
                MeasureUnitTypeCode.TimePeriodUnit => new TimePeriod(measureFactory, baseMeasure),
                MeasureUnitTypeCode.Pieces => new PieceCount(measureFactory, baseMeasure),
                MeasureUnitTypeCode.VolumeUnit => new Volume(measureFactory, baseMeasure),
                MeasureUnitTypeCode.WeightUnit => new Weight(measureFactory, baseMeasure),

                _ => throw new InvalidOperationException(null),
            };
        }

        //private void ValidateBaseMeasureFactoryParams(IMeasurementFactory measurementFactory, RateComponentCode rateComponentCode)
        //{
        //    _ = NullChecked(measurementFactory, nameof(measurementFactory));

        //    if (Enum.IsDefined(typeof(RateComponentCode), rateComponentCode)) return;

        //    throw InvalidRateComponentCodeArgumentException(RateComponentCode);
        //}
        #endregion

    }
}
