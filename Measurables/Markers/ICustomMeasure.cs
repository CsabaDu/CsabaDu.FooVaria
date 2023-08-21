namespace CsabaDu.FooVaria.Measurables.Markers
{
    public interface ICustomMeasure
    {
    }
    public interface ICustomMeasure<T, U, V> : ICustomMeasure where T : class, IMeasure where U : struct where V : struct, Enum
    {
        T GetNextCustomMeasure(U quantity, decimal exchangeRate, string? customName = null);
        bool TryGetCustomMeasure(U quantity, V measureUnit, decimal exchangeRate, string? customName, [NotNullWhen(true)] out T? customMeasure);

    }
}
