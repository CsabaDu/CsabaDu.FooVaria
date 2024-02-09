namespace CsabaDu.FooVaria.Measurements.Types.Implementations;

internal sealed class ConstantMeasurement(IMeasurementFactory factory, Enum measureUnit) : Measurement(factory, measureUnit), IConstantMeasurement
{
    #region Public methods
    public override IEnumerable<MeasureUnitCode> GetMeasureUnitCodes()
    {
        return base.GetMeasureUnitCodes().Where(x => !x.IsCustomMeasureUnitCode());
    }
    #endregion
}