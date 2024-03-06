namespace CsabaDu.FooVaria.BaseTypes.Measurables.Behaviors;

public interface IMeasureUnitCode
{
    bool HasMeasureUnitCode(MeasureUnitCode measureUnitCode);
    MeasureUnitCode GetMeasureUnitCode();

    void ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, [DisallowNull] string paramName);
}
