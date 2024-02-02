namespace CsabaDu.FooVaria.RateComponents.Factories.Implementations;

public sealed class DenominatorFactory : RateComponentFactory<IDenominator>, IDenominatorFactory
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


    public IDenominator Create(string name)
    {
        IMeasurement measurement = MeasurementFactory.Create(name);

        return GetOrCreateStoredDenominator(measurement);
    }

    public IDenominator Create(IMeasurement measurement)
    {
        return GetOrCreateStoredDenominator(measurement);
    }

    public IDenominator Create(IBaseMeasure baseMeasure, ValueType quantity)
    {
        Enum measureUnit = NullChecked(baseMeasure, nameof(baseMeasure)).GetMeasureUnit();

        return GetOrCreateStoredDenominator(measureUnit, quantity);
    }

    #region Override methods
    public override IDenominator Create(string name, ValueType quantity)
    {
        IMeasurement measurement = MeasurementFactory.Create(name);

        return GetOrCreateStoredDenominator(measurement, quantity);
    }

    public override IDenominator Create(Enum measureUnit, ValueType quantity)
    {
        return GetOrCreateStoredDenominator(measureUnit, quantity);
    }

    public override IDenominator? Create(Enum measureUnit, decimal exchangeRate, ValueType quantity, string customName)
    {
        IMeasurement? measurement = MeasurementFactory.Create(measureUnit, exchangeRate, customName);

        if (measurement == null) return null;

        return GetOrCreateStoredDenominator(measurement, quantity);
    }

    public override IDenominator? Create(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate, ValueType quantity)
    {
        IMeasurement? measurement = MeasurementFactory.Create(customName, measureUnitCode, exchangeRate);

        if (measurement == null) return null;

        return GetOrCreateStoredDenominator(measurement, quantity);
    }

    public override IDenominator Create(IBaseMeasure baseMeasure)
    {
        if (NullChecked(baseMeasure, nameof(baseMeasure)) is IDenominator denominator) return CreateNew(denominator);

        Enum measureUnit = baseMeasure.GetMeasureUnit();
        ValueType quantity = baseMeasure.GetDecimalQuantity();

        return GetOrCreateStoredDenominator(measureUnit, quantity);
    }

    public override IDenominator CreateBaseMeasure(IBaseMeasurement baseMeasurement, ValueType quantity)
    {
        Enum measureUnit = NullChecked(baseMeasurement, nameof(baseMeasurement)).GetMeasureUnit();

        return GetOrCreateStoredDenominator(measureUnit, quantity);
    }

    public override IDenominator? CreateDefault(MeasureUnitCode measureUnitCode)
    {
        IMeasurement? measurement = (IMeasurement?)MeasurementFactory.CreateDefault(measureUnitCode);

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
    private IDenominator GetOrCreateStoredDenominator(IMeasurement measurement, ValueType quantity)
    {
        decimal convertedQuantity = (decimal)ConvertQuantity(quantity);

        IDenominator denominator = new Denominator(this, measurement, convertedQuantity);

        return CreateNew(denominator);
    }

    private IDenominator GetOrCreateStoredDenominator(IMeasurement measurement)
    {
        decimal quantity = (decimal)DefaultRateComponentQuantity;

        return GetOrCreateStoredDenominator(measurement, quantity);
    }

    private IDenominator GetOrCreateStoredDenominator(Enum measureUnit, ValueType quantity)
    {
        IMeasurement measurement = MeasurementFactory.Create(measureUnit);

        return GetOrCreateStoredDenominator(measurement, quantity);
    }
    #endregion
}
