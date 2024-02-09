namespace CsabaDu.FooVaria.BulkSpreads.Types;

public interface IBulkBody : IBulkSpread<IBulkBody, IVolume, VolumeUnit>, IBody
{
    IBulkBody GetBulkBody(IExtent radius, IExtent height);
    IBulkBody GetBulkBody(IExtent length, IExtent width, IExtent height);
}
