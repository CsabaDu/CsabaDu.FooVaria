using CsabaDu.FooVaria.BaseTypes.Common.Behaviors;

namespace CsabaDu.FooVaria.Masses.Factories;

public interface IBulkMassFactory : IMassFactory, IDeepCopyFactory<IBulkMass>, IConcreteFactory
{
    IBulkMass Create(IWeight weight, IVolume volume);
}
