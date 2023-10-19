using CsabaDu.FooVaria.Common.Types;

namespace CsabaDu.FooVaria.Common.Factories;

public interface IBaseSpreadFactory : IBaseMeasurableFactory
{
    IBaseSpread Create(ISpreadMeasure spreadMeasure);
    //IBaseMeasurableFactory GetMeasureFactory();
}