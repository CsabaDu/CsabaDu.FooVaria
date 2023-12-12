namespace CsabaDu.FooVaria.RateComponents.Factories.Implementations;

public abstract class RateComponentFactory : IRateComponentFactory
{
    #region Constructors
    private protected RateComponentFactory(IMeasurementFactory measurementFactory)
    {
        MeasurementFactory = NullChecked(measurementFactory, nameof(measurementFactory));
    }
    #endregion

    #region Properties
    public IMeasurementFactory MeasurementFactory { get; init; }
    public abstract RateComponentCode RateComponentCode { get; }
    public virtual object DefaultRateComponentQuantity => default(int);
    #endregion

    #region Public methods
    #region Abstract methods
    //public abstract IDefaultMeasurable Create(IDefaultMeasurable other);
    #endregion
    #endregion

    protected static (ValueType Quantity, IMeasurement Measurement) GetBaseMeasureParams(IRateComponent baseMeasure)
    {
        IMeasurement measurement = NullChecked(baseMeasure, nameof(baseMeasure)).Measurement;
        ValueType quantity = (ValueType)baseMeasure.Quantity;

        return (quantity, measurement);
    }

    public abstract IRateComponent Create(IRateComponent other);
    public abstract IRateComponent Create(Enum measureUnit, ValueType quantity);
}
