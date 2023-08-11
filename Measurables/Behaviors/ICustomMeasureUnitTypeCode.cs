namespace CsabaDu.FooVaria.Measurables.Behaviors;
public interface ICustomMeasureUnitTypeCode
{
    bool IsCustomMeasureUnitTypeCode(MeasureUnitTypeCode? measureUnitTypeCode = null);
    IEnumerable<MeasureUnitTypeCode> GetCustomMeasureUnitTypeCodes();

    void ValidateCustomMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode);
}
