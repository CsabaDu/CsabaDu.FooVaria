namespace CsabaDu.FooVaria.Spreads.Types;

public interface IBulkBody : ISpread<IBulkBody, IVolume, VolumeUnit>, IBody
{
    IBulkBody GetBulkBody(IExtent radius, IExtent height);
    IBulkBody GetBulkBody(IExtent length, IExtent width, IExtent height);
}
