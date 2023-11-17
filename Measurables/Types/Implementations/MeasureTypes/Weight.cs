namespace CsabaDu.FooVaria.Measurables.Types.Implementations.MeasureTypes;

internal sealed class Weight : Measure<IWeight, double, WeightUnit>, IWeight
{
    #region Constructors
    internal Weight(IMeasureFactory factory, ValueType quantity, WeightUnit weightUnit) : base(factory, quantity, weightUnit)
    {
    }
    #endregion

    #region Public methods
    public IWeight ConvertFrom(IVolume volume)
    {
        return NullChecked(volume, nameof(volume)).ConvertMeasure();
    }

    public IVolume ConvertMeasure()
    {
        return ConvertMeasure<IVolume, VolumeUnit>(ConvertMode.Multiply);
    }
    #endregion
}
