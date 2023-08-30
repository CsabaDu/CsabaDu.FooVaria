namespace CsabaDu.FooVaria.Measurables.Types.Implementations.MeasureTypes
{
    internal sealed class Extent : Measure, IExtent
    {
        internal Extent(IExtent extent) : base(extent)
        {
        }

        internal Extent(IMeasureFactory measureFactory, double quantity, Enum measureUnit) : base(measureFactory, quantity, measureUnit)
        {
        }

        internal Extent(IMeasureFactory measureFactory, double quantity, IMeasurement measurement) : base(measureFactory, quantity, measurement)
        {
        }

        public IExtent ConvertFrom(IDistance distance)
        {
            return NullChecked(distance, nameof(distance)).ConvertMeasure();
        }

        public IDistance ConvertMeasure()
        {
            decimal quantity = DefaultQuantity / DistancePerExtent;

            return (IDistance)GetMeasure(quantity, default(DistanceUnit));
        }

        public IExtent GetExtent(double quantity, ExtentUnit extentUnit)
        {
            return (IExtent)GetMeasure(quantity, extentUnit);
        }

        public IExtent GetExtent(ValueType quantity, string name)
        {
            return (IExtent)GetMeasure(quantity, name);
        }

        public IExtent GetExtent(ValueType quantity, IMeasurement? measurement = null)
        {
            return (IExtent)GetMeasure(quantity, measurement);
        }

        public IExtent GetExtent(IBaseMeasure baseMeasure)
        {
            return (IExtent)GetMeasure(baseMeasure);
        }

        public IExtent GetExtent(IExtent? other = null)
        {
            return (IExtent)GetMeasure(other);
        }
    }
}
