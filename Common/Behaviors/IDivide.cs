namespace CsabaDu.FooVaria.Common.Behaviors;

public interface IDivide<in U, out T> where U : notnull where T : class, IBaseMeasurable, IQuantifiable, ICalculable
{
    T Divide(U divisor);
}
