using CsabaDu.FooVaria.Measurables.Types.Implementations;
using CsabaDu.FooVaria.Measurables.Types.Implementations.MeasureTypes;

namespace CsabaDu.FooVaria.Measurables.Factories.Implementations;

public abstract class BaseMeasureFactory : MeasurableFactory, IBaseMeasureFactory
{
    #region Constructors
    private protected BaseMeasureFactory(IMeasurementFactory measurementFactory)
    {
        MeasurementFactory = NullChecked(measurementFactory, nameof(measurementFactory));
    }
    #endregion

    #region Properties
    public IMeasurementFactory MeasurementFactory { get; init; }
    public abstract RateComponentCode RateComponentCode { get; }
    #endregion

    #region Public methods
    public IBaseMeasure Create(IBaseMeasureFactory baseMeasureFactory, IBaseMeasure baseMeasure)
    {
        return NullChecked(baseMeasureFactory, nameof(baseMeasureFactory)) switch
        {
            DenominatorFactory denominatorFactory => CreateDenominator(denominatorFactory, baseMeasure),
            MeasureFactory measureFactory => CreateMeasure(measureFactory, baseMeasure),
            LimitFactory limitFactory => CreateLimit(limitFactory, baseMeasure, baseMeasure?.GetLimitMode()),

            _ => throw new InvalidOperationException(null),
        };
    }
    #endregion

    #region Protected methods
    protected static IDenominator CreateDenominator(IDenominatorFactory denominatorFactory, IBaseMeasure baseMeasure)
    {
        if (baseMeasure is IDenominator denominator) return CreateDenominator(denominator);

        IMeasurement measurement = NullChecked(baseMeasure, nameof(baseMeasure)).Measurement;
        ValueType quantity = baseMeasure.GetQuantity();

        return new Denominator(denominatorFactory, quantity, measurement);
    }

    protected static IMeasure CreateMeasure(IMeasureFactory measureFactory, IBaseMeasure baseMeasure)
    {
        if (baseMeasure is IMeasure measure) return CreateMeasure(measure);

        IMeasurement measurement = NullChecked(baseMeasure, nameof(baseMeasure)).Measurement;
        ValueType quantity = baseMeasure.GetQuantity();

        return CreateMeasure(measureFactory, quantity, measurement);
    }

    protected static ILimit CreateLimit(ILimitFactory limitFactory, IBaseMeasure baseMeasure, LimitMode? limitMode)
    {
        if (baseMeasure is ILimit limit) return CreateLimit(limit, limitMode);

        IMeasurement measurement = NullChecked(baseMeasure, nameof(baseMeasure)).Measurement;
        ValueType quantity = baseMeasure.GetQuantity();

        return new Limit(limitFactory, quantity, measurement, limitMode ?? baseMeasure.GetLimitMode());
    }

    protected static IDenominator CreateDenominator(IDenominatorFactory denominatorFactory, ValueType? quantity, IMeasurement measurement)
    {
        return new Denominator(denominatorFactory, quantity, measurement);
    }

    protected static IMeasure CreateMeasure(IMeasureFactory measureFactory, ValueType quantity, IMeasurement measurement)
    {
        MeasureUnitTypeCode measureUnitTypeCode = NullChecked(measurement, nameof(measurement)).MeasureUnitTypeCode;

        return measureUnitTypeCode switch
        {
            MeasureUnitTypeCode.AreaUnit => new Area(measureFactory, quantity, measurement),
            MeasureUnitTypeCode.Currency => new Cash(measureFactory, quantity, measurement),
            MeasureUnitTypeCode.DistanceUnit => new Distance(measureFactory, quantity, measurement),
            MeasureUnitTypeCode.ExtentUnit => new Extent(measureFactory, quantity, measurement),
            MeasureUnitTypeCode.TimePeriodUnit => new TimePeriod(measureFactory, quantity, measurement),
            MeasureUnitTypeCode.Pieces => new PieceCount(measureFactory, quantity, measurement),
            MeasureUnitTypeCode.VolumeUnit => new Volume(measureFactory, quantity, measurement),
            MeasureUnitTypeCode.WeightUnit => new Weight(measureFactory, quantity, measurement),

            _ => throw new InvalidOperationException(null),
        };
    }

    protected static ILimit CreateLimit(ILimitFactory limitFactory, ValueType? quantity, IMeasurement measurement, LimitMode? limitMode)
    {
        return new Limit(limitFactory, quantity, measurement, limitMode);
    }
    #endregion
}
