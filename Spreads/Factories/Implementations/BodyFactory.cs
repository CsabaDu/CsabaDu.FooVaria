using CsabaDu.FooVaria.Measurables.Factories;
using CsabaDu.FooVaria.Spreads.Statics;
using CsabaDu.FooVaria.Spreads.Types.Implementations;

namespace CsabaDu.FooVaria.Spreads.Factories.Implementations;

public sealed class BodyFactory : SpreadFactory<IBody, IVolume>, IBodyFactory
{
    public BodyFactory(IMeasureFactory measureFactory) : base(measureFactory)
    {
    }

    public override IBody Create(IVolume volume)
    {
        return new Body(this, volume);
    }

    public override IBody Create(IBody other)
    {
        return new Body(other);
    }

    public override IBody Create(params IExtent[] shapeExtents)
    {
        IVolume volume = SpreadMeasures.GetVolume(MeasureFactory, shapeExtents);

        return Create(volume);
    }
}

