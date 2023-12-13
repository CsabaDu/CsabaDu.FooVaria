using CsabaDu.FooVaria.RateComponents.Types;
using CsabaDu.FooVaria.RateComponents.Types.Implementations;

namespace CsabaDu.FooVaria.RateComponents.Factories.Implementations;

public sealed class LimitFactory : RateComponentFactory, ILimitFactory
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
    public ILimit? CreateDefault(MeasureUnitTypeCode measureUnitTypeCode)
    {
        IMeasurement? measurement = MeasurementFactory.CreateDefault(measureUnitTypeCode);

        if (measurement == null) return null;

        ValueType quantity = (ulong)DefaultRateComponentQuantity;
        LimitMode limitMode = default;

        return GetOrCreateStoredLimit(measurement, quantity, limitMode);
    }

    public ILimit Create(ILimit limit)
    {
        return GetStoredLimit(NullChecked(limit, nameof(limit)));
    }

    public ILimit Create(IMeasurement measurement, ValueType quantity, LimitMode limitMode)
    {
        return GetOrCreateStoredLimit(measurement, quantity, limitMode);
    }

    public ILimit Create(string name, ValueType quantity, LimitMode limitMode)
    {
        IMeasurement measurement = MeasurementFactory.Create(name);

        return GetOrCreateStoredLimit(measurement, quantity, limitMode);
    }

    public ILimit Create(Enum measureUnit, ValueType quantity, LimitMode limitMode)
    {
        IMeasurement measurement = MeasurementFactory.Create(measureUnit);

        return GetOrCreateStoredLimit(measurement, quantity, limitMode);
    }

    public ILimit? Create(Enum measureUnit, decimal exchangeRate, string customName, ValueType quantity, LimitMode limitMode)
    {
        IMeasurement? measurement = MeasurementFactory.Create(measureUnit, exchangeRate, customName);

        if (measurement == null) return null;

        return GetOrCreateStoredLimit(measurement, quantity, limitMode);
    }

    public ILimit? Create(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ValueType quantity, LimitMode limitMode)
    {
        IMeasurement? measurement = MeasurementFactory.Create(customName, measureUnitTypeCode, exchangeRate);

        if (measurement == null) return null;

        return GetOrCreateStoredLimit(measurement, quantity, limitMode);
    }

    public ILimit Create(IRateComponent rateComponent, LimitMode limitMode)
    {
        var (measurement, quantity) = GetRateComponentParams(rateComponent);

        return GetOrCreateStoredLimit(measurement, quantity, limitMode);
    }

    public ILimit Create(ILimit limit, ValueType quantity)
    {
        IMeasurement measurement = NullChecked(limit, nameof(limit)).Measurement;
        LimitMode limitMode = limit.LimitMode;

        return GetOrCreateStoredLimit(measurement, quantity, limitMode);
    }

    public override ILimit Create(Enum measureUnit, ValueType quantity)
    {
        IMeasurement measurement = MeasurementFactory.Create(measureUnit);
        LimitMode limitMode = default;

        return GetOrCreateStoredLimit(measurement, quantity, limitMode);
    }

    public ILimit? Create(Enum measureUnit, decimal exchangeRate, ValueType quantity, string customName, LimitMode limitMode)
    {
        IMeasurement? measurement = MeasurementFactory.Create(measureUnit, exchangeRate, customName);

        if (measurement == null) return null;

        return GetOrCreateStoredLimit(measurement, quantity, limitMode);
    }
    #endregion

    #region Private methods
    #region Static methods
    private static ILimit GetStoredLimit([DisallowNull] ILimit limit)
    {
        bool exists = LimitSet.Contains(limit) || LimitSet.Add(limit);

        if (exists
            && LimitSet.TryGetValue(limit, out ILimit? stored)
            && stored != null)
        {
            return stored;
        }

        throw new InvalidOperationException(null);
    }

    private ILimit GetOrCreateStoredLimit(IMeasurement measurement, ValueType quantity, LimitMode limitMode)
    {
        ILimit limit = new Limit(this, measurement, quantity, limitMode);

        return GetStoredLimit(limit);
    }
    #endregion
    #endregion
}
