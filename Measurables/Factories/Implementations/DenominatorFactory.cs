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
    public override int DefaultRateComponentQuantity => 1;

    #region Private properties
    #region Static properties
    private static HashSet<IDenominator> DenominatorSet { get; set; } = new();
    #endregion
    #endregion
    #endregion

    #region Public methods
    public IDenominator CreateDefault(MeasureUnitTypeCode measureUnitTypeCode)
    {
        IDenominator denominator = new Denominator(this, measureUnitTypeCode);

        return Create(denominator);
    }

    public IDenominator Create(IDenominator denominator)
    {
        return GetStoredDenominator(denominator);
    }

    public IDenominator Create(IMeasurement measurement, ValueType quantity)
    {
        IDenominator denominator = new Denominator(this, quantity, measurement);

        return Create(denominator);
    }

    public IDenominator Create(string name)
    {
        IMeasurement measurement = MeasurementFactory.Create(name);

        return Create(measurement);
    }

    public IDenominator Create(string name, ValueType quantity)
    {
        IMeasurement measurement = MeasurementFactory.Create(name);

        return Create(measurement, quantity);
    }

    public IDenominator Create(Enum measureUnit)
    {
        IMeasurement measurement = MeasurementFactory.Create(measureUnit);

        return Create(measurement);
    }

    public IDenominator Create(Enum measureUnit, ValueType quantity)
    {
        IMeasurement measurement = MeasurementFactory.Create(measureUnit);

        return Create(measurement, quantity);
    }

    public IDenominator Create(Enum measureUnit, decimal exchangeRate, string customName, ValueType quantity)
    {
        IMeasurement measurement = MeasurementFactory.Create(measureUnit, exchangeRate, customName);

        return Create(measurement, quantity);
    }

    public IDenominator Create(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ValueType quantity)
    {
        IMeasurement measurement = MeasurementFactory.Create(customName, measureUnitTypeCode, exchangeRate);

        return Create(measurement, quantity);
    }

    public IDenominator Create(IBaseMeasure baseMeasure)
    {
        if (baseMeasure is IDenominator denominator) return Create(denominator);

        var (quantity, measurement) = GetBaseMeasureParams(baseMeasure);

        return Create(measurement, quantity);
    }

    public IDenominator Create(IMeasurement measurement)
    {
        return Create(measurement, (decimal)DefaultRateComponentQuantity);
    }

    public override IDenominator Create(IMeasurable other)
    {
        return NullChecked(other, nameof(other)) switch
        {
            Measurement measurement => Create(measurement),
            BaseMeasure baseMeasure => Create(baseMeasure),
            Rate rate => Create(rate.Denominator),

            _ => throw new InvalidOperationException(null),
        };
    }
    #endregion

    #region Private methods
    #region Static methods
    private static IDenominator GetStoredDenominator([DisallowNull] IDenominator denominator)
    {
        bool isExistingDenominator = DenominatorSet.Contains(denominator) || DenominatorSet.Add(denominator);

        if (isExistingDenominator
            && DenominatorSet.TryGetValue(denominator, out IDenominator? storedDenominator)
            && storedDenominator != null)
        {
            return storedDenominator;
        }

        throw new InvalidOperationException(null);
    }
    #endregion
    #endregion
}
