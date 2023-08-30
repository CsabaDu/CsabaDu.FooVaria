namespace CsabaDu.FooVaria.Measurables.Types.Implementations.MeasureTypes
{
    internal sealed class PieceCount : Measure, IPieceCount
    {
        internal PieceCount(IPieceCount pieceCount) : base(pieceCount)
        {
        }

        internal PieceCount(IMeasureFactory measureFactory, ValueType quantity, Enum measureUnit) : base(measureFactory, quantity, measureUnit)
        {
        }

        internal PieceCount(IMeasureFactory measureFactory, ValueType quantity, IMeasurement measurement) : base(measureFactory, quantity, measurement)
        {
        }

        internal PieceCount(IMeasureFactory measureFactory, ValueType quantity, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate) : base(measureFactory, quantity, customName, measureUnitTypeCode, exchangeRate)
        {
        }

        internal PieceCount(IMeasureFactory measureFactory, ValueType quantity, Enum measureUnit, decimal exchangeRate, string customName) : base(measureFactory, quantity, measureUnit, exchangeRate, customName)
        {
        }

        public IPieceCount GetNextCustomMeasure(long quantity, string customName, decimal exchangeRate)
        {
            return (IPieceCount)GetMeasure(quantity, customName, MeasureUnitTypeCode, exchangeRate);
        }

        public IPieceCount GetPieceCount(ValueType quantity, string name)
        {
            return (IPieceCount)GetMeasure(quantity, name);
        }

        public IPieceCount GetPieceCount(long quantity, Pieces pieces)
        {
            return (IPieceCount)GetMeasure(quantity, pieces);
        }

        public IPieceCount GetCustomMeasure(long quantity, Pieces pieces, decimal exchangeRate, string customName)
        {
            return (IPieceCount)GetMeasure(quantity, pieces, exchangeRate, customName);
        }

        public IPieceCount GetPieceCount(ValueType quantity, IMeasurement? measurement = null)
        {
            return (IPieceCount)GetMeasure(quantity, measurement);
        }

        public IPieceCount GetPieceCount(IBaseMeasure baseMeasure)
        {
            return (IPieceCount)GetMeasure(baseMeasure);
        }

        public IPieceCount GetPieceCount(IPieceCount? other = null)
        {
            return (IPieceCount)GetMeasure(other);
        }
    }
}
