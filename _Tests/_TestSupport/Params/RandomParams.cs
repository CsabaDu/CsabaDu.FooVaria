namespace CsabaDu.FooVaria.Tests.TestSupport.Params;

public class RandomParams
{
    #region Private fields
    private const int ItemCount = 1000;
    private static readonly Random Random = Random.Shared;
    private static readonly IEnumerable<MeasureUnitCode> CustomMeasureUnitCodes = MeasureUnitCodes.Where(x => x.IsCustomMeasureUnitCode());
    #endregion

    private static Enum[] RandomMeasureUnits => GetRandomItems(GetAllMeasureUnits().ToArray(), ItemCount);
    private static Enum[] RandomMeasureUnitsAndMeasureUnitCodes => GetRandomMeasureUnitAndMeasureUnitCodes();

    private static Enum[] GetRandomMeasureUnitAndMeasureUnitCodes()
    {
        IEnumerable<Enum> measureUnits = GetAllMeasureUnits().Union([.. MeasureUnitCodes]).ToArray();

        return GetRandomItems(measureUnits.ToArray(), ItemCount);
    }

    #region Public methods
    public MeasureUnitCode GetRandomMeasureUnitCode(MeasureUnitCode? excludedMeasureUnitCode = null)
    {
        MeasureUnitCode randomMeasureUnitCode = getRandomMeasureUnitCode();

        while (randomMeasureUnitCode == excludedMeasureUnitCode)
        {
            randomMeasureUnitCode = getRandomMeasureUnitCode();
        }

        return randomMeasureUnitCode;

        #region Local methods
        static MeasureUnitCode getRandomMeasureUnitCode()
        {
            return GetRandomItem(MeasureUnitCodes);
        }
        #endregion
    }

    public Enum GetRandomMeasureUnit(MeasureUnitCode? measureUnitTypeCode = null, Enum excludedMeasureUnit = null)
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

    public Enum GetRandomNotDefinedMeasureUnit(MeasureUnitCode? excludedMeasureUnitCode = null)
    {
        MeasureUnitCode measureUnitCode = GetRandomMeasureUnitCode(excludedMeasureUnitCode);

        return SampleParams.GetNotDefinedMeasureUnit(measureUnitCode);
    }

    public Enum GetRandomValidMeasureUnit(Enum excludedMeasureUnit = null)
    {
        object randomMeasureUnit = getRandomValidMeasureUnit();

        while (randomMeasureUnit.Equals(excludedMeasureUnit))
        {
            randomMeasureUnit = getRandomValidMeasureUnit();
        }

        return (Enum)randomMeasureUnit;

        #region Local methods
        object getRandomValidMeasureUnit()
        {
            return GetRandomItem(BaseMeasurement.ExchangeRateCollection.Keys);
        }
        #endregion
    }

    public string GetRandomParamName()
    {
        IEnumerable<string> paramNames = ParamNames.GetParamNames();

        return GetRandomItem(paramNames);
    }

    public Enum GetRandomNotUsedCustomMeasureUnit()
    {
        MeasureUnitCode customMeasureUnitCode = GetRandomItem(CustomMeasureUnitCodes);

        Enum measureUnit = GetRandomMeasureUnit(customMeasureUnitCode);

        while (BaseMeasurement.ExchangeRateCollection.ContainsKey(measureUnit))
        {
            measureUnit = GetRandomMeasureUnit(customMeasureUnitCode);
        }

        return measureUnit;
    }

    public MeasureUnitCode[] GetRandomCountRandomMeasureUnitCodes()
    {
        int count = Random.Next(1, MeasureUnitCodes.Length);

        return GetRandomItems(MeasureUnitCodes, count);
    }

    #region Private methods
    #region Static methods
    private static T GetRandomItem<T>(T[] items)
    {
        return Random.GetItems(items, 1)[0];
    }

    private static T[] GetRandomItems<T>(T[] items, int count)
    {
        return Random.GetItems(items, count);
    }


    private static T GetRandomItem<T>(IEnumerable<T> items)
    {
        return GetRandomItem(items.ToArray());
    }

