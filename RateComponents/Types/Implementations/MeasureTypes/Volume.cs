namespace CsabaDu.FooVaria.RateComponents.Types.Implementations.MeasureTypes;

internal sealed class Volume : Measure<IVolume, double, VolumeUnit>, IVolume
{
    #region Constructors
    internal Volume(IMeasureFactory factory, VolumeUnit volumeUnit, ValueType quantity) : base(factory, volumeUnit, quantity)
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
        return ConvertMeasure<IWeight>(ConvertMode.Divide);
    }

    public MeasureUnitTypeCode GetMeasureUnitTypeCode()
    {
        return MeasureUnitTypeCode;
    }

    public ISpreadMeasure GetSpreadMeasure()
    {
        return this;
    }

    public override void ValidateQuantity(ValueType? quantity, string paramName)
    {
        ValidateSpreadQuantity(quantity, paramName);
    }

    public void ValidateSpreadMeasure(ISpreadMeasure? spreadMeasure, string paramName)
    {
        ValidateSpreadMeasure(paramName, spreadMeasure);
    }
    #endregion
}
