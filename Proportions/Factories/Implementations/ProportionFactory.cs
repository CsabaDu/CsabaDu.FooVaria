namespace CsabaDu.FooVaria.Proportions.Factories.Implementations;

public sealed class ProportionFactory(IMeasureFactory measureFactory) : SimpleRateFactory, IProportionFactory
{
    #region Properties
    public IMeasureFactory MeasureFactory { get; init; } = NullChecked(measureFactory, nameof(measureFactory));
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

        if (numeratorMeasureUnit is MeasureUnitCode measureUnitCode)
        {
            numeratorMeasureUnit = measureUnitCode.GetDefaultMeasureUnit();
        }

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

    public IProportion<TDEnum> Create<TDEnum>(MeasureUnitCode numeratorCode, decimal defaultQuantity, TDEnum denominatorMeasureUnit)
        where TDEnum : struct, Enum
    {
        Enum numeratorMeasureUnit = numeratorCode.GetDefaultMeasureUnit();

        return Create(numeratorMeasureUnit, defaultQuantity, denominatorMeasureUnit);
    }

    public IProportion<TDEnum> Create<TDEnum>(IBaseMeasure numerator, TDEnum denominatorMeasureUnit)
        where TDEnum : struct, Enum
    {
        Enum numeratorMeasureUnit = NullChecked(numerator, nameof(numerator)).GetMeasureUnit();
        decimal quantity = numerator.GetDefaultQuantity() / GetExchangeRate(numeratorMeasureUnit, nameof(numerator));

        return Create(numeratorMeasureUnit, quantity, denominatorMeasureUnit);
    }
    #endregion

    #region IProportion
    public IProportion Create(Enum numeratorMeasureUnit, ValueType quantity, Enum denominatorMeasureUnit)
    {
        string paramName = nameof(denominatorMeasureUnit);

        if (denominatorMeasureUnit is MeasureUnitCode measureUnitCode)
        {
            denominatorMeasureUnit = measureUnitCode.GetDefaultMeasureUnit();
        }

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
        return (IProportion)CreateBaseRate(numerator, denominator);
    }

    public IProportion Create(IBaseRate baseRate)
    {
        MeasureUnitCode numeratorCode = NullChecked(baseRate, nameof(baseRate)).GetNumeratorCode();
        MeasureUnitCode denominatorCode = baseRate.GetDenominatorCode();
        decimal defaultQuantity = baseRate.GetDefaultQuantity();

        return Create(numeratorCode, defaultQuantity, denominatorCode);
    }

    public IProportion Create(MeasureUnitCode numeratorCode, decimal defaultQuantity, MeasureUnitCode denominatorCode)
    {
        return Create(numeratorCode, defaultQuantity, denominatorCode);
    }

    public override IBaseRate CreateBaseRate(IBaseRate baseRate)
    {
        return Create(baseRate);
    }

    public override ISimpleRate CreateSimpleRate(MeasureUnitCode numeratorCode, decimal defaultQuantity, MeasureUnitCode denominatorCode)
    {
        return Create(numeratorCode, defaultQuantity, denominatorCode);
    }
    #endregion
    #endregion

    #region Private methods
    #region Static methods

    #endregion
    #endregion
}
