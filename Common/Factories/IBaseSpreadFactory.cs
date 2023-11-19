namespace CsabaDu.FooVaria.Common.Factories;

public interface IBaseSpreadFactory : IMeasurableFactory
{
    IBaseSpread Create(ISpreadMeasure spreadMeasure);
}