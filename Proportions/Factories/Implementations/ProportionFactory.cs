namespace CsabaDu.FooVaria.Proportions.Factories.Implementations;

public sealed class ProportionFactory : IProportionFactory
{
    #region Constructors
    public ProportionFactory(IMeasureFactory measureFactory)
    {
        MeasureFactory = NullChecked(measureFactory, nameof(measureFactory));
    }
    #endregion

    #region Properties
    public IMeasureFactory MeasureFactory { get; init; }
    #endregion

    #region Public methods
    #region IProportion<TNEnum, TDEnum>
    public IProportion<TNEnum, TDEnum> Create<TNEnum, TDEnum>(TNEnum numeratorMeasureUnit, ValueType quantity, TDEnum denominatorMeasureUnit)
        where TNEnum : struct, Enum
        where TDEnum : struct, Enum
    {
        return new Proportion<TNEnum, TDEnum>(this, numeratorMeasureUnit, quantity, denominatorMeasureUnit);
    }
    #endregion

    #region IProportion<TDEnum>
    public IProportion<TDEnum> Create<TDEnum>(Enum numeratorMeasureUnit, ValueType quantity, TDEnum denominatorMeasureUnit)
        where TDEnum : struct, Enum
    {
        string paramName = nameof(numeratorMeasureUnit);

        return NullChecked(numeratorMeasureUnit, paramName) switch
        {
            AreaUnit areaUnit => create(areaUnit),
            Currency currency => create(currency),
            DistanceUnit distanceUnit => create(distanceUnit),
            ExtentUnit extentUnit => create(extentUnit),
            Pieces pieces => create(pieces),
            TimePeriodUnit timePeriodUnit => create(timePeriodUnit),
            VolumeUnit volumeUnit => create(volumeUnit),
            WeightUnit weightUnit => create(weightUnit),

            _ => throw InvalidMeasureUnitEnumArgumentException(numeratorMeasureUnit),
        };

        #region Local methods
        IProportion<TNEnum, TDEnum> create<TNEnum>(TNEnum measureUnit)
            where TNEnum : struct, Enum
        {
            return Create(measureUnit, quantity, denominatorMeasureUnit);
        }
        #endregion
    }

    public IProportion<TDEnum> Create<TDEnum>(MeasureUnitCode numeratorMeasureUnitCode, decimal defaultQuantity, TDEnum denominatorMeasureUnit)
        where TDEnum : struct, Enum
    {
        Enum numeratorMeasureUnit = numeratorMeasureUnitCode.GetDefaultMeasureUnit();

        return Create(numeratorMeasureUnit, defaultQuantity, denominatorMeasureUnit);
    }

    public IProportion<TDEnum> Create<TDEnum>(IBaseMeasure numerator, TDEnum denominatorMeasureUnit)
        where TDEnum : struct, Enum
    {
        Enum numeratorMeasureUnit = NullChecked(numerator, nameof(numerator)).GetMeasureUnit();
        decimal quantity = numerator.GetDefaultQuantity() / GetExchangeRate(numeratorMeasureUnit);

        return Create(numeratorMeasureUnit, quantity, denominatorMeasureUnit);
    }
    #endregion

    #region IProportion
    public IProportion Create(Enum numeratorMeasureUnit, ValueType quantity, Enum denominatorMeasureUnit)
    {
        string paramName = nameof(denominatorMeasureUnit);

        return NullChecked(denominatorMeasureUnit, paramName) switch
        {
            AreaUnit areaUnit => create(areaUnit),
            Currency currency => create(currency),
            DistanceUnit distanceUnit => create(distanceUnit),
            ExtentUnit extentUnit => create(extentUnit),
            Pieces pieces => create(pieces),
            TimePeriodUnit timePeriodUnit => create(timePeriodUnit),
            VolumeUnit volumeUnit => create(volumeUnit),
            WeightUnit weightUnit => create(weightUnit),

            _ => throw InvalidMeasureUnitEnumArgumentException(denominatorMeasureUnit),
        };

        #region Local methods
        IProportion<TDEnum> create<TDEnum>(TDEnum measureUnit)
            where TDEnum : struct, Enum
        {
            return Create(numeratorMeasureUnit, quantity, measureUnit);
        }
        #endregion
    }

