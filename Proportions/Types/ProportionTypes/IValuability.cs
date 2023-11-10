using CsabaDu.FooVaria.Measurables.Types.MeasureTypes;

namespace CsabaDu.FooVaria.Proportions.Types.ProportionTypes;

public interface IValuability : IProportion<IValuability, Currency, WeightUnit>, IMeasuresProportion<IValuability, ICash, IWeight>
{
}