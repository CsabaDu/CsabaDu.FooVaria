namespace CsabaDu.FooVaria.Measurables.Types
{
    public interface IDefaultRateComponent : IBaseMeasurable
    {
    }

    public interface IDefaultBaseMeasure<out T, U> : IDefaultRateComponent, IDefaultBaseMeasurable<T>, IQuantifiable<U> where T : class, IRateComponent where U : struct
    {
        U GetDefaultRateComponentQuantity();
        T GetDefaultRateComponent();
    }
}
