﻿namespace CsabaDu.FooVaria.Measurements.Types.Implementations;

internal sealed class ConstantMeasurement : Measurement, IConstantMeasurement
{
    #region Constructors
    internal ConstantMeasurement(IMeasurementFactory factory, Enum measureUnit) : base(factory, measureUnit)
    {
    }
    #endregion

    #region Public methods
    public override IEnumerable<MeasureUnitCode> GetMeasureUnitCodes()
    {
        return base.GetMeasureUnitCodes().Where(x => !x.IsCustomMeasureUnitCode());
    }
    #endregion
}