namespace CsabaDu.FooVaria.RateComponents.Factories.Implementations;

public sealed class LimitFactory : RateComponentFactory<ILimit, ulong>, ILimitFactory
{
    #region Constructors
    #region Static constructor
    static LimitFactory()
    {
        LimitSet = new();
    }
    #endregion

    public LimitFactory(IMeasurementFactory measurementFactory) : base(measurementFactory)
    {
    }
    #endregion

    #region Properties
    #region Override properties
    public override RateComponentCode RateComponentCode => RateComponentCode.Limit;
    public override object DefaultRateComponentQuantity => default(ulong);
    #endregion

    #region Private properties
    #region Static properties
    private static HashSet<ILimit> LimitSet { get; set; }
    #endregion
    #endregion
    #endregion

    #region Public methods
    public ILimit Create(IMeasurement measurement, ulong quantity, LimitMode limitMode)
    {
        ILimit other = new Limit(this, measurement, quantity, limitMode);

        return GetStoredRateComponent(other, LimitSet) ?? throw new InvalidOperationException(null);
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

    public ILimit? Create(Enum measureUnit, decimal exchangeRate, ValueType quantity, string customName, LimitMode limitMode)
    {
        IMeasurement? measurement = MeasurementFactory.Create(measureUnit, exchangeRate, customName);

        if (measurement == null) return null;

        return GetOrCreateStoredLimit(measurement, quantity, limitMode);
    }

    public ILimit? Create(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate, ValueType quantity, LimitMode limitMode)
    {
        IMeasurement? measurement = MeasurementFactory.Create(customName, measureUnitCode, exchangeRate);

        if (measurement == null) return null;

        return GetOrCreateStoredLimit(measurement, quantity, limitMode);
    }

    public ILimit Create(IBaseMeasure baseMeasure, LimitMode limitMode)
    {
        Enum measureUnit = NullChecked(baseMeasure, nameof(baseMeasure)).GetMeasureUnit();
        ValueType quantity = (ValueType)baseMeasure.Quantity;

        return Create(measureUnit, quantity, limitMode);
    }

    public ILimit Create(ILimit limit, ValueType quantity)
    {
        IMeasurement measurement = NullChecked(limit, nameof(limit)).Measurement;
        LimitMode limitMode = limit.LimitMode;

        return GetOrCreateStoredLimit(measurement, quantity, limitMode);
    }

    #region Override methods
    public override ILimit Create(IMeasurement measurement, ulong quantity)
    {
        return Create(measurement, quantity, default);
    }

    public override IBaseMeasure CreateBaseMeasure(IBaseMeasurement baseMeasurement, ValueType quantity)
    {
        return GetOrCreateRateComponent(baseMeasurement, quantity);
    }

    public override ILimit? CreateDefault(MeasureUnitCode measureUnitCode)
    {
        IMeasurement? measurement = MeasurementFactory.CreateDefault(measureUnitCode);

        if (measurement == null) return null;

        return Create(measurement, (ulong)DefaultRateComponentQuantity);
    }

    //public ILimit CreateNew(ILimit other)
    //{
    //    return GetStoredRateComponent(other, LimitSet) ?? throw new InvalidOperationException(null);
    //}
    #endregion
    #endregion

    #region Private methods
    private ILimit GetOrCreateStoredLimit(IMeasurement measurement, ValueType quantity, LimitMode limitMode)
    {
        ulong convertedQuantity = ConvertQuantity(quantity);

        return Create(measurement, convertedQuantity, limitMode);
    }
    #endregion
}
