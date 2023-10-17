using CsabaDu.FooVaria.Common.Behaviors;

namespace CsabaDu.FooVaria.Tests.TestSupport.Fakes.Common.Factories;

internal sealed class BaseSpreadFactoryImplementation : IBaseSpreadFactory
{
    public IBaseSpread Create(ISpreadMeasure spreadMeasure)
    {
        throw new NotImplementedException();
    }

    public IFactory GetMeasureFactory()
    {
        throw new NotImplementedException();
    }
}