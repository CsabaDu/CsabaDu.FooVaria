namespace CsabaDu.FooVaria.Common.Types;

public interface IBaseMeasurable : ICommonBase, IDefaultMeasureUnit, IMeasureUnitType
{
    MeasureUnitTypeCode MeasureUnitTypeCode { get; init; }
}
