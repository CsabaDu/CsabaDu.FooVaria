using CsabaDu.FooVaria.BaseMeasurements.Types;
using CsabaDu.FooVaria.Measures.Types.Implementations;

namespace CsabaDu.FooVaria.Measures.Factories.Implementations;

public sealed class MeasureFactory : IMeasureFactory
{
    #region Constructors
    public MeasureFactory(IMeasurementFactory measurementFactory)
    {
        MeasurementFactory = NullChecked(measurementFactory, nameof(measurementFactory));
    }
    #endregion

    #region Properties
    public IMeasurementFactory MeasurementFactory { get; init; }

    public RateComponentCode RateComponentCode => throw new NotImplementedException();
    #endregion

    #region Public methods
    //public override IMeasure Create(Enum measureUnit, ValueType quantity)
    //{
    //    return CreateMeasure(NullChecked(measureUnit, nameof(measureUnit)), quantity);
    //}

    //public override IMeasure Create(IMeasurement measurement, ValueType quantity)
    //{
    //    return CreateMeasure(NullChecked(measurement, nameof(measurement)), quantity);
    //}

    //public override IMeasure? Create(Enum measureUnit, decimal exchangeRate, ValueType quantity, string customName)
    //{
    //    IMeasurement? measurement = MeasurementFactory.Create(measureUnit, exchangeRate, customName);

    //    if (measurement == null) return null;

    //    return CreateMeasure(measurement, quantity);
    //}

    //public override IMeasure? Create(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate, ValueType quantity)
    //{
    //    IMeasurement? measurement = MeasurementFactory.Create(customName, measureUnitCode, exchangeRate);

    //    if (measurement == null) return null;

    //    return CreateMeasure(measurement, quantity);
    //}

    //public IMeasure CreateNew(IMeasure other)
    //{
    //    var (measurement, quantity) = GetBaseMeasureParams(other);

    //    return CreateMeasure(measurement, quantity);
    //}

    //public IMeasure? CreateDefault(MeasureUnitCode measureUnitCode)
    //{
    //    IMeasurement? measurement = MeasurementFactory.CreateDefault(measureUnitCode);

    //    if (measurement == null) return null;

    //    ValueType quantity = (ValueType)DefaultRateComponentQuantity;

    //    return CreateMeasure(measurement, quantity);
    //}
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

    public IMeasure Create(string name, ValueType quantity)
    {
        throw new NotImplementedException();
    }

    public IMeasure Create(IBaseMeasure baseMeasure)
    {
        throw new NotImplementedException();
    }

    public IBaseMeasure CreateBaseMeasure(IBaseMeasurement baseMeasurement, ValueType quantity)
    {
        throw new NotImplementedException();
    }

    public IMeasure Create(Enum measureUnit, ValueType quantity)
    {
        throw new NotImplementedException();
    }

    public IMeasure? Create(Enum measureUnit, decimal exchangeRate, ValueType quantity, string customName)
    {
        throw new NotImplementedException();
    }

    public IMeasure? Create(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate, ValueType quantity)
    {
        throw new NotImplementedException();
    }

    public IMeasure CreateNew(IMeasure other)
    {
        throw new NotImplementedException();
    }

    public IMeasure? CreateDefault(MeasureUnitCode measureUnitCode)
    {
        throw new NotImplementedException();
    }
    #endregion
}
