
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
