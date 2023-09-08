using CsabaDu.FooVaria.Measurables.Types.Implementations;

namespace CsabaDu.FooVaria.Measurables.Factories.Implementations;

public sealed class LimitFactory : BaseMeasureFactory, ILimitFactory
{
    #region Constructors
    public LimitFactory(IMeasurementFactory measurementFactory) : base(measurementFactory)
    {
    }
    #endregion

    #region Properties
    public override RateComponentCode RateComponentCode => RateComponentCode.Limit;
    #endregion

    #region Public methods
    public ILimit Create(string name, ValueType? quantity, LimitMode? limitMode)
    {
        return CreateLimit(name, quantity, limitMode);
    }

    public ILimit Create(Enum measureUnit, ValueType? quantity, LimitMode? limitMode)
    {
        return CreateLimit(measureUnit, quantity, limitMode);
    }

    public ILimit Create(Enum measureUnit, decimal exchangeRate, string customName, ValueType? quantity, LimitMode? limitMode)
    {
        return CreateLimit(measureUnit, exchangeRate, customName, quantity, limitMode);
    }

    public ILimit Create(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ValueType? quantity, LimitMode? limitMode)
    {
        return CreateLimit(customName, measureUnitTypeCode, exchangeRate, quantity, limitMode);
    }

    public ILimit Create(IMeasurement measurement, ValueType? quantity, LimitMode? limitMode)
    {
        return CreateLimit(measurement, quantity, limitMode);
    }

    public ILimit Create(IBaseMeasure baseMeasure, LimitMode? limitMode)
    {
        return CreateLimit(this, baseMeasure, limitMode);
    }

    public ILimit Create(IDenominator denominator)
    {
        return Create(denominator, null);
    }

    public ILimit Create(ILimit limit, LimitMode? limitMode)
    {
        return CreateLimit(limit, limitMode);
    }
    #endregion

    #region Private methods
    private ILimit CreateLimit(Enum measureUnit, decimal exchangeRate, string customName, ValueType? quantity, LimitMode? limitMode)
    {
        IMeasurement measurement = MeasurementFactory.Create(measureUnit, exchangeRate, customName);

        return CreateLimit(measurement, quantity, limitMode);
    }

    private ILimit CreateLimit(IMeasurement measurement, ValueType? quantity, LimitMode? limitMode)
    {
        return new Limit(this, quantity, measurement, limitMode);
    }

    private ILimit CreateLimit(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ValueType? quantity, LimitMode? limitMode)
    {
        IMeasurement measurement = MeasurementFactory.Create(customName, measureUnitTypeCode, exchangeRate);

        return CreateLimit(measurement, quantity, limitMode);
    }

    private ILimit CreateLimit(Enum measureUnit, ValueType? quantity, LimitMode? limitMode)
    {
        IMeasurement measurement = MeasurementFactory.Create(measureUnit);

        return CreateLimit(measurement, quantity, limitMode);
    }

    private ILimit CreateLimit(string name, ValueType? quantity, LimitMode? limitMode)
    {
        IMeasurement measurement = MeasurementFactory.Create(name);

        return CreateLimit(measurement, quantity, limitMode);
    }
    #endregion

}
