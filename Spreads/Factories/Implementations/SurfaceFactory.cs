using CsabaDu.FooVaria.Measurables.Factories;
using CsabaDu.FooVaria.Spreads.Statics;
using CsabaDu.FooVaria.Spreads.Types.Implementations;

namespace CsabaDu.FooVaria.Spreads.Factories.Implementations;

public sealed class SurfaceFactory : SpreadFactory<ISurface, IArea>, ISurfaceFactory
{
    public SurfaceFactory(IMeasureFactory measureFactory) : base(measureFactory)
    {
    }

    public override ISurface Create(IArea area)
    {
        return new Surface(this, area);
    }

    public override ISurface Create(ISurface other)
    {
        return new Surface(other);
    }

    public override ISurface Create(params IExtent[] shapeExtents)
    {
        IArea area = SpreadMeasures.GetArea(MeasureFactory, shapeExtents);

        return Create(area);
    }
}

