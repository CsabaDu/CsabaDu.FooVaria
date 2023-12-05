namespace CsabaDu.FooVaria.RateComponents.Types.Implementations.MeasureTypes;

internal sealed class Distance : Measure<IDistance, double, DistanceUnit>, IDistance
{
    #region Constructors
    internal Distance(IMeasureFactory factory, ValueType quantity, DistanceUnit distanceUnit) : base(factory, quantity, distanceUnit)
    {
    }
    #endregion

    #region Public methods
    public IDistance ConvertFrom(IExtent extent)
    {
        return NullChecked(extent, nameof(extent)).ConvertMeasure();
    }

    public IExtent ConvertMeasure()
    {
        return ConvertMeasure<IExtent, ExtentUnit>(ConvertMode.Multiply);
    }
    #endregion
}


    //public IDistance GetDefaultRateComponent()
    //{
    //    return GetDefault(this);
    //}

    //public double GetDefaultRateComponentQuantity()
    //{
    //    return GetDefaultRateComponentQuantity<double>();
    //}

    //public override IDistance GetMeasure(IRateComponent baseMeasure)
    //{
    //    return GetMeasure(this, baseMeasure);
    //}

    //public IDistance GetMeasure(double quantity, DistanceUnit measureUnit)
    //{
    //    return GetMeasure(this, quantity, measureUnit);
    //}

    //public IDistance GetMeasure(double quantity, string name)
    //{
    //    return GetMeasure(this, quantity, name);
    //}

    //public IDistance GetMeasure(double quantity, IMeasurement measurement)
    //{
    //    return GetMeasure(this, quantity, measurement);
    //}

    //public IDistance GetMeasure(IDistance other)
    //{
    //    return GetMeasure(this as IDistance, other);
    //}

    //public IDistance GetMeasure(double quantity)
    //{
    //    return GetMeasure(this, quantity);
    //}

    //public DistanceUnit GetMeasureUnit()
    //{
    //    return GetMeasureUnit<DistanceUnit>(this);
    //}