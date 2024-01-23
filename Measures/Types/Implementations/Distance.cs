﻿namespace CsabaDu.FooVaria.Measures.Types.Implementations;

internal sealed class Distance : Measure<IDistance, double, DistanceUnit>, IDistance
{
    #region Constructors
    internal Distance(IMeasureFactory factory, DistanceUnit distanceUnit, double quantity) : base(factory, distanceUnit, quantity)
    {
    }
    #endregion

    #region Public methods
    public IDistance ConvertFrom(IExtent extent)
    {
        return NullChecked(extent, nameof(extent)).ConvertMeasure();
    }

    public IExtent ConvertMeasure()
    {
        return ConvertMeasure<IExtent>(MeasureOperationMode.Multiply);
    }
    #endregion
}
