﻿using CsabaDu.FooVaria.Measurables.Types.Implementations;
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
                Denominator measure => CreateDenominator(measure),
                Measure measure => CreateMeasure(measure),
                Limit limit => CreateLimit(limit, null),

                _ => throw new InvalidOperationException(null),
            };
        }

        protected static IDenominator CreateDenominator(IDenominator measure)
        {
            return new Denominator(measure);
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
}
