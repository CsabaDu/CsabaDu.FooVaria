namespace CsabaDu.FooVaria.Measurables.Behaviors;

public interface ICalculate<in U, T> : ISum<T>, IMultiply<U, T>, IDivide<U, T> where U : notnull where T : class, IMeasurable, ICalculable
{
}
