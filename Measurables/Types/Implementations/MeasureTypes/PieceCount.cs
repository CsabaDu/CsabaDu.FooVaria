namespace CsabaDu.FooVaria.Measurables.Types.Implementations.MeasureTypes;

internal sealed class PieceCount : Measure, IPieceCount
{
    #region Constructors
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
    #endregion

    #region Public methods
    public IPieceCount GetCustomMeasure(long quantity, Pieces pieces, decimal exchangeRate, string customName)
    {
        return (IPieceCount)GetMeasure(quantity, pieces, exchangeRate, customName);
    }

    public override IPieceCount GetMeasure(IBaseMeasure baseMeasure)
    {
        ValidateBaseMeasure(baseMeasure);

        return (IPieceCount)base.GetMeasure(baseMeasure);
    }

    public IPieceCount GetMeasure(double quantity, Pieces measureUnit)
    {
        return (IPieceCount)base.GetMeasure(quantity, measureUnit);
    }

    public IPieceCount GetMeasure(double quantity, string name)
    {
        return (IPieceCount)base.GetMeasure(quantity, name);
    }

    public IPieceCount GetMeasure(double quantity, IMeasurement? measurement = null)
    {
        return (IPieceCount)base.GetMeasure(quantity, measurement);
    }

    public IPieceCount GetMeasure(IPieceCount? other = null)
    {
        return (IPieceCount)base.GetMeasure(other);
    }

    public IPieceCount GetNextCustomMeasure(long quantity, string customName, decimal exchangeRate)
    {
        return (IPieceCount)GetMeasure(quantity, customName, exchangeRate);
    }

    public double GetQuantity()
    {
        return (long)Quantity;
    }
    #endregion
}
