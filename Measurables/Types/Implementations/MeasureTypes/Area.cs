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
        public IDistance ConvertFrom(IExtent other, Enum? measureUnit = null)
        {
            throw new NotImplementedException();
        }

        public IExtent ConvertMeasure(IDistance? convertibleMeasure = null)
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
