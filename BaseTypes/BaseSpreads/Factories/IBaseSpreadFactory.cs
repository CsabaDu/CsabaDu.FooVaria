namespace CsabaDu.FooVaria.BaseTypes.BaseSpreads.Factories;

public interface IBaseSpreadFactory : IQuantifiableFactory<IBaseSpread>
{
    IBaseSpread CreateBaseSpread(ISpreadMeasure spreadMeasure);
}