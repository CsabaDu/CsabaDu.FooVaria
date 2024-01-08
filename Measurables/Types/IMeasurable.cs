namespace CsabaDu.FooVaria.Measurables.Types;

public interface IMeasurable : ICommonBase, IDefaultMeasureUnit, IMeasureUnitType, IQuantityType, IMeasureUnit
{
    MeasureUnitTypeCode MeasureUnitTypeCode { get; init; }
}
