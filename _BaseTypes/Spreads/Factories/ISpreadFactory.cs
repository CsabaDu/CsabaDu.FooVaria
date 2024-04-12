namespace CsabaDu.FooVaria.BaseTypes.Spreads.Factories;

public interface ISpreadFactory : IQuantifiableFactory
{
    ISpreadMeasure? CreateSpreadMeasure(Enum measureUnit, ValueType quantity);
    ISpread CreateSpread(ISpreadMeasure spreadMeasure);
}