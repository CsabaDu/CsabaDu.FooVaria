﻿using CsabaDu.FooVaria.RateComponents.Factories;
using CsabaDu.FooVaria.Spreads.Types.Implementations;

namespace CsabaDu.FooVaria.Spreads.Factories.Implementations;

public sealed class BulkSurfaceFactory : SpreadFactory<IBulkSurface, IArea>, IBulkSurfaceFactory
{
    #region Constructors
    public BulkSurfaceFactory(IMeasureFactory measureFactory) : base(measureFactory)
    {
    }
    #endregion

    #region Public methods
    #region Override methods
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

    public ISurface Create(ISurface surface)
    {
        IArea area = (IArea)NullChecked(surface, nameof(surface)).GetSpreadMeasure();

        return Create(area);
    }

    public IBulkSurface Create(AreaUnit areaUnit, double quantity)
    {
        IArea area = (IArea)MeasureFactory.Create(areaUnit, quantity);

        return Create(area);
    }
    #endregion
    #endregion
}

