using CsabaDu.FooVaria.BaseTypes.Common.Behaviors;

namespace CsabaDu.FooVaria.BulkSpreads.Types;

public interface IBulkBody : IBulkSpread<IBulkBody, IVolume, VolumeUnit>, IBody, IGetFactory<IBulkBodyFactory>
{
    //IBulkBodyFactory Factory { get; init; }

    IBulkBody GetBulkBody(IExtent radius, IExtent height);
    IBulkBody GetBulkBody(IExtent length, IExtent width, IExtent height);
}
