namespace CsabaDu.FooVaria.Masses.Factories;

public interface IDryMassFactory : IMassFactory, IFactory<IDryMass>
{
    IDryMass Create(IWeight weight, IDryBody dryBody);
    IDryMass Create(IWeight weight, IPlaneShape baseFace, IExtent height);
    IDryMass Create(IWeight weight, params IExtent[] shapeExtents);
}
