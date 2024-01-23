namespace CsabaDu.FooVaria.BaseTypes.Quantifiables.Behaviors;

public interface ISum<TSelf>
    where TSelf : class, IQuantifiable, ICalculate
{
    TSelf Add(TSelf? other);
    TSelf Subtract(TSelf? other);
}

