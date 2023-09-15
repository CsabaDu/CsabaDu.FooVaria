namespace CsabaDu.FooVaria.Tests.TestSupport.Params;

public class RandomParams
{
    #region Private fields
    private static readonly Random Random = Random.Shared;
    #endregion

    #region Public methods
    public MeasureUnitTypeCode GetRandomMeasureUnitTypeCode(MeasureUnitTypeCode? measureUnitTypeCode = null)
    {
        int randomIndex = getRandomIndex();

        if (measureUnitTypeCode == null) return getRandomMeasureUnitTypeCode();

        while (randomIndex == (int)measureUnitTypeCode)
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

    public Enum GetRandomMeasureUnit(MeasureUnitTypeCode? measureUnitTypeCode = null, Enum measureUnit = null)
    {
        int count = getAllMeasureUnits().Count();
        int randomIndex = getRandomIndex();

        if (measureUnit == null) return getRandomMeasureUnit();

        while (randomIndex == getMeasureUnitIndex())
        {
            randomIndex = getRandomIndex();
        }

        return getRandomMeasureUnit();

        #region Local methods
        int getRandomIndex()
        {
            return Random.Next(count);
        }

        int getMeasureUnitIndex()
        {
            for (int i = 0; i < count; i++)
            {
                Enum other = getAllMeasureUnits().ElementAt(i);

                if (measureUnit.Equals(other)) return i;
            }

            throw ExceptionMethods.InvalidMeasureUnitEnumArgumentException(measureUnit);
        }

        Enum getRandomMeasureUnit()
        {
            return getAllMeasureUnits().ElementAt(randomIndex);
        }

        IEnumerable<Enum> getAllMeasureUnits()
        {
            return MeasureUnitTypes.GetAllMeasureUnits(measureUnitTypeCode);
        }
        #endregion
    }

    public Enum GetRandomInvalidMeasureUnit()
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
    #endregion
}
