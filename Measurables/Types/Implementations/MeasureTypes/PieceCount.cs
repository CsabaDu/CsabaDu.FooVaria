using CsabaDu.FooVaria.Measurables.Behaviors;

namespace CsabaDu.FooVaria.Measurables.Types.Implementations.MeasureTypes;

internal sealed class PieceCount : Measure, IPieceCount
{
    #region Constructors
    internal PieceCount(IPieceCount pieceCount) : base(pieceCount)
    {
    }

    internal PieceCount(IMeasureFactory measureFactory, ValueType quantity, IMeasurement measurement) : base(measureFactory, quantity, measurement)
    {
    }
    #endregion

    #region Public methods
    public IPieceCount GetCustomMeasure(long quantity, Pieces measureUnit, decimal exchangeRate, string customName)
    {
        return GetMeasure(this, quantity, measureUnit, exchangeRate, customName);
    }

    public override IPieceCount GetMeasure(IBaseMeasure baseMeasure)
    {
        return GetMeasure(this, baseMeasure);
    }

    public IPieceCount GetMeasure(double quantity, Pieces measureUnit)
    {
        return GetMeasure(this, quantity, measureUnit);
    }

    public IPieceCount GetMeasure(double quantity, string name)
    {
        return GetMeasure(this, quantity, name);
    }

    public IPieceCount GetMeasure(double quantity, IMeasurement? measurement = null)
    {
        return GetMeasure(this, quantity, measurement);
    }

    public IPieceCount GetMeasure(IPieceCount? other = null)
    {
        return GetMeasure(this, other as PieceCount);
    }

    public IPieceCount GetNextCustomMeasure(long quantity, string customName, decimal exchangeRate)
    {
        return GetMeasure(this, quantity, customName, exchangeRate);
    }

    public double GetQuantity()
    {
        return (long)Quantity;
    }
    #endregion
}
