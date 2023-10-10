namespace CsabaDu.FooVaria.Spreads.Types;

public interface ISurface : ISpread<IArea, AreaUnit>
{
    ISurface GetSurface(IExtent radius);
    ISurface GetSurface(IExtent length, IExtent width);
}
