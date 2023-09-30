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
    #endregion
}
