namespace CsabaDu.FooVaria.Spreads.Types;

public interface IBulkSurface : ISpread<IArea, AreaUnit>, ISurface
{
    IBulkSurface GetBulkSurface(IExtent radius);
    IBulkSurface GetBulkSurface(IExtent length, IExtent width);
}
