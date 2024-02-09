namespace CsabaDu.FooVaria.Measures.Types.Implementations;

internal sealed class PieceCount(IMeasureFactory factory, Pieces pieces, long quantity)
    : Measure<IPieceCount, long, Pieces>(factory, pieces, quantity), IPieceCount
{
    #region Public methods
    public IPieceCount? GetCustomMeasure(Pieces pieces, decimal exchangeRate, long quantity, string customName)
    {
        return (IPieceCount?)GetBaseMeasure(pieces, exchangeRate, quantity, customName);
    }

    public IPieceCount? GetNextCustomMeasure(string customName, decimal exchangeRate, long quantity)
    {
        return (IPieceCount?)GetBaseMeasure(customName, MeasureUnitCode, exchangeRate, quantity);
    }
    #endregion
}
