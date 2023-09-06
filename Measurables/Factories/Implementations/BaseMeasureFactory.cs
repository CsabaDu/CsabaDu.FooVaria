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
        return CreateBaseMeasure(NullChecked(baseMeasureFactory, nameof(baseMeasureFactory)), baseMeasure);
    }
    #endregion

    #region Protected methods
    protected static ILimit CreateLimit(ILimitFactory limitFactory, IBaseMeasure baseMeasure, LimitMode? limitMode)
    {
        if (baseMeasure is ILimit limit) return CreateLimit(limit, limitMode);

        return new Limit(limitFactory, baseMeasure, limitMode);
    }

    protected static IDenominator CreateDenominator(IDenominatorFactory denominatorFactory, IBaseMeasure baseMeasure)
    {
        if (baseMeasure is IDenominator denominator) return CreateDenominator(denominator);

        return new Denominator(denominatorFactory, baseMeasure);
    }

    protected static IMeasure CreateMeasure(IMeasureFactory measureFactory, IBaseMeasure baseMeasure)
    {
        if (baseMeasure is IMeasure measure) return CreateMeasure(measure);

        MeasureUnitTypeCode measureUnitTypeCode = NullChecked(baseMeasure, nameof(baseMeasure)).MeasureUnitTypeCode;

        return measureUnitTypeCode switch
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
    #endregion

    #region Private methods
    private static IBaseMeasure CreateBaseMeasure(IBaseMeasureFactory baseMeasureFactory, IBaseMeasure baseMeasure)
    {
        return baseMeasureFactory switch
        {
            DenominatorFactory denominatorFactory => CreateDenominator(denominatorFactory, baseMeasure),
            MeasureFactory measureFactory => CreateMeasure(measureFactory, baseMeasure),
            LimitFactory limitFactory => CreateLimit(limitFactory, baseMeasure, baseMeasure?.GetLimitMode()),

            _ => throw new InvalidOperationException(null),
        };
    }
    #endregion
}
