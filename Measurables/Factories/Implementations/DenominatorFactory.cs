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
    public override object DefaultRateComponentQuantity => decimal.One;
    private static SortedSet<IDenominator> DenominatorSet { get; set; } = new();
    #endregion

    #region Public methods
    public override IDenominator CreateDefault(MeasureUnitTypeCode measureUnitTypeCode)
    {
        IDenominator denominator = new Denominator(this, measureUnitTypeCode);

        return GetStoredDenominator(denominator);
    }

    public IDenominator Create(string name, ValueType quantity)
    {
        IMeasurement measurement = MeasurementFactory.Create(name);
        IDenominator denominator = Create(measurement, quantity);

        return GetStoredDenominator(denominator);
    }

    public IDenominator Create(Enum measureUnit, ValueType quantity)
    {
        IMeasurement measurement = MeasurementFactory.Create(measureUnit);
        IDenominator denominator = Create(measurement, quantity);

        return GetStoredDenominator(denominator);
    }

    public IDenominator Create(Enum measureUnit, decimal exchangeRate, string customName, ValueType quantity)
    {
        IMeasurement measurement = MeasurementFactory.Create(measureUnit, exchangeRate, customName);
        IDenominator denominator = Create(measurement, quantity);

        return GetStoredDenominator(denominator);
    }

    public IDenominator Create(IMeasurement measurement, ValueType quantity)
    {
        IDenominator denominator = new Denominator(this, quantity, measurement);

        return GetStoredDenominator(denominator);
    }

    public IDenominator Create(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ValueType quantity)
    {
        IMeasurement measurement = MeasurementFactory.Create(customName, measureUnitTypeCode, exchangeRate);
        IDenominator denominator = Create(measurement, quantity);

        return GetStoredDenominator(denominator);
    }

    public IDenominator Create(IDenominator denominator)
    {
        return GetStoredDenominator(denominator);
    }

    public IDenominator Create(IBaseMeasure baseMeasure)
    {
        if (baseMeasure is IDenominator denominator) return GetStoredDenominator(denominator);

        IMeasurement measurement = NullChecked(baseMeasure, nameof(baseMeasure)).Measurement;
        ValueType quantity = baseMeasure.GetQuantity();
        denominator = Create(measurement, quantity);

        return GetStoredDenominator(denominator);
    }

    public IDenominator Create(IMeasurement measurement)
    {
        IDenominator denominator = Create(measurement, (ValueType)DefaultRateComponentQuantity);

        return GetStoredDenominator(denominator);
    }

    public override IDenominator Create(IMeasurable other)
    {
        _ = NullChecked(other, nameof(other));

        if (other is IDenominator denominator) return GetStoredDenominator(denominator);

        if (other is IBaseMeasure baseMeasure) return Create(baseMeasure);

        if (other is IMeasurement measurement) return Create(measurement);

        if (other is IRate rate) return GetStoredDenominator(rate.Denominator);

        throw new InvalidOperationException(null);
    }
    #endregion

    #region Private methods
    #region Static methods
    private static IDenominator GetStoredDenominator([DisallowNull] IDenominator denominator)
    {
        if (DenominatorSet.Contains(denominator) || DenominatorSet.Add(denominator))
        {
            if (DenominatorSet.TryGetValue(denominator, out IDenominator? storedDenominator) && storedDenominator != null)
            {
                return storedDenominator;
            }
        }

        throw new InvalidOperationException(null);
    }
    #endregion
    #endregion
}
