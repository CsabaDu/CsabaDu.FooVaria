namespace CsabaDu.FooVaria.Measurables.Types.Implementations.MeasureTypes;

internal sealed class Extent : Measure<IExtent, double,ExtentUnit>, IExtent
{
    #region Constructors
    internal Extent(IMeasureFactory factory, ValueType quantity, ExtentUnit extentUnit) : base(factory, quantity, extentUnit)
    {
    }
    #endregion

    #region Public methos
    public IExtent ConvertFrom(IDistance distance)
    {
        return NullChecked(distance, nameof(distance)).ConvertMeasure();
    }

    public IDistance ConvertMeasure()
    {
        return ConvertMeasure<IDistance, DistanceUnit>(this, ConvertMode.Divide);
    }
    #endregion
}

    //public IExtent GetDefaultRateComponent()
    //{
    //    return GetDefault(this);
    //}

    //public double GetDefaultRateComponentQuantity()
    //{
    //    return GetDefaultRateComponentQuantity<double>();
    //}

    //public override IExtent GetMeasure(IBaseMeasure baseMeasure)
    //{
    //    return GetMeasure(this, baseMeasure);
    //}

    //public IExtent GetMeasure(double quantity, ExtentUnit measureUnit)
    //{
    //    return GetMeasure(this, quantity, measureUnit);
    //}

    //public IExtent GetMeasure(double quantity, string name)
    //{
    //    return GetMeasure(this, quantity, name);
    //}

    //public IExtent GetMeasure(double quantity, IMeasurement measurement)
    //{
    //    return GetMeasure(this, quantity, measurement);
    //}

    //public IExtent GetMeasure(IExtent other)
    //{
    //    return GetMeasure(this as IExtent, other);
    //}

    //public IExtent GetMeasure(double quantity)
    //{
    //    return GetMeasure(this, quantity);
    //}

    //public ExtentUnit GetMeasureUnit()
    //{
    //    return GetMeasureUnit<ExtentUnit>(this);
    //}