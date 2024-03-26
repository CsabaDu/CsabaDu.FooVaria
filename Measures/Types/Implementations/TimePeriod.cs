
namespace CsabaDu.FooVaria.Measures.Types.Implementations;

internal sealed class TimePeriod(IMeasureFactory factory, TimePeriodUnit timePeriodUnit, double quantity)
    : Measure<ITimePeriod, double, TimePeriodUnit>(factory, timePeriodUnit, quantity), ITimePeriod
{
    #region Public methods
    public int CompareTo(TimeSpan other)
    {
        ITimePeriod converted = ConvertFrom(other);

        return CompareTo(converted);
    }

    public ITimePeriod ConvertFrom(TimeSpan timeSpan)
    {
        double quantity = timeSpan.Ticks / TimeSpan.TicksPerMinute;

        return GetMeasure(default, quantity);
    }

    public TimeSpan ConvertMeasure()
    {
        object minutes = GetDefaultQuantity().ToQuantity(TypeCode.Int64) ?? throw new InvalidOperationException(null);
        long ticks = (long)minutes * TimeSpan.TicksPerMinute;

        return new TimeSpan(ticks);
    }

    public bool Equals(TimeSpan other)
    {
        ITimePeriod converted = ConvertFrom(other);

        return Equals(converted);
    }

    public decimal ProportionalTo(TimeSpan other)
    {
        ITimePeriod converted = ConvertFrom(other);

        return ProportionalTo(converted);
    }
    #endregion
}
