namespace CsabaDu.FooVaria.Measurements.Behaviors;
public interface ICustomMeasureUnitType
{
    bool IsCustomMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode);
    IEnumerable<MeasureUnitTypeCode> GetCustomMeasureUnitTypeCodes();

    void ValidateCustomMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode);
}
