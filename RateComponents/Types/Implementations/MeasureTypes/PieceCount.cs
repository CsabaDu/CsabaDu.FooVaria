namespace CsabaDu.FooVaria.RateComponents.Types.Implementations.MeasureTypes;

internal sealed class PieceCount : Measure<IPieceCount, long, Pieces>, IPieceCount
{
    #region Constructors
    internal PieceCount(IMeasureFactory factory, Pieces pieces, ValueType quantity) : base(factory, pieces, quantity)
    {
    }
    #endregion

    #region Public methods
    public IPieceCount? GetCustomMeasure(Pieces pieces, decimal exchangeRate, long quantity, string customName)
    {
        return GetRateComponent(pieces, exchangeRate, quantity, customName);
    }

    public IPieceCount? GetNextCustomMeasure(string customName, decimal exchangeRate, long quantity)
    {
        return GetRateComponent(customName, MeasureUnitTypeCode, exchangeRate, quantity);
    }
    #endregion
}
