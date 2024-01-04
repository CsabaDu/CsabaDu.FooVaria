using CsabaDu.FooVaria.RateComponents.Factories;

namespace CsabaDu.FooVaria.Masses.Factories
{
    public interface IMassFactory : IQuantifiableFactory
    {
        IBodyFactory BodyFactory { get; init; }
        IProportionFactory ProportionFactory { get; init; }

        IMass Create(IWeight weight, IBody body);
        IProportion<WeightUnit, VolumeUnit> CreateDensity(IWeight weight, IBody body);
        IMeasureFactory GetMeasureFactory();
        IBodyFactory GetBodyFactory();
    }
}
