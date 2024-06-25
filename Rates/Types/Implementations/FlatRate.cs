namespace CsabaDu.FooVaria.Rates.Types.Implementations;

internal sealed class FlatRate : Rate, IFlatRate
{
    #region Constructors
    internal FlatRate(IFlatRate other) : base(other)
    {
        //Factory = other.Factory;
    }

    internal FlatRate(IFlatRateFactory factory, IRate rate) : base(factory, rate)
    {
        //Factory = factory;
    }

    internal FlatRate(IFlatRateFactory factory, IMeasure numerator, IDenominator denominator) : base(factory, numerator, denominator)
    {
        //Factory = factory;
    }
    #endregion

    #region Properties
    //public IFlatRateFactory Factory { get; init; }
    #endregion

    #region Public methods
    public IFlatRate Add(IFlatRate? other)
    {
        return GetSum(other, SummingMode.Add);
    }

    public IFlatRate Divide(decimal divisor)
    {
        IMeasure numerator = Numerator.Divide(divisor);

        return GetFlatRate(numerator);
    }

    public IFlatRate GetFlatRate(IMeasure numerator, string name, ValueType quantity)
    {
        return GetFactory().Create(numerator, name, quantity);
    }

    public IFlatRate GetFlatRate(IMeasure numerator, string name)
    {
        return GetFactory().Create(numerator, name);
    }

    public IFlatRate GetFlatRate(IMeasure numerator, Enum denominatorMeasureUnit, ValueType quantity)
    {
        return GetFactory().Create(numerator, denominatorMeasureUnit, quantity);
    }

    public IFlatRate GetFlatRate(IMeasure numerator, IMeasurement denominatorMeasurement)
    {
        return GetFactory().Create(numerator, denominatorMeasurement);
    }

    public IFlatRate GetFlatRate(IMeasure numerator, MeasureUnitCode denominatorCode)
    {
        return GetFactory().Create(numerator, denominatorCode);
    }

    public IFlatRate GetFlatRate(IMeasure numerator, IDenominator denominator)
    {
        return GetFactory().Create(numerator, denominator);
    }

    public IFlatRate GetFlatRate(IMeasure numerator)
    {
        return GetFlatRate(numerator, Denominator);
    }

    public IFlatRate GetNew(IFlatRate other)
    {
        return GetFactory().CreateNew(other);
    }

    public IFlatRate Multiply(decimal multiplier)
    {
        IMeasure numerator = Numerator.Multiply(multiplier);

        return GetFlatRate(numerator);
    }

    public IFlatRate Subtract(IFlatRate? other)
    {
        return GetSum(other, SummingMode.Subtract);
    }

    #region Override methods
    public IFlatRateFactory GetFactory()
    {
        return (IFlatRateFactory)Factory;
    }

    public override ILimit? GetLimit()
    {
        return null;
    }

    public override IFlatRate GetRate(IRate rate)
    {
        return (IFlatRate)GetFactory().CreateNew(rate);
    }
    #endregion
    #endregion

    #region Private methods
    private IFlatRate GetSum(IFlatRate? other, SummingMode summingMode)
    {
        if (other is null) return GetRate(this);

        if (!other.TryExchangeTo(Denominator, out IRate? exchanged))
        {
            throw InvalidMeasureUnitCodeEnumArgumentException(other!.GetDenominatorCode(), nameof(other));
        }

        IMeasure numerator = summingMode switch
        {
            SummingMode.Add => Numerator.Add(exchanged!.Numerator),
            SummingMode.Subtract => Numerator.Subtract(exchanged!.Numerator),

            _ => throw new InvalidOperationException(null),
        };

        return GetFlatRate(numerator);
    }
    #endregion
}
