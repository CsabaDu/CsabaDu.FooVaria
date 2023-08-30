namespace CsabaDu.FooVaria.Measurables.Types.Implementations.MeasureTypes
{
    internal sealed class Area : Measure, IArea
    {
        internal Area(IArea area) : base(area)
        {
        }

        internal Area(IMeasureFactory measureFactory, double quantity, Enum measureUnit) : base(measureFactory, quantity, measureUnit)
        {
        }

        internal Area(IMeasureFactory measureFactory, double quantity, IMeasurement measurement) : base(measureFactory, quantity, measurement)
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

        public IArea GetArea(ValueType quantity, IMeasurement? measurement = null)
        {
            return (IArea)GetMeasure(quantity, measurement);
        }

        public IArea GetArea(IBaseMeasure baseMeasure)
        {
            return (IArea)GetMeasure(baseMeasure);
        }

        public IArea GetArea(IArea? other = null)
        {
            return (IArea)GetMeasure(other);
        }

        public ISpreadMeasure GetSpreadMeasure()
        {
            return GetArea();
        }
    }

    internal sealed class Volume : Measure, IVolume
    {
        internal Volume(IVolume volume) : base(volume)
        {
        }

        internal Volume(IMeasureFactory measureFactory, ValueType quantity, Enum measureUnit) : base(measureFactory, quantity, measureUnit)
        {
        }

        internal Volume(IMeasureFactory measureFactory, ValueType quantity, IMeasurement measurement) : base(measureFactory, quantity, measurement)
        {
        }

        public ISpreadMeasure GetSpreadMeasure()
        {
            throw new NotImplementedException();
        }

        public IVolume GetVolume(double quantity, VolumeUnit volumeUnit)
        {
            throw new NotImplementedException();
        }

        public IVolume GetVolume(ValueType quantity, string name)
        {
            throw new NotImplementedException();
        }

        public IVolume GetVolume(ValueType quantity, IMeasurement? measurement = null)
        {
            throw new NotImplementedException();
        }

        public IVolume GetVolume(IBaseMeasure baseMeasure)
        {
            throw new NotImplementedException();
        }

        public IVolume GetVolume(IVolume? other = null)
        {
            throw new NotImplementedException();
        }

        public IWeight ToVolumetricWeight(decimal ratio, WeightUnit weightUnit = WeightUnit.g)
        {
            throw new NotImplementedException();
        }
    }

    internal sealed class Weight : Measure, IWeight
    {
        internal Weight(IWeight weight) : base(weight)
        {
        }

        internal Weight(IMeasureFactory measureFactory, ValueType quantity, Enum measureUnit) : base(measureFactory, quantity, measureUnit)
        {
        }

        internal Weight(IMeasureFactory measureFactory, ValueType quantity, IMeasurement measurement) : base(measureFactory, quantity, measurement)
        {
        }

        public IWeight GetVolumetricWeight(IVolume volume, decimal ratio, bool isGreater = true)
        {
            throw new NotImplementedException();
        }

        public IWeight GetWeight(double quantity, WeightUnit weightUnit)
        {
            throw new NotImplementedException();
        }

        public IWeight GetWeight(ValueType quantity, string name)
        {
            throw new NotImplementedException();
        }

        public IWeight GetWeight(ValueType quantity, IMeasurement? measurement = null)
        {
            throw new NotImplementedException();
        }

        public IWeight GetWeight(IBaseMeasure baseMeasure)
        {
            throw new NotImplementedException();
        }

        public IWeight GetWeight(IWeight? other = null)
        {
            throw new NotImplementedException();
        }
    }
}
