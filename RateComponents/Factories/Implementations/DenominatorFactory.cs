namespace CsabaDu.FooVaria.RateComponents.Factories.Implementations;

public sealed class DenominatorFactory : RateComponentFactory<IDenominator, decimal>, IDenominatorFactory
{
    #region Constructors
    #region Static constructor
    static DenominatorFactory()
    {
        DenominatorSet = new();
    }
    #endregion

    public DenominatorFactory(IMeasurementFactory measurementFactory) : base(measurementFactory)
    {
    }
    #endregion

    #region Properties
    #region Override properties
    public override RateComponentCode RateComponentCode => RateComponentCode.Denominator;
    public override object DefaultRateComponentQuantity => decimal.One;
    #endregion

    #region Private properties
    #region Static properties
    private static HashSet<IDenominator> DenominatorSet { get; set; }
    #endregion
    #endregion
    #endregion

    #region Public methods
    public IDenominator Create(Enum measureUnit)
    {
        IMeasurement measurement = MeasurementFactory.Create(measureUnit);

        return GetOrCreateStoredDenominator(measurement);
    }

    public IDenominator Create(string name, ValueType quantity)
    {
        IMeasurement measurement = MeasurementFactory.Create(name);

        return GetOrCreateStoredRateComponent(measurement, quantity);
    }

    public IDenominator Create(string name)
    {
        IMeasurement measurement = MeasurementFactory.Create(name);

        return GetOrCreateStoredDenominator(measurement);
    }

    public IDenominator Create(IMeasurement measurement)
    {
        return GetOrCreateStoredDenominator(measurement);
    }

    public IDenominator Create(IRateComponent rateComponent, ValueType quantity)
    {
        IMeasurement measurement = NullChecked(rateComponent, nameof(rateComponent)).Measurement;

        return GetOrCreateStoredRateComponent(measurement, quantity);
    }

    public IDenominator Create(Enum measureUnit, ValueType quantity)
    {
        IMeasurement measurement = MeasurementFactory.Create(measureUnit);

        return GetOrCreateStoredRateComponent(measurement, quantity);
    }

    public IDenominator? Create(Enum measureUnit, decimal exchangeRate, ValueType quantity, string customName)
    {
        IMeasurement? measurement = MeasurementFactory.Create(measureUnit, exchangeRate, customName);

        if (measurement == null) return null;

        return GetOrCreateStoredRateComponent(measurement, quantity);
    }

    public IDenominator? Create(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate, ValueType quantity)
    {
        IMeasurement? measurement = MeasurementFactory.Create(customName, measureUnitCode, exchangeRate);

        if (measurement == null) return null;

        return GetOrCreateStoredRateComponent(measurement, quantity);
    }

    #region Override methods
    public override IDenominator Create(IMeasurement measurement, decimal quantity)
    {
        IDenominator other = new Denominator(this, measurement, quantity);

        return GetStoredRateComponent(other, DenominatorSet) ?? throw new InvalidOperationException(null);
    }

    public override IBaseMeasure CreateBaseMeasure(IBaseMeasurement baseMeasurement, ValueType quantity)
    {
        return GetOrCreateRateComponent(baseMeasurement, quantity);
    }

    public override IDenominator? CreateDefault(MeasureUnitCode measureUnitCode)
    {
        IMeasurement? measurement = MeasurementFactory.CreateDefault(measureUnitCode);

        if (measurement == null) return null;

        return GetOrCreateStoredDenominator(measurement);
    }

    public override IDenominator CreateNew(IDenominator other)
    {
        return GetStoredRateComponent(other, DenominatorSet) ?? throw new InvalidOperationException(null);
    }
    #endregion
    #endregion

    #region Private methods
    private IDenominator GetOrCreateStoredDenominator(IMeasurement measurement)
    {
        decimal quantity = (decimal)DefaultRateComponentQuantity;

        return Create(measurement, quantity);
    }
    #endregion
}
