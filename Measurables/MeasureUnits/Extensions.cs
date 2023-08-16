namespace CsabaDu.FooVaria.Measurables.MeasureUnits
{
    public static class Extensions
    {
        private static MeasureUnit MeasureUnit => new();
        private static IEnumerable<string> MeasureUnitTypeCodeNames => Enum.GetNames<MeasureUnitTypeCode>();

        #region System.Enum
        public static bool IsDefinedMeasureUnit(this Enum measureUnit)
        {
            return TryGetMeasureUnitTypeName(measureUnit, out string? measureUnitTypeName)
                && MeasureUnitTypeCodeNames.Contains(measureUnitTypeName);
        }

        public static bool IsDefinedMeasureUnit(this Enum measureUnit, MeasureUnitTypeCode measureUnitTypeCode)
        {
            string? measureUnitTypeCodeName = Enum.GetName(measureUnitTypeCode);

            return TryGetMeasureUnitTypeName(measureUnit, out string? measureUnitTypeName)
                && measureUnitTypeCodeName == measureUnitTypeName;
        }

        private static bool TryGetMeasureUnitTypeName(Enum measureUnit, out string? measureUnitTypeName)
        {
            Type measureUnitType = NullChecked(measureUnit, nameof(measureUnit)).GetType();
            string? measureUnitNamespace = measureUnitType.Namespace;
            measureUnitTypeName = measureUnitNamespace == MeasureUnit.Namespace ?
                measureUnitType.Name
                : null;

            return measureUnitTypeName != null;
        }
        #endregion
    }

    internal sealed class MeasureUnit
    {
        internal string? Namespace => GetType().Namespace;
    }
}
