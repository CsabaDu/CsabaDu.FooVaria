//using CsabaDu.FooVaria.RateComponents.Types.Implementations;

//namespace CsabaDu.FooVaria.RateComponents.Factories.Implementations;

//public sealed class LimitedRateFactory : RateFactory, ILimitedRateFactory
//{
//    #region Constructors
//    public LimitedRateFactory(IDenominatorFactory denominatorFactory, ILimitFactory limitFactory) : base(denominatorFactory)
//    {
//        LimitFactory = NullChecked(limitFactory, nameof(limitFactory));
//    }
//    #endregion

//    #region Properties
//    public ILimitFactory LimitFactory { get; init; }
//    #endregion

//    #region Public methods
//    public ILimitedRate CreateNew(ILimitedRate other)
//    {
//        return new LimitedRate(other);
//    }

//    public ILimitedRate CreateNew(IMeasure numerator, string name, ValueType quantity, ILimit limit)
//    {
//        IDenominator denominator = DenominatorFactory.CreateNew(name, quantity);

//        return CreateNew(numerator, denominator, limit);
//    }

//    public ILimitedRate CreateNew(IMeasure numerator, Enum measureUnit, ValueType quantity, ILimit limit)
//    {
//        IDenominator denominator = DenominatorFactory.CreateNew(measureUnit, quantity);

//        return CreateNew(numerator, denominator, limit);
//    }

//    public ILimitedRate CreateNew(IMeasure numerator, string name, ILimit limit)
//    {
//        IDenominator denominator = DenominatorFactory.CreateNew(name);

//        return CreateNew(numerator, denominator, limit);
//    }

//    public ILimitedRate CreateNew(IMeasure numerator, Enum measureUnit, ILimit limit)
//    {
//        IDenominator denominator = DenominatorFactory.CreateNew(measureUnit);

//        return CreateNew(numerator, denominator, limit);
//    }

//    public ILimitedRate CreateNew(IMeasure numerator, IMeasurement measurement, ILimit limit)
//    {
//        return new LimitedRate(this, numerator, measurement, limit);
//    }

//    public ILimitedRate CreateNew(IMeasure numerator, IMeasurement measurement, ValueType quantity, ILimit limit)
//    {
//        IDenominator denominator = DenominatorFactory.CreateNew(measurement, quantity);

//        return CreateNew(numerator, denominator, limit);
//    }

//    public ILimitedRate CreateNew(IMeasure numerator, IDenominator denominator, ILimit limit)
//    {
//        return new LimitedRate(this, numerator, denominator, limit);
//    }

//    public ILimitedRate CreateNew(IRate rate, ILimit limit)
//    {
//        IMeasure numerator = NullChecked(rate, nameof(rate)).Numerator;
//        IDenominator denominator = rate.Denominator;

//        return CreateNew(numerator, denominator, limit);
//    }

//    public override ILimitedRate CreateNew(IMeasurable other)
//    {
//        _ = NullChecked(other, nameof(other));

//        if (other is ILimitedRate limitedRate) return CreateNew(limitedRate);

//        if (other is IRate rate) return CreateNew(rate, CreateLimit(rate.Denominator));

//        throw new ArgumentOutOfRangeException(nameof(other), other.GetType(), null);
//    }

//    public ILimit CreateLimit(IDenominator denominator)
//    {
//        return (ILimit)LimitFactory.CreateNew(denominator);
//    }

//    public override ILimitedRate CreateNew(IBaseMeasure numerator, MeasureUnitTypeCode measureUnitTypeCode)
//    {
//        string name = nameof(numerator);

//        if (NullChecked(numerator, name) is not IMeasure measure) throw ArgumentTypeOutOfRangeException(name, numerator);

//        ILimit limit = LimitFactory.CreateDefault(measureUnitTypeCode);

//        return new LimitedRate(this, measure, measureUnitTypeCode, limit);
//    }

//    public override IBaseRate CreateNew(IBaseMeasure numerator, Enum denominatorMeasureUnit)
//    {
//        string name = nameof(numerator);

//        if (NullChecked(numerator, name) is not IMeasure measure) throw ArgumentTypeOutOfRangeException(name, numerator);

//        name = nameof(denominatorMeasureUnit);

//        measure.ValidateMeasureUnit(denominatorMeasureUnit, name);

//        MeasureUnitTypeCode measureUnitTypeCode = MeasureUnitTypes.GetMeasureUnitTypeCode(denominatorMeasureUnit);
//        ILimit limit = LimitFactory.CreateDefault(measureUnitTypeCode);

//        return new LimitedRate(this, measure, denominatorMeasureUnit, limit);
//    }

//    public override IRate CreateNew(IRate other)
//    {
//        throw new NotImplementedException();
//    }
//    #endregion
//}
