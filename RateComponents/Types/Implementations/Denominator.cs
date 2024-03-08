namespace CsabaDu.FooVaria.RateComponents.Types.Implementations;

internal sealed class Denominator(IDenominatorFactory factory, IMeasurement measurement, decimal quantity) : RateComponent<IDenominator>(factory, measurement), IDenominator
{
    #region Properties
    public decimal Quantity { get; init; } = GetDenominatorQuantity(quantity);
    public IDenominatorFactory Factory { get; init; } = factory;
    #endregion

    #region Public methods
    public IDenominator GetBaseMeasure(decimal quantity)
    {
        return (IDenominator)GetBaseMeasure((ValueType)quantity);
    }

    public IDenominator GetDenominator(Enum measureUnit)
    {
        return Factory.Create(measureUnit);
    }

    public IDenominator GetDenominator(string name)
    {
        return Factory.Create(name);
    }

    public IDenominator GetDenominator(IMeasurement measurement)
    {
        return Factory.Create(measurement);
    }

    public IDenominator GetDenominator(IBaseMeasure baseMeasure, ValueType quantity)
    {
        return Factory.Create(baseMeasure, quantity);
    }

    public IDenominator GetNew(IDenominator other)
    {
        return Factory.CreateNew(other);
    }

    #region Override methods
    public override ValueType GetBaseQuantity()
    {
        return Quantity;
    }

    public override IDenominatorFactory GetFactory()
    {
        return Factory;
    }

    public override void ValidateQuantity(ValueType? quantity, string paramName)
    {
        ValidatePositiveQuantity(quantity, paramName);
    }

    public IDenominator GetDenominator(IMeasurement measurement, decimal quantity)
    {
        return (IDenominator)GetBaseMeasure(measurement, quantity);
    }

    public decimal GetQuantity()
    {
        return Quantity;
    }

    public IDenominator GetBaseMeasure(Enum measureUnit, ValueType quantity)
    {
        return Factory.Create(measureUnit, quantity);
    }

    public IDenominator GetBaseMeasure(string name, ValueType quantity)
    {
        return Factory.Create(name, quantity);
    }

    public IDenominator? GetBaseMeasure(Enum measureUnit, decimal exchangeRate, ValueType quantity, string customName)
    {
        return Factory.Create(measureUnit, exchangeRate, quantity, customName);
    }

    public IDenominator? GetBaseMeasure(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate, ValueType quantity)
    {
        return Factory.Create(customName, measureUnitCode, exchangeRate, quantity);
    }

    public IDenominator GetBaseMeasure(IBaseMeasure baseMeasure)
    {
        return Factory.Create(baseMeasure);
    }
    #endregion
    #endregion

    #region Private methods
    #region Static methods
    private static decimal GetDenominatorQuantity(decimal quantity)
    {
        return quantity > 0 ?
            quantity
            : throw QuantityArgumentOutOfRangeException(quantity);
    }
    #endregion
    #endregion
}
