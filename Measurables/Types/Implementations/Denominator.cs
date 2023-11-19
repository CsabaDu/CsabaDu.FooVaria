namespace CsabaDu.FooVaria.RateComponents.Types.Implementations;

internal sealed class Denominator : RateComponent, IDenominator
{
    #region Constructors
    internal Denominator(IDenominatorFactory factory, ValueType quantity, IMeasurement measurement) : base(factory, quantity, measurement)
    {
    }

    internal Denominator(IDenominatorFactory factory, MeasureUnitTypeCode measureUnitTypeCode) : base(factory, measureUnitTypeCode)
    {
    }
    #endregion

    #region Public methods
    public IDenominator GetDefaultRateComponent()
    {
        return GetDefault(this);
    }

    public decimal GetDefaultRateComponentQuantity()
    {
        return GetDefaultRateComponentQuantity<decimal>();
    }

    public IDenominator GetDenominator(IMeasurement measurement, ValueType quantity)
    {
        return GetFactory().Create(measurement, quantity);
    }

    public IDenominator GetDenominator(IMeasurement measurement)
    {
        return GetFactory().Create(measurement);
    }

    public IDenominator GetDenominator(IRateComponent baseMeasure)
    {
        return (IDenominator)GetFactory().Create(baseMeasure);
    }

    public IDenominator GetDenominator(IDenominator other)
    {
        return GetFactory().Create(other);
    }

    public IDenominator GetDenominator(Enum measureUnit, ValueType quantity)
    {
        return GetFactory().Create(measureUnit, quantity);
    }

    public IDenominator GetDenominator(Enum measureUnit)
    {
        return GetFactory().Create(measureUnit, GetDefaultRateComponentQuantity());
    }

    public IDenominator GetDenominator(Enum measureUnit, decimal exchangeRate, string customName, ValueType quantity)
    {
        return GetFactory().Create(measureUnit, exchangeRate, customName, quantity);
    }

    public IDenominator GetDenominator(Enum measureUnit, decimal exchangeRate, string customName)
    {
        return GetDenominator(measureUnit, exchangeRate, customName, GetDefaultRateComponentQuantity());
    }

    public IDenominator GetDenominator(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ValueType quantity)
    {
        return GetFactory().Create(customName, measureUnitTypeCode, exchangeRate, quantity);
    }

    public IDenominator GetDenominator(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate)
    {
        return GetDenominator(customName, measureUnitTypeCode, exchangeRate, GetDefaultRateComponentQuantity());
    }

    public IDenominator GetDenominator(string name, ValueType quantity)
    {
        return GetFactory().Create(name, quantity);
    }

    public IDenominator GetDenominator(string name)
    {
        return GetDenominator(name, GetDefaultRateComponentQuantity());
    }

    public IDenominator GetDenominator(IRateComponent baseMeasure, ValueType quantity)
    {
        return GetFactory().Create(baseMeasure.Measurement, quantity);
    }

    public Enum GetMeasureUnit()
    {
        return Measurement.GetMeasureUnit();
    }

    public decimal GetQuantity()
    {
        return (decimal)Quantity;
    }

    #region Override methods
    public override bool Equals(IRateComponent? other)
    {
        return other is IDenominator
            && base.Equals(other);
    }

    public override IRateComponent GetRateComponent(ValueType quantity, Enum measureUnit)
    {
        return GetDenominator(measureUnit, quantity);
    }

    public override IDenominatorFactory GetFactory()
    {
        return (IDenominatorFactory)Factory;
    }

    public override LimitMode? GetLimitMode()
    {
        return null;
    }

    //public override IDenominator GetMeasurable(IMeasurable other)
    //{
    //    return (IDenominator)GetFactory().Create(other);
    //}

    public override TypeCode GetQuantityTypeCode()
    {
        return TypeCode.Decimal;
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

    public override IRateComponent GetRateComponent(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ValueType quantity)
    {
        throw new NotImplementedException();
    }

    public IDenominator GetDefault(MeasureUnitTypeCode measureUnitTypeCode)
    {
        throw new NotImplementedException();
    }
    #endregion
    #endregion
}
