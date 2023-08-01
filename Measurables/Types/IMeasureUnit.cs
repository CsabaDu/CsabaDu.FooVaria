namespace CsabaDu.FooVaria.Measurables.Types
{
    public interface IMeasureUnit
    {
        MeasureUnitTypeCode MeasureUnitTypeCode { get; init; }

        Enum GetMeasureUnit();
        Enum GetDefinedMeasureUnit(Enum measureUnit);
        Enum GetNextCustomMeasureUnit(MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate);
        bool TryAddCustomMeasureUnit(Enum measureUnit, decimal exchangeRate);
        bool HasSameMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode);

        void ValidateMeasureUnit(Enum measureUnit, MeasureUnitTypeCode measureUnitTypeCode = default);
    }

}
