using CsabaDu.FooVaria.Spreads.Factories;

namespace CsabaDu.FooVaria.Spreads.Types.Implementations;

internal sealed class Surface : Spread<IArea, AreaUnit>, ISurface
{
    public Surface(ISurface other) : base(other)
    {
    }

    public Surface(ISurfaceFactory factory, IArea area) : base(factory, area)
    {
    }

    public override ISurfaceFactory GetFactory()
    {
        return (ISurfaceFactory)Factory;
    }

    public override ISurface GetSpread(IArea area)
    {
        return GetFactory().Create(area);
    }

    public override ISurface GetSpread(AreaUnit measureUnit)
    {
        return (ISurface?)ExchangeTo(measureUnit) ?? throw InvalidMeasureUnitEnumArgumentException(measureUnit);
    }

    public override ISurface GetSpread(ISpreadMeasure spreadMeasure)
    {
        if (NullChecked(spreadMeasure, nameof(spreadMeasure)) is IArea area) return GetSpread(area);

        throw new ArgumentOutOfRangeException(nameof(spreadMeasure), spreadMeasure.GetType().Name, null);
    }

    public override ISurface GetSpread(ISpread other)
    {
        if (NullChecked(other, nameof(other)) is ISurface surface) return GetFactory().Create(surface);

        throw new ArgumentOutOfRangeException(nameof(other), other.GetType().Name, null);
    }

    public override ISurface GetSpread(params IExtent[] shapeExtents)
    {
        return GetFactory().Create(shapeExtents);
    }

    public ISurface GetSurface(IExtent radius)
    {
        return GetSpread(radius);
    }

    public ISurface GetSurface(IExtent length, IExtent width)
    {
        return GetSpread(length, width);
    }

    public override void Validate(IFactory? factory)
    {
        Validate(this, factory);
    }
}
