namespace CsabaDu.FooVaria.BaseTypes.BaseQuantifiables.Behaviors;

public interface IMultiply<out TSelf, in TOperand>
    where TSelf : class, IBaseQuantifiable
    where TOperand : notnull
{
    TSelf Multiply(TOperand multiplier);
}
