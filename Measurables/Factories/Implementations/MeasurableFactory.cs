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
            return CreateMeasurable(NullChecked(measurable, nameof(measurable)));
        }
        #endregion

        #region Protected methods
        protected static IBaseMeasure CreateBaseMeasure(IBaseMeasure baseMeasure)
        {
            return baseMeasure switch
            {
                Denominator denominator => CreateDenominator(denominator),
                Measure measure => CreateMeasure(measure),
                Limit limit => CreateLimit(limit, null),

                _ => throw new InvalidOperationException(null),
            };
        }

        protected static IDenominator CreateDenominator(IDenominator denominator)
        {
            return new Denominator(denominator);
        }

        protected static IFlatRate CreateFlatRate(IFlatRate flatRate)
        {
            return new FlatRate(flatRate);
        }

        protected static ILimit CreateLimit(ILimit other, LimitMode? limitMode)
        {
            return new Limit(other, limitMode);
        }

        protected static ILimitedRate CreateLimitedRate(ILimitedRate limitedRate)
        {
            return new LimitedRate(limitedRate);
        }

        protected static IMeasure CreateMeasure(IMeasure measure)
        {
            return measure switch
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

        protected static IRate CreateRate(IRate rate)
        {
            return rate switch
            {
                FlatRate flatRate => CreateFlatRate(flatRate),
                LimitedRate limitedRate => CreateLimitedRate(limitedRate),

                _ => throw new InvalidOperationException(null),
            };
        }

        protected static IMeasurement GetMeasurement(IMeasurement measurement)
        {
            return GetMeasurement(measurement.GetMeasureUnit());
        }

        protected static IMeasurement GetMeasurement(Enum measureUnit)
        {
            return Measurements[measureUnit];
        }
        #endregion

        #region Private methods
        private static IMeasurable CreateMeasurable(IMeasurable measurable)
        {
            return measurable switch
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
        private protected BaseMeasureFactory(IMeasurementFactory measurementFactory)
        {
            MeasurementFactory = NullChecked(measurementFactory, nameof(measurementFactory));
        }

        public IMeasurementFactory MeasurementFactory { get; init; }
        public abstract RateComponentCode RateComponentCode { get; }

        public IBaseMeasure Create(IBaseMeasureFactory baseMeasureFactory, IBaseMeasure baseMeasure)
        {
            return CreateBaseMeasure(NullChecked(baseMeasureFactory, nameof(baseMeasureFactory)), baseMeasure);
        }

        public RateComponentCode GetValidRateComponentCode(RateComponentCode rateComponentCode)
        {
            return MeasurementFactory.GetValidRateComponentCode(rateComponentCode);
        }


        #region Protected methods
        protected static ILimit CreateLimit(ILimitFactory limitFactory, IBaseMeasure baseMeasure, LimitMode? limitMode)
        {
            if (baseMeasure is ILimit limit) return CreateLimit(limit, limitMode);

            return new Limit(limitFactory, baseMeasure, limitMode);
        }
        #endregion

        #region Private methods
        private static IDenominator CreateDenominator(IDenominatorFactory denominatorFactory, IBaseMeasure baseMeasure)
        {
            if (baseMeasure is IDenominator denominator) return CreateDenominator(denominator);

            return new Denominator(denominatorFactory, baseMeasure);
        }

        private static IMeasure CreateMeasure(IMeasureFactory measureFactory, IBaseMeasure baseMeasure)
        {
            if (baseMeasure is IMeasure measure) return CreateMeasure(measure);

            return baseMeasure.MeasureUnitTypeCode switch
            {
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

        private static IBaseMeasure CreateBaseMeasure(IBaseMeasureFactory baseMeasureFactory, IBaseMeasure baseMeasure)
        {
            return baseMeasureFactory switch
            {
                DenominatorFactory denominatorFactory => CreateDenominator(denominatorFactory, baseMeasure),
                MeasureFactory measureFactory => CreateMeasure(measureFactory, baseMeasure),
                LimitFactory limitFactory => CreateLimit(limitFactory, baseMeasure, baseMeasure.GetLimitMode()),

                _ => throw new InvalidOperationException(null),
            };
        }
        #endregion

       //public abstract IBaseMeasure CreateDefault(RateComponentCode rateComponentCode, MeasureUnitTypeCode measureUnitTypeCode);

        //private static IBaseMeasure? CreateBaseMeasure(IRate rate, RateComponentCode? rateComponentCode)
        //{
        //    IBaseMeasure? baseMeasure = GetBaseMeasure(rate, rateComponentCode ?? default);

        //    return baseMeasure == null ? null : CreateBaseMeasure(baseMeasure);
        //}

        //private void ValidateBaseMeasureFactoryParams(IMeasurementFactory measurementFactory, RateComponentCode rateComponentCode)
        //{
        //    _ = NullChecked(measurementFactory, nameof(measurementFactory));

        //    if (Enum.IsDefined(typeof(RateComponentCode), rateComponentCode)) return;

        //    throw InvalidRateComponentCodeArgumentException(RateComponentCode);
        //}
    }

    public sealed class DenominatorFactory : BaseMeasureFactory, IDenominatorFactory
    {
        public DenominatorFactory(IMeasurementFactory measurementFactory) : base(measurementFactory)
        {
        }

        public override RateComponentCode RateComponentCode => RateComponentCode.Denominator;

        public IDenominator Create(string name, ValueType? quantity)
        {
            throw new NotImplementedException();
        }

        public IDenominator Create(Enum measureUnit, ValueType? quantity)
        {
            throw new NotImplementedException();
        }

        public IDenominator Create(Enum measureUnit, decimal exchangeRate, string customName, ValueType? quantity)
        {
            throw new NotImplementedException();
        }

        public IDenominator Create(IMeasurement measurement, ValueType? quantity)
        {
            throw new NotImplementedException();
        }

        public IDenominator Create(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ValueType? quantity)
        {
            throw new NotImplementedException();
        }

        public IDenominator Create(IBaseMeasure baseMeasure)
        {
            throw new NotImplementedException();
        }

        public IDenominator Create(IDenominator denominator)
        {
            throw new NotImplementedException();
        }
    }

    public sealed class MeasureFactory : BaseMeasureFactory, IMeasureFactory
    {
        public MeasureFactory(IMeasurementFactory measurementFactory) : base(measurementFactory)
        {
        }

        public override RateComponentCode RateComponentCode => RateComponentCode.Numerator;

        public IMeasure Create(ValueType quantity, Enum measureUnit)
        {
            throw new NotImplementedException();
        }

        public IMeasure Create(ValueType quantity, string name)
        {
            throw new NotImplementedException();
        }

        public IMeasure Create(ValueType quantity, Enum measureUnit, decimal exchangeRate, string customName)
        {
            throw new NotImplementedException();
        }

        public IMeasure Create(ValueType quantity, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate)
        {
            throw new NotImplementedException();
        }

        public IMeasure Create(ValueType quantity, IMeasurement measurement)
        {
            throw new NotImplementedException();
        }

        public IMeasure Create(IBaseMeasure baseMeasure)
        {
            throw new NotImplementedException();
        }

        public IMeasure Create(IMeasure measure)
        {
            throw new NotImplementedException();
        }
    }

    public sealed class LimitFactory : BaseMeasureFactory, ILimitFactory
    {
        public LimitFactory(IMeasurementFactory measurementFactory) : base(measurementFactory)
        {
        }

        public override RateComponentCode RateComponentCode => RateComponentCode.Limit;

        public ILimit Create(string name, ValueType? quantity, LimitMode? limitMode)
        {
            throw new NotImplementedException();
        }

        public ILimit Create(Enum measureUnit, ValueType? quantity, LimitMode? limitMode)
        {
            throw new NotImplementedException();
        }

        public ILimit Create(Enum measureUnit, decimal exchangeRate, string customName, ValueType? quantity, LimitMode? limitMode)
        {
            throw new NotImplementedException();
        }

        public ILimit Create(string name, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ValueType? quantity, LimitMode? limitMode)
        {
            throw new NotImplementedException();
        }

        public ILimit Create(IMeasurement measurement, ValueType? quantity, LimitMode? limitMode)
        {
            throw new NotImplementedException();
        }

        public ILimit Create(IBaseMeasure baseMeasure, LimitMode? limitMode)
        {
            throw new NotImplementedException();
        }

        public ILimit Create(IDenominator denominator)
        {
            throw new NotImplementedException();
        }

        public ILimit Create(ILimit limit, LimitMode? limitMode)
        {
            throw new NotImplementedException();
        }
    }

}