    private static IEnumerable<T> GetRandomItems<T>(IEnumerable<T> items, int count)
    {
        int maxValue = items.Count();

        int[] randomIndexArray = GetRandomIndexArray(maxValue, count);

        foreach (int index in randomIndexArray)
        {
            yield return items.ElementAt(index);
        }
    }

    private static int[] GetRandomIndexArray(int maxValue, int count)
    {
        int[] randomIndexArray = new int[count];

        for (int i = 0; i  <= count; i++)
        {
            int item = Random.Next(maxValue);

            while (randomIndexArray.Contains(item))
            {
                item = Random.Next(1, maxValue);
            }

            randomIndexArray[i] = item;
        }

        return randomIndexArray;
    }
    #endregion
    #endregion

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

    //public Enum GetRandomNotDefaultConstantMeasureUnit(MeasureUnitCode? customMeasureUnitCode = null, Enum excludedMeasureUnit = null)
    //{
    //    if (!customMeasureUnitCode.HasValue)
    //    {
    //        customMeasureUnitCode = GetRandomMeasureUnitCode();
    //    }

    //    IEnumerable<object> constantMeasureUnits = GetConstantMeasureUnits()
    //        .Where(x => x.GetType().Equals(customMeasureUnitCode.Value.GetMeasureUnitType()))
    //        .Where(x => (int)x > 0)
    //        .Where(x => !x.Equals(excludedMeasureUnit));

    //    int randomindex = Random.Next(constantMeasureUnits.Count());

    //    return (Enum)constantMeasureUnits.ElementAt(randomindex);
    //}

    //public Type GetRandomMeasureUnitType(out MeasureUnitCode customMeasureUnitCode)
    //{
    //    customMeasureUnitCode = GetRandomMeasureUnitCode();

    //    return MeasureUnitTypes.GetMeasureUnitType(customMeasureUnitCode);
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

    //public Enum GetRandomNotUsedCustomMeasureUnit(MeasureUnitCode? customMeasureUnitCode = null)
    //{
    //    int randomIndex;

    //    if (!customMeasureUnitCode.HasValue)
    //    {
    //        randomIndex = Random.Next(2);
    //        customMeasureUnitCode = randomIndex switch
    //        {
    //            0 => MeasureUnitCode.Currency,
    //            1 => MeasureUnitCode.Pieces,

    //            _ => throw new InvalidOperationException(null),
    //        };
    //    }

    //    object randomMeasureUnit;

    //    do
    //    {
    //        Type measureUnitType = MeasureUnitTypes.GetMeasureUnitType(customMeasureUnitCode.Value);
    //        randomIndex = Random.Next(1, 1000);

    //        randomMeasureUnit = Enum.ToObject(measureUnitType, randomIndex);
    //    }
    //    while (GetValidMeasureUnits().Contains(randomMeasureUnit));

    //    return (Enum)randomMeasureUnit;
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

    //private IBaseMeasure GetDefaultBaseMeasure(IBaseMeasureFactory baseMeasureFactory, MeasureUnitCode customMeasureUnitCode)
    //{
    //    return baseMeasureFactory switch
    //    {
    //        DenominatorFactory denominatorFactory => denominatorFactory.CreateDefault(customMeasureUnitCode),
    //        MeasureFactory measureFactory => measureFactory.CreateDefault(customMeasureUnitCode),
    //        LimitFactory limitFactory => limitFactory.CreateDefault(customMeasureUnitCode),

    //        _ => throw new InvalidOperationException(null),
    //    };
    //}

    //public IMeasurable GetRandomDefaultMeasurable(MeasureUnitCode customMeasureUnitCode, IMeasurable excludedMeasurable = null)
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
    //            MeasurementFactory measurementFactory => measurementFactory.CreateDefault(customMeasureUnitCode),
    //            BaseMeasureFactory baseMeasureFactory => GetDefaultBaseMeasure(baseMeasureFactory, customMeasureUnitCode),
    //            RateFactory rateFactory => GetDefaultRate(rateFactory, customMeasureUnitCode),

    //            _ => throw new InvalidOperationException(null),
    //        };
    //    }
    //    #endregion
    //}
    #endregion
}
