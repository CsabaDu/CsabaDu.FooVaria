using CsabaDu.FooVaria.BaseTypes.Measurables.Behaviors;

namespace CsabaDu.FooVaria.BaseTypes.Quantifiables.Types;

public interface IQuantifiable : IBaseQuantifiable, ITryExchange<IQuantifiable, Enum>, IFit<IQuantifiable>/*, IProportional<IQuantifiable>, IExchangeable<Enum>*/, IDecimalQuantity
{
    IQuantifiable GetQuantifiable(MeasureUnitCode measureUnitCode, decimal defaultQuantity);
}
