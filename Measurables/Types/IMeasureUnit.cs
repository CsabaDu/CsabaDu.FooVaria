namespace CsabaDu.FooVaria.Measurables.Types
{
    public interface IMeasureUnit
    {
        MeasureUnitTypeCode MeasureUnitTypeCode { get; init; }

        Enum GetMeasureUnit();
        Enum GetDefinedMeasureUnit(Enum measureUnit);
        bool HasSameMeasureUnitTypeCode(Enum measureUnit);

        void ValidateMeasureUnit(Enum measureUnit, MeasureUnitTypeCode measureUnitTypeCode = default);
    }

}
