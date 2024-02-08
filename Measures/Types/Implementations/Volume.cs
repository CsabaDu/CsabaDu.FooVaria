namespace CsabaDu.FooVaria.Measures.Types.Implementations;

internal sealed class Volume : Measure<IVolume, double, VolumeUnit>, IVolume
{
    #region Constructors
    internal Volume(IMeasureFactory factory, VolumeUnit volumeUnit, double quantity) : base(factory, volumeUnit, quantity)
    {
    }
    #endregion

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
        return MeasureUnitCode;
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
