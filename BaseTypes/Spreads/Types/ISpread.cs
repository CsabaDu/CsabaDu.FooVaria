namespace CsabaDu.FooVaria.BaseTypes.Spreads.Types;

public interface ISpread : IQuantifiable<ISpread>, ISpreadMeasure
{
    ISpread GetSpread(ISpreadMeasure spreadMeasure);
}
