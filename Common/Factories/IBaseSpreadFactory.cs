using CsabaDu.FooVaria.Common.Types;

namespace CsabaDu.FooVaria.Common.Factories;

public interface IBaseSpreadFactory : IFactory
{
    IBaseSpread Create(ISpreadMeasure spreadMeasure);
    IFactory GetMeasureFactory();
}