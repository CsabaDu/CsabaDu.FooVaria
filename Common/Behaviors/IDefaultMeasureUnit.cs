namespace CsabaDu.FooVaria.Common.Behaviors;

public interface IDefaultMeasureUnit : IMeasureUnit
{
    Enum GetDefaultMeasureUnit(); // TODO
    IEnumerable<string> GetDefaultNames(); // TODO
}

    //Enum GetDefaultMeasureUnit(MeasureUnitTypeCode measureUnitTypeCode); // TODO
    //IEnumerable<string> GetDefaultNames(MeasureUnitTypeCode measureUnitTypeCode); // TODO