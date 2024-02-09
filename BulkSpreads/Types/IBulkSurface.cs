namespace CsabaDu.FooVaria.BulkSpreads.Types;

public interface IBulkSurface : IBulkSpread<IBulkSurface, IArea, AreaUnit>, ISurface
{
    IBulkSurface GetBulkSurface(IExtent radius);
    IBulkSurface GetBulkSurface(IExtent length, IExtent width);
}
