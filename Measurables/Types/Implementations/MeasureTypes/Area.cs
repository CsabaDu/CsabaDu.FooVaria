namespace CsabaDu.FooVaria.Measurables.Types.Implementations.MeasureTypes;

internal sealed class Area : Measure, IArea
{
    #region Constructors
    internal Area(IMeasureFactory factory, ValueType quantity, AreaUnit areaUnit) : base(factory, quantity, areaUnit)
    {
    }
    #endregion

    #region Public methods
    public override IArea GetMeasure(IBaseMeasure baseMeasure)
    {
        return GetMeasure(this, baseMeasure);
    }

    public IArea GetMeasure(double quantity, AreaUnit measureUnit)
    {
        return GetMeasure(this, quantity, measureUnit);
    }

    public IArea GetDefaultRateComponent()
    {
        return GetDefault(this);
    }

    public double GetDefaultRateComponentQuantity()
    {
        return GetDefaultRateComponentQuantity<double>();
    }

    public IArea GetMeasure(IArea other)
    {
        return GetMeasure(this as IArea, other);
    }

    public IArea GetMeasure(double quantity, string name)
    {
        return GetMeasure(this, quantity, name);
    }

    public IArea GetMeasure(double quantity, IMeasurement measurement)
    {
        return GetMeasure(this, quantity, measurement);
    }

    public IArea GetMeasure(double quantity)
    {
        return GetMeasure(this, quantity);
    }

    public ISpreadMeasure GetSpreadMeasure()
    {
        return this;
    }

    //public override void ValidateCommonBase(ICommonBase? other)
    //{
    //    ValidateCommonBase(this, other);
    //}
    #endregion
}
