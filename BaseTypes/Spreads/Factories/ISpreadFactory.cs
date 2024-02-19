namespace CsabaDu.FooVaria.BaseTypes.Spreads.Factories;

public interface ISpreadFactory : IQuantifiableFactory
{
    ISpread CreateSpread(ISpreadMeasure spreadMeasure);
}