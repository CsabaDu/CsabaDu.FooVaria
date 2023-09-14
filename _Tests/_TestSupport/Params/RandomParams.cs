namespace CsabaDu.FooVaria.Tests.TestSupport.Params
{
    public static class RandomParams
    {
        private static readonly Random Random = Random.Shared;

        public static MeasureUnitTypeCode GetRandomMeasureUnitTypeCode(MeasureUnitTypeCode? measureUnitTypeCode = null)
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

        public static Enum GetRandomMeasureUnit(MeasureUnitTypeCode? measureUnitTypeCode = null, Enum measureUnit = null)
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

            IEnumerable<Enum> getAllMeasureUnits()
            {
                return MeasureUnitTypes.GetAllMeasureUnits(measureUnitTypeCode);
            }
            #endregion
        }
    }
}
