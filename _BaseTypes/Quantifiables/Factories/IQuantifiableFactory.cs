namespace CsabaDu.FooVaria.BaseTypes.Quantifiables.Factories;

public interface IQuantifiableFactory : IBaseQuantifiableFactory
{
    IQuantifiable CreateQuantifiable(MeasureUnitCode measureUnitCode, decimal defaultQuantity);
}
