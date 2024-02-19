namespace CsabaDu.FooVaria.BaseTypes.Measurables.Types;

public interface IMeasurable : ICommonBase, IDefaultMeasureUnit, IMeasureUnitType, IQuantityType, IMeasureUnit
{
    //MeasureUnitCode MeasureUnitCode { get; init; }

    void ValidateMeasureUnitCode(IMeasurable? measurable, [DisallowNull] string paramName);
}
