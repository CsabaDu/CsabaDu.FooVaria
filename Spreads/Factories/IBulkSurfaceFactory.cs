namespace CsabaDu.FooVaria.Spreads.Factories;

public interface IBulkSurfaceFactory : ISurfaceFactory, ISpreadFactory<IBulkSurface, IArea, AreaUnit>
{
    //IBulkSurface CreateNew(ISurface surface);
}
