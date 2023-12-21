using CsabaDu.FooVaria.RateComponents.Types.Implementations;

namespace CsabaDu.FooVaria.RateComponents.Factories.Implementations;

public sealed class DenominatorFactory : RateComponentFactory<IDenominator>, IDenominatorFactory
{
    #region Constructors
    public DenominatorFactory(IMeasurementFactory measurementFactory) : base(measurementFactory)
    {
    }
    #endregion

    #region Properties
    #region Override properties
    public override RateComponentCode RateComponentCode => RateComponentCode.Denominator;
    public override object DefaultRateComponentQuantity => 1;
    #endregion

    #region Private properties
    #region Static properties
    private static HashSet<IDenominator> DenominatorSet { get; set; } = new();
    #endregion
    #endregion
    #endregion

    #region Public methods
    public IDenominator CreateNew(IDenominator denominator)
    {
        return GetStoredDenominator(NullChecked(denominator, nameof(denominator)));
    }

    public IDenominator Create(Enum measureUnit)
    {
        IMeasurement measurement = MeasurementFactory.Create(measureUnit);

        return GetOrCreateStoredDenominator(measurement);
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

        return GetOrCreateStoredDenominator(measurement, quantity);
    }

    public override  IDenominator Create(IMeasurement measurement, ValueType quantity)
    {
        return GetOrCreateStoredDenominator(measurement, quantity);
    }

    public override IDenominator? Create(Enum measureUnit, decimal exchangeRate, ValueType quantity, string customName)
    {
        IMeasurement? measurement = MeasurementFactory.Create(measureUnit, exchangeRate, customName);

        if (measurement == null) return null;

        return GetOrCreateStoredDenominator(measurement, quantity);
    }

    public override IDenominator? Create(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ValueType quantity)
    {
        IMeasurement? measurement = MeasurementFactory.Create(customName, measureUnitTypeCode, exchangeRate);

        if (measurement == null) return null;

        return GetOrCreateStoredDenominator(measurement, quantity);
    }

    public IDenominator? CreateDefault(MeasureUnitTypeCode measureUnitTypeCode)
    {
        IMeasurement? measurement = MeasurementFactory.CreateDefault(measureUnitTypeCode);

        if (measurement == null) return null;

        return GetOrCreateStoredDenominator(measurement);
    }

    #region Override methods
    public override IDenominator Create(Enum measureUnit, ValueType quantity)
    {
        IMeasurement measurement = MeasurementFactory.Create(measureUnit);

        return GetOrCreateStoredDenominator(measurement, quantity);
    }
    #endregion
    #endregion

    #region Private methods
    private IDenominator GetOrCreateStoredDenominator(IMeasurement measurement, ValueType quantity)
    {
        IDenominator denominator = new Denominator(this, measurement, quantity);

        return GetStoredDenominator(denominator);
    }

    private IDenominator GetOrCreateStoredDenominator(IMeasurement measurement)
    {
        ValueType quantity = (decimal)DefaultRateComponentQuantity;

        return GetOrCreateStoredDenominator(measurement, quantity);
    }

    #region Static methods
    private static IDenominator GetStoredDenominator([DisallowNull] IDenominator denominator)
    {
        return GetStored(denominator, DenominatorSet);
    }
    #endregion
    #endregion
}
