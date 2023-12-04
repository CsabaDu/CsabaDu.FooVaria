using CsabaDu.FooVaria.RateComponents.Types.Implementations;

namespace CsabaDu.FooVaria.RateComponents.Factories.Implementations;

public sealed class LimitFactory : BaseMeasureFactory, ILimitFactory
{
    #region Constructors
    public LimitFactory(IMeasurementFactory measurementFactory) : base(measurementFactory)
    {
    }
    #endregion

    #region Properties
    public override RateComponentCode RateComponentCode => RateComponentCode.Limit;

    #region Private properties
    #region Static properties
    private static HashSet<ILimit> LimitSet { get; set; } = new();
    #endregion
    #endregion
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

    public ILimit Create(IRateComponent baseMeasure, LimitMode limitMode)
    {
        var (quantity, measurement) = GetBaseMeasureParams(baseMeasure);

        return Create(measurement, quantity, limitMode);
    }

    //public override ILimit Create(IDefaultMeasurable other)
    //{
    //    return NullChecked(other, nameof(other)) switch
    //    {
    //        Measurement measurement => Create(measurement),
    //        BaseMeasure baseMeasure => Create(baseMeasure),
    //        FlatRate flatRate => Create(flatRate.Denominator, default),
    //        LimitedRate limitedRate => Create(limitedRate.Limit),

    //        _ => throw new InvalidOperationException(null),
    //    };

    //    //_ = NullChecked(other, nameof(other));

    //    //if (other is ILimit limit) return Create(limit);

    //    //if (other is IRateComponent baseMeasure) return Create(baseMeasure, default);

    //    //if (other is IMeasurement measurement) return CreateDefault(measurement.MeasureUnitTypeCode);

    //    //if (other is IFlatRate flatRate) return Create(flatRate.Denominator, default);

    //    //if (other is ILimitedRate limitedRate) return Create(limitedRate.Limit);

    //    //throw new InvalidOperationException(null);
    //}

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
        if ((LimitSet.Contains(limit) || LimitSet.Add(limit))
            && LimitSet.TryGetValue(limit, out ILimit? storedLimit)
            && storedLimit != null)
        {
            return storedLimit;
        }

        throw new InvalidOperationException(null);
    }

    public override ILimit Create(IRateComponent other)
    {
        if (other is ILimit limit) return Create(limit);

        return Create(other, default);
    }
    #endregion
    #endregion
}
