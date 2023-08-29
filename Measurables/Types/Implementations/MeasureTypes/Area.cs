using CsabaDu.FooVaria.Measurables.Types.MeasureTypes;

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
}
