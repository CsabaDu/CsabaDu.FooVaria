namespace CsabaDu.FooVaria.BaseTypes.Measurables.Types;

public interface IMeasurable : ICommonBase, IDefaultMeasureUnit, IMeasureUnitCode, IQuantityType, IMeasureUnit
{
    void ValidateMeasureUnitCode(IMeasurable? measurable, [DisallowNull] string paramName);
}
