﻿namespace CsabaDu.FooVaria.BulkSpreads.Factories.Implementations;

public sealed class BulkSurfaceFactory(IMeasureFactory measureFactory) : BulkSpreadFactory<IBulkSurface, IArea, AreaUnit>(measureFactory), IBulkSurfaceFactory
{
    #region Public methods
    #region Override methods
    public override IBulkSurface Create(IArea area)
    {
        return new BulkSurface(this, area);
    }

    public override IBulkSurface CreateNew(IBulkSurface other)
    {
        return new BulkSurface(other);
    }

    public override IBulkSurface Create(params IExtent[] shapeExtents)
    {
        IArea area = GetArea(MeasureFactory, shapeExtents);

        return Create(area);
    }

    public override IBulkSurface Create(AreaUnit areaUnit, double quantity)
    {
        IArea area = CreateSpreadMeasure(areaUnit, quantity) ?? throw QuantityArgumentOutOfRangeException(quantity);

        return Create(area);
    }

    public override IBulkSurface? Create(ISpread spread)
    {
        if (spread?.GetSpreadMeasure() is not IArea area) return null;

        return Create(area);
    }
    #endregion
    #endregion
}
