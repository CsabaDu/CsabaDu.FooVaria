namespace CsabaDu.FooVaria.Common.Statics;

public static class MeasureUnitTypes
{
    #region Properties
    private static readonly Dictionary<MeasureUnitTypeCode, Type> MeasureUnitTypeCollection = new()
    {
            { MeasureUnitTypeCode.AreaUnit, typeof(AreaUnit) },
            { MeasureUnitTypeCode.Currency, typeof(Currency) },
            { MeasureUnitTypeCode.DistanceUnit, typeof(DistanceUnit) },
            { MeasureUnitTypeCode.ExtentUnit, typeof(ExtentUnit) },
            { MeasureUnitTypeCode.TimePeriodUnit, typeof(TimePeriodUnit) },
            { MeasureUnitTypeCode.Pieces, typeof(Pieces) },
            { MeasureUnitTypeCode.VolumeUnit, typeof(VolumeUnit) },
            { MeasureUnitTypeCode.WeightUnit, typeof(WeightUnit) },
    };
    #endregion

    #region Public methods
    public static Enum GetMeasureUnit(MeasureUnitTypeCode measureUnitTypeCode, int value)
    {
        Type measureUnitType = GetMeasureUnitType(measureUnitTypeCode);

        return (Enum)Enum.ToObject(measureUnitType, value);
    }

    public static Enum GetDefaultMeasureUnit(MeasureUnitTypeCode measureUnitTypeCode)
    {
        return GetAllMeasureUnits(measureUnitTypeCode).OrderBy(x => x).First();
    }

    public static IEnumerable<Enum> GetAllMeasureUnits(MeasureUnitTypeCode? measureUnitTypeCode = null)
    {
        if (measureUnitTypeCode == null)
        {
            IEnumerable<Enum> allMeasureUnits = new List<Enum>();

            foreach (Type item in GetMeasureUnitTypes())
            {
                IEnumerable<Enum> next = getAllMeasureUnits(item);
                allMeasureUnits = allMeasureUnits.Union(next);
            }

            return allMeasureUnits;
        }

        ValidateMeasureUnitTypeCode(measureUnitTypeCode.Value);

        return getAllMeasureUnits(GetMeasureUnitType(measureUnitTypeCode!.Value));

        #region Local methods
        static IEnumerable<Enum> getAllMeasureUnits(Type measureUnitType)
        {
            foreach (Enum item in Enum.GetValues(measureUnitType))
            {
                yield return item;
            }
        }
        #endregion
    }

    public static IDictionary<MeasureUnitTypeCode, Type> GetMeasureUnitTypeCollection()
    {
        return MeasureUnitTypeCollection;
    }

    public static Enum GetDefaultMeasureUnit(Type measureUnitType)
    {
        ValidateMeasureUnitType(measureUnitType);

        return (Enum)Enum.ToObject(measureUnitType, default(int));
    }

    public static string GetDefaultName(Enum measureUnit)
    {
        ValidateMeasureUnit(measureUnit);

        Type measureUnitType = measureUnit.GetType();

        return measureUnit.GetType().Name + "." + Enum.GetName(measureUnitType, measureUnit)!;
    
    }

    public static IEnumerable<string> GetDefaultNames(MeasureUnitTypeCode? measureUnitTypeCode = null)
    {
        return GetAllMeasureUnits(measureUnitTypeCode).Select(x => GetDefaultName(x));
    }

    public static Type GetMeasureUnitType(MeasureUnitTypeCode measureUnitTypeCode) // Extensions
    {
        ValidateMeasureUnitTypeCode(measureUnitTypeCode);

        return MeasureUnitTypeCollection[measureUnitTypeCode];
    }

    public static MeasureUnitTypeCode GetMeasureUnitTypeCode(Enum measureUnit)
    {
        ValidateMeasureUnit(measureUnit);

        Type measureUnitType = measureUnit.GetType();

        return GetMeasureUnitTypeCollection().First(x => x.Value == measureUnitType).Key;
    }

    public static IEnumerable<MeasureUnitTypeCode> GetMeasureUnitTypeCodes()
    {
        return MeasureUnitTypeCollection.Keys;
    }

    public static IEnumerable<Type> GetMeasureUnitTypes()
    {
        return MeasureUnitTypeCollection.Values;
    }

    public static MeasureUnitTypeCode GetValidMeasureUnitTypeCode(Enum measureUnit)
    {
        ValidateMeasureUnit(measureUnit);

        string measureUnitTypeName = measureUnit.GetType().Name;

        return GetMeasureUnitTypeCodes().First(x => Enum.GetName(x) == measureUnitTypeName);
    }

    public static bool IsDefinedMeasureUnit(Enum measureUnit)
    {
        Type measureUnitType = NullChecked(measureUnit, nameof(measureUnit)).GetType();

        return GetMeasureUnitTypes().Contains(measureUnitType)
            && Enum.IsDefined(measureUnitType, measureUnit);
    }

    public static void ValidateMeasureUnit(Enum measureUnit, MeasureUnitTypeCode? measureUnitTypeCode = null)
    {
        if (IsDefinedMeasureUnit(measureUnit))
        {
            if (measureUnitTypeCode == null) return;

            measureUnitTypeCode ??= GetValidMeasureUnitTypeCode(measureUnit);
            string measureUnitTypeCodeName = Enum.GetName(typeof(MeasureUnitTypeCode), measureUnitTypeCode)!;
            string measureUnitTypeName = measureUnit.GetType().Name;

            if (measureUnitTypeName == measureUnitTypeCodeName) return;
        }

        throw InvalidMeasureUnitEnumArgumentException(measureUnit);
    }

    public static void ValidateMeasureUnitType(Type measureUnitType, MeasureUnitTypeCode? measureUnitTypeCode = null)
    {
        if (GetMeasureUnitTypes().Contains(NullChecked(measureUnitType, nameof(measureUnitType))))
        {
            if (measureUnitTypeCode == null) return;

            if (measureUnitType == GetMeasureUnitType(measureUnitTypeCode.Value)) return;
        }

        throw new ArgumentOutOfRangeException(nameof(measureUnitType), measureUnitType.FullName, null);
    }

    public static void ValidateMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode)
    {
        if (Enum.IsDefined(measureUnitTypeCode)) return;

        throw InvalidMeasureUnitTypeCodeEnumArgumentException(measureUnitTypeCode);
    }
    #endregion
}
