namespace CsabaDu.FooVaria.Measurables.Statics;

public static class MeasureUnitTypes
{
    #region Constructor
    static MeasureUnitTypes()
    {
        MeasureUnitTypeCollection = InitMeasureUnitTypeCollection();
    }
    #endregion

    public const string DefaultCustomMeasureUnitDefaultName = "Default";

    #region Properties

    private static readonly HashSet<Type> MeasureUnitTypeSet = new()
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

    private static IDictionary<MeasureUnitCode, Type> MeasureUnitTypeCollection { get; }
    #endregion

    #region Public methods
    public static Enum GetMeasureUnit(MeasureUnitCode measureUnitCode, int value)
    {
        Type measureUnitType = GetMeasureUnitType(measureUnitCode);

        return (Enum)Enum.ToObject(measureUnitType, value);
    }

    public static Enum? GetDefaultMeasureUnit(MeasureUnitCode measureUnitCode)
    {
        if (!Enum.IsDefined(measureUnitCode)) return null;

        return measureUnitCode.GetDefaultMeasureUnit();
    }

    public static IEnumerable<Enum> GetAllMeasureUnits(MeasureUnitCode measureUnitCode)
    {
        return measureUnitCode.GetAllMeasureUnits();
    }

    public static IEnumerable<Enum> GetAllMeasureUnits()
    {

        IEnumerable<MeasureUnitCode> measureUnitCodes = GetMeasureUnitCodes();

        IEnumerable<Enum> allMeasureUnits = measureUnitCodes.First().GetAllMeasureUnits();

        for (int i = 1; i < measureUnitCodes.Count(); i++)
        {
            IEnumerable<Enum> next = measureUnitCodes.ElementAt(i).GetAllMeasureUnits();
            allMeasureUnits = allMeasureUnits.Union(next);
        }

        return allMeasureUnits;
    }

    public static IDictionary<MeasureUnitCode, Type> GetMeasureUnitTypeCollection()
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
        Type measureUnitType = DefinedMeasureUnit(measureUnit, nameof(measureUnit)).GetType();
        string measureUnitName = Enum.GetName(measureUnitType, measureUnit)!;
        string measureUnitTypeName = measureUnitType.Name;

