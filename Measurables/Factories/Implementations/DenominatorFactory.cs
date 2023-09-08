namespace CsabaDu.FooVaria.Measurables.Factories.Implementations;

public sealed class DenominatorFactory : BaseMeasureFactory, IDenominatorFactory
{
    #region Constructors
    public DenominatorFactory(IMeasurementFactory measurementFactory) : base(measurementFactory)
    {
    }
    #endregion

    #region Properties
    public override RateComponentCode RateComponentCode => RateComponentCode.Denominator;
    #endregion

    #region Public methods
    public IDenominator Create(string name, ValueType? quantity)
    {
        IMeasurement measurement = MeasurementFactory.Create(name);

        return CreateDenominator(this, quantity, measurement);
    }

    public IDenominator Create(Enum measureUnit, ValueType? quantity)
    {
        IMeasurement measurement = MeasurementFactory.Create(measureUnit);

        return CreateDenominator(this, quantity, measurement);
    }

    public IDenominator Create(Enum measureUnit, decimal exchangeRate, string customName, ValueType? quantity)
    {
        IMeasurement measurement = MeasurementFactory.Create(measureUnit, exchangeRate, customName);

        return CreateDenominator(this, quantity, measurement);
    }

    public IDenominator Create(IMeasurement measurement, ValueType? quantity)
    {
        return CreateDenominator(this, quantity, measurement);
    }

    public IDenominator Create(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ValueType? quantity)
    {
        IMeasurement measurement = MeasurementFactory.Create(customName, measureUnitTypeCode, exchangeRate);

        return CreateDenominator(this, quantity, measurement);
    }

    public IDenominator Create(IBaseMeasure baseMeasure)
    {
        return CreateDenominator(this, baseMeasure);
    }

    public IDenominator Create(IDenominator denominator)
    {
        return CreateDenominator(denominator);
    }
    #endregion
}
