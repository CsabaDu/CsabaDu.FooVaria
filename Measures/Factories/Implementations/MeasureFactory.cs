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

    public RateComponentCode RateComponentCode => RateComponentCode.Numerator;
    #endregion

    #region Public methods
    public IMeasure Create(string name, ValueType quantity)
    {
        IMeasurement measurement = MeasurementFactory.Create(name);

        return CreateMeasure(measurement, quantity);
    }

    public IMeasure Create(IBaseMeasure baseMeasure)
    {
        return CreateMeasure(baseMeasure, nameof(baseMeasure));
    }

    public IBaseMeasure CreateBaseMeasure(IBaseMeasurement baseMeasurement, ValueType quantity)
    {
        Enum measureUnit = NullChecked(baseMeasurement, nameof(baseMeasurement)).GetMeasureUnit();

        return CreateMeasure(measureUnit, quantity);
    }

    public IMeasure Create(Enum measureUnit, ValueType quantity)
    {
        return CreateMeasure(measureUnit, quantity);
    }

    public IMeasure? Create(Enum measureUnit, decimal exchangeRate, ValueType quantity, string customName)
    {
        IMeasurement? measurement = MeasurementFactory.Create(measureUnit, exchangeRate, customName);

        if (measurement == null) return null;

        return CreateMeasure(measurement, quantity);
    }

    public IMeasure? Create(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate, ValueType quantity)
    {
        IMeasurement? measurement = MeasurementFactory.Create(customName, measureUnitCode, exchangeRate);

        if (measurement == null) return null;

        return CreateMeasure(measurement, quantity);
    }
    public IMeasure CreateNew(IMeasure other)
    {
        return CreateMeasure(other, nameof(other));
    }

    public IBaseMeasure CreateQuantifiable(MeasureUnitCode measureUnitCode, decimal defaultQuantity)
    {
        return CreateMeasure(measureUnitCode, defaultQuantity);
    }
    #endregion

    #region Private methods
    private IMeasure CreateMeasure([DisallowNull] Enum measureUnit, ValueType quantity)
    {
        if (measureUnit is MeasureUnitCode measureUnitCode)
        {
            measureUnit = measureUnitCode.GetDefaultMeasureUnit();
        }
        else
        {
            measureUnitCode = GetMeasureUnitCode(measureUnit);
        }

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
            TypeCode quantityTypeCode = measureUnitCode.GetQuantityTypeCode();

            return ConvertQuantity(quantity, nameof(quantity), quantityTypeCode);
        }
    }

    private IMeasure CreateMeasure(IBaseMeasurement baseMeasurement, ValueType quantity)
    {
        Enum measureUnit = baseMeasurement.GetMeasureUnit();

        return CreateMeasure(measureUnit, quantity);
    }

    private IMeasure CreateMeasure(IBaseMeasure baseMeasure, string paramName)
    {
        Enum measureUnit = NullChecked(baseMeasure, paramName).GetMeasureUnit();
        ValueType quantity = baseMeasure.GetDecimalQuantity();

        return CreateMeasure(measureUnit, quantity);
    }
    #endregion
}
