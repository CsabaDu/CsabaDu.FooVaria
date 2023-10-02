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
    public abstract object DefaultRateComponentQuantity { get; }
    #endregion

    #region Public methods
    #region Abstract methods
    public abstract IMeasurable Create(IMeasurable other);
    #endregion
    #endregion
}
