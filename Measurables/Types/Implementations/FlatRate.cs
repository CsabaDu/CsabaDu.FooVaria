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

    public IFlatRate GetFlatRate(IMeasure numerator, Enum measureUnit, decimal exchangeRate, string customName, decimal? quantity = null)
    {
        return GetFlatRateFactory().Create(numerator, measureUnit, exchangeRate, customName, quantity);
    }

    public IFlatRate GetFlatRate(IMeasure numerator, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, decimal? quantity = null)
    {
        return GetFlatRateFactory().Create(numerator, customName, measureUnitTypeCode, exchangeRate, quantity);
    }

    public IFlatRate GetFlatRate(IMeasure numerator, string customName, decimal? quantity = null)
    {
        return GetFlatRateFactory().Create(numerator, customName, quantity);
    }

    public IFlatRate GetFlatRate(IMeasure numerator, Enum measureUnit, decimal? quantity = null)
    {
        return GetFlatRateFactory().Create(numerator, measureUnit, quantity);
    }

    public IFlatRate GetFlatRate(IMeasure numerator, IMeasurement measurement, decimal? quantity = null)
    {
        return GetFlatRateFactory().Create(numerator, measurement, quantity);
    }

    public IFlatRate GetFlatRate(IMeasure numerator, IDenominator? denominator = null)
    {
        return GetFlatRateFactory().Create(numerator, denominator ?? Denominator);
    }

    public IFlatRate GetFlatRate(IRate rate) // Check!
    {
        return GetFlatRateFactory().Create(rate);
    }

    public IFlatRate GetFlatRate(IFlatRate? other = null)
    {
        return GetFlatRateFactory().Create(other ?? this);
    }

    public IFlatRateFactory GetFlatRateFactory()
    {
        return MeasurableFactory as IFlatRateFactory ?? throw new InvalidOperationException(null);
    }

    public override ILimit? GetLimit()
    {
        return null;
    }

    public override IRate GetRate(IMeasure numerator, string customName, decimal? quantity = null, ILimit? limit = null)
    {
        return GetFlatRate(numerator, customName, quantity);
    }

    public override IRate GetRate(IMeasure numerator, Enum measureUnit, decimal? quantity = null, ILimit? limit = null)
    {
        return GetFlatRate(numerator, measureUnit, quantity);
    }

    public override IRate GetRate(IMeasure numerator, Enum measureUnit, decimal exchangeRate, string customName, decimal? quantity = null, ILimit? limit = null)
    {
        return GetFlatRate(numerator, measureUnit, exchangeRate, customName, quantity);
    }

    public override IRate GetRate(IMeasure numerator, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, decimal? quantity = null, ILimit? limit = null)
    {
        return GetFlatRate(numerator, customName, measureUnitTypeCode, exchangeRate, quantity);
    }

    public override IRate GetRate(IMeasure numerator, IMeasurement measurement, decimal? quantity = null, ILimit? limit = null)
    {
        return GetFlatRate(numerator, measurement, quantity);
    }

    public override IRate GetRate(IMeasure numerator, IDenominator? denominator = null, ILimit? limit = null)
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
    #endregion

    #region Private methods
    private IFlatRate GetSum(IFlatRate? other, SummingMode summingMode)
    {
        if (other == null) return GetFlatRate();

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
