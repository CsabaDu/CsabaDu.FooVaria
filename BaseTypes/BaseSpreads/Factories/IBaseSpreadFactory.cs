namespace CsabaDu.FooVaria.BaseTypes.BaseSpreads.Factories;

public interface IBaseSpreadFactory : IQuantifiableFactory
{
    IBaseSpread CreateBaseSpread(ISpreadMeasure spreadMeasure);
}