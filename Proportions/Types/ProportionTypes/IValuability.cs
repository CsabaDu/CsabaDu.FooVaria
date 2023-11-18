using CsabaDu.FooVaria.RateComponents.Types.MeasureTypes;

namespace CsabaDu.FooVaria.Proportions.Types.ProportionTypes;

public interface IValuability : IProportion<IValuability, Currency, WeightUnit>, IMeasureProportion<IValuability, ICash, IWeight>
{
}