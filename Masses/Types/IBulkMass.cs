using CsabaDu.FooVaria.BaseTypes.Common.Behaviors;

namespace CsabaDu.FooVaria.Masses.Types;

public interface IBulkMass : IMass, IDeepCopy<IBulkMass>, IExchange<IBulkMass, VolumeUnit>, IConcrete<IBulkMassFactory>
{
    IBulkBody BulkBody { get; init; }
    //IBulkMassFactory Factory { get; init; }

    IBulkMass GetBulkMass(IWeight weight, IBody body);
    IBulkMass GetBulkMass(IWeight weight, IVolume volume);
    IBulkMass GetBulkMass(IWeight weight, IProportion density);
    IBulkMass GetBulkMass(IVolume volume, IProportion density);
}
