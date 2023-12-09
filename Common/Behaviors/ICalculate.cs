namespace CsabaDu.FooVaria.Common.Behaviors
{
    public interface ICalculate
    {
    }

    public interface ICalculate<in TOperand, TSelf> : ISum<TSelf>, IMultiply<TOperand, TSelf>, IDivide<TOperand, TSelf>, ICalculate where TOperand : notnull where TSelf : class, IBaseMeasure, ICalculate
    {
    }
}