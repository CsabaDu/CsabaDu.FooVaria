namespace CsabaDu.FooVaria.BaseTypes.Quantifiables.Behaviors
{
    public interface ICalculate
    {
    }

    public interface ICalculate<TSelf, in TOperand> : ISum<TSelf>, IMultiply<TSelf, TOperand>, IDivide<TSelf, TOperand>, ICalculate
        where TSelf : class, IQuantifiable, ICalculate
        where TOperand : notnull
    {
    }
}