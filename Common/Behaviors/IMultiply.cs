namespace CsabaDu.FooVaria.Common.Behaviors;

public interface IMultiply<in U, out T> where U : notnull where T : class, IQuantifiable/*, IBaseMeasurable, ICalculate*/
{
    T Multiply(U multiplier);
}
