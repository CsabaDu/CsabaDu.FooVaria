namespace CsabaDu.FooVaria.Common.Types;

public interface IBaseMeasurable : IDefaultMeasureUnit, IMeasureUnitType
{
    MeasureUnitTypeCode MeasureUnitTypeCode { get; init; }
}
