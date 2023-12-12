
namespace CsabaDu.FooVaria.RateComponents.Types.Implementations;

internal sealed class Denominator : RateComponent<IDenominator>, IDenominator
{
    #region Constructors
    internal Denominator(IDenominatorFactory factory, IMeasurement measurement, ValueType quantity) : base(factory, measurement, quantity)
    {
    }
    #endregion

    #region Public methods
    public IDenominator GetDefault()
    {
        throw new NotImplementedException();
    }

    public IDenominator? GetDefault(MeasureUnitTypeCode measureUnitTypeCode)
    {
        return GetFactory().CreateDefault(measureUnitTypeCode);
    }

    public decimal GetDefaultRateComponentQuantity()
    {
        return GetDefaultRateComponentQuantity<ulong>();
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

    public IDenominator GetNew(IDenominator other)
    {
        return GetFactory().Create(other);
    }

    public decimal GetQuantity()
    {
        return (decimal)Quantity;
    }

    public IDenominator GetRateComponent(decimal quantity)
    {
        return GetFactory().Create(Measurement, quantity);
    }

    public IDenominator GetRateComponent(IRateComponent rateComponent)
    {
        return (IDenominator)GetFactory().Create(rateComponent);
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

    //public IDenominator GetDefault()
    //{
    //    return GetDefault(MeasureUnitTypeCode);
    //}

    //public decimal GetDefaultRateComponentQuantity()
    //{
    //    return GetDefaultRateComponentQuantity<decimal>();
    //}

    //public IDenominator GetDenominator(IMeasurement measurement)
    //{
    //    return GetFactory().Create(measurement);
    //}

    //public IDenominator GetDenominator(Enum measureUnit)
    //{

    //}

    //public IDenominator GetDenominator(Enum measureUnit, decimal exchangeRate, string customName, ValueType quantity)
    //{
    //    return GetFactory().Create(measureUnit, exchangeRate, customName, quantity);
    //}

    //public IDenominator GetDenominator(Enum measureUnit, decimal exchangeRate, string customName)
    //{
    //    return GetDenominator(measureUnit, exchangeRate, customName, GetDefaultRateComponentQuantity());
    //}

    //public IDenominator GetDenominator(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ValueType quantity)
    //{
    //    return GetFactory().Create(customName, measureUnitTypeCode, exchangeRate, quantity);
    //}

    //public IDenominator GetDenominator(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate)
    //{
    //    return GetDenominator(customName, measureUnitTypeCode, exchangeRate, GetDefaultRateComponentQuantity());
    //}

    //public IDenominator GetDenominator(string name, ValueType quantity)
    //{
    //    return GetFactory().Create(name, quantity);
    //}

    //public IDenominator GetDenominator(string name)
    //{
    //    return GetDenominator(name, GetDefaultRateComponentQuantity());
    //}

    //public IDenominator GetDenominator(IRateComponent baseMeasure, ValueType quantity)
    //{
    //    return GetFactory().Create(baseMeasure.Measurement, quantity);
    //}

    //public Enum GetMeasureUnit()
    //{
    //    return Measurement.GetMeasureUnit();
    //}

    //public decimal GetQuantity()
    //{
    //    return (decimal)Quantity;
    //}