namespace CsabaDu.FooVaria.BaseTypes.Spreads.Types;

public interface ISpread : IQuantifiable, ISpreadMeasure
{
    ISpread GetSpread(ISpreadMeasure spreadMeasure);
}
