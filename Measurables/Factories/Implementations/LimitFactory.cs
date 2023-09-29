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
    public override object DefaultRateComponentQuantity => default(ulong);
    private static SortedSet<ILimit> LimitSet { get; set; } = new();
    #endregion

    #region Public methods
    public override ILimit CreateDefault(MeasureUnitTypeCode measureUnitTypeCode)
    {
        ILimit limit = new Limit(this, measureUnitTypeCode);

        return Create(limit);
    }

    public ILimit Create(ILimit limit)
    {
        return GetStoredLimit(limit);
    }

    public ILimit Create(IMeasurement measurement, ValueType quantity, LimitMode limitMode)
    {
        ILimit limit = new Limit(this, quantity, measurement, limitMode);

        return Create(limit);
    }

    public ILimit Create(string name, ValueType quantity, LimitMode limitMode)
    {
        IMeasurement measurement = MeasurementFactory.Create(name);

        return Create(measurement, quantity, limitMode);
    }

    public ILimit Create(Enum measureUnit, ValueType quantity, LimitMode limitMode)
    {
        IMeasurement measurement = MeasurementFactory.Create(measureUnit);

        return Create(measurement, quantity, limitMode);
    }

    public ILimit Create(Enum measureUnit, decimal exchangeRate, string customName, ValueType quantity, LimitMode limitMode)
    {
        IMeasurement measurement = MeasurementFactory.Create(measureUnit, exchangeRate, customName);

        return Create(measurement, quantity, limitMode);
    }

    public ILimit Create(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ValueType quantity, LimitMode limitMode)
    {
        IMeasurement measurement = MeasurementFactory.Create(customName, measureUnitTypeCode, exchangeRate);

        return Create(measurement, quantity, limitMode);
    }

    public ILimit Create(IBaseMeasure baseMeasure, LimitMode limitMode)
    {
        IMeasurement measurement = NullChecked(baseMeasure, nameof(baseMeasure)).Measurement;
        ValueType quantity = baseMeasure.GetQuantity();

        return Create(measurement, quantity, limitMode);
    }

    public override ILimit Create(IMeasurable other)
    {
        _ = NullChecked(other, nameof(other));

        if (other is ILimit limit) return Create(limit);

        if (other is IBaseMeasure baseMeasure) return Create(baseMeasure, default);

        if (other is IMeasurement measurement) return CreateDefault(measurement.MeasureUnitTypeCode);

        if (other is IFlatRate flatRate) return Create(flatRate.Denominator, default);

        if (other is ILimitedRate limitedRate) return Create(limitedRate.Limit);

        throw new InvalidOperationException(null);
    }

    public ILimit Create(ILimit limit, ValueType quantity)
    {
        IMeasurement measurement = NullChecked(limit, nameof(limit)).Measurement;
        LimitMode limitMode = limit.LimitMode;

        return Create(measurement, quantity, limitMode);
    }
    #endregion

    #region Private methods
    #region Static methods
    private static ILimit GetStoredLimit([DisallowNull] ILimit limit)
    {
        if (LimitSet.Contains(limit) || LimitSet.Add(limit))
        {
            if (LimitSet.TryGetValue(limit, out ILimit? storedLimit) && storedLimit != null)
            {
                return storedLimit;
            }
        }

        throw new InvalidOperationException(null);
    }
    #endregion
    #endregion
}
