namespace CsabaDu.FooVaria.BaseSpreads.Factories;

public interface IBaseSpreadFactory : IQuantifiableFactory
{
    IBaseSpread CreateBaseSpread(ISpreadMeasure spreadMeasure);
}