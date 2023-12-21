namespace CsabaDu.FooVaria.Common.Factories;

public interface IBaseSpreadFactory : IQuantifiableFactory
{
    IBaseSpread CreateBaseSpread(ISpreadMeasure spreadMeasure);
}