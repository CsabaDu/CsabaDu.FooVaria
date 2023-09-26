namespace CsabaDu.FooVaria.Common.Statics;

public static class MeasureUnitTypes
{
    static MeasureUnitTypes()
    {
        MeasureUnitTypeCollection = InitMeasureUnitTypeCollection();
    }
    #region Properties

    private static readonly HashSet<Type> MeasureUnitTypSet = new()
    {
        typeof(AreaUnit),
        typeof(Currency),
        typeof(DistanceUnit),
        typeof(ExtentUnit),
        typeof(TimePeriodUnit),
        typeof(Pieces),
        typeof(VolumeUnit),
        typeof(WeightUnit),
    };

    private static Dictionary<MeasureUnitTypeCode, Type> InitMeasureUnitTypeCollection()
    {
        return initMeasureUnitTypeCollection().ToDictionary(x => x.Key, x => x.Value);

        IEnumerable<KeyValuePair<MeasureUnitTypeCode, Type>> initMeasureUnitTypeCollection()
        {
            foreach (MeasureUnitTypeCode item in GetMeasureUnitTypeCodes())
            {
                Type measureUnitType = MeasureUnitTypSet.First(x => x.Name == Enum.GetName(item));

                yield return new KeyValuePair<MeasureUnitTypeCode, Type>(item, measureUnitType);
            }
        }
    }

    private static IDictionary<MeasureUnitTypeCode, Type> MeasureUnitTypeCollection { get; }
    //{
    //    { MeasureUnitTypeCode.AreaUnit, typeof(AreaUnit) },
    //    { MeasureUnitTypeCode.Currency, typeof(Currency) },
    //    { MeasureUnitTypeCode.DistanceUnit, typeof(DistanceUnit) },
    //    { MeasureUnitTypeCode.ExtentUnit, typeof(ExtentUnit) },
    //    { MeasureUnitTypeCode.TimePeriodUnit, typeof(TimePeriodUnit) },
    //    { MeasureUnitTypeCode.Pieces, typeof(Pieces) },
    //    { MeasureUnitTypeCode.VolumeUnit, typeof(VolumeUnit) },
    //    { MeasureUnitTypeCode.WeightUnit, typeof(WeightUnit) },
    //};
    #endregion

    #region Public methods
    public static Enum GetMeasureUnit(MeasureUnitTypeCode measureUnitTypeCode, int value)
    {
        Type measureUnitType = GetMeasureUnitType(measureUnitTypeCode);

        return (Enum)Enum.ToObject(measureUnitType, value);
    }

    public static Enum GetDefaultMeasureUnit(MeasureUnitTypeCode measureUnitTypeCode)
    {
        return measureUnitTypeCode.GetDefaultMeasureUnit();
    }

    public static IEnumerable<Enum> GetAllMeasureUnits(MeasureUnitTypeCode? measureUnitTypeCode)
    {
        if (measureUnitTypeCode == null)
        {
            IEnumerable<MeasureUnitTypeCode> measureUnitTypeCodes = GetMeasureUnitTypeCodes();

            IEnumerable<Enum> allMeasureUnits = measureUnitTypeCodes.First().GetAllMeasureUnits();

            for (int i = 1; i < measureUnitTypeCodes.Count(); i++)
            {
                IEnumerable<Enum> next = measureUnitTypeCodes.ElementAt(i).GetAllMeasureUnits();
                allMeasureUnits = allMeasureUnits.Union(next);
            }

            return allMeasureUnits;
        }

        return Defined(measureUnitTypeCode.Value, nameof(measureUnitTypeCode)).GetAllMeasureUnits();
    }

    public static IDictionary<MeasureUnitTypeCode, Type> GetMeasureUnitTypeCollection()
    {
        return MeasureUnitTypeCollection;
    }

    public static Enum GetDefaultMeasureUnit(Type measureUnitType)
    {
        ValidateMeasureUnitType(measureUnitType, null);

        return (Enum)Enum.ToObject(measureUnitType, default(int));
    }

    public static string GetDefaultName(Enum measureUnit)
    {
        ValidateMeasureUnit(measureUnit, null);

        Type measureUnitType = measureUnit.GetType();
        string defaultName = Enum.GetName(measureUnitType, measureUnit)!;
        string measureUnitTypeName = measureUnit.GetType().Name;

        return defaultName + measureUnitTypeName;
    
    }

    public static IEnumerable<string> GetDefaultNames(MeasureUnitTypeCode? measureUnitTypeCode)
    {
        return GetAllMeasureUnits(measureUnitTypeCode).Select(x => GetDefaultName(x));
    }

    public static Type GetMeasureUnitType(MeasureUnitTypeCode measureUnitTypeCode) // Extensions
    {
        ValidateMeasureUnitTypeCode(measureUnitTypeCode);

        return MeasureUnitTypeCollection[measureUnitTypeCode];
    }

    public static MeasureUnitTypeCode GetValidMeasureUnitTypeCode(Enum measureUnit)
    {
        ValidateMeasureUnit(measureUnit, null);

        Type measureUnitType = measureUnit.GetType();

        return GetMeasureUnitTypeCollection().First(x => x.Value == measureUnitType).Key;
    }

    public static IEnumerable<MeasureUnitTypeCode> GetMeasureUnitTypeCodes()
    {
        return Enum.GetValues<MeasureUnitTypeCode>();
    }

    public static IEnumerable<Type> GetMeasureUnitTypes()
    {
        return MeasureUnitTypSet;
    }

    public static MeasureUnitTypeCode GetMeasureUnitTypeCode(Enum measureUnit)
    {
        string measureUnitTypeName = NullChecked(measureUnit, nameof(measureUnit)).GetType().Name;

        return GetMeasureUnitTypeCodes().First(x => Enum.GetName(x) == measureUnitTypeName);
    }

    public static bool IsDefinedMeasureUnit(Enum measureUnit)
    {
        Type measureUnitType = NullChecked(measureUnit, nameof(measureUnit)).GetType();

        return GetMeasureUnitTypes().Contains(measureUnitType)
            && Enum.IsDefined(measureUnitType, measureUnit);
    }

    public static void ValidateMeasureUnit(Enum measureUnit, MeasureUnitTypeCode? measureUnitTypeCode)
    {
        if (IsDefinedMeasureUnit(NullChecked(measureUnit, nameof(measureUnit))))
        {
            if (measureUnitTypeCode == null) return;

            measureUnitTypeCode ??= GetMeasureUnitTypeCode(measureUnit);

            ValidateMeasureUnitTypeCode(measureUnitTypeCode.Value);

            string measureUnitTypeCodeName = Enum.GetName(typeof(MeasureUnitTypeCode), measureUnitTypeCode)!;
            string measureUnitTypeName = measureUnit.GetType().Name;

            if (measureUnitTypeName == measureUnitTypeCodeName) return;
        }

        throw InvalidMeasureUnitEnumArgumentException(measureUnit);
    }

    public static void ValidateMeasureUnitType(Type measureUnitType, MeasureUnitTypeCode? measureUnitTypeCode)
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
