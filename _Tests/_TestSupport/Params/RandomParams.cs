using CsabaDu.FooVaria.Common.Enums;
using CsabaDu.FooVaria.Common.Statics;
using static CsabaDu.FooVaria.Tests.TestSupport.Params.SampleParams;


namespace CsabaDu.FooVaria.Tests.TestSupport.Params
{
    public class RandomParams
    {
        private static readonly Random Random = Random.Shared;
        //private static readonly IEnumerable<Enum> ValidMeasureUnits = ExchangeRates.GetValidMeasureUnits();

        public static MeasureUnitTypeCode GetRandomMeasureUnitTypeCode()
        {
            int randomIndex = Random.Next(MeasureUnitTypeCodeCount);

            return (MeasureUnitTypeCode)randomIndex;
        }

        public static Enum GetRandomMeasureUnit()
        {
            int randomIndex = Random.Next(getAllMeasureUnits().Count());

            return getAllMeasureUnits().ElementAt(randomIndex);

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
        }
    }
}
