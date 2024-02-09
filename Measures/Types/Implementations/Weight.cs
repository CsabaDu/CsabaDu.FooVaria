namespace CsabaDu.FooVaria.Measures.Types.Implementations;

internal sealed class Weight(IMeasureFactory factory, WeightUnit weightUnit, double quantity)
    : Measure<IWeight, double, WeightUnit>(factory, weightUnit, quantity), IWeight
{
    #region Public methods
    public IWeight ConvertFrom(IVolume volume)
    {
        return NullChecked(volume, nameof(volume)).ConvertMeasure();
    }

    public IVolume ConvertMeasure()
    {
        return ConvertMeasure<IVolume>(MeasureOperationMode.Multiply);
    }
    #endregion
}
