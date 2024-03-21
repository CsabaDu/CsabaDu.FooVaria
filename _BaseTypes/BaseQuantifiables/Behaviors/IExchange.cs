namespace CsabaDu.FooVaria.BaseTypes.BaseQuantifiables.Behaviors;

public interface IExchange<TSelf, in TEnum>
    where TSelf : class, IBaseQuantifiable
    where TEnum : Enum
{
    TSelf? ExchangeTo(TEnum measureUnit);
}
