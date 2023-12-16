namespace CsabaDu.FooVaria.RateComponents.Types.Implementations;

internal sealed class Denominator : RateComponent<IDenominator, decimal>, IDenominator
{
    #region Constructors
    internal Denominator(IDenominatorFactory factory, IMeasurement measurement, ValueType quantity) : base(factory, measurement, quantity)
    {
    }
    #endregion

    #region Public methods
    public override IDenominator? GetDefault(MeasureUnitTypeCode measureUnitTypeCode)
    {
        return GetFactory().CreateDefault(measureUnitTypeCode);
    }

    public IDenominator GetDenominator(Enum measureUnit)
    {
        return GetFactory().Create(measureUnit);
    }

    public IDenominator GetDenominator(string name)
    {
        return GetFactory().Create(name);
    }

    public IDenominator GetDenominator(IMeasurement measurement)
    {
        return GetFactory().Create(measurement);
    }

    public IDenominator GetDenominator(IRateComponent rateComponent, ValueType quantity)
    {
        return GetFactory().Create(rateComponent, quantity);
    }

    public Enum GetMeasureUnit()
    {
        return Measurement.GetMeasureUnit();
    }

    public override IDenominator GetRateComponent(IRateComponent rateComponent)
    {
        if (rateComponent is IDenominator other) return GetNew(other);

        return (IDenominator)GetRateComponent(rateComponent, GetFactory());
    }

    #region Override methods
    public override bool Equals(IRateComponent? other)
    {
        return other is IDenominator
            && base.Equals(other);
    }

    public override IDenominatorFactory GetFactory()
    {
        return (IDenominatorFactory)Factory;
    }

    public override void Validate(IRootObject? rootObject, string paramName)
    {
        Validate(this, rootObject, validateDenominator, paramName);

        #region Local methods
        void validateDenominator()
        {
            ValidateBaseMeasure(this, rootObject!, paramName);
        }
        #endregion
    }
    #endregion
    #endregion
}
