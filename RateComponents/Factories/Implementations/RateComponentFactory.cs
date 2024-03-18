using CsabaDu.FooVaria.BaseTypes.Quantifiables.Types;

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
    public TypeCode QuantityTypeCode => Type.GetTypeCode(DefaultRateComponentQuantity.GetType());

    #region Abstract properties
    public abstract object DefaultRateComponentQuantity {  get; }
    public abstract RateComponentCode RateComponentCode { get; }
    #endregion
    #endregion

    #region Public methods
    public IQuantifiable CreateQuantifiable(MeasureUnitCode measureUnitCode, decimal defaultQuantity)
    {
        IBaseMeasurement baseMeasurement = MeasurementFactory.CreateBaseMeasurement(Defined(measureUnitCode, nameof(measureUnitCode)))!;

        return CreateBaseMeasure(baseMeasurement, defaultQuantity);
    }

    #region Abstract methods
    public abstract IBaseMeasure CreateBaseMeasure(IBaseMeasurement baseMeasurement, ValueType quantity);
    public abstract IMeasurable? CreateDefault(MeasureUnitCode measureUnitCode);
    #endregion
    #endregion

    #region Protected methods
    protected object ConvertQuantity(ValueType quantity)
    {
        return BaseQuantifiable.ConvertQuantity(quantity, nameof(quantity), QuantityTypeCode);
    }

    #region Static methods
    protected static T? GetOrAddStoredRateComponent<T>(T? other, HashSet<T> rateComponentSet)
        where T : class, IRateComponent
    {
        bool exists = rateComponentSet.Contains(NullChecked(other, nameof(other)))
            || rateComponentSet.Add(other!);

        return exists && rateComponentSet.TryGetValue(other!, out T? stored) ?
            stored
            : null;
    }
    #endregion
    #endregion
}
