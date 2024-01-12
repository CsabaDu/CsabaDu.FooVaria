namespace CsabaDu.FooVaria.Quantifiables.Behaviors;

public interface ISum<TSelf>
    where TSelf : class, IBaseMeasure, ICalculate
{
    TSelf Add(TSelf? other);
    TSelf Subtract(TSelf? other);
}

