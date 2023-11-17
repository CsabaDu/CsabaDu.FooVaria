namespace CsabaDu.FooVaria.Measurements.Behaviors
{
    public interface ICustomMeasure
    {
    }

    public interface ICustomMeasure<T, U, W> : ICustomMeasure where T : class, IBaseMeasureTemp where U : struct where W : struct, Enum
    {
        T GetNextCustomMeasure(U quantity, string customName, decimal exchangeRate);
        T GetCustomMeasure(U quantity, W measureUnit, decimal exchangeRate, string customName);
    }
}
