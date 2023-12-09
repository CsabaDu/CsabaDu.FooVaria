namespace CsabaDu.FooVaria.Common.Behaviors;

public interface IDivide<in TOperand, out TSelf> where TOperand : notnull where TSelf : class, IBaseMeasure, ICalculate
{
    TSelf Divide(TOperand divisor);
}
