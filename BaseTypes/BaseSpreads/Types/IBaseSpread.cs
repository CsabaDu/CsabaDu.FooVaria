namespace CsabaDu.FooVaria.BaseTypes.BaseSpreads.Types;

public interface IBaseSpread : IQuantifiable, ISpreadMeasure, IExchange<IBaseSpread, Enum>, IFit<IBaseSpread>
{
    IBaseSpread GetBaseSpread(ISpreadMeasure spreadMeasure);
}
