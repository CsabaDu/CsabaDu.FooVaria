namespace CsabaDu.FooVaria.BaseTypes.BaseQuantifiables.Behaviors
{
    public interface ICalculate;

    public interface ICalculate<TSelf, in TOperand> : ISum<TSelf>, IMultiply<TSelf, TOperand>, IDivide<TSelf, TOperand>, ICalculate
        where TSelf : class, IBaseQuantifiable, ICalculate
        where TOperand : notnull;
}