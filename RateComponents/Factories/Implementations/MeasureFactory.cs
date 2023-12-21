using CsabaDu.FooVaria.RateComponents.Types.Implementations.MeasureTypes;

namespace CsabaDu.FooVaria.RateComponents.Factories.Implementations;

public sealed class MeasureFactory : RateComponentFactory<IMeasure>, IMeasureFactory
{
    #region Constructors
    public MeasureFactory(IMeasurementFactory measurementFactory) : base(measurementFactory)
    {
    }
    #endregion

    #region Properties
    #region Override properties
    public override RateComponentCode RateComponentCode => RateComponentCode.Numerator;
    #endregion
    #endregion

    #region Public methods
    public override IMeasure Create(Enum measureUnit, ValueType quantity)
    {
        return CreateMeasure(NullChecked(measureUnit, nameof(measureUnit)), quantity);
    }

    public override IMeasure Create(IMeasurement measurement, ValueType quantity)
    {
        return CreateMeasure(NullChecked(measurement, nameof(measurement)), quantity);
    }

    public override IMeasure? Create(Enum measureUnit, decimal exchangeRate, ValueType quantity, string customName)
    {
        IMeasurement? measurement = MeasurementFactory.Create(measureUnit, exchangeRate, customName);

        if (measurement == null) return null;

        return CreateMeasure(measurement, quantity);
    }

    public override IMeasure? Create(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ValueType quantity)
    {
        IMeasurement? measurement = MeasurementFactory.Create(customName, measureUnitTypeCode, exchangeRate);

        if (measurement == null) return null;

        return CreateMeasure(measurement, quantity);
    }

    public IMeasure CreateNew(IMeasure other)
    {
        var (measurement, quantity) = GetRateComponentParams(other);

        return CreateMeasure(measurement, quantity);
    }

    public IMeasure? CreateDefault(MeasureUnitTypeCode measureUnitTypeCode)
    {
        IMeasurement? measurement = MeasurementFactory.CreateDefault(measureUnitTypeCode);

        if (measurement == null) return null;

        ValueType quantity = (ValueType)DefaultRateComponentQuantity;

        return CreateMeasure(measurement, quantity);
    }
    #endregion

    #region Private methods
    private IMeasure CreateMeasure([DisallowNull] Enum measureUnit, ValueType quantity)
    {
        return measureUnit switch
        {
            AreaUnit areaUnit => new Area(this, areaUnit, quantity),
            Currency currency => new Cash(this, currency, quantity),
            DistanceUnit distanceUnit => new Distance(this, distanceUnit, quantity),
            ExtentUnit extentUnit => new Extent(this, extentUnit, quantity),
            Pieces pieces => new PieceCount(this, pieces, quantity),
            TimePeriodUnit timePeriodUnit => new TimePeriod(this, timePeriodUnit, quantity),
            VolumeUnit volumeUnit => new Volume(this, volumeUnit, quantity),
            WeightUnit weightUnit => new Weight(this, weightUnit, quantity),

            _ => throw InvalidMeasureUnitEnumArgumentException(measureUnit),
        };
    }

    private IMeasure CreateMeasure([DisallowNull] IMeasurement measurement, ValueType quantity)
    {
        Enum measureUnit = measurement.GetMeasureUnit();

        return CreateMeasure(measureUnit, quantity);
    }
    #endregion
}
