namespace CsabaDu.FooVaria.Common.Behaviors
{
    public interface ICalculate
    {
    }

    public interface ICalculate<TSelf, in TOperand> : ISum<TSelf>, IMultiply<TSelf, TOperand>, IDivide<TSelf, TOperand>, ICalculate
        where TSelf : class, IBaseMeasure, ICalculate
        where TOperand : notnull
    {
    }
}