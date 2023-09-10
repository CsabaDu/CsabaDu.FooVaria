namespace CsabaDu.FooVaria.Common.Statics
{
    public static class MeasureUnitTypes
    {
        private static HashSet<Type> MeasureUnitTypeSet => new()
        {
            typeof(AreaUnit),
            typeof(Currency),
            typeof(DistanceUnit),
            typeof(ExtentUnit),
            typeof(Pieces),
            typeof(TimePeriodUnit),
            typeof(VolumeUnit),
            typeof(WeightUnit),
        };

        public static IDictionary<MeasureUnitTypeCode, Type> GetMeasureUnitTypeCollection()
        {
            return MeasureUnitTypeSet.ToDictionary
                (
                    x => (MeasureUnitTypeCode)Enum.Parse(typeof(MeasureUnitTypeCode), x.Name),
                    x => x
                );
        }

        public static bool IsDefinedMeasureUnit(Enum measureUnit)
        {
            Type measureUnitType = NullChecked(measureUnit, nameof(measureUnit)).GetType();

            return MeasureUnitTypeSet.Contains(measureUnitType)
                && Enum.IsDefined(measureUnitType, measureUnit);
        }

        public static void ValidateMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode)
        {
            if (Enum.IsDefined(measureUnitTypeCode)) return;

            throw InvalidMeasureUnitTypeCodeEnumArgumentException(measureUnitTypeCode);
        }

        public static void ValidateMeasureUnit(Enum measureUnit, MeasureUnitTypeCode? measureUnitTypeCode = null)
        {
            if (IsDefinedMeasureUnit(measureUnit))
            {
                if (measureUnitTypeCode == null) return;

                measureUnitTypeCode ??= GetValidMeasureUnitTypeCode(measureUnitTypeCode.Value);
                string measureUnitTypeCodeName = Enum.GetName(typeof(MeasureUnitTypeCode), measureUnitTypeCode)!;
                string measureUnitTypeName = measureUnit.GetType().Name;

                if (measureUnitTypeName == measureUnitTypeCodeName) return;
            }

            throw InvalidMeasureUnitEnumArgumentException(measureUnit);
        }

        public static MeasureUnitTypeCode GetValidMeasureUnitTypeCode(Enum measureUnit)
        {
            ValidateMeasureUnit(measureUnit);

            string measureUnitTypeName = measureUnit.GetType().Name;

            return (MeasureUnitTypeCode)Enum.Parse(typeof(MeasureUnitTypeCode), measureUnitTypeName);
        }
    
        public static IEnumerable<Type> GetMeasureUnitTypes()
        {
            return MeasureUnitTypeSet;
        }

        public static Type GetMeasureUnitType(MeasureUnitTypeCode measureUnitTypeCode)
        {
            ValidateMeasureUnitTypeCode(measureUnitTypeCode);

            string? measureUnitTypeCodeName = Enum.GetName(measureUnitTypeCode);

            return MeasureUnitTypeSet.First(x => x.Name == measureUnitTypeCodeName);
        }

        public static MeasureUnitTypeCode GetMeasureUnitTypeCode(Enum measureUnit)
        {
            ValidateMeasureUnit(measureUnit);

            Type measureUnitType = measureUnit.GetType();

            return GetMeasureUnitTypeCollection().First(x => x.Value == measureUnitType).Key;
        }
    }
}
