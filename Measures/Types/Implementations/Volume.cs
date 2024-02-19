namespace CsabaDu.FooVaria.Measures.Types.Implementations;

internal sealed class Volume(IMeasureFactory factory, VolumeUnit volumeUnit, double quantity)
    : Measure<IVolume, double, VolumeUnit>(factory, volumeUnit, quantity), IVolume
{
    #region Public methods
    public IVolume ConvertFrom(IWeight weight)
    {
        return NullChecked(weight, nameof(weight)).ConvertMeasure();
    }

    public IWeight ConvertMeasure()
    {
        return ConvertMeasure<IWeight>(MeasureOperationMode.Divide);
    }

    public MeasureUnitCode GetSpreadMeasureUnitCode()
    {
        return GetMeasureUnitCode();
    }

    public ISpreadMeasure GetSpreadMeasure()
    {
        return this;
    }

    public void ValidateSpreadMeasure(ISpreadMeasure? spreadMeasure, string paramName)
    {
        ValidateSpreadMeasure(paramName, spreadMeasure);
    }
    #endregion
}
