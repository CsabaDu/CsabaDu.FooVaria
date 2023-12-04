namespace CsabaDu.FooVaria.Common.Types;

public interface IMeasurable : ICommonBase, IDefaultMeasureUnit, IMeasureUnitType
{
    MeasureUnitTypeCode MeasureUnitTypeCode { get; init; }
}
