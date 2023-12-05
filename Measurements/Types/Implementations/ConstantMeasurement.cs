namespace CsabaDu.FooVaria.Measurements.Types.Implementations;

internal sealed class ConstantMeasurement : Measurement, IConstantMeasurement
{
    internal ConstantMeasurement(IMeasurementFactory factory, Enum measureUnit) : base(factory, measureUnit)
    {
    }

    public override IEnumerable<MeasureUnitTypeCode> GetMeasureUnitTypeCodes()
    {
        return base.GetMeasureUnitTypeCodes().Where(x => !x.IsCustomMeasureUnitTypeCode());
    }
}