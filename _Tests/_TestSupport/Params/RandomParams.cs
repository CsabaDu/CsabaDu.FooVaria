namespace CsabaDu.FooVaria.Tests.TestSupport.Params;

public static class RandomParams
{
    #region Private fields
    private static readonly Random Random = Random.Shared;
    private static T GetRandomItem<T>(T[] items)
    {
        return Random.GetItems(items, 1)[0];
    }

    private static T GetRandomItem<T>(IEnumerable<T> items)
    {
        return GetRandomItem(items.ToArray());
    }

    #endregion

    #region Public methods
    public static MeasureUnitCode GetRandomMeasureUnitCode(MeasureUnitCode? excludedMeasureUnitCode = null)
    {
        MeasureUnitCode randomMeasureUnitCode = getRandomMeasureUnitCode();

        while (randomMeasureUnitCode == excludedMeasureUnitCode)
        {
            randomMeasureUnitCode = getRandomMeasureUnitCode();
        }

        return randomMeasureUnitCode;

        #region Local methods
        MeasureUnitCode getRandomMeasureUnitCode()
        {
            return GetRandomItem(Measurable.MeasureUnitCodes);
        }
        #endregion
    }

    public static Enum GetRandomMeasureUnit(MeasureUnitCode? measureUnitTypeCode = null, Enum excludedMeasureUnit = null)
    {
        measureUnitTypeCode ??= GetRandomMeasureUnitCode();
        Enum randomMeasureUnit = getRandomMeasureUnit();

        while (randomMeasureUnit.Equals(excludedMeasureUnit))
        {
            randomMeasureUnit = getRandomMeasureUnit();
        }

        return randomMeasureUnit;

        #region Local methods
        Enum getRandomMeasureUnit()
        {
            return GetRandomItem(measureUnitTypeCode.Value.GetAllMeasureUnits());
        }
        #endregion
    }

    //public Enum GetRandomNotDefinedMeasureUnit()
    //{
    //    MeasureUnitCode measureUnitCode = GetRandomMeasureUnitCode();
    //    int count = MeasureUnitTypes.GetDefaultNames(measureUnitCode).Count();
    //    Type measureUnitType = MeasureUnitTypes.GetMeasureUnitType(measureUnitCode);

    //    return (Enum)Enum.ToObject(measureUnitType, count);
    //}

    //public Enum GetRandomValidMeasureUnit(Enum excludedMeasureUnit = null)
    //{
    //    IEnumerable<object> validMeasureUnits = GetValidMeasureUnits();
    //    Enum validMeasureUnit = getRandomValidMeasureUnit();

    //    if (excludedMeasureUnit == null) return validMeasureUnit;

    //    while (validMeasureUnit.Equals(excludedMeasureUnit))
    //    {
    //        validMeasureUnit = getRandomValidMeasureUnit();
    //    }

    //    return validMeasureUnit;

    //    #region Local methods
    //    Enum getRandomValidMeasureUnit()
    //    {
    //        int randomIndex = Random.Next(validMeasureUnits.Count());

    //        return (Enum)validMeasureUnits.ElementAt(randomIndex);
    //    }
    //    #endregion
    //}

    //public Enum GetRandomDefaultMeasureUnit(MeasureUnitCode? measurementUnitTypeCode = null)
    //{
    //    measurementUnitTypeCode = GetRandomMeasureUnitCode(measurementUnitTypeCode);

    //    return MeasureUnitTypes.GetDefaultMeasureUnit(measurementUnitTypeCode.Value);
    //}

    //public Enum GetRandomNotDefaultConstantMeasureUnit(MeasureUnitCode? measureUnitCode = null, Enum excludedMeasureUnit = null)
    //{
    //    if (!measureUnitCode.HasValue)
    //    {
    //        measureUnitCode = GetRandomMeasureUnitCode();
    //    }

    //    IEnumerable<object> constantMeasureUnits = GetConstantMeasureUnits()
    //        .Where(x => x.GetType().Equals(measureUnitCode.Value.GetMeasureUnitType()))
    //        .Where(x => (int)x > 0)
    //        .Where(x => !x.Equals(excludedMeasureUnit));

    //    int randomindex = Random.Next(constantMeasureUnits.Count());

    //    return (Enum)constantMeasureUnits.ElementAt(randomindex);
    //}

    //public Type GetRandomMeasureUnitType(out MeasureUnitCode measureUnitCode)
    //{
    //    measureUnitCode = GetRandomMeasureUnitCode();

    //    return MeasureUnitTypes.GetMeasureUnitType(measureUnitCode);
    //}

    //public IMeasurableFactory GetRandomMeasurableFactory()
    //{
    //    IMeasurementFactory measurementFactory = new MeasurementFactory();

    //    int randomIndex = Random.Next(3);

    //    return randomIndex switch
    //    {
    //        0 => measurementFactory,
    //        1 => GetRandomBaseMeasureFactory(measurementFactory),
    //        2 => GetRandomRateFactory(measurementFactory),

    //        _ => throw new InvalidOperationException(null),
    //    };
    //}

    //public Enum GetRandomNotUsedCustomMeasureUnit(MeasureUnitCode? measureUnitCode = null)
    //{
    //    int randomIndex;

