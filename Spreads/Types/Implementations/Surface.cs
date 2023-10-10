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
        throw new NotImplementedException();
    }

    public override ISurface GetSpread(AreaUnit areaUnit)
    {
        throw new NotImplementedException();
    }

    public override ISurface GetSpread(ISpreadMeasure spreadMeasure)
    {
        throw new NotImplementedException();
    }

    public override ISurface GetSpread(ISpread other)
    {
        throw new NotImplementedException();
    }

    public override void Validate(IFactory? factory)
    {
        Validate(this, factory);
    }
}
