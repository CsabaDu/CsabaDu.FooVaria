namespace CsabaDu.FooVaria.Measurables.Behaviors
{
    public interface IDefaultRateComponent
    {
    }

    public interface IDefaultRateComponent<out T, U> : IDefaultRateComponent, IQuantifiable<U> where T : class, IBaseMeasure where U : struct
    {
        U GetDefaultRateComponentQuantity();
        T GetDefaultRateComponent();
    }
}