    public IProportion Create(IBaseMeasure numerator, IBaseMeasure denominator)
    {
        var (numeratorMeasureUnit, quantity) = GetNumeratorComponents(numerator);
        Enum denominatorMeasureUnit = NullChecked(denominator, nameof(denominator)).GetMeasureUnit();
        quantity *= GetExchangeRate(denominatorMeasureUnit);
        quantity /= denominator!.GetDefaultQuantity();

        return Create(numeratorMeasureUnit, quantity, denominatorMeasureUnit);
    }

    public IProportion Create(IBaseRate baseRate)
    {
        MeasureUnitCode denominatorMeasureUnitCode = NullChecked(baseRate, nameof(baseRate)).MeasureUnitCode;
        decimal defaultQuantity = baseRate.DefaultQuantity;
        MeasureUnitCode numeratorMeasureUnitCode = baseRate.GetNumeratorMeasureUnitCode();

        return Create(numeratorMeasureUnitCode, defaultQuantity, denominatorMeasureUnitCode);
    }

    public IProportion Create(MeasureUnitCode numeratorMeasureUnitCode, decimal defaultQuantity, MeasureUnitCode denominatorMeasureUnitCode)
    {
        Enum numeratorMeasureUnit = numeratorMeasureUnitCode.GetDefaultMeasureUnit();
        Enum denominatorMeasureUnit = denominatorMeasureUnitCode.GetDefaultMeasureUnit();

        return Create(numeratorMeasureUnit, defaultQuantity, denominatorMeasureUnit);
    }

    public IBaseRate CreateBaseRate(IBaseMeasure numerator, IBaseMeasurement denominatorMeasurement)
    {
        var (numeratorMeasureUnit, quantity) = GetNumeratorComponents(numerator);
        quantity *= denominatorMeasurement.GetExchangeRate();
        Enum denominatorMeasureUnit = (NullChecked(denominatorMeasurement, nameof(denominatorMeasurement)).GetMeasureUnit());

        return Create(numeratorMeasureUnit, quantity, denominatorMeasureUnit);
    }

    public IBaseRate CreateBaseRate(params IBaseMeasure[] baseMeasures)
    {
        int count = baseMeasures?.Length ?? 0;

        return count switch
        {
            1 => createProportionFrom1Param(),
            2 or 3 => createProportionFrom2or3Params(),

            _ => throw CountArgumentOutOfRangeException(count, nameof(baseMeasures)),
        };

        #region Local methods
        IProportion createProportionFrom1Param()
        {
            if (baseMeasures![0] is IBaseRate baseRate) return Create(baseRate);

            throw exception();
        }

        IProportion createProportionFrom2or3Params()
        {
            if (baseMeasures is IBaseMeasure[] rateComponents) return Create(rateComponents[0], rateComponents[1]);

            throw exception();
        }

        ArgumentOutOfRangeException exception()
        {
            return ArgumentTypeOutOfRangeException(nameof(baseMeasures), baseMeasures!);
        }
        #endregion
    }

    public IBaseRate CreateBaseRate(IBaseMeasure numerator, MeasureUnitCode denominatorMeasureUnitCode)
    {
        var (numeratorMeasureUnit, quantity) = GetNumeratorComponents(numerator);
        Enum denominatorMeasureUnit = denominatorMeasureUnitCode.GetDefaultMeasureUnit();

        return Create(numeratorMeasureUnit, quantity, denominatorMeasureUnit);
    }

    public IBaseRate CreateBaseRate(IBaseMeasure numerator, Enum denominatorMeasureUnit)
    {
        var (numeratorMeasureUnit, quantity) = GetNumeratorComponents(numerator);
        quantity *= GetExchangeRate(denominatorMeasureUnit);

        return Create(numeratorMeasureUnit, quantity, denominatorMeasureUnit);
    }
    #endregion

    #region IBaseMeasure
    public IBaseMeasure CreateBaseMeasure(Enum measureUnit, ValueType quantity)
    {
        return MeasureFactory.Create(measureUnit, quantity);
    }
    #endregion
    #endregion

    #region Private methods
    #region Static methods
    private static (Enum MeasureUnit, decimal Quantity) GetNumeratorComponents(IBaseMeasure numerator)
    {

        Enum nmeasureUnit = NullChecked(numerator, nameof(numerator)) is IBaseMeasure rateComponent ?
            rateComponent.GetMeasureUnit()
            : throw ArgumentTypeOutOfRangeException(nameof(numerator), numerator);

        decimal quantity = rateComponent.GetDefaultQuantity() / GetExchangeRate(nmeasureUnit);

        return (nmeasureUnit, quantity);
    }
    #endregion
    #endregion
}
