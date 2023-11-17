namespace CsabaDu.FooVaria.Measurables.Types
{
    public interface IDefaultBaseMeasure : IBaseMeasurable
    {
    }

    public interface IDefaultBaseMeasure<out T, U> : IDefaultBaseMeasure, IQuantifiable<U> where T : class, IBaseMeasure where U : struct
    {
        U GetDefaultRateComponentQuantity();
        T GetDefaultRateComponent();
    }
}
