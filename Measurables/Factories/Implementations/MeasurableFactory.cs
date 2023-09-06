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

        protected static ILimit CreateLimit(ILimit limit, LimitMode? limitMode)
        {
            return new Limit(limit, limitMode);
        }

        protected static ILimitedRate CreateLimitedRate(ILimitedRate limitedRate, ILimit? limit)
        {
            return new LimitedRate(limitedRate, limit);
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
                LimitedRate limitedRate => CreateLimitedRate(limitedRate, null),

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

    public abstract class RateFactory : MeasurableFactory, IRateFactory
    {
        #region Constructors
        protected RateFactory(IDenominatorFactory denominatorFactory)
        {
            DenominatorFactory = NullChecked(denominatorFactory, nameof(denominatorFactory));
        }
        #endregion

        #region Properties
        public IDenominatorFactory DenominatorFactory { get; init; }
        #endregion

        #region Public methods
        public IRate Create(IRateFactory rateFactory, IRate rate)
        {
            return CreateRate(rateFactory, rate);
        }
        #endregion

        #region Protected methods
        protected static IFlatRate CreateFlatRate(IFlatRateFactory flatRateFactory, IRate rate)
        {
            if (rate is IFlatRate flatRate) return CreateFlatRate(flatRate);

            return new FlatRate(flatRateFactory, rate);
        }

        protected static ILimitedRate CreateLimitedRate(ILimitedRateFactory limitedRateFactory, IRate rate, ILimit? limit)
        {
            if (rate is ILimitedRate limitedRate) return CreateLimitedRate(limitedRate, limit);

            return new LimitedRate(limitedRateFactory, rate, limit);
        }
        #endregion

        #region Private methods
        private static IRate CreateRate(IRateFactory rateFactory, IRate rate)
        {
            return rateFactory switch
            {
                FlatRateFactory flatRateFactory => CreateFlatRate(flatRateFactory, rate),
                LimitedRateFactory limitedRateFactory => CreateLimitedRate(limitedRateFactory, rate, rate?.GetLimit()),

                _ => throw new InvalidOperationException(null),
            };
        }
        #endregion
    }

    public sealed class FlatRateFactory : RateFactory, IFlatRateFactory
    {
        public FlatRateFactory(IDenominatorFactory denominatorFactory) : base(denominatorFactory)
        {
        }

        public IFlatRate Create(IFlatRate flatRate)
        {
            return CreateFlatRate(flatRate);
        }

        public IFlatRate Create(IMeasure numerator, string customName, decimal? quantity)
        {
            throw new NotImplementedException();
        }

        public IFlatRate Create(IMeasure numerator, Enum measureUnit, decimal? quantity)
        {
            throw new NotImplementedException();
        }

        public IFlatRate Create(IMeasure numerator, Enum measureUnit, decimal exchangeRate, string customName, decimal? quantity)
        {
            throw new NotImplementedException();
        }

        public IFlatRate Create(IMeasure numerator, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, decimal? quantity)
        {
            throw new NotImplementedException();
        }

        public IFlatRate Create(IMeasure numerator, IMeasurement measurement, decimal? quantity)
        {
            throw new NotImplementedException();
        }

        public IFlatRate Create(IMeasure numerator, IDenominator denominator)
        {
            throw new NotImplementedException();
        }

        public IFlatRate Create(IRate rate)
        {
            return CreateFlatRate(this, rate);
        }
    }

    public sealed class LimitedRateFactory : RateFactory, ILimitedRateFactory
    {
        public LimitedRateFactory(IDenominatorFactory denominatorFactory, ILimitFactory limitFactory) : base(denominatorFactory)
        {
            LimitFactory = NullChecked(limitFactory, nameof(limitFactory));
        }

        public ILimitFactory LimitFactory { get; init; }

        public ILimitedRate Create(ILimitedRate limitedRate, ILimit? limit)
        {
            return CreateLimitedRate(limitedRate, limit);
        }

        public ILimitedRate Create(IMeasure numerator, string name, decimal? quantity, ILimit? limit)
        {
            throw new NotImplementedException();
        }

        public ILimitedRate Create(IMeasure numerator, Enum measureUnit, decimal? quantity, ILimit? limit)
        {
            throw new NotImplementedException();
        }

        public ILimitedRate Create(IMeasure numerator, Enum measureUnit, decimal exchangeRate, string customName, decimal? quantity, ILimit? limit)
        {
            throw new NotImplementedException();
        }

        public ILimitedRate Create(IMeasure numerator, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, decimal? quantity, ILimit? limit)
        {
            throw new NotImplementedException();
        }

        public ILimitedRate Create(IMeasure numerator, IMeasurement measurement, decimal? quantity, ILimit? limit)
        {
            throw new NotImplementedException();
        }

        public ILimitedRate Create(IMeasure numerator, IDenominator denominator, ILimit? limit)
        {
            throw new NotImplementedException();
        }

        public ILimitedRate Create(IRate rate, ILimit? limit)
        {
            return CreateLimitedRate(this, rate, limit);
        }
    }
}
