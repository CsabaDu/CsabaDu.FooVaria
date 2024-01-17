using CsabaDu.FooVaria.BaseMeasures.Factories;
using CsabaDu.FooVaria.BaseMeasures.Types;

namespace CsabaDu.FooVaria.RateComponents.Factories;

public interface IDenominatorFactory : IRateComponentFactory<IDenominator, decimal>, IBaseMeasureFactory<IDenominator>, IDefaultBaseMeasureFactory<IDenominator>
{
    IDenominator Create(Enum measureUnit);
    IDenominator Create(string name);
    IDenominator Create(IMeasurement measurement);
    IDenominator Create(IBaseMeasure baseMeasure, ValueType quantity);
}
