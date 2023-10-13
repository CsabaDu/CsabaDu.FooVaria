namespace CsabaDu.FooVaria.Tests.TestSupport.Fakes.Measurables.Types
{
    internal sealed class MeasurableChild : Measurable
    {
        public MeasurableChild(IMeasurable other) : base(other)
        {
        }

        public MeasurableChild(IMeasurableFactory factory, MeasureUnitTypeCode measureUnitTypeCode) : base(factory, measureUnitTypeCode)
        {
        }

        public MeasurableChild(IMeasurableFactory factory, Enum measureUnit) : base(factory, measureUnit)
        {
        }

        public MeasurableChild(IMeasurableFactory factory, IBaseMeasurable baseMeasurable) : base(factory, baseMeasurable)
        {
        }

        public override IMeasurable GetDefault()
        {
            throw new NotImplementedException();
        }

        public override IMeasurable GetMeasurable(IMeasurable other)
        {
            throw new NotImplementedException();
        }

        public override Enum GetMeasureUnit()
        {
            throw new NotImplementedException();
        }
    }

    internal sealed class BaseMeasureChild : BaseMeasure
    {
        public BaseMeasureChild(IBaseMeasureFactory factory, MeasureUnitTypeCode measureUnitTypeCode) : base(factory, measureUnitTypeCode)
        {
        }

        public BaseMeasureChild(IBaseMeasureFactory factory, ValueType quantity, Enum measureUnit) : base(factory, quantity, measureUnit)
        {
        }

        public BaseMeasureChild(IBaseMeasureFactory factory, ValueType quantity, IMeasurement measurement) : base(factory, quantity, measurement)
        {
        }

        public override IBaseMeasure GetBaseMeasure(ValueType quantity, Enum measureUnit)
        {
            throw new NotImplementedException();
        }

        public override IMeasurable GetDefault()
        {
            throw new NotImplementedException();
        }

        public override LimitMode? GetLimitMode()
        {
            throw new NotImplementedException();
        }

        public override IMeasurable GetMeasurable(IMeasurable other)
        {
            throw new NotImplementedException();
        }
    }
}

