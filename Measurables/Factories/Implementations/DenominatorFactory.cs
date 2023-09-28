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
    #endregion

    #region Public methods
    public IDenominator Create(string name, ValueType quantity)
    {
        IMeasurement measurement = MeasurementFactory.Create(name);

        return Create(measurement, quantity);
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

    public IDenominator Create(IMeasurement measurement, ValueType quantity)
    {
        return new Denominator(this, quantity, measurement);
    }

    public IDenominator Create(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ValueType quantity)
    {
        IMeasurement measurement = MeasurementFactory.Create(customName, measureUnitTypeCode, exchangeRate);

        return Create(measurement, quantity);
    }

    public IDenominator Create(IDenominator other)
    {
        return new Denominator(other);
    }

    public IDenominator Create(IBaseMeasure baseMeasure)
    {
        if (baseMeasure is IDenominator denominator) return Create(denominator);

        IMeasurement measurement = NullChecked(baseMeasure, nameof(baseMeasure)).Measurement;
        ValueType quantity = baseMeasure.GetQuantity();

        return Create(measurement, quantity);
    }

    public IDenominator Create(IMeasurement measurement)
    {
        return new Denominator(this, measurement);
    }

    public override IDenominator Create(IMeasurable other)
    {
        _ = NullChecked(other, nameof(other));

        if (other is IDenominator denominator) return Create(denominator);

        if (other is IBaseMeasure baseMeasure) return Create(baseMeasure);

        if (other is IMeasurement measurement) return Create(measurement);

        if (other is IRate rate) return Create(rate.Denominator);

        throw new InvalidOperationException(null);
    }

    public override IDenominator CreateDefault(MeasureUnitTypeCode measureUnitTypeCode)
    {
        return new Denominator(this, measureUnitTypeCode);
    }
    #endregion

    private static IDenominator CreateDenominator(IDenominatorFactory denominatorFactory, ValueType quantity, IMeasurement measurement)
    {
        return new Denominator(denominatorFactory, quantity, measurement);
    }

    private IDenominator CreateDenominator(IDenominatorFactory denominatorFactory, IBaseMeasure baseMeasure)
    {
        if (baseMeasure is IDenominator other) return Create(other);

        IMeasurement measurement = NullChecked(baseMeasure, nameof(baseMeasure)).Measurement;
        ValueType quantity = baseMeasure.GetQuantity();

        return new Denominator(denominatorFactory, quantity, measurement);
    }

    private static IDenominator CreateDenominator(IDenominator denominator)
    {
        return new Denominator(denominator);
    }

    private IDenominator CreateDefaultDenominator(MeasureUnitTypeCode measureUnitTypeCode)
    {
        return new Denominator(this, measureUnitTypeCode);
    }
}
