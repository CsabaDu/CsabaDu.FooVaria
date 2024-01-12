namespace CsabaDu.FooVaria.Measurables.Types;

public interface IDefaultMeasurable<out TSelf> : IMeasurable
    where TSelf : class, IMeasurable
{
    TSelf? GetDefault(MeasureUnitCode measureUnitCode);
}
