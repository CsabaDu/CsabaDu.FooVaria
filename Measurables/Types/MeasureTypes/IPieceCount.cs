namespace CsabaDu.FooVaria.Measurables.Types.MeasureTypes;

public interface IPieceCount : IMeasure, ICustomMeasure
{
    IPieceCount GetPieceCount(long quantity, Pieces pieces, decimal? exchangeRate = null);
    IPieceCount GetPieceCount(ValueType quantity, IMeasurement measurement);
    IPieceCount GetPieceCount(IBaseMeasure baseMeasure);
    IPieceCount GetPieceCount(IPieceCount? other = null);
}
