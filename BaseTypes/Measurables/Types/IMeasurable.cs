namespace CsabaDu.FooVaria.Measurables.Types;

public interface IMeasurable : ICommonBase, IDefaultMeasureUnit, IMeasureUnitType, IQuantityType, IMeasureUnit
{
    MeasureUnitCode MeasureUnitCode { get; init; }
}
