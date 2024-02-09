namespace CsabaDu.FooVaria.BaseTypes.Spreads.Factories;

public interface ISpreadFactory : IQuantifiableFactory<ISpread>
{
    ISpread CreateSpread(ISpreadMeasure spreadMeasure);
}