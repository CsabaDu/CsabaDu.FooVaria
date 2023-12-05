namespace CsabaDu.FooVaria.Common.Factories;

public interface IBaseSpreadFactory : IQuantifiableFactory
{
    IBaseSpread Create(ISpreadMeasure spreadMeasure);
}