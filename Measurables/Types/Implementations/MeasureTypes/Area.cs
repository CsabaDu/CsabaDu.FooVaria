namespace CsabaDu.FooVaria.Measurables.Types.Implementations.MeasureTypes
{
    internal sealed class Area : Measure, IArea
    {
        internal Area(IArea area) : base(area)
        {
        }

        internal Area(IMeasureFactory measureFactory, ValueType quantity, Enum measureUnit) : base(measureFactory, quantity, measureUnit)
        {
        }

        internal Area(IMeasureFactory measureFactory, ValueType quantity, IMeasurement measurement) : base(measureFactory, quantity, measurement)
        {
        }

        public IArea GetArea(ValueType quantity, string name)
        {
            return(IArea)GetMeasure(quantity, name);
        }

        public IArea GetArea(double quantity, AreaUnit areaUnit)
        {
            return (IArea)GetMeasure(quantity, areaUnit);
        }

        public IArea GetArea(ValueType quantity, IMeasurement measurement)
        {
            return (IArea)GetMeasure(quantity, measurement);
        }

        public IArea GetArea(IBaseMeasure baseMeasure)
        {
            return (IArea)GetMeasure(baseMeasure);
        }

        public IArea GetArea(IArea? other = null)
        {
            return (IArea)GetMeasure(other ?? this);
        }

        public ISpreadMeasure GetSpreadMeasure()
        {
            return GetArea();
        }
    }

    internal class TimePeriod : Measure, ITimePeriod
    {
        public TimePeriod(ITimePeriod timePeriod) : base(timePeriod)
        {
        }

        public TimePeriod(IMeasureFactory measureFactory, ValueType quantity, Enum measureUnit) : base(measureFactory, quantity, measureUnit)
        {
        }

        public TimePeriod(IMeasureFactory measureFactory, ValueType quantity, IMeasurement measurement) : base(measureFactory, quantity, measurement)
        {
        }

        public ITimePeriod ConvertFrom(TimeSpan timeSpan)
        {
            throw new NotImplementedException();
        }

        public TimeSpan ConvertMeasure(ITimePeriod timePeriod)
        {
            throw new NotImplementedException();
        }

        public ITimePeriod GetTimePeriod(double quantity, TimePeriodUnit timePeriodUnit)
        {
            throw new NotImplementedException();
        }

        public ITimePeriod GetTimePeriod(ValueType quantity, string name)
        {
            throw new NotImplementedException();
        }

        public ITimePeriod GetTimePeriod(ValueType quantity, IMeasurement measurement)
        {
            throw new NotImplementedException();
        }

        public ITimePeriod GetTimePeriod(IBaseMeasure baseMeasure)
        {
            throw new NotImplementedException();
        }

        public ITimePeriod GetTimePeriod(ITimePeriod? other = null)
        {
            throw new NotImplementedException();
        }
    }

    internal sealed class Distance : Measure, IDistance
    {
        internal Distance(IDistance distance) : base(distance)
        {
        }

        internal Distance(IMeasureFactory measureFactory, ValueType quantity, Enum measureUnit) : base(measureFactory, quantity, measureUnit)
        {
        }

        internal Distance(IMeasureFactory measureFactory, ValueType quantity, IMeasurement measurement) : base(measureFactory, quantity, measurement)
        {
        }
        public IDistance ConvertFrom(IExtent extent)
        {
            throw new NotImplementedException();
        }

        public IExtent ConvertMeasure(IDistance distance)
        {
            throw new NotImplementedException();
        }

        public IDistance GetDistance(double quantity, DistanceUnit distanceUnit)
        {
            return (IDistance)GetMeasure(quantity, distanceUnit);
        }

        public IDistance GetDistance(ValueType quantity, string name)
        {
            return (IDistance)GetMeasure(quantity, name);
        }

        public IDistance GetDistance(ValueType quantity, IMeasurement measurement)
        {
            return (IDistance)GetMeasure(quantity, measurement);
        }

        public IDistance GetDistance(IBaseMeasure baseMeasure)
        {
            return (IDistance)GetMeasure(baseMeasure);
        }

        public IDistance GetDistance(IDistance? other = null)
        {
            return (IDistance)GetMeasure(other ?? this);
        }
    }
}
