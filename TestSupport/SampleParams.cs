using CsabaDu.FooVaria.Common.Enums;

namespace CsabaDu.FooVaria.Tests.TestSupport
{
    public static class SampleParams
    {
        public static MeasureUnitTypeCode InvalidMeasureUnitTypeCode => (MeasureUnitTypeCode)Enum.GetNames(typeof(MeasureUnitTypeCode)).Length;

    }
}