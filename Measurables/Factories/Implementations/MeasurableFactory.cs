using CsabaDu.FooVaria.Measurables.Types.Implementations;
using CsabaDu.FooVaria.Measurables.Types.Implementations.MeasureTypes;

namespace CsabaDu.FooVaria.Measurables.Factories.Implementations;

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
        return NullChecked(measurable, nameof(measurable)) switch
        {
            Measurement measurement => GetMeasurement(measurement),
            BaseMeasure baseMeasure => baseMeasure switch
            {
                Denominator denominator => CreateDenominator(denominator),
                Measure measure => CreateMeasure(measure),
                Limit limit => CreateLimit(limit, null),

                _ => throw new InvalidOperationException(null),
            },
            Rate rate => rate switch
            {
                FlatRate flatRate => CreateFlatRate(flatRate),
                LimitedRate limitedRate => CreateLimitedRate(limitedRate, null),

                _ => throw new InvalidOperationException(null),
            },

            _ => throw new InvalidOperationException(null),
        };
    }
    #endregion

    #region Protected methods
    protected static IMeasurement GetMeasurement(IMeasurement measurement)
    {
        return GetMeasurement(measurement.GetMeasureUnit());
    }

    protected static IDenominator CreateDenominator(IDenominator denominator)
    {
        return new Denominator(denominator);
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

    protected static ILimit CreateLimit(ILimit limit, LimitMode? limitMode)
    {
        return new Limit(limit, limitMode);
    }

    protected static IFlatRate CreateFlatRate(IFlatRate flatRate)
    {
        return new FlatRate(flatRate);
    }

    protected static ILimitedRate CreateLimitedRate(ILimitedRate limitedRate, ILimit? limit)
    {
        return new LimitedRate(limitedRate, limit);
    }

    protected static IMeasurement GetMeasurement(Enum measureUnit)
    {
        return Measurements[measureUnit];
    }
    #endregion
}
