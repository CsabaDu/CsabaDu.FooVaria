namespace CsabaDu.FooVaria.RateComponents.Behaviors
{
    public interface ICustomMeasure
    {
    }

    public interface ICustomMeasure<TSelf, TNum, TEnum> : ICustomMeasure where TSelf : class, IBaseMeasure where TNum : struct where TEnum : struct, Enum
    {
        TSelf GetNextCustomMeasure(TNum quantity, string customName, decimal exchangeRate);
        TSelf GetCustomMeasure(TNum quantity, TEnum measureUnit, decimal exchangeRate, string customName);
    }
}
