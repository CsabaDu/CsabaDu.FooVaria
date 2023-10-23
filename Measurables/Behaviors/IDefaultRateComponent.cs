namespace CsabaDu.FooVaria.Measurables.Behaviors
{
    public interface IDefaultRateComponent
    {
    }

    public interface IDefaultRateComponent<out T, out U> : IDefaultRateComponent where T : class, IBaseMeasure where U : struct
    {
        U GetQuantity();
        U GetDefaultRateComponentQuantity();
        T GetDefaultRateComponent();
    }
}
