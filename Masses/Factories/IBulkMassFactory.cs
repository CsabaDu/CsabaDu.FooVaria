namespace CsabaDu.FooVaria.Masses.Factories;

public interface IBulkMassFactory : IMassFactory, IDeepCopyFactory<IBulkMass>
{
    IBulkMass Create(IWeight weight, IVolume volume);
}
