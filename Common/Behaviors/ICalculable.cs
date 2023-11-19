namespace CsabaDu.FooVaria.Common.Behaviors
{
    public interface ICalculate
    {
    }

    public interface ICalculate<in U, T> : ISum<T>, IMultiply<U, T>, IDivide<U, T>, ICalculate where U : notnull where T : class, IBaseMeasure, ICalculate
    {
    }

}