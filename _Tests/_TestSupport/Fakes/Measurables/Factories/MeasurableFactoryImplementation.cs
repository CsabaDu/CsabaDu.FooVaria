using CsabaDu.FooVaria.Measurables.Enums;
using CsabaDu.FooVaria.Measurables.Factories.Implementations;

namespace CsabaDu.FooVaria.Tests.TestSupport.Fakes.Measurables.Factories
{
    internal sealed class MeasurableFactoryImplementation : IMeasurableFactory
    {
        public IMeasurable Create(IMeasurable other)
        {
            throw new NotImplementedException();
        }
    }

    internal sealed class BaseMeasureFactoryImplementation : BaseMeasureFactory
    {
        public BaseMeasureFactoryImplementation(IMeasurementFactory measurementFactory) : base(measurementFactory)
        {
        }

        public override RateComponentCode RateComponentCode => throw new NotImplementedException();

        public override object DefaultRateComponentQuantity => throw new NotImplementedException();

        public override IMeasurable Create(IMeasurable other)
        {
            throw new NotImplementedException();
        }
    }

    internal sealed class BaseMeasurementFactoryImplementation : BaseMeasurementFactory
    {
        public override IMeasurable Create(IMeasurable other)
        {
            throw new NotImplementedException();
        }
    }
}

