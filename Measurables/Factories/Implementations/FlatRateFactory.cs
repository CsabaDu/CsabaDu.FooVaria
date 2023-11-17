using CsabaDu.FooVaria.Common.Enums;
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
        if (measureUnit is MeasureUnitTypeCode measureUnitTypeCode) return Create(numerator, measureUnitTypeCode);

        IDenominator denominator = DenominatorFactory.Create(measureUnit);

        return Create(numerator, denominator);
    }

    public IFlatRate Create(IMeasure numerator, IMeasurement measurement)
    {
        return new FlatRate(this, numerator, measurement);
    }

    public IFlatRate Create(IMeasure numerator, IMeasurement measurement, ValueType quantity)
    {
        IDenominator denominator = DenominatorFactory.Create(measurement, quantity);

        return Create(numerator, denominator);
    }

    public override IFlatRate Create(IRate rate)
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

    public override IFlatRate Create(IBaseMeasureTemp numerator, MeasureUnitTypeCode denominatorMeasureUnitTpeCode)
    {
        string name = nameof(numerator);

        if (NullChecked(numerator, name) is not IMeasure measure) throw ArgumentTypeOutOfRangeException(name, numerator);

        return new FlatRate(this, measure, denominatorMeasureUnitTpeCode);
    }

    public override IBaseRate Create(IBaseMeasureTemp numerator, Enum denominatorMeasureUnit)
    {
        string name = nameof(numerator);

        if (NullChecked(numerator, name) is not IMeasure measure) throw ArgumentTypeOutOfRangeException(name, numerator);

        name = nameof(denominatorMeasureUnit);

        measure.ValidateMeasureUnit(denominatorMeasureUnit, name);

        return new FlatRate(this, measure, denominatorMeasureUnit);
    }
    #endregion
}
