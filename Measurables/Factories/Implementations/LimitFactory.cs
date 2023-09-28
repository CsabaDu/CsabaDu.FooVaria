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

    public override object DefaultRateComponentQuantity => default(ulong);
    #endregion

    #region Public methods
    public ILimit Create(string name, ValueType? quantity, LimitMode? limitMode)
    {
        IMeasurement measurement = MeasurementFactory.Create(name);

        return CreateLimit(this, quantity, measurement, limitMode);
    }

    public ILimit Create(Enum measureUnit, ValueType? quantity, LimitMode? limitMode)
    {
        IMeasurement measurement = MeasurementFactory.Create(measureUnit);

        return CreateLimit(this, quantity, measurement, limitMode);
    }

    public ILimit Create(Enum measureUnit, decimal exchangeRate, string customName, ValueType? quantity, LimitMode? limitMode)
    {
        IMeasurement measurement = MeasurementFactory.Create(measureUnit, exchangeRate, customName);

        return CreateLimit(this, quantity, measurement, limitMode);
    }

    public ILimit Create(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ValueType? quantity, LimitMode? limitMode)
    {
        IMeasurement measurement = MeasurementFactory.Create(customName, measureUnitTypeCode, exchangeRate);

        return CreateLimit(this, quantity, measurement, limitMode);
    }

    public ILimit Create(IMeasurement measurement, ValueType? quantity, LimitMode? limitMode)
    {
        return CreateLimit(this, quantity, measurement, limitMode);
    }

    public ILimit Create(IBaseMeasure baseMeasure, LimitMode? limitMode)
    {
        return CreateLimit(this, baseMeasure, limitMode);
    }

    public ILimit Create(IDenominator denominator)
    {
        return CreateLimit(this, denominator, null);
    }

    public ILimit Create(ILimit limit, LimitMode? limitMode)
    {
        return CreateLimit(limit, limitMode);
    }

    public override IMeasurable Create(IMeasurable other)
    {
        throw new NotImplementedException();
    }

    public ILimit Create(string name, ValueType quantity, LimitMode limitMode)
    {
        throw new NotImplementedException();
    }

    public ILimit Create(Enum measureUnit, ValueType quantity, LimitMode limitMode)
    {
        throw new NotImplementedException();
    }

    public ILimit Create(IMeasurement measurement, ValueType quantity, LimitMode limitMode)
    {
        throw new NotImplementedException();
    }

    public ILimit Create(IBaseMeasure baseMeasure, LimitMode limitMode)
    {
        throw new NotImplementedException();
    }

    public ILimit Create(ILimit limit, ValueType quantity)
    {
        throw new NotImplementedException();
    }

    public override IBaseMeasure CreateDefault(MeasureUnitTypeCode measureUnitTypeCode)
    {
        throw new NotImplementedException();
    }
    #endregion
}
