namespace CsabaDu.FooVaria.Masses.Factories;

public interface IMassFactory : IQuantifiableFactory, IBaseSpreadFactory
{
    IBodyFactory BodyFactory { get; init; }
    IProportionFactory ProportionFactory { get; init; }

    IMass Create(IWeight weight, IBody body);
    IProportion<WeightUnit, VolumeUnit> CreateDensity(IMass mass);
    IMeasureFactory GetMeasureFactory();
    IBodyFactory GetBodyFactory();
}
