namespace CsabaDu.FooVaria.BaseTypes.BaseQuantifiables.Behaviors;

public interface IDivide<out TSelf, in TOperand>
    where TSelf : class, IBaseQuantifiable, ICalculate
    where TOperand : notnull
{
    TSelf Divide(TOperand divisor);
}
