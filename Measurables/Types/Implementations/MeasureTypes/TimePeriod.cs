namespace CsabaDu.FooVaria.Measurables.Types.Implementations.MeasureTypes;

internal sealed class TimePeriod : Measure, ITimePeriod
{
    #region Constructors
    internal TimePeriod(ITimePeriod other) : base(other)
    {
    }

    internal TimePeriod(IMeasureFactory measureFactory, ValueType quantity, IMeasurement measurement) : base(measureFactory, quantity, measurement)
    {
    }
    #endregion

    #region Public methods
    public ITimePeriod ConvertFrom(TimeSpan timeSpan)
    {
        long quantity = timeSpan.Ticks / TimeSpan.TicksPerMinute;

        return GetMeasure(quantity, default(TimePeriodUnit));
    }

    public TimeSpan ConvertMeasure()
    {
        long ticks = (long)DefaultQuantity.ToQuantity(TypeCode.Int64)! * TimeSpan.TicksPerMinute;

        return new TimeSpan(ticks);
    }

    public override ITimePeriod GetMeasure(IBaseMeasure baseMeasure)
    {
        return GetMeasure(this, baseMeasure);
    }

    public ITimePeriod GetMeasure(double quantity, TimePeriodUnit measureUnit)
    {
        return GetMeasure(this, quantity, measureUnit);
    }

    public ITimePeriod GetMeasure(double quantity, string name)
    {
        return GetMeasure(this, quantity, name);
    }

    public ITimePeriod GetMeasure(double quantity, IMeasurement measurement)
    {
        return GetMeasure(this, quantity, measurement);
    }

    public ITimePeriod GetMeasure(ITimePeriod other)
    {
        return GetMeasure(this as ITimePeriod, other);
    }

    public ITimePeriod GetMeasure(double quantity)
    {
        return GetMeasure(this, quantity);
    }
    #endregion
}
