namespace CsabaDu.FooVaria.Measurables.Types.Implementations.MeasureTypes
{
    internal sealed class TimePeriod : Measure, ITimePeriod
    {
        internal TimePeriod(ITimePeriod timePeriod) : base(timePeriod)
        {
        }

        internal TimePeriod(IMeasureFactory measureFactory, double quantity, Enum measureUnit) : base(measureFactory, quantity, measureUnit)
        {
        }

        internal TimePeriod(IMeasureFactory measureFactory, double quantity, IMeasurement measurement) : base(measureFactory, quantity, measurement)
        {
        }

        public ITimePeriod ConvertFrom(TimeSpan timeSpan)
        {
            long quantity = timeSpan.Ticks / TimeSpan.TicksPerMinute;

            return (ITimePeriod)GetMeasure(quantity, default(TimePeriodUnit));
        }

        public TimeSpan ConvertMeasure()
        {
            long ticks = (long)DefaultQuantity.ToQuantity(TypeCode.Int64)! * TimeSpan.TicksPerMinute;

            return new TimeSpan(ticks);
        }

        public ITimePeriod GetTimePeriod(double quantity, TimePeriodUnit timePeriodUnit)
        {
            throw new NotImplementedException();
        }

        public ITimePeriod GetTimePeriod(ValueType quantity, string name)
        {
            return (ITimePeriod)GetMeasure(quantity, name);
        }

        public ITimePeriod GetTimePeriod(ValueType quantity, IMeasurement? measurement = null)
        {
            return (ITimePeriod)GetMeasure(quantity, measurement);
        }

        public ITimePeriod GetTimePeriod(IBaseMeasure baseMeasure)
        {
            return (ITimePeriod)GetMeasure(baseMeasure);
        }

        public ITimePeriod GetTimePeriod(ITimePeriod? other = null)
        {
            return (ITimePeriod)GetMeasure(other);
        }
    }
}