    //    if (!measureUnitCode.HasValue)
    //    {
    //        randomIndex = Random.Next(2);
    //        measureUnitCode = randomIndex switch
    //        {
    //            0 => MeasureUnitCode.Currency,
    //            1 => MeasureUnitCode.Pieces,

    //            _ => throw new InvalidOperationException(null),
    //        };
    //    }

    //    object measureUnit;

    //    do
    //    {
    //        Type measureUnitType = MeasureUnitTypes.GetMeasureUnitType(measureUnitCode.Value);
    //        randomIndex = Random.Next(1, 1000);

    //        measureUnit = Enum.ToObject(measureUnitType, randomIndex);
    //    }
    //    while (GetValidMeasureUnits().Contains(measureUnit));

    //    return (Enum)measureUnit;
    //}

    //public IBaseMeasureFactory GetRandomBaseMeasureFactory(IMeasurementFactory measurementFactory)
    //{
    //    int randomIndex = Random.Next(3);

    //    return randomIndex switch
    //    {
    //        0 => new DenominatorFactory(measurementFactory),
    //        1 => new LimitFactory(measurementFactory),
    //        2 => new MeasureFactory(measurementFactory),

    //        _ => throw new InvalidOperationException(null),
    //    };
    //}

    //public IRateFactory GetRandomRateFactory(IMeasurementFactory measurementFactory)
    //{
    //    int randomIndex = Random.Next(2);
    //    IDenominatorFactory denominatorFactory = new DenominatorFactory(measurementFactory);
    //    ILimitFactory limitFactory = new LimitFactory(measurementFactory);

    //    return randomIndex switch
    //    {
    //        0 => new FlatRateFactory(denominatorFactory),
    //        1 => new LimitedRateFactory(denominatorFactory, limitFactory),

    //        _ => throw new InvalidOperationException(null),
    //    };
    //}

    //private IRate GetDefaultRate(IRateFactory rateFactory, MeasureUnitCode denominatorMeasureUnitCode)
    //{
    //    Enum denominatorDefaultMeasureUnit = denominatorMeasureUnitCode.GetDefaultMeasureUnit();

    //    return rateFactory switch
    //    {
    //        FlatRateFactory factory => factory.Create(getDefaultNumerator(), denominatorDefaultMeasureUnit),
    //        LimitedRateFactory factory => factory.Create(getDefaultNumerator(), denominatorDefaultMeasureUnit, getDefaultLimit()),

    //        _ => throw new InvalidOperationException(null),
    //    };

    //    #region Local methods
    //    IMeasure getDefaultNumerator()
    //    {
    //        IDenominatorFactory denominatorFactory = rateFactory.DenominatorFactory;
    //        IMeasurementFactory measurementFactory = denominatorFactory.MeasurementFactory;
    //        MeasureUnitCode numeratorMeasureUnitCode = GetRandomMeasureUnitCode(denominatorMeasureUnitCode);

    //        return new MeasureFactory(measurementFactory).CreateDefault(numeratorMeasureUnitCode);
    //    }

    //    ILimit getDefaultLimit()
    //    {
    //        MeasureUnitCode limitMeasureUnitCode = GetRandomMeasureUnitCode();

    //        return ((ILimitedRateFactory)rateFactory).LimitFactory.CreateDefault(limitMeasureUnitCode);
    //    }
    //    #endregion
    //}

    //private IBaseMeasure GetDefaultBaseMeasure(IBaseMeasureFactory baseMeasureFactory, MeasureUnitCode measureUnitCode)
    //{
    //    return baseMeasureFactory switch
    //    {
    //        DenominatorFactory denominatorFactory => denominatorFactory.CreateDefault(measureUnitCode),
    //        MeasureFactory measureFactory => measureFactory.CreateDefault(measureUnitCode),
    //        LimitFactory limitFactory => limitFactory.CreateDefault(measureUnitCode),

    //        _ => throw new InvalidOperationException(null),
    //    };
    //}

    //public IMeasurable GetRandomDefaultMeasurable(MeasureUnitCode measureUnitCode, IMeasurable excludedMeasurable = null)
    //{
    //    IMeasurableFactory measurableFactory = GetRandomMeasurableFactory();
    //    IMeasurable randomDefaultMeasurable = getDefaultMeasurable();

    //    if (excludedMeasurable == null) return randomDefaultMeasurable;

    //    while (excludedMeasurable.GetType() == randomDefaultMeasurable.GetType())
    //    {
    //        randomDefaultMeasurable = getDefaultMeasurable();
    //    }

    //    return randomDefaultMeasurable;

    //    #region Local methods
    //    IMeasurable getDefaultMeasurable()
    //    {
    //        return measurableFactory switch
    //        {
    //            MeasurementFactory measurementFactory => measurementFactory.CreateDefault(measureUnitCode),
    //            BaseMeasureFactory baseMeasureFactory => GetDefaultBaseMeasure(baseMeasureFactory, measureUnitCode),
    //            RateFactory rateFactory => GetDefaultRate(rateFactory, measureUnitCode),

    //            _ => throw new InvalidOperationException(null),
    //        };
    //    }
    //    #endregion
    //}
    #endregion
}
