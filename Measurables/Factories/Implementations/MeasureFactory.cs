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
    #endregion

    #region Public methods
    public IMeasure Create(ValueType quantity, Enum measureUnit)
    {
        return CreateMeasure(quantity, measureUnit);
    }

    public IMeasure Create(ValueType quantity, string name)
    {
        return CreateMeasure(quantity, name);
    }

    public IMeasure Create(ValueType quantity, Enum measureUnit, decimal exchangeRate, string customName)
    {
        return CreateCustomMeasure(quantity, measureUnit, exchangeRate, customName);
    }

    public IMeasure Create(ValueType quantity, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate)
    {
        return CreateCustomMeasure(quantity, customName, measureUnitTypeCode, exchangeRate);
    }

    public IMeasure Create(ValueType quantity, IMeasurement measurement)
    {
        return CreateMeasure(quantity, measurement);
    }

    public IMeasure Create(IBaseMeasure baseMeasure)
    {
        return CreateMeasure(this, baseMeasure);
    }

    public IMeasure Create(IMeasure measure)
    {
        return CreateMeasure(measure);
    }
    #endregion

    #region Private methods
    private IMeasure CreateCustomMeasure(ValueType quantity, Enum measureUnit, decimal exchangeRate, string customName)
    {
        return GetMeasureUnitTypeCode(measureUnit) switch
        {
            MeasureUnitTypeCode.Currency => new Cash(this, quantity, measureUnit, exchangeRate, customName),
            MeasureUnitTypeCode.Pieces => new PieceCount(this, quantity, measureUnit, exchangeRate, customName),

            _ => throw InvalidMeasureUnitEnumArgumentException(measureUnit),
        };
    }

    private IMeasure CreateMeasure(ValueType quantity, IMeasurement measurement)
    {
        MeasureUnitTypeCode measureUnitTypeCode = NullChecked(measurement, nameof(measurement)).MeasureUnitTypeCode;

        return measureUnitTypeCode switch
        {
            MeasureUnitTypeCode.AreaUnit => new Area(this, quantity, measurement),
            MeasureUnitTypeCode.Currency => new Cash(this, quantity, measurement),
            MeasureUnitTypeCode.DistanceUnit => new Distance(this, quantity, measurement),
            MeasureUnitTypeCode.ExtentUnit => new Extent(this, quantity, measurement),
            MeasureUnitTypeCode.TimePeriodUnit => new TimePeriod(this, quantity, measurement),
            MeasureUnitTypeCode.Pieces => new PieceCount(this, quantity, measurement),
            MeasureUnitTypeCode.VolumeUnit => new Volume(this, quantity, measurement),
            MeasureUnitTypeCode.WeightUnit => new Weight(this, quantity, measurement),

            _ => throw new InvalidOperationException(null),
        };
    }

    private IMeasure CreateCustomMeasure(ValueType quantity, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate)
    {
        return measureUnitTypeCode switch
        {
            MeasureUnitTypeCode.Currency => new Cash(this, quantity, customName, measureUnitTypeCode, exchangeRate),
            MeasureUnitTypeCode.Pieces => new PieceCount(this, quantity, customName, measureUnitTypeCode, exchangeRate),

            _ => throw InvalidMeasureUnitTypeCodeEnumArgumentException(measureUnitTypeCode),
        };
    }

    private IMeasure CreateMeasure(ValueType quantity, Enum measureUnit)
    {
        return GetMeasureUnitTypeCode(measureUnit) switch
        {
            MeasureUnitTypeCode.AreaUnit => new Area(this, quantity, measureUnit),
            MeasureUnitTypeCode.Currency => new Cash(this, quantity, measureUnit),
            MeasureUnitTypeCode.DistanceUnit => new Distance(this, quantity, measureUnit),
            MeasureUnitTypeCode.ExtentUnit => new Extent(this, quantity, measureUnit),
            MeasureUnitTypeCode.TimePeriodUnit => new TimePeriod(this, quantity, measureUnit),
            MeasureUnitTypeCode.Pieces => new PieceCount(this, quantity, measureUnit),
            MeasureUnitTypeCode.VolumeUnit => new Volume(this, quantity, measureUnit),
            MeasureUnitTypeCode.WeightUnit => new Weight(this, quantity, measureUnit),

            _ => throw new InvalidOperationException(null),
        };
    }

    private IMeasure CreateMeasure(ValueType quantity, string name)
    {
        IMeasurement measurement = MeasurementFactory.Create(name);

        return CreateMeasure(quantity, measurement);
    }

    private static MeasureUnitTypeCode GetMeasureUnitTypeCode(Enum measureUnit)
    {
        if (!IsDefinedMeasureUnit(measureUnit)) throw InvalidMeasureUnitEnumArgumentException(measureUnit);

        string measureUnitTypeName = measureUnit.GetType().Name;

        return (MeasureUnitTypeCode)Enum.Parse(typeof(MeasureUnitTypeCode), measureUnitTypeName);
    }
    #endregion
}
