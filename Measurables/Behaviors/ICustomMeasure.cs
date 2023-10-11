namespace CsabaDu.FooVaria.Measurables.Behaviors
{
    public interface ICustomMeasure
    {
    }

    public interface ICustomMeasure<T, U, V> : ICustomMeasure where T : class, IMeasure where U : struct where V : struct, Enum
    {
        T GetNextCustomMeasure(U quantity, string customName, decimal exchangeRate);
        T GetCustomMeasure(U quantity, V measureUnit, decimal exchangeRate, string customName);
    }
}
