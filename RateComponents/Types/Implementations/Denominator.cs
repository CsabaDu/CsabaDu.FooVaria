namespace CsabaDu.FooVaria.RateComponents.Types.Implementations;

internal sealed class Denominator : RateComponent<IDenominator>, IDenominator
{
    #region Constructors
    internal Denominator(IDenominatorFactory factory, IMeasurement measurement, decimal quantity) : base(factory, measurement)
    {
        Quantity = quantity > 0 ?
            quantity
            : throw QuantityArgumentOutOfRangeException(quantity);
    }
    #endregion

    #region Properties
    #region Override properties
    public override object Quantity { get; init; }
    #endregion
    #endregion

    #region Public methods
    public override IDenominator GetDefault()
    {
        return GetDefault(this);
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

    public IDenominator GetDenominator(IBaseMeasure baseMeasure, ValueType quantity)
    {
        return GetFactory().Create(baseMeasure, quantity);
    }

    #region Override methods
    public override decimal GetDefaultQuantity()
    {
        return GetDefaultQuantity(Quantity);
    }

    public override IDenominatorFactory GetFactory()
    {
        return (IDenominatorFactory)Factory;
    }

    public IDenominator GetDenominator(IMeasurement measurement, decimal quantity)
    {
        return (IDenominator)GetFactory().CreateBaseMeasure(measurement, quantity);
    }

    public IDenominator GetBaseMeasure(decimal quantity)
    {
        throw new NotImplementedException();
    }

    public decimal GetQuantity()
    {
        throw new NotImplementedException();
    }

    public IDenominator GetBaseMeasure(Enum measureUnit, ValueType quantity)
    {
        throw new NotImplementedException();
    }

    public IDenominator GetBaseMeasure(string name, ValueType quantity)
    {
        throw new NotImplementedException();
    }

    public IDenominator? GetBaseMeasure(Enum measureUnit, decimal exchangeRate, ValueType quantity, string customName)
    {
        throw new NotImplementedException();
    }

    public IDenominator? GetBaseMeasure(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate, ValueType quantity)
    {
        throw new NotImplementedException();
    }

    public IDenominator GetBaseMeasure(IBaseMeasure baseMeasure)
    {
        throw new NotImplementedException();
    }
    #endregion
    #endregion
}
