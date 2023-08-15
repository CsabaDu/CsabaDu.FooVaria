namespace CsabaDu.FooVaria.Measurables.MeasureUnits
{
    public static class Extensions
    {
        private static MeasureUnit MeasureUnit => new();
        private static IEnumerable<string> MeasureUnitTypeCodeNames => Enum.GetNames<MeasureUnitTypeCode>();

        #region System.Enum
        public static bool IsDefinedMeasureUnit(this Enum measureUnit)
        {
            Type measureUnitType = measureUnit.GetType();
            string? measureUnitNamespace = measureUnitType.Namespace;
            string? measureUnitTypeName = measureUnitType.Name;

            return measureUnitNamespace == MeasureUnit.Namespace
                && MeasureUnitTypeCodeNames.Contains(measureUnitTypeName);
        }

        public static bool IsDefinedMeasureUnit(this Enum measureUnit, MeasureUnitTypeCode measureUnitTypeCode)
        {
            Type measureUnitType = measureUnit.GetType();
            string? measureUnitNamespace = measureUnitType.Namespace;
            string? measureUnitTypeName = measureUnitType.Name;
            string? measureUnitTypeCodeName = Enum.GetName(measureUnitTypeCode);

            return measureUnitNamespace == MeasureUnit.Namespace
                && measureUnitTypeCodeName == measureUnitTypeName;
        }
        #endregion
    }

    internal sealed class MeasureUnit
    {
        internal string? Namespace => GetType().Namespace;
    }
}
