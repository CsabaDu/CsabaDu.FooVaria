namespace CsabaDu.FooVaria.BaseTypes.Quantifiables.Types;

public interface IQuantifiable : IBaseQuantifiable, IExchange<IQuantifiable, Enum>, IFit<IQuantifiable>, IRound<IQuantifiable>, ILimitable
{
    IQuantifiable GetQuantifiable(MeasureUnitCode measureUnitCode, decimal defaultQuantity);

    void ValidateQuantifiable(IBaseQuantifiable? baseQuantifiable, string paramName);
}
