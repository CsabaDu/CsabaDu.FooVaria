using CsabaDu.FooVaria.Measurables.Factories.Implementations;
using CsabaDu.FooVaria.Measurables.Markers;

namespace CsabaDu.FooVaria.Tests.TestSupport.Params;

public class RandomParams
{
    #region Private fields
    private static readonly Random Random = Random.Shared;
    #endregion

    #region Public methods
    public MeasureUnitTypeCode GetRandomMeasureUnitTypeCode(MeasureUnitTypeCode? excludedMeasureUnitTypeCode = null)
    {
        int randomIndex = getRandomIndex();

        if (!excludedMeasureUnitTypeCode.HasValue) return getRandomMeasureUnitTypeCode();

        while (randomIndex == (int)excludedMeasureUnitTypeCode)
        {
            randomIndex = getRandomIndex();
        }

        return getRandomMeasureUnitTypeCode();

        #region Local methods
        static int getRandomIndex()
        {
            return Random.Next(SampleParams.MeasureUnitTypeCodeCount);
        }

        MeasureUnitTypeCode getRandomMeasureUnitTypeCode()
        {
            return (MeasureUnitTypeCode)randomIndex;
        }
        #endregion
    }

    public Enum GetRandomMeasureUnit(MeasureUnitTypeCode? measureUnitTypeCode = null, Enum excludedMeasureUnit = null)
    {
        int count = getAllMeasureUnits().Count();
        int randomIndex = getRandomIndex();

        if (excludedMeasureUnit == null) return getRandomMeasureUnit();

        while (randomIndex == getExcludedMeasureUnitIndex())
        {
            randomIndex = getRandomIndex();
        }

        return getRandomMeasureUnit();

        #region Local methods
        int getRandomIndex()
        {
            return Random.Next(count);
        }

        int getExcludedMeasureUnitIndex()
        {
            for (int i = 0; i < count; i++)
            {
                Enum other = getAllMeasureUnits().ElementAt(i);

                if (excludedMeasureUnit.Equals(other)) return i;
            }

            throw ExceptionMethods.InvalidMeasureUnitEnumArgumentException(excludedMeasureUnit);
        }

        Enum getRandomMeasureUnit()
        {
            return getAllMeasureUnits().ElementAt(randomIndex);
        }

        IEnumerable<Enum> getAllMeasureUnits()
        {
            if (measureUnitTypeCode.HasValue) return MeasureUnitTypes.GetAllMeasureUnits(measureUnitTypeCode.Value);

            return MeasureUnitTypes.GetAllMeasureUnits();
        }
        #endregion
    }

    public Enum GetRandomNotDefinedMeasureUnit()
    {
        MeasureUnitTypeCode measureUnitTypeCode = GetRandomMeasureUnitTypeCode();
        int count = MeasureUnitTypes.GetDefaultNames(measureUnitTypeCode).Count();
        Type measureUnitType = MeasureUnitTypes.GetMeasureUnitType(measureUnitTypeCode);

        return (Enum)Enum.ToObject(measureUnitType, count);
    }

    public Enum GetRandomDefaultMeasureUnit()
    {
        MeasureUnitTypeCode measurementUnitTypeCode = GetRandomMeasureUnitTypeCode();

        return MeasureUnitTypes.GetDefaultMeasureUnit(measurementUnitTypeCode);
    }

    public Type GetRandomMeasureUnitType(out MeasureUnitTypeCode measureUnitTypeCode)
    {
        measureUnitTypeCode = GetRandomMeasureUnitTypeCode();

        return MeasureUnitTypes.GetMeasureUnitType(measureUnitTypeCode);
    }

