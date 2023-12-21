//using CsabaDu.FooVaria.Common.Enums;
//using CsabaDu.FooVaria.RateComponents.Types.Implementations;

//namespace CsabaDu.FooVaria.RateComponents.Factories.Implementations;

//public sealed class FlatRateFactory : RateFactory, IFlatRateFactory
//{
//    #region Constructors
//    public FlatRateFactory(IDenominatorFactory denominatorFactory) : base(denominatorFactory)
//    {
//    }
//    #endregion

//    #region Public methods
//    public IFlatRate CreateNew(IFlatRate flatRate)
//    {
//        return new FlatRate(flatRate);
//    }

//    public IFlatRate CreateNew(IMeasure numerator, IDenominator denominator)
//    {
//        return new FlatRate(this, numerator, denominator);
//    }

//    public IFlatRate CreateNew(IMeasure numerator, string name, ValueType quantity)
//    {
//        IDenominator denominator = DenominatorFactory.CreateNew(name, quantity);

//        return CreateNew(numerator, denominator);
//    }

//    public IFlatRate CreateNew(IMeasure numerator, Enum measureUnit, ValueType quantity)
//    {
//        IDenominator denominator = DenominatorFactory.CreateNew(measureUnit, quantity);

//        return CreateNew(numerator, denominator);
//    }

//    public IFlatRate CreateNew(IMeasure numerator, string name)
//    {
//        IDenominator denominator = DenominatorFactory.CreateNew(name);

//        return CreateNew(numerator, denominator);
//    }

//    public IFlatRate CreateNew(IMeasure numerator, Enum measureUnit)
//    {
//        if (measureUnit is MeasureUnitTypeCode measureUnitTypeCode) return CreateNew(numerator, measureUnitTypeCode);

//        IDenominator denominator = DenominatorFactory.CreateNew(measureUnit);

//        return CreateNew(numerator, denominator);
//    }

//    public IFlatRate CreateNew(IMeasure numerator, IMeasurement measurement)
//    {
//        return new FlatRate(this, numerator, measurement);
//    }

//    public IFlatRate CreateNew(IMeasure numerator, IMeasurement measurement, ValueType quantity)
//    {
//        IDenominator denominator = DenominatorFactory.CreateNew(measurement, quantity);

//        return CreateNew(numerator, denominator);
//    }

//    public override IFlatRate CreateNew(IRate rate)
//    {
//        IMeasure numerator = NullChecked(rate, nameof(rate)).Numerator;
//        IDenominator denominator = rate.Denominator;

//        return CreateNew(numerator, denominator);
//    }

//    public override IFlatRate CreateNew(IMeasurable other)
//    {
//        _ = NullChecked(other, nameof(other));

//        if (other is IFlatRate flatRate) return CreateNew(flatRate);

//        if (other is IRate rate) return CreateNew(rate);

//        throw new ArgumentOutOfRangeException(nameof(other), other.GetType(), null);
//    }

//    public override IFlatRate CreateNew(IBaseMeasure numerator, MeasureUnitTypeCode denominatorMeasureUnitTpeCode)
//    {
//        string name = nameof(numerator);

//        if (NullChecked(numerator, name) is not IMeasure measure) throw ArgumentTypeOutOfRangeException(name, numerator);

//        return new FlatRate(this, measure, denominatorMeasureUnitTpeCode);
//    }

//    public override IBaseRate CreateNew(IBaseMeasure numerator, Enum denominatorMeasureUnit)
//    {
//        string name = nameof(numerator);

//        if (NullChecked(numerator, name) is not IMeasure measure) throw ArgumentTypeOutOfRangeException(name, numerator);

//        name = nameof(denominatorMeasureUnit);

//        measure.ValidateMeasureUnit(denominatorMeasureUnit, name);

//        return new FlatRate(this, measure, denominatorMeasureUnit);
//    }
//    #endregion
//}
