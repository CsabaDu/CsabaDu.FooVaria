namespace CsabaDu.FooVaria.Measures.Types.Implementations;

internal sealed class TimePeriod : Measure<ITimePeriod, double, TimePeriodUnit>, ITimePeriod
{
    #region Constructors
    internal TimePeriod(IMeasureFactory factory, TimePeriodUnit timePeriodUnit, ValueType quantity) : base(factory, timePeriodUnit, quantity)
    {
    }
    #endregion

    #region Public methods
    public ITimePeriod ConvertFrom(TimeSpan timeSpan)
    {
        double quantity = timeSpan.Ticks / TimeSpan.TicksPerMinute;

        return GetMeasure(default, quantity);
    }

    public TimeSpan ConvertMeasure()
    {
        object quantity = GetDefaultQuantity().ToQuantity(TypeCode.Int64) ?? throw new InvalidOperationException(null);
        long ticks = (long)quantity * TimeSpan.TicksPerMinute;

        return new TimeSpan(ticks);
    }
    #endregion
}
