using CsabaDu.FooVaria.Measurables.Factories.Implementations;
using static CsabaDu.FooVaria.Measurables.Statics.MeasureUnits;

namespace CsabaDu.FooVaria.Tests.TestSupport.Params;

public class RandomParams
{
    #region Private fields
    private static readonly Random Random = Random.Shared;
    private IMeasurement Measurement { get; set; }
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

    public Enum GetRandomValidMeasureUnit(Enum excludedMeasureUnit = null)
    {
        IEnumerable<object> validMeasureUnits = GetValidMeasureUnits();
        Enum validMeasureUnit = getRandomValidMeasureUnit();

        if (excludedMeasureUnit == null) return validMeasureUnit;

        while (validMeasureUnit.Equals(excludedMeasureUnit))
        {
            validMeasureUnit = getRandomValidMeasureUnit();
        }

        return validMeasureUnit;

        #region Local methods
        Enum getRandomValidMeasureUnit()
        {
            int randomIndex = Random.Next(validMeasureUnits.Count());

            return (Enum)validMeasureUnits.ElementAt(randomIndex);
        }
        #endregion
    }

    public Enum GetRandomDefaultMeasureUnit(MeasureUnitTypeCode? measurementUnitTypeCode = null)
    {
        measurementUnitTypeCode = GetRandomMeasureUnitTypeCode(measurementUnitTypeCode);

        return MeasureUnitTypes.GetDefaultMeasureUnit(measurementUnitTypeCode.Value);
    }

    public Enum GetRandomNotDefaultConstantMeasureUnit(MeasureUnitTypeCode? measureUnitTypeCode = null, Enum excludedMeasureUnit = null)
    {
        if (!measureUnitTypeCode.HasValue)
        {
            measureUnitTypeCode = GetRandomMeasureUnitTypeCode();
        }

        IEnumerable<object> constantMeasureUnits = GetConstantMeasureUnits()
            .Where(x => x.GetType().Equals(measureUnitTypeCode.Value.GetMeasureUnitType()))
            .Where(x => (int)x > 0)
            .Where(x => !x.Equals(excludedMeasureUnit));

        int randomindex = Random.Next(constantMeasureUnits.Count());

        return (Enum)constantMeasureUnits.ElementAt(randomindex);
    }

    public Type GetRandomMeasureUnitType(out MeasureUnitTypeCode measureUnitTypeCode)
    {
        measureUnitTypeCode = GetRandomMeasureUnitTypeCode();

        return MeasureUnitTypes.GetMeasureUnitType(measureUnitTypeCode);
    }

    public IMeasurableFactory GetRandomMeasurableFactory()
    {
        IMeasurementFactory measurementFactory = new MeasurementFactory();

        int randomIndex = Random.Next(3);

        return randomIndex switch
        {
            0 => measurementFactory,
            1 => GetRandomBaseMeasureFactory(measurementFactory),
            2 => GetRandomRateFactory(measurementFactory),

            _ => throw new InvalidOperationException(null),
        };
    }

    public Enum GetRandomNotUsedCustomMeasureUnit(MeasureUnitTypeCode? measureUnitTypeCode = null)
    {
        int randomIndex;

        if (!measureUnitTypeCode.HasValue)
        {
            randomIndex = Random.Next(2);
            measureUnitTypeCode = randomIndex switch
            {
                0 => MeasureUnitTypeCode.Currency,
                1 => MeasureUnitTypeCode.Pieces,

                _ => throw new InvalidOperationException(null),
            };
        }

        object measureUnit;

        do
        {
            Type measureUnitType = MeasureUnitTypes.GetMeasureUnitType(measureUnitTypeCode.Value);
            randomIndex = Random.Next(1, 1000);

            measureUnit = Enum.ToObject(measureUnitType, randomIndex);
        }
        while (GetValidMeasureUnits().Contains(measureUnit));

        return(Enum)measureUnit;
    }

    public IBaseMeasureFactory GetRandomBaseMeasureFactory(IMeasurementFactory measurementFactory)
    {
        int randomIndex = Random.Next(3);

        return randomIndex switch
        {
            0 => new DenominatorFactory(measurementFactory),
            1 => new LimitFactory(measurementFactory),
            2 => new MeasureFactory(measurementFactory),

            _ => throw new InvalidOperationException(null),
        };
    }

    public IRateFactory GetRandomRateFactory(IMeasurementFactory measurementFactory)
    {
        int randomIndex = Random.Next(2);
        IDenominatorFactory denominatorFactory = new DenominatorFactory(measurementFactory);
        ILimitFactory limitFactory = new LimitFactory(measurementFactory);

        return randomIndex switch
        {
            0 => new FlatRateFactory(denominatorFactory),
            1 => new LimitedRateFactory(denominatorFactory, limitFactory),

            _ => throw new InvalidOperationException(null),
        };
    }

    private IRate GetDefaultRate(IRateFactory rateFactory, MeasureUnitTypeCode denominatorMeasureUnitTypeCode)
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

    private IBaseMeasure GetDefaultBaseMeasure(IBaseMeasureFactory baseMeasureFactory, MeasureUnitTypeCode measureUnitTypeCode)
    {
        return baseMeasureFactory switch
        {
            DenominatorFactory denominatorFactory => denominatorFactory.CreateDefault(measureUnitTypeCode),
            MeasureFactory measureFactory => measureFactory.CreateDefault(measureUnitTypeCode),
            LimitFactory limitFactory => limitFactory.CreateDefault(measureUnitTypeCode),

            _ => throw new InvalidOperationException(null),
        };
    }

    public IMeasurable GetRandomDefaultMeasurable(MeasureUnitTypeCode measureUnitTypeCode, IMeasurable excludedMeasurable = null)
    {
        IMeasurableFactory measurableFactory = GetRandomMeasurableFactory();
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
                BaseMeasureFactory baseMeasureFactory => GetDefaultBaseMeasure(baseMeasureFactory, measureUnitTypeCode),
                RateFactory rateFactory => GetDefaultRate(rateFactory, measureUnitTypeCode),

                _ => throw new InvalidOperationException(null),
            };
        }
        #endregion
    }
    #endregion
}
