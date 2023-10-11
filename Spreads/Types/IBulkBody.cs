namespace CsabaDu.FooVaria.Spreads.Types;

public interface IBulkBody : ISpread<IVolume, VolumeUnit>, IBody
{
    IBulkBody GetBulkBody(IExtent radius, IExtent height);
    IBulkBody GetBulkBody(IExtent length, IExtent width, IExtent height);
}
