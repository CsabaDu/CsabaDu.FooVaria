namespace CsabaDu.FooVaria.Measurables.Behaviors;

public interface IDivide<in U, out T> where U : notnull where T : class, IMeasurable, ICalculable
{
    T Divide(U divisor);
}
