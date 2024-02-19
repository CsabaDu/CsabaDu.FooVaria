namespace CsabaDu.FooVaria.BaseTypes.BaseQuantifiables.Behaviors;

public interface ISum<TSelf>
    where TSelf : class, IBaseQuantifiable, ICalculate
{
    TSelf Add(TSelf? other);
    TSelf Subtract(TSelf? other);
}

