namespace CsabaDu.FooVaria.BaseTypes.Spreads.Types;

public interface ISpread : IQuantifiable, ISpreadMeasure
{
    ISpread GetSpread(ISpreadMeasure spreadMeasure);
    ISpreadMeasure? GetSpreadMeasure(IQuantifiable? quantifiable);
}
