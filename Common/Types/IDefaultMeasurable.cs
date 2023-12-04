namespace CsabaDu.FooVaria.Common.Types;

public interface IDefaultMeasurable<out T> : IMeasurable where T : class, IMeasurable
{
    T GetDefault(MeasureUnitTypeCode measureUnitTypeCode);
}
