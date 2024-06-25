
namespace CsabaDu.FooVaria.RateComponents.Types.Implementations;

internal sealed class Denominator(IDenominatorFactory factory, IMeasurement measurement, decimal quantity) : RateComponent<IDenominator>(factory, measurement), IDenominator
{
    #region Properties
    public decimal Quantity { get; init; } = GetDenominatorQuantity(quantity);
    //public IDenominatorFactory Factory { get; init; } = factory;
    #endregion

    #region Public methods
    public IDenominator GetBaseMeasure(decimal quantity)
    {
        return (IDenominator)GetBaseMeasure((ValueType)quantity);
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
    public override ValueType GetBaseQuantity()
    {
        return Quantity;
    }

    public IDenominatorFactory GetFactory()
    {
        return (IDenominatorFactory)Factory;
    }

    public override LimitMode? GetLimitMode()
    {
        return default;
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
