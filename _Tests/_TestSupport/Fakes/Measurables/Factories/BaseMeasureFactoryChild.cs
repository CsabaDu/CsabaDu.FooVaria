using CsabaDu.FooVaria.Measurables.Enums;
using CsabaDu.FooVaria.Measurables.Factories.Implementations;

namespace CsabaDu.FooVaria.Tests.TestSupport.Fakes.Measurables.Factories;

internal sealed class BaseMeasureFactoryChild : BaseMeasureFactory
{
    public BaseMeasureFactoryChild(IMeasurementFactory measurementFactory) : base(measurementFactory)
    {
    }

    public override RateComponentCode RateComponentCode => throw new NotImplementedException();

    public override IMeasurable Create(IMeasurable other)
    {
        throw new NotImplementedException();
    }
}

