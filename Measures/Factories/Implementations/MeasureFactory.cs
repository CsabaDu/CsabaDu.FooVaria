using CsabaDu.FooVaria.BaseMeasurements.Statics;
using CsabaDu.FooVaria.BaseMeasurements.Types;
using CsabaDu.FooVaria.BaseMeasurements.Types.Implementations;
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
    public IMeasure Create(string name, ValueType quantity)
    {
        IMeasurement measurement = MeasurementFactory.Create(name);

        return CreateMeasure(measurement, quantity);
    }

    public IMeasure Create(IBaseMeasure baseMeasure)
    {
        if (baseMeasure is IMeasure other) return CreateNew(other);

        IBaseMeasurement baseMeasurement = NullChecked(baseMeasure, nameof(baseMeasure)).GetBaseMeasurement();
        ValueType quantity = baseMeasure.GetDecimalQuantity();

        return (IMeasure)CreateBaseMeasure(baseMeasurement, quantity);
    }

    public IBaseMeasure CreateBaseMeasure(IBaseMeasurement baseMeasurement, ValueType quantity)
    {
        Enum measureUnit = NullChecked(baseMeasurement, nameof(baseMeasurement)).GetMeasureUnit();

        return CreateMeasure(measureUnit, quantity);
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

    #endregion

    #region Private methods
    private IMeasure CreateMeasure([DisallowNull] Enum measureUnit, ValueType quantity)
    {
        return measureUnit switch
        {
            AreaUnit areaUnit => new Area(this, areaUnit, (double)convertQuantity()),
            Currency currency => new Cash(this, currency, (decimal)convertQuantity()),
            DistanceUnit distanceUnit => new Distance(this, distanceUnit, (double)convertQuantity()),
            ExtentUnit extentUnit => new Extent(this, extentUnit, (double)convertQuantity()),
            Pieces pieces => new PieceCount(this, pieces, (long)convertQuantity()),
            TimePeriodUnit timePeriodUnit => new TimePeriod(this, timePeriodUnit, (double)convertQuantity()),
            VolumeUnit volumeUnit => new Volume(this, volumeUnit, (double)convertQuantity()),
            WeightUnit weightUnit => new Weight(this, weightUnit, (double)convertQuantity()),

            _ => throw InvalidMeasureUnitEnumArgumentException(measureUnit),
        };

        object convertQuantity()
        {
            Type quantityType = NullChecked(quantity, nameof(quantity)).GetType();
            MeasureUnitCode measureUnitCode = MeasureUnitTypes.GetMeasureUnitCode(measureUnit);
            TypeCode quantityTypeCode = measureUnitCode.GetQuantityTypeCode();

            if (quantityTypeCode == Type.GetTypeCode(quantityType)) return quantity;

            return quantity.ToQuantity(quantityTypeCode) ?? throw QuantityArgumentOutOfRangeException(quantity);
        }
    }

    private IMeasure CreateMeasure([DisallowNull] IMeasurement measurement, ValueType quantity)
    {
        Enum measureUnit = measurement.GetMeasureUnit();

        return CreateMeasure(measureUnit, quantity);
    }
    #endregion
}
