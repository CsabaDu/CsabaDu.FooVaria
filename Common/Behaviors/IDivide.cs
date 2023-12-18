namespace CsabaDu.FooVaria.Common.Behaviors;

public interface IDivide<out TSelf, in TOperand>
    where TSelf : class, IBaseMeasure, ICalculate
    where TOperand : notnull
{
    TSelf Divide(TOperand divisor);
}
