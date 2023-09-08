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
        IMeasurement measurement = MeasurementFactory.Create(measureUnit);

        return CreateMeasure(this, quantity, measurement);
    }

    public IMeasure Create(ValueType quantity, string name)
    {
        IMeasurement measurement = MeasurementFactory.Create(name);

        return CreateMeasure(this, quantity, measurement);
    }

    public IMeasure Create(ValueType quantity, Enum measureUnit, decimal exchangeRate, string customName)
    {
        IMeasurement measurement = MeasurementFactory.Create(measureUnit, exchangeRate, customName);

        return CreateMeasure(this, quantity, measurement);
    }

    public IMeasure Create(ValueType quantity, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate)
    {
        IMeasurement measurement = MeasurementFactory.Create(customName, measureUnitTypeCode, exchangeRate);

        return CreateMeasure(this, quantity, measurement);
    }

    public IMeasure Create(ValueType quantity, IMeasurement measurement)
    {
        return CreateMeasure(this, quantity, measurement);
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
}
