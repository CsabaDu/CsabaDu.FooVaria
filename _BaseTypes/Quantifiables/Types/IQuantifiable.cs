namespace CsabaDu.FooVaria.BaseTypes.Quantifiables.Types;

public interface IQuantifiable : IBaseQuantifiable, ITryExchange<IQuantifiable, Enum>, IFit<IQuantifiable>, IRound<IQuantifiable>, IDecimalQuantity
{
    IQuantifiable GetQuantifiable(MeasureUnitCode measureUnitCode, decimal defaultQuantity);
}
