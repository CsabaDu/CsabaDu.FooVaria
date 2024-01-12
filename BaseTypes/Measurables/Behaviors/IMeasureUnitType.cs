namespace CsabaDu.FooVaria.Measurables.Behaviors;

public interface IMeasureUnitType
{
    Type GetMeasureUnitType();
    IEnumerable<MeasureUnitCode> GetMeasureUnitCodes();
    bool HasMeasureUnitCode(MeasureUnitCode measureUnitCode);
    bool IsValidMeasureUnitCode(MeasureUnitCode measureUnitCode);

    void ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, string paramName);
}
