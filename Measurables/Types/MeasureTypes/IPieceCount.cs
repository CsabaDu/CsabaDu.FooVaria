namespace CsabaDu.FooVaria.Measurables.Types.MeasureTypes;

public interface IPieceCount : IMeasure, ICustomMeasure<IPieceCount, long, Pieces>
{
    IPieceCount GetPieceCount(ValueType quantity, string name);
    IPieceCount GetPieceCount(long quantity, Pieces pieces);
    IPieceCount GetPieceCount(long quantity, Pieces pieces, decimal exchangeRate, string customName);
    IPieceCount GetPieceCount(ValueType quantity, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate);
    IPieceCount GetPieceCount(ValueType quantity, IMeasurement measurement);
    IPieceCount GetPieceCount(IBaseMeasure baseMeasure);
    IPieceCount GetPieceCount(IPieceCount? other = null);
}
