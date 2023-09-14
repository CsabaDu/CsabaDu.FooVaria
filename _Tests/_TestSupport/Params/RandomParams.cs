using static CsabaDu.FooVaria.Tests.TestSupport.Params.SampleParams;


namespace CsabaDu.FooVaria.Tests.TestSupport.Params
{
    public static class RandomParams
    {
        private static readonly Random Random = Random.Shared;

        public static MeasureUnitTypeCode GetRandomMeasureUnitTypeCode(MeasureUnitTypeCode? measureUnitTypeCode = null)
        {
            int randomIndex = getRandomIndex();

            if (measureUnitTypeCode == null) return getRandomMeasureUnitTypeCode();

            MeasureUnitTypes.ValidateMeasureUnitTypeCode(measureUnitTypeCode.Value);

            while (randomIndex == (int)measureUnitTypeCode)
            {
                randomIndex = getRandomIndex();
            }

            return getRandomMeasureUnitTypeCode();

            #region Local methods
            static int getRandomIndex()
            {
                return Random.Next(MeasureUnitTypeCodeCount);
            }

            MeasureUnitTypeCode getRandomMeasureUnitTypeCode()
            {
                return (MeasureUnitTypeCode)randomIndex;
            }
            #endregion
        }

        public static Enum GetRandomMeasureUnit(Enum measureUnit = null)
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
                    if (measureUnit.Equals(getAllMeasureUnits().ElementAt(i)))
                    {
                        return i;
                    }
                }

                throw new InvalidOperationException(null);
            }

            Enum getRandomMeasureUnit()
            {
                return getAllMeasureUnits().ElementAt(randomIndex);
            }

            static IEnumerable<Enum> getAllMeasureUnits()
            {
                foreach (Type item in MeasureUnitTypes.GetMeasureUnitTypes())
                {
                    foreach (Enum measureUnit in Enum.GetValues(item))
                    {
                        yield return measureUnit;
                    }
                }
            }
            #endregion
        }
    }
}
