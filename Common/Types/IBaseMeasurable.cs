namespace CsabaDu.FooVaria.Common.Types;

public interface IBaseMeasurable : ICommonBase, IDefaultMeasureUnit, IMeasureUnitType, IExchangeable<MeasureUnitTypeCode>/*, IExchange<IBaseMeasurable, Enum>*/
{
    MeasureUnitTypeCode MeasureUnitTypeCode { get; init; }
}
