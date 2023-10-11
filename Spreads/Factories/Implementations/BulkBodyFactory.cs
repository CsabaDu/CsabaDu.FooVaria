using CsabaDu.FooVaria.Measurables.Factories;
using CsabaDu.FooVaria.Spreads.Statics;
using CsabaDu.FooVaria.Spreads.Types.Implementations;

namespace CsabaDu.FooVaria.Spreads.Factories.Implementations;

public sealed class BulkBodyFactory : SpreadFactory<IBulkBody, IVolume>, IBulkBodyFactory
{
    public BulkBodyFactory(IMeasureFactory measureFactory) : base(measureFactory)
    {
    }

    public override IBulkBody Create(IVolume volume)
    {
        return new BulkBody(this, volume);
    }

    public override IBulkBody Create(IBulkBody other)
    {
        return new BulkBody(other);
    }

    public override IBulkBody Create(params IExtent[] shapeExtents)
    {
        IVolume volume = SpreadMeasures.GetVolume(MeasureFactory, shapeExtents);

        return Create(volume);
    }
}

