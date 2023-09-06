using CsabaDu.FooVaria.Measurables.Types.Implementations;

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
        return CreateDenominator(name, quantity);
    }

    public IDenominator Create(Enum measureUnit, ValueType? quantity)
    {
        return CreateDenominator(measureUnit, quantity);
    }

    public IDenominator Create(Enum measureUnit, decimal exchangeRate, string customName, ValueType? quantity)
    {
        return CreateDenominator(measureUnit, exchangeRate, customName, quantity);
    }

    public IDenominator Create(IMeasurement measurement, ValueType? quantity)
    {
        return CreateDenominator(measurement, quantity);
    }

    public IDenominator Create(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ValueType? quantity)
    {
        return CreateDenominator(customName, measureUnitTypeCode, exchangeRate, quantity);
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

    #region Private methods
    private IDenominator CreateDenominator(Enum measureUnit, decimal exchangeRate, string customName, ValueType? quantity)
    {
        return new Denominator(this, quantity, measureUnit, exchangeRate, customName);
    }

    private IDenominator CreateDenominator(IMeasurement measurement, ValueType? quantity)
    {
        return new Denominator(this, quantity, measurement);
    }

    private IDenominator CreateDenominator(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ValueType? quantity)
    {
        return new Denominator(this, quantity, customName, measureUnitTypeCode, exchangeRate);
    }

    private IDenominator CreateDenominator(Enum measureUnit, ValueType? quantity)
    {
        return new Denominator(this, quantity, measureUnit);
    }

    private IDenominator CreateDenominator(string name, ValueType? quantity)
    {
        IMeasurement measurement = MeasurementFactory.Create(name);

        return CreateDenominator(measurement, quantity);
    }
    #endregion
}
