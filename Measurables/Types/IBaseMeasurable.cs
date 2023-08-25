namespace CsabaDu.FooVaria.Measurables.Types;

public interface IBaseMeasurable : IMeasureUnitType, IDefaultMeasureUnit
{
    MeasureUnitTypeCode MeasureUnitTypeCode { get; init; }
}
