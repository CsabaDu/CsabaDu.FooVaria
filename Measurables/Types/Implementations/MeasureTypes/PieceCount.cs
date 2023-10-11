namespace CsabaDu.FooVaria.Measurables.Types.Implementations.MeasureTypes;

internal sealed class PieceCount : Measure, IPieceCount
{
    #region Constructors
    internal PieceCount(IMeasureFactory factory, ValueType quantity, Pieces pieces) : base(factory, quantity, pieces)
    {
    }
    #endregion

    #region Public methods
    public IPieceCount GetCustomMeasure(long quantity, Pieces measureUnit, decimal exchangeRate, string customName)
    {
        return GetMeasure(this, quantity, measureUnit, exchangeRate, customName);
    }

    public IPieceCount GetDefaultRateComponent()
    {
        return GetDefault(this);
    }

    public long GetDefaultRateComponentQuantity()
    {
        return GetDefaultRateComponentQuantity<long>();
    }

    public override IPieceCount GetMeasure(IBaseMeasure baseMeasure)
    {
        return GetMeasure(this, baseMeasure);
    }

    public IPieceCount GetMeasure(long quantity, Pieces measureUnit)
    {
        return GetMeasure(this, quantity, measureUnit);
    }

    public IPieceCount GetMeasure(long quantity, string name)
    {
        return GetMeasure(this, quantity, name);
    }

    public IPieceCount GetMeasure(long quantity, IMeasurement measurement)
    {
        return GetMeasure(this, quantity, measurement);
    }

    public IPieceCount GetMeasure(IPieceCount other)
    {
        return GetMeasure(this as IPieceCount, other);
    }

    public IPieceCount GetMeasure(long quantity)
    {
        return GetMeasure(this, quantity);
    }

    public IPieceCount GetNextCustomMeasure(long quantity, string customName, decimal exchangeRate)
    {
        return GetMeasure(this, quantity, customName, exchangeRate);
    }

    //public override void Validate(ICommonBase? other)
    //{
    //    Validate(this, other);
    //}
    #endregion
}
