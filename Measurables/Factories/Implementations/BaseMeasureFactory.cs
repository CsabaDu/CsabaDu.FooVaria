namespace CsabaDu.FooVaria.Measurables.Factories.Implementations;

public abstract class BaseMeasureFactory : IBaseMeasureFactory
{
    #region Constructors
    private protected BaseMeasureFactory(IMeasurementFactory measurementFactory)
    {
        MeasurementFactory = NullChecked(measurementFactory, nameof(measurementFactory));
    }
    #endregion

    #region Properties
    public IMeasurementFactory MeasurementFactory { get; init; }
    public abstract RateComponentCode RateComponentCode { get; }
    public virtual int DefaultRateComponentQuantity => default;
    #endregion

    #region Public methods
    #region Abstract methods
    public abstract IMeasurable Create(IMeasurable other);
    #endregion
    #endregion

    protected static (ValueType Quantity, IMeasurement Measurement) GetBaseMeasureParams(IBaseMeasure baseMeasure)
    {
        IMeasurement measurement = NullChecked(baseMeasure, nameof(baseMeasure)).Measurement;
        ValueType quantity = (ValueType)baseMeasure.Quantity;

        return (quantity, measurement);
    }
}
