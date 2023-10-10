using CsabaDu.FooVaria.Spreads.Factories;

namespace CsabaDu.FooVaria.Spreads.Types.Implementations;

internal sealed class Body : Spread<IVolume, VolumeUnit>, IBody
{
    public Body(IBody other) : base(other)
    {
    }

    public Body(IBodyFactory factory, IVolume volume) : base(factory, volume)
    {
    }

    public override IBodyFactory GetFactory()
    {
        return (IBodyFactory)Factory;
    }

    public override IBody GetSpread(IVolume volume)
    {
        throw new NotImplementedException();
    }

    public override IBody GetSpread(VolumeUnit volumeUnit)
    {
        throw new NotImplementedException();
    }

    public override IBody GetSpread(ISpreadMeasure spreadMeasure)
    {
        throw new NotImplementedException();
    }

    public override IBody GetSpread(ISpread other)
    {
        throw new NotImplementedException();
    }

    public override void Validate(IFactory? factory)
    {
        Validate(this, factory);
    }
}
