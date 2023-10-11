using CsabaDu.FooVaria.Measurables.Factories;
using CsabaDu.FooVaria.Spreads.Statics;
using CsabaDu.FooVaria.Spreads.Types.Implementations;

namespace CsabaDu.FooVaria.Spreads.Factories.Implementations;

public sealed class BulkSurfaceFactory : SpreadFactory<IBulkSurface, IArea>, IBulkSurfaceFactory
{
    public BulkSurfaceFactory(IMeasureFactory measureFactory) : base(measureFactory)
    {
    }

    public override IBulkSurface Create(IArea area)
    {
        return new BulkSurface(this, area);
    }

    public override IBulkSurface Create(IBulkSurface other)
    {
        return new BulkSurface(other);
    }

    public override IBulkSurface Create(params IExtent[] shapeExtents)
    {
        IArea area = SpreadMeasures.GetArea(MeasureFactory, shapeExtents);

        return Create(area);
    }
}

