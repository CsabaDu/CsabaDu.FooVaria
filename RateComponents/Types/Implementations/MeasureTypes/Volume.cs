namespace CsabaDu.FooVaria.RateComponents.Types.Implementations.MeasureTypes;

internal sealed class Volume : Measure<IVolume, double, VolumeUnit>, IVolume
{
    #region Constructors
    internal Volume(IMeasureFactory factory, ValueType quantity, VolumeUnit volumeUnit) : base(factory, quantity, volumeUnit)
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
        return ConvertMeasure<IWeight, WeightUnit>(ConvertMode.Divide);
    }

    public MeasureUnitTypeCode GetMeasureUnitTypeCode()
    {
        return MeasureUnitTypeCode;
    }

    public ISpreadMeasure GetSpreadMeasure()
    {
        return this;
    }

    public override bool TryGetRateComponent(Enum measureUnit, ValueType quantity, decimal exchangeRate, string customName, [NotNullWhen(true)] out IRateComponent? baseMeasure)
    {
        throw new NotImplementedException();
    }

    public override void ValidateQuantity(ValueType? quantity, string paramName)
    {
        throw new NotImplementedException();
    }

    public void ValidateSpreadMeasure(ISpreadMeasure? spreadMeasure, string paramName)
    {
        ValidateSpreadMeasure(paramName, spreadMeasure);
    }
    #endregion
}
