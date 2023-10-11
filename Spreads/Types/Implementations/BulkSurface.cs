namespace CsabaDu.FooVaria.Spreads.Types.Implementations;

internal sealed class BulkSurface : Spread<IArea, AreaUnit>, IBulkSurface
{
    public BulkSurface(IBulkSurface other) : base(other)
    {
    }

    public BulkSurface(IBulkSurfaceFactory factory, IArea area) : base(factory, area)
    {
    }

    public override IBulkSurfaceFactory GetFactory()
    {
        return (IBulkSurfaceFactory)Factory;
    }

    public override IBulkSurface GetSpread(IArea area)
    {
        return GetFactory().Create(area);
    }

    public override IBulkSurface GetSpread(AreaUnit measureUnit)
    {
        return (IBulkSurface?)ExchangeTo(measureUnit) ?? throw InvalidMeasureUnitEnumArgumentException(measureUnit);
    }

    public override IBulkSurface GetSpread(ISpreadMeasure spreadMeasure)
    {
        if (NullChecked(spreadMeasure, nameof(spreadMeasure)) is IArea area) return GetSpread(area);

        throw ArgumentTypeOutOfRangeException(nameof(spreadMeasure), spreadMeasure);
    }

    public ISurface GetBaseSpread(IBaseSpread other)
    {
        if (NullChecked(other, nameof(other)) is ISurface surface) return GetSpread(surface.GetSpreadMeasure());

        throw ArgumentTypeOutOfRangeException(nameof(other), other);
    }

    public override IBulkSurface GetSpread(params IExtent[] shapeExtents)
    {
        return GetFactory().Create(shapeExtents);
    }

    public IBulkSurface GetBulkSurface(IExtent radius)
    {
        return GetSpread(radius);
    }

    public IBulkSurface GetBulkSurface(IExtent length, IExtent width)
    {
        return GetSpread(length, width);
    }
}
