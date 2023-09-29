namespace CsabaDu.FooVaria.Measurables.Behaviors;

public interface IDefaultRateComponent<out T> where T : class, IBaseMeasure
{
    ValueType GetDefaultRateComponentQuantity();
    T GetDefaultRateComponent();
}