    public IMeasurableFactory GetRandomMeasurableFactory()
    {
        IMeasurementFactory measurementFactory = new MeasurementFactory();

        int randomIndex = Random.Next(0, 6);

        return randomIndex switch
        {
            0 => measurementFactory,
            1 => getDenominatorFactory(),
            2 => getMeasureFactory(),
            3 => getLimitFactory(),
            4 => getFlatRateFactory(),
            5 => getLimitedRateFactory(),

            _ => throw new InvalidOperationException(null),
        };

        IDenominatorFactory getDenominatorFactory() => new DenominatorFactory(measurementFactory);
        ILimitFactory getLimitFactory() => new LimitFactory(measurementFactory);
        IMeasureFactory getMeasureFactory() => new MeasureFactory(measurementFactory);
        IFlatRateFactory getFlatRateFactory() => new FlatRateFactory(getDenominatorFactory());
        ILimitedRateFactory getLimitedRateFactory() => new LimitedRateFactory(getDenominatorFactory(), getLimitFactory());
    }

    public IRate GetRandomDefaultRate(IRateFactory rateFactory, MeasureUnitTypeCode denominatorMeasureUnitTypeCode)
    {
        Enum denominatorDefaultMeasureUnit = denominatorMeasureUnitTypeCode.GetDefaultMeasureUnit();

        return rateFactory switch
        {
            FlatRateFactory factory => factory.Create(getDefaultNumerator(), denominatorDefaultMeasureUnit),
            LimitedRateFactory factory => factory.Create(getDefaultNumerator(), denominatorDefaultMeasureUnit, getDefaultLimit()),

            _ => throw new InvalidOperationException(null),
        };

        #region Local methods
        IMeasure getDefaultNumerator()
        {
            IDenominatorFactory denominatorFactory = rateFactory.DenominatorFactory;
            IMeasurementFactory measurementFactory = denominatorFactory.MeasurementFactory;
            MeasureUnitTypeCode numeratorMeasureUnitTypeCode = GetRandomMeasureUnitTypeCode(denominatorMeasureUnitTypeCode);

            return new MeasureFactory(measurementFactory).CreateDefault(numeratorMeasureUnitTypeCode);
        }

        ILimit getDefaultLimit()
        {
            MeasureUnitTypeCode limitMeasureUnitTypeCode = GetRandomMeasureUnitTypeCode();
            
            return ((ILimitedRateFactory)rateFactory).LimitFactory.CreateDefault(limitMeasureUnitTypeCode);
        }
        #endregion
    }

    public IMeasurable GetRandomDefaultMeasurable(IMeasurable excludedMeasurable = null)
    {
        IMeasurableFactory measurableFactory = GetRandomMeasurableFactory();
        MeasureUnitTypeCode measureUnitTypeCode = GetRandomMeasureUnitTypeCode();
        IMeasurable randomDefaultMeasurable = getDefaultMeasurable();

        if (excludedMeasurable == null) return randomDefaultMeasurable;

        while (excludedMeasurable.GetType() == randomDefaultMeasurable.GetType())
        {
            randomDefaultMeasurable = getDefaultMeasurable();
        }

        return randomDefaultMeasurable;

        #region Local methods
        IMeasurable getDefaultMeasurable()
        {
            return measurableFactory switch
            {
                MeasurementFactory measurementFactory => measurementFactory.CreateDefault(measureUnitTypeCode),
                BaseMeasureFactory baseMeasureFactory => getDefaultBaseMeasure(baseMeasureFactory),
                RateFactory rateFactory => GetRandomDefaultRate(rateFactory, measureUnitTypeCode),

                _ => throw new InvalidOperationException(null),
            };
        }

        IBaseMeasure getDefaultBaseMeasure(IBaseMeasureFactory baseMeasureFactory)
        {
            return baseMeasureFactory switch
            {
                DenominatorFactory denominatorFactory => denominatorFactory.CreateDefault(measureUnitTypeCode),
                MeasureFactory measureFactory => measureFactory.CreateDefault(measureUnitTypeCode),
                LimitFactory limitFactory => limitFactory.CreateDefault(measureUnitTypeCode),

                _ => throw new InvalidOperationException(null),
            };
        }
        #endregion
    }
    #endregion
}
