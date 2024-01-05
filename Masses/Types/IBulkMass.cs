namespace CsabaDu.FooVaria.Masses.Types;

public interface IBulkMass : IMass, ICommonBase<IBulkMass>
{
    IBulkBody BulkBody { get; init; }

    IBulkMass GetBulkMass(IWeight weight, IBody body);
    IBulkMass GetBulkMass(IWeight weight, IVolume volume);
}
