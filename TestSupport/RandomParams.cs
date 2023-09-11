using CsabaDu.FooVaria.Common.Enums;

namespace CsabaDu.FooVaria.Tests.TestSupport
{
    public static class RandomParams
    {
        private static readonly Random Random = Random.Shared;

        public static MeasureUnitTypeCode GetRandomMeasureUnitTypeCode()
        {
            int count = Enum.GetNames(typeof(MeasureUnitTypeCode)).Length;
            int randomIndex = Random.Next(count);

            return (MeasureUnitTypeCode)randomIndex;
        }
    }
}
