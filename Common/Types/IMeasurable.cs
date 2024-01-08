namespace CsabaDu.FooVaria.Common.Types;

public interface IMeasurable : ICommonBase/*, IDefaultMeasureUnit, IMeasureUnitType, IQuantityType*/, IMeasureUnit
{
    MeasureUnitTypeCode MeasureUnitTypeCode { get; init; }
}
