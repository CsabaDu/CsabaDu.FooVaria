
namespace CsabaDu.FooVaria.Rates.Types.Implementations;

internal sealed class FlatRate : Rate, IFlatRate
{
    internal FlatRate(IFlatRate other) : base(other)
    {
    }

    internal FlatRate(IFlatRateFactory factory, IRate baseRate) : base(factory, baseRate)
    {
    }

    internal FlatRate(IFlatRateFactory factory, IMeasure numerator, MeasureUnitTypeCode denominatorMeasureUnitTypeCode) : base(factory, numerator, denominatorMeasureUnitTypeCode)
    {
    }

    internal FlatRate(IFlatRateFactory factory, IMeasure numerator, Enum denominatorMeasureUnit) : base(factory, numerator, denominatorMeasureUnit)
    {
    }

    internal FlatRate(IFlatRateFactory factory, IMeasure numerator, IDenominator denominator) : base(factory, numerator, denominator)
    {
    }

    public IFlatRate Add(IFlatRate? other)
    {
        throw new NotImplementedException();
    }

    public IFlatRate Divide(decimal divisor)
    {
        throw new NotImplementedException();
    }

    public IFlatRate GetFlatRate(IMeasure numerator, string name, decimal quantity)
    {
        throw new NotImplementedException();
    }

    public IFlatRate GetFlatRate(IMeasure numerator, string name)
    {
        throw new NotImplementedException();
    }

    public IFlatRate GetFlatRate(IMeasure numerator, Enum measureUnit, decimal quantity)
    {
        throw new NotImplementedException();
    }

    public IFlatRate GetFlatRate(IMeasure numerator, Enum measureUnit)
    {
        throw new NotImplementedException();
    }

    public IFlatRate GetFlatRate(IMeasure numerator, IDenominator denominator)
    {
        throw new NotImplementedException();
    }

    public IFlatRate GetFlatRate(IMeasure numerator)
    {
        throw new NotImplementedException();
    }

    public IFlatRate GetFlatRate(IBaseRate baseRate)
    {
        return NullChecked(baseRate, nameof(baseRate)) switch
        {
            FlatRate flatRate => GetNew(flatRate),
            Rate rate => GetFactory().Create(rate),

            _ => getFlatRate(),
        };

        #region Local methods
        IFlatRate getFlatRate()
        {
            MeasureUnitTypeCode numeratorMeasureUnitTypeCode = getMeasureUnitTypeCode(RateComponentCode.Numerator);
            decimal defaultQuantity = baseRate.DefaultQuantity;
            MeasureUnitTypeCode denominatorMeasureUnitTypeCode = getMeasureUnitTypeCode(RateComponentCode.Denominator);

            return (IFlatRate)GetBaseRate(numeratorMeasureUnitTypeCode, defaultQuantity, denominatorMeasureUnitTypeCode);
        }

        MeasureUnitTypeCode getMeasureUnitTypeCode(RateComponentCode rateComponentCode)
        {
            return baseRate[rateComponentCode]!.Value;
        }
        #endregion
    }

    public override ILimit? GetLimit()
    {
        return null;
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
        throw new NotImplementedException();
    }

    public override IFlatRateFactory GetFactory()
    {
        return (IFlatRateFactory)Factory;
    }
}


//{
//    #region Constructors
//    internal FlatRate(IFlatRate other) : base(other)
//    {
//    }

//    internal FlatRate(IFlatRateFactory factory, IMeasure numerator, IDenominator denominator) : base(factory, numerator, denominator)
//    {
//    }

//    internal FlatRate(IFlatRateFactory factory, IMeasure numerator, Enum measureUnit) : base(factory, numerator, measureUnit)
//    {
//    }

//    internal FlatRate(IFlatRateFactory factory, IMeasure numerator, MeasureUnitTypeCode measureUnitTypeCode) : base(factory, numerator, measureUnitTypeCode)
//    {
//    }

//    internal FlatRate(IFlatRateFactory factory, IMeasure numerator, IMeasurement measurement) : base(factory, numerator, measurement)
//    {
//    }
//    #endregion

//    #region Public methods
//    public IFlatRate Add(IFlatRate? other)
//    {
//        return GetSum(other, SummingMode.Add);
//    }

//    public IFlatRate Divide(decimal divisor)
//    {
//        return GetFlatRate(Numerator.Divide(divisor));
//    }

//    public IFlatRate GetFlatRate(IMeasure numerator, string name, decimal quantity)
//    {
//        return GetFactory().CreateNew(numerator, name, quantity);
//    }

