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

    public IBody GetBody(IExtent radius, IExtent height)
    {
        return GetSpread(radius, height);
    }

    public IBody GetBody(IExtent length, IExtent width, IExtent height)
    {
        return GetSpread(length, width, height);
    }

    public override IBodyFactory GetFactory()
    {
        return (IBodyFactory)Factory;
    }

    public override IBody GetSpread(IVolume volume)
    {
        return GetFactory().Create(volume);
    }

    public override IBody GetSpread(VolumeUnit measureUnit)
    {
        return (IBody?)ExchangeTo(measureUnit) ?? throw InvalidMeasureUnitEnumArgumentException(measureUnit);
    }

    public override IBody GetSpread(ISpreadMeasure spreadMeasure)
    {
        if (NullChecked(spreadMeasure, nameof(spreadMeasure)) is IVolume volume) return GetFactory().Create(volume);

        throw new ArgumentOutOfRangeException(nameof(spreadMeasure), spreadMeasure.GetType().Name, null);
    }

    public override IBody GetSpread(ISpread other)
    {
        if (NullChecked(other, nameof(other)) is IBody body) return GetSpread(body);

        throw new ArgumentOutOfRangeException(nameof(other), other.GetType().Name, null);
    }

    public override IBody GetSpread(params IExtent[] shapeExtents)
    {
        return GetFactory().Create(shapeExtents);
    }

    //public override void Validate(IFactory? factory)
    //{
    //    Validate(this, factory);
    //}
}
