namespace CsabaDu.FooVaria.BaseTypes.Spreads.Factories;

public interface ISpreadFactory : IQuantifiableFactory
{
    //ISpread CreateQuantifiable(MeasureUnitCode measureUnitCode, decimal defaultQuantity);
    ISpread CreateSpread(ISpreadMeasure spreadMeasure);
}