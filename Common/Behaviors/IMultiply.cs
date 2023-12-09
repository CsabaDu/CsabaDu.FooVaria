namespace CsabaDu.FooVaria.Common.Behaviors;

public interface IMultiply<in TOperand, out TSelf> where TOperand : notnull where TSelf : class, IQuantifiable/*, IBaseMeasurable, ICalculate*/
{
    TSelf Multiply(TOperand multiplier);
}
