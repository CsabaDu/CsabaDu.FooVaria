
namespace CsabaDu.FooVaria.Measures.Types.Implementations;

/// <summary>
/// Represents a time period measure.
/// </summary>
/// <param name="factory">The measure factory.</param>
/// <param name="timePeriodUnit">The measure unit of the time period.</param>
/// <param name="quantity">The quantity of the time period.</param>
internal sealed class TimePeriod(IMeasureFactory factory, TimePeriodUnit timePeriodUnit, double quantity)
    : Measure<ITimePeriod, double, TimePeriodUnit>(factory, timePeriodUnit, quantity), ITimePeriod
{
    #region Public methods

    /// <summary>
    /// Compares the current TimePeriod instance to a specified TimeSpan object.
    /// </summary>
    /// <param name="other">The TimeSpan object to compare to.</param>
    /// <returns>An integer that indicates the relative order of the objects being compared.</returns>
    public int CompareTo(TimeSpan other)
    {
        ITimePeriod converted = ConvertFrom(other);

        return CompareTo(converted);
    }

    /// <summary>
    /// Converts a TimeSpan object to an ITimePeriod instance.
    /// </summary>
    /// <param name="timeSpan">The TimeSpan object to convert.</param>
    /// <returns>An ITimePeriod instance that represents the converted TimeSpan.</returns>
    public ITimePeriod ConvertFrom(TimeSpan timeSpan)
    {
        double quantity = timeSpan.Ticks / TimeSpan.TicksPerMinute;

        return GetMeasure(default, quantity);
    }

    /// <summary>
    /// Converts the current TimePeriod instance to a TimeSpan object.
    /// </summary>
    /// <returns>A TimeSpan object that represents the current TimePeriod instance.</returns>
    public TimeSpan ConvertMeasure()
    {
        object minutes = GetDefaultQuantity().ToQuantity(TypeCode.Int64) ?? throw new InvalidOperationException(null);
        long ticks = (long)minutes * TimeSpan.TicksPerMinute;

        return new TimeSpan(ticks);
    }

    /// <summary>
    /// Determines whether the current TimePeriod instance is equal to a specified TimeSpan object.
    /// </summary>
    /// <param name="other">The TimeSpan object to compare to.</param>
    /// <returns>true if the current instance is equal to the specified TimeSpan; otherwise, false.</returns>
    public bool Equals(TimeSpan other)
    {
        ITimePeriod converted = ConvertFrom(other);

        return Equals(converted);
    }

    /// <summary>
    /// Calculates the proportional value of the current TimePeriod instance relative to a specified TimeSpan object.
    /// </summary>
    /// <param name="other">The TimeSpan object to compare to.</param>
    /// <returns>A decimal value that represents the proportional value.</returns>
    public decimal ProportionalTo(TimeSpan other)
    {
        ITimePeriod converted = ConvertFrom(other);

        return ProportionalTo(converted);
    }
    #endregion
}
