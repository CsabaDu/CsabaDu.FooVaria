namespace CsabaDu.FooVaria.Common.Types;

public interface IMeasurable : ICommonBase, IDefaultMeasureUnit, IMeasureUnitType, IQuantityType
{
    MeasureUnitTypeCode MeasureUnitTypeCode { get; init; }
}
