namespace CsabaDu.FooVaria.Measurables.Behaviors;

public interface IDefaultRateComponent<out T, out U>  where T : class, IBaseMeasure where U : struct
{
    U GetDefaultRateComponentQuantity();
    T GetDefaultRateComponent();
}
