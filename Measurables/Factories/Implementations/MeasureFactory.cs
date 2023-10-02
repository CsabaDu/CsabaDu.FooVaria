using CsabaDu.FooVaria.Measurables.Types.Implementations.MeasureTypes;

namespace CsabaDu.FooVaria.Measurables.Factories.Implementations;

public sealed class MeasureFactory : BaseMeasureFactory, IMeasureFactory
{
    #region Constructors
    public MeasureFactory(IMeasurementFactory measurementFactory) : base(measurementFactory)
    {
    }
    #endregion

    #region Properties
    public override RateComponentCode RateComponentCode => RateComponentCode.Numerator;
    public override object DefaultRateComponentQuantity => default(int);
    #endregion

    #region Public methods
    public IMeasure CreateDefault(MeasureUnitTypeCode measureUnitTypeCode)
    {
        Enum measureUnit = measureUnitTypeCode.GetDefaultMeasureUnit();

        return Create((ValueType)DefaultRateComponentQuantity, measureUnit);
    }

    public IMeasure Create(ValueType quantity, Enum measureUnit)
    {
        return NullChecked(measureUnit, nameof(measureUnit)) switch
        {
            AreaUnit areaUnit => new Area(this, quantity, areaUnit),
            Currency currency => new Cash(this, quantity, currency),
            DistanceUnit distanceUnit => new Distance(this, quantity, distanceUnit),
            ExtentUnit extentUnit => new Extent(this, quantity, extentUnit),
            Pieces pieces => new PieceCount(this, quantity, pieces),
            TimePeriodUnit timePeriodUnit => new TimePeriod(this, quantity, timePeriodUnit),
            VolumeUnit volumeUnit => new Volume(this, quantity, volumeUnit),
            WeightUnit weightUnit => new Weight(this, quantity, weightUnit),

            _ => throw InvalidMeasureUnitEnumArgumentException(measureUnit),
        };
    }

    public IMeasure Create(ValueType quantity, string name)
    {
        IMeasurement measurement = MeasurementFactory.Create(name);

        return Create(quantity, measurement);
    }

    public IMeasure Create(ValueType quantity, Enum measureUnit, decimal exchangeRate, string customName)
    {
        IMeasurement measurement = MeasurementFactory.Create(measureUnit, exchangeRate, customName);

        return Create(quantity, measurement);
    }

    public IMeasure Create(ValueType quantity, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate)
    {
        IMeasurement measurement = MeasurementFactory.Create(customName, measureUnitTypeCode, exchangeRate);

        return Create(quantity, measurement);
    }

    public IMeasure Create(ValueType quantity, IMeasurement measurement)
    {
        Enum measureUnit = NullChecked(measurement, nameof(measurement)).GetMeasureUnit();

        return Create(quantity, measureUnit);
    }

    public IMeasure Create(IBaseMeasure baseMeasure)
    {
        Enum measureUnit = NullChecked(baseMeasure, nameof(baseMeasure)).GetMeasureUnit();
        ValueType quantity = baseMeasure.GetQuantity();

        return Create(quantity, measureUnit);
    }

    public override IMeasure Create(IMeasurable other)
    {
        _ = NullChecked(other, nameof(other));

        if (other is IBaseMeasure baseMeasure) return Create(baseMeasure);

        if (other is IMeasurement measurement) return Create(measurement);

        if (other is IRate rate) return Create(rate.Numerator);

        throw new InvalidOperationException(null);
    }
    #endregion
}
