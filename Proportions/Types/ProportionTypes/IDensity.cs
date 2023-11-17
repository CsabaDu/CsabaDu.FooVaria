using CsabaDu.FooVaria.Measurables.Types.MeasureTypes;

namespace CsabaDu.FooVaria.Proportions.Types.ProportionTypes;

public interface IDensity : IProportion<IDensity, WeightUnit, VolumeUnit>, IMeasureProportion<IDensity, IWeight, IVolume>
{
}