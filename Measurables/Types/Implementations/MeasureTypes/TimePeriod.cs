﻿namespace CsabaDu.FooVaria.Measurables.Types.Implementations.MeasureTypes;

internal sealed class TimePeriod : Measure, ITimePeriod
{
    #region Constructors
    internal TimePeriod(ITimePeriod timePeriod) : base(timePeriod)
    {
    }

    internal TimePeriod(IMeasureFactory measureFactory, double quantity, Enum measureUnit) : base(measureFactory, quantity, measureUnit)
    {
    }

    internal TimePeriod(IMeasureFactory measureFactory, double quantity, IMeasurement measurement) : base(measureFactory, quantity, measurement)
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
        ValidateBaseMeasure(baseMeasure);

        return (ITimePeriod)base.GetMeasure(baseMeasure);
    }

    public ITimePeriod GetMeasure(double quantity, TimePeriodUnit measureUnit)
    {
        return (ITimePeriod)base.GetMeasure(quantity, measureUnit);
    }

    public ITimePeriod GetMeasure(double quantity, string name)
    {
        return (ITimePeriod)base.GetMeasure(quantity, name);
    }

    public ITimePeriod GetMeasure(double quantity, IMeasurement? measurement = null)
    {
        return (ITimePeriod)base.GetMeasure(quantity, measurement);
    }

    public ITimePeriod GetMeasure(ITimePeriod? other = null)
    {
        return (ITimePeriod)base.GetMeasure(other);
    }

    public override Enum GetMeasureUnit()
    {
        return (TimePeriodUnit)Measurement.MeasureUnit;
    }

    public double GetQuantity()
    {
        return (double)Quantity;
    }
    #endregion
}
