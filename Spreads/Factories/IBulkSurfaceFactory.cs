namespace CsabaDu.FooVaria.Spreads.Factories;

public interface IBulkSurfaceFactory : ISpreadFactory<IBulkSurface, IArea>
{
    IBulkSurface Create(ISurface surface);
}
