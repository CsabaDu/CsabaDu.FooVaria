namespace CsabaDu.FooVaria.Quantifiables.Behaviors;

public interface IDivide<out TSelf, in TOperand>
    where TSelf : class, IQuantifiable, ICalculate
    where TOperand : notnull
{
    TSelf Divide(TOperand divisor);
}
