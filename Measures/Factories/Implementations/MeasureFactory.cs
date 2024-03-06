namespace CsabaDu.FooVaria.Measures.Factories.Implementations;

public sealed class MeasureFactory(IMeasurementFactory measurementFactory) : IMeasureFactory
{
    #region Properties
    public IMeasurementFactory MeasurementFactory { get; init; } = NullChecked(measurementFactory, nameof(measurementFactory));
    public RateComponentCode RateComponentCode => RateComponentCode.Numerator;
    #endregion

    #region Public methods
    public IMeasure Create(string name, ValueType quantity)
    {
        IMeasurement measurement = MeasurementFactory.Create(name);

        return CreateMeasure(measurement, quantity);
    }

    public IMeasure Create(IQuantifiable quantifiable)
    {
        return CreateMeasure(quantifiable, nameof(quantifiable));
    }

    public IBaseMeasure CreateBaseMeasure(IBaseMeasurement baseMeasurement, ValueType quantity)
    {
        Enum measureUnit = NullChecked(baseMeasurement, nameof(baseMeasurement)).GetBaseMeasureUnit();

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

    public IQuantifiable CreateQuantifiable(MeasureUnitCode measureUnitCode, decimal defaultQuantity)
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
            measureUnitCode = GetDefinedMeasureUnitCode(measureUnit);
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
        Enum measureUnit = baseMeasurement.GetBaseMeasureUnit();

        return CreateMeasure(measureUnit, quantity);
    }

    private IMeasure CreateMeasure(IQuantifiable quantifiable, string paramName)
    {
        Enum measureUnit = NullChecked(quantifiable, paramName).GetBaseMeasureUnit();
        ValueType quantity = quantifiable.GetDecimalQuantity();

        return CreateMeasure(measureUnit, quantity);
    }
    #endregion
}
