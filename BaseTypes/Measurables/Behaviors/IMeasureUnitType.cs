namespace CsabaDu.FooVaria.BaseTypes.Measurables.Behaviors;

public interface IMeasureUnitType
{
    Type GetMeasureUnitType();
    //IEnumerable<MeasureUnitCode> GetMeasureUnitCodes();
    bool HasMeasureUnitCode(MeasureUnitCode measureUnitCode);
    //bool IsValidMeasureUnitCode(MeasureUnitCode measureUnitCode);

    void ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, [DisallowNull] string paramName);
}
