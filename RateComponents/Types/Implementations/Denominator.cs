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

    public IDenominator GetNew(IDenominator other)
    {
        return GetFactory().CreateNew(other);
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

    public override void ValidateQuantity(ValueType? quantity, string paramName)
    {
        ValidateQuantity(this, quantity, paramName);
    }

    public IDenominator GetDenominator(IMeasurement measurement, decimal quantity)
    {
        return (IDenominator)GetBaseMeasure(measurement, quantity);
    }

    public IDenominator GetBaseMeasure(decimal quantity)
    {
        return (IDenominator)GetBaseMeasure((ValueType)quantity);
    }

    public decimal GetQuantity()
    {
        return (decimal)Quantity;
    }

    public IDenominator GetBaseMeasure(Enum measureUnit, ValueType quantity)
    {
        return GetFactory().Create(measureUnit, quantity);
    }

    public IDenominator GetBaseMeasure(string name, ValueType quantity)
    {
        return GetFactory().Create(name, quantity);
    }

    public IDenominator? GetBaseMeasure(Enum measureUnit, decimal exchangeRate, ValueType quantity, string customName)
    {
        return GetFactory().Create(measureUnit, exchangeRate, quantity, customName);
    }

    public IDenominator? GetBaseMeasure(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate, ValueType quantity)
    {
        return GetFactory().Create(customName, measureUnitCode, exchangeRate, quantity);
    }

    public IDenominator GetBaseMeasure(IBaseMeasure baseMeasure)
    {
        return GetFactory().Create(baseMeasure);
    }

    public IDenominator ConvertToLimitable(ILimiter limiter)
    {
        return ConvertToLimitable(this, limiter);
    }
    #endregion
    #endregion
}