        return measureUnitName + measureUnitTypeName;
    }

    public static IEnumerable<string> GetDefaultNames(MeasureUnitCode measureUnitCode)
    {
        return GetAllMeasureUnits(measureUnitCode).Select(x => GetDefaultName(x));
    }

    public static IEnumerable<string> GetDefaultNames()
    {
        return GetAllMeasureUnits().Select(x => GetDefaultName(x));

        //foreach (MeasureUnitCode item in GetMeasureUnitCodes())
        //{
        //    foreach (string name in item.GetMeasureUnitDefaultNames())
        //    {
        //        yield return name;
        //    }
        //}
    }

    public static Type GetMeasureUnitType(MeasureUnitCode measureUnitCode)
    {
        ValidateMeasureUnitCode(measureUnitCode, nameof(measureUnitCode));

        return MeasureUnitTypeCollection[measureUnitCode];
    }

    public static MeasureUnitCode GetValidMeasureUnitCode(Enum measureUnit)
    {
        ValidateMeasureUnit(measureUnit, nameof(measureUnit));

        Type measureUnitType = measureUnit.GetType();

        return GetMeasureUnitTypeCollection().First(x => x.Value == measureUnitType).Key;
    }

    public static IEnumerable<MeasureUnitCode> GetMeasureUnitCodes()
    {
        return Enum.GetValues<MeasureUnitCode>();
    }

    public static IEnumerable<Type> GetMeasureUnitTypes()
    {
        return MeasureUnitTypeSet;
    }

    public static MeasureUnitCode GetMeasureUnitCode(Enum measureUnit)
    {
        string name = DefinedMeasureUnit(measureUnit, nameof(measureUnit)).GetType().Name;

        return GetMeasureUnitCode(name);
    }

    public static MeasureUnitCode GetMeasureUnitCode(Type measureUnitType)
    {
        string name = NullChecked(measureUnitType, nameof(measureUnitType)).Name;

        return GetMeasureUnitCode(name);
    }

    public static MeasureUnitCode GetMeasureUnitCode(string name)
    {
        return GetMeasureUnitCodes().First(x => Enum.GetName(x) == name);
    }

    public static bool HasMeasureUnitCode(MeasureUnitCode measureUnitCode, Enum measureUnit)
    {
        return IsDefinedMeasureUnit(measureUnit)
            && measureUnitCode == GetMeasureUnitCode(measureUnit!);
    }


    public static bool IsDefaultMeasureUnit(Enum measureUnit)
    {
        return IsDefinedMeasureUnit(measureUnit)
            && (int)(object)measureUnit == default;
    }

    public static bool IsDefinedMeasureUnit(Enum? measureUnit)
    {
        if (measureUnit == null) return false;

        Type measureUnitType = measureUnit.GetType();

        return GetMeasureUnitTypes().Contains(measureUnitType)
            && Enum.IsDefined(measureUnitType, measureUnit);
    }

    public static bool TryGetMeasureUnitCode(Enum? measureUnit, [NotNullWhen(true)] out MeasureUnitCode? measureUnitCode)
    {
        measureUnitCode = default;

        if (!IsDefinedMeasureUnit(measureUnit)) return false;

        measureUnitCode = GetMeasureUnitCode(measureUnit!);

        return true;
    }

    public static void ValidateMeasureUnit(Enum measureUnit, string paramName)
    {
        _ = DefinedMeasureUnit(measureUnit, paramName);
    }

    public static void ValidateMeasureUnit(Enum measureUnit, string measureUnitName, MeasureUnitCode measureUnitCode)
    {
        ValidateMeasureUnit(measureUnit, measureUnitName);
        ValidateMeasureUnitCode(measureUnitCode, nameof(measureUnitCode));

        string measureUnitCodeName = Enum.GetName(measureUnitCode)!;
        string measureUnitTypeName = measureUnit.GetType().Name;

        if (measureUnitTypeName == measureUnitCodeName) return;

        throw InvalidMeasureUnitEnumArgumentException(measureUnit);
    }

    public static void ValidateMeasureUnitType(Type measureUnitType)
    {
        if (MeasureUnitTypeSet.Contains(NullChecked(measureUnitType, nameof(measureUnitType)))) return;

        throw MeasureUnitTypeArgumentOutOfRangeException(measureUnitType);
    }

    public static void ValidateMeasureUnitType(Type measureUnitType, MeasureUnitCode measureUnitCode)
    {
        ValidateMeasureUnitType(measureUnitType);
        ValidateMeasureUnitCode(measureUnitCode, nameof(measureUnitCode));

        if (measureUnitCode == GetMeasureUnitCode(measureUnitType)) return;

        throw MeasureUnitTypeArgumentOutOfRangeException(measureUnitType);
    }

    public static void ValidateMeasureUnit(Enum measureUnit, Type measureUnitType)
    {
        MeasureUnitCode measureUnitCode = GetMeasureUnitCode(measureUnit);

        if (NullChecked(measureUnitType, nameof(measureUnitType)) == GetMeasureUnitType(measureUnitCode)) return;

        throw InvalidMeasureUnitEnumArgumentException(measureUnit);
    }

    public static void ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, string paramName)
    {
        if (Enum.IsDefined(measureUnitCode)) return;

        throw InvalidMeasureUnitCodeEnumArgumentException(measureUnitCode, paramName);
    }
    #endregion

    #region Private methods
    private static IDictionary<MeasureUnitCode, Type> InitMeasureUnitTypeCollection()
    {
        return initMeasureUnitTypeCollection().ToDictionary(x => x.Key, x => x.Value);

        #region Local methods
        IEnumerable<KeyValuePair<MeasureUnitCode, Type>> initMeasureUnitTypeCollection()
        {
            foreach (MeasureUnitCode item in GetMeasureUnitCodes())
            {
                Type measureUnitType = MeasureUnitTypeSet.First(x => x.Name == Enum.GetName(item));

                yield return new KeyValuePair<MeasureUnitCode, Type>(item, measureUnitType);
            }
        }
        #endregion
    }
    #endregion
}
