namespace CsabaDu.FooVaria.BaseTypes.Spreads.Types;

public interface ISpread : IQuantifiable, ISpreadMeasure, IRound<ISpread>
{
    ISpread GetSpread(ISpreadMeasure spreadMeasure);
}
