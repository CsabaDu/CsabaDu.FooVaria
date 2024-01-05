using CsabaDu.FooVaria.RateComponents.Factories;

namespace CsabaDu.FooVaria.Masses.Factories
{
    public interface IMassFactory : IQuantifiableFactory, IBaseSpreadFactory
    {
        IBodyFactory BodyFactory { get; init; }
        IProportionFactory ProportionFactory { get; init; }

        IProportion<WeightUnit, VolumeUnit> CreateDensity(IMass mass);
        IMeasureFactory GetMeasureFactory();
        IBodyFactory GetBodyFactory();
    }

    public interface IBulkMassFactory : IMassFactory, IFactory<IBulkMass>
    {
        IBulkMass Create(IWeight weight, IVolume volume);
        IBulkMass Create(IWeight weight, IBody body);
    }

    public interface IDryMassFactory : IMassFactory, IFactory<IDryMass>
    {
        IDryMass Create(IWeight weight, IDryBody dryBody);
        IDryMass Create(IWeight weight, IPlaneShape baseFace, IExtent height);
        IDryMass Create(IWeight weight, params IExtent[] shapeExtents);
    }
}