//    public IFlatRate GetFlatRate(IMeasure numerator, string name)
//    {
//        return GetFactory().CreateNew(numerator, name);
//    }

//    public IFlatRate GetFlatRate(IMeasure numerator, Enum measureUnit, decimal quantity)
//    {
//        return GetFactory().CreateNew(numerator, measureUnit, quantity);
//    }

//    public IFlatRate GetFlatRate(IMeasure numerator, Enum measureUnit)
//    {
//        return GetFactory().CreateNew(numerator, measureUnit);
//    }

//    public IFlatRate GetFlatRate(IMeasure numerator, IMeasurement measurement, decimal quantity)
//    {
//        return GetFactory().CreateNew(numerator, measurement, quantity);
//    }

//    public IFlatRate GetFlatRate(IMeasure numerator, IMeasurement measurement)
//    {
//        return GetFactory().CreateNew(numerator, measurement);
//    }

//    public IFlatRate GetFlatRate(IMeasure numerator, IDenominator denominator)
//    {
//        return GetFactory().CreateNew(numerator, denominator);
//    }

//    public IFlatRate GetFlatRate(IMeasure numerator)
//    {
//        return GetFactory().CreateNew(numerator, Denominator);
//    }

//    public IFlatRate GetFlatRate(IRate rate)
//    {
//        return (IFlatRate)GetFactory().CreateNew(rate);
//    }

//    public IFlatRate GetFlatRate(IFlatRate other)
//    {
//        return GetFactory().CreateNew(other);
//    }

//    public IFlatRate Denominate(decimal multiplier)
//    {
//        return GetFlatRate(Numerator.Denominate(multiplier));
//    }

//    //public IMeasure Denominate(IMeasure multiplier)
//    //{
//    //    if (NullChecked(multiplier, nameof(multiplier)).IsExchangeableTo(MeasureUnitTypeCode))
//    //    {
//    //        decimal quantity = Numerator.GetDecimalQuantity() / Denominator.DefaultQuantity * multiplier.DefaultQuantity;

//    //        return multiplier.GetMeasure(quantity, Numerator.Measurement);
//    //    }

//    //    throw new ArgumentOutOfRangeException(nameof(multiplier), multiplier.MeasureUnitTypeCode, null);
//    //}

//    public IFlatRate Subtract(IFlatRate? other)
//    {
//        return GetSum(other, SummingMode.Subtract);
//    }

//    #region Override methods
//    public override bool Equals(IBaseRate? other)
//    {
//        return other is IFlatRate
//            && base.Equals(other);
//    }

//    public override IFlatRateFactory GetFactory()
//    {
//        return (IFlatRateFactory)Factory;
//    }

//    public override IFlatRate GetRate(IMeasure numerator, IDenominator denominator, ILimit? limit)
//    {
//        return GetFlatRate(numerator, denominator);
//    }

//    //public override IFlatRate GetMeasurable(IDefaultMeasurable other)
//    //{
//    //    return (IFlatRate)GetFactory().CreateNew(other);
//    //}
//    #endregion
//    #endregion

//    #region Private methods
//    private IFlatRate GetSum(IFlatRate? other, SummingMode summingMode)
//    {
//        if (other == null) return GetFlatRate(this);

//        if (!other.Denominator.IsExchangeableTo(MeasureUnitTypeCode)) throw exception(other.MeasureUnitTypeCode);

//        if (other.Numerator.TryExchangeTo(Numerator.Measurement.GetMeasureUnit(), out IRateComponent? exchanged) && exchanged is IMeasure numerator)
//        {
//            return GetFlatRate(getNumeratorSum());
//        }

//        throw exception(other.GetNumeratorMeasureUnitTypeCode());

//        #region Local methods
//        IMeasure getNumeratorSum()
//        {
//            return summingMode switch
//            {
//                SummingMode.Add => Numerator.Add(numerator),
//                SummingMode.Subtract => Numerator.Subtract(numerator),

//                _ => throw new InvalidOperationException(null),
//            };
//        }

//        static ArgumentOutOfRangeException exception(MeasureUnitTypeCode measureUnitTypeCode)
//        {
//            return new ArgumentOutOfRangeException(nameof(other), measureUnitTypeCode, null);
//        }
//        #endregion
//    }

//    public void ValidateQuantityTypeCode(TypeCode quantityTypeCode, string paramName)
//    {
//        throw new NotImplementedException();
//    }
//    #endregion
//}
