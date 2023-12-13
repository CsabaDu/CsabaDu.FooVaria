namespace CsabaDu.FooVaria.RateComponents.Behaviors
{
    public interface ICustomMeasure
    {
    }

    public interface ICustomMeasure<TSelf, TNum, TEnum> : ICustomMeasure where TSelf : class, IMeasure<TSelf, TNum, TEnum> where TNum : struct where TEnum : struct, Enum
    {
        TSelf? GetNextCustomMeasure(string customName, decimal exchangeRate, TNum quantity);
        TSelf? GetCustomMeasure(TEnum measureUnit, decimal exchangeRate, TNum quantity, string customName);
    }
}
