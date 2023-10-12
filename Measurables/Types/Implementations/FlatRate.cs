namespace CsabaDu.FooVaria.Measurables.Types.Implementations;

internal sealed class FlatRate : Rate, IFlatRate
{
    #region Constructors
    internal FlatRate(IFlatRate other) : base(other)
    {
    }

    internal FlatRate(IFlatRateFactory factory, IMeasure numerator, IDenominator denominator) : base(factory, numerator, denominator)
    {
    }
    #endregion

    #region Public methods
    public IFlatRate Add(IFlatRate? other)
    {
        return GetSum(other, SummingMode.Add);
    }

    public IFlatRate Divide(decimal divisor)
    {
        return GetFlatRate(Numerator.Divide(divisor));
    }

    public IFlatRate GetFlatRate(IMeasure numerator, string name, decimal quantity)
    {
        return GetFactory().Create(numerator, name, quantity);
    }

    public IFlatRate GetFlatRate(IMeasure numerator, string name)
    {
        return GetFactory().Create(numerator, name);
    }

    public IFlatRate GetFlatRate(IMeasure numerator, Enum measureUnit, decimal quantity)
    {
        return GetFactory().Create(numerator, measureUnit, quantity);
    }

    public IFlatRate GetFlatRate(IMeasure numerator, Enum measureUnit)
    {
        return GetFactory().Create(numerator, measureUnit);
    }

    public IFlatRate GetFlatRate(IMeasure numerator, IMeasurement measurement, decimal quantity)
    {
        return GetFactory().Create(numerator, measurement, quantity);
    }

    public IFlatRate GetFlatRate(IMeasure numerator, IMeasurement measurement)
    {
        return GetFactory().Create(numerator, measurement);
    }

    public IFlatRate GetFlatRate(IMeasure numerator, IDenominator denominator)
    {
        return GetFactory().Create(numerator, denominator);
    }

    public IFlatRate GetFlatRate(IMeasure numerator)
    {
        return GetFactory().Create(numerator, Denominator);
    }

    public IFlatRate GetFlatRate(IRate rate)
    {
        return GetFactory().Create(rate);
    }

    public IFlatRate GetFlatRate(IFlatRate other)
    {
        return GetFactory().Create(other);
    }

    public override ILimit? GetLimit()
    {
        return null;
    }

    public override IRate GetRate(IMeasure numerator, IDenominator denominator, ILimit? limit)
    {
        return GetFlatRate(numerator, denominator);
    }

    public IFlatRate Multiply(decimal multiplier)
    {
        return GetFlatRate(Numerator.Multiply(multiplier));
    }

    public IMeasure Multiply(IMeasure multiplier)
    {
        if (NullChecked(multiplier, nameof(multiplier)).IsExchangeableTo(MeasureUnitTypeCode))
        {
            decimal quantity = Numerator.GetDecimalQuantity() / Denominator.DefaultQuantity * multiplier.DefaultQuantity;

            return multiplier.GetMeasure(quantity, Numerator.Measurement);
        }

        throw new ArgumentOutOfRangeException(nameof(multiplier), multiplier.MeasureUnitTypeCode, null);
    }

    public IFlatRate Subtract(IFlatRate? other)
    {
        return GetSum(other, SummingMode.Subtract);
    }

    #region Override methods
    public override bool Equals(IRate? other)
    {
        return other is IFlatRate
            && base.Equals(other);
    }

    public override IFlatRateFactory GetFactory()
    {
        return (IFlatRateFactory)Factory;
    }

    public override IFlatRate GetMeasurable(IMeasurable other)
    {
        return (IFlatRate)GetFactory().Create(other);
    }
    #endregion
    #endregion

    #region Private methods
    private IFlatRate GetSum(IFlatRate? other, SummingMode summingMode)
    {
        if (other == null) return GetFlatRate(this);

        if (!other.Denominator.IsExchangeableTo(MeasureUnitTypeCode))
        {
            throw new ArgumentOutOfRangeException(nameof(other), other.MeasureUnitTypeCode, null);
        }

        if (other.Numerator.TryExchangeTo(Numerator.GetMeasureUnit(), out IBaseMeasure? exchanged))
        {
            IMeasure numerator = GetSum(Numerator, (IMeasure)exchanged, summingMode);

            return GetFlatRate(numerator);
        }

        throw new ArgumentOutOfRangeException(nameof(other), other.Numerator.MeasureUnitTypeCode, null);
    }
    #endregion
}
