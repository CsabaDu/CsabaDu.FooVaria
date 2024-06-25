using CsabaDu.FooVaria.BaseTypes.Common.Behaviors;

namespace CsabaDu.FooVaria.BulkSpreads.Types;

public interface IBulkSurface : IBulkSpread<IBulkSurface, IArea, AreaUnit>, ISurface, IGetFactory<IBulkSurfaceFactory>
{
    //IBulkSurfaceFactory Factory { get; init; }

    IBulkSurface GetBulkSurface(IExtent radius);
    IBulkSurface GetBulkSurface(IExtent length, IExtent width);
}
