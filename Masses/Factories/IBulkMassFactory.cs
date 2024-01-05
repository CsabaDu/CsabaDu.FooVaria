namespace CsabaDu.FooVaria.Masses.Factories;

public interface IBulkMassFactory : IMassFactory, IFactory<IBulkMass>
{
    IBulkMass Create(IWeight weight, IVolume volume);
}
