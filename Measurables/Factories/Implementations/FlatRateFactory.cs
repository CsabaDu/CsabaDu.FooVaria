using CsabaDu.FooVaria.Measurables.Types.Implementations;

namespace CsabaDu.FooVaria.Measurables.Factories.Implementations;

public sealed class FlatRateFactory : RateFactory, IFlatRateFactory
{
    #region Constructors
    public FlatRateFactory(IDenominatorFactory denominatorFactory) : base(denominatorFactory)
    {
    }
    #endregion

    #region Public methods
    public IFlatRate Create(IFlatRate flatRate)
    {
        return new FlatRate(flatRate);
    }

    public IFlatRate Create(IMeasure numerator, IDenominator denominator)
    {
        return new FlatRate(this, numerator, denominator);
    }

    public IFlatRate Create(IMeasure numerator, string name, ValueType quantity)
    {
        IDenominator denominator = DenominatorFactory.Create(name, quantity);

        return Create(numerator, denominator);
    }

    public IFlatRate Create(IMeasure numerator, Enum measureUnit, ValueType quantity)
    {
        IDenominator denominator = DenominatorFactory.Create(measureUnit, quantity);

        return Create(numerator, denominator);
    }

    public IFlatRate Create(IMeasure numerator, string name)
    {
        IDenominator denominator = DenominatorFactory.Create(name);

        return Create(numerator, denominator);
    }

    public IFlatRate Create(IMeasure numerator, Enum measureUnit)
    {
        IDenominator denominator = DenominatorFactory.Create(measureUnit);

        return Create(numerator, denominator);
    }

    public IFlatRate Create(IMeasure numerator, IMeasurement measurement)
    {
        IDenominator denominator = DenominatorFactory.Create(measurement);

        return Create(numerator, denominator);
    }

    public IFlatRate Create(IMeasure numerator, IMeasurement measurement, ValueType quantity)
    {
        IDenominator denominator = DenominatorFactory.Create(measurement, quantity);

        return Create(numerator, denominator);
    }

    public IFlatRate Create(IRate rate)
    {
        IMeasure numerator = NullChecked(rate, nameof(rate)).Numerator;
        IDenominator denominator = rate.Denominator;

        return Create(numerator, denominator);
    }

    public override IFlatRate Create(IMeasurable other)
    {
        _ = NullChecked(other, nameof(other));

        if (other is IFlatRate flatRate) return Create(flatRate);

        if (other is IRate rate) return Create(rate);

        throw new ArgumentOutOfRangeException(nameof(other), other.GetType(), null);
    }
    #endregion
}
