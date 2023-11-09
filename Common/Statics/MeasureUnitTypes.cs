﻿
namespace CsabaDu.FooVaria.Common.Statics;

public static class MeasureUnitTypes
{
    #region Constructor
    static MeasureUnitTypes()
    {
        MeasureUnitTypeCollection = InitMeasureUnitTypeCollection();
    }
    #endregion

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

    private static IDictionary<MeasureUnitTypeCode, Type> MeasureUnitTypeCollection { get; }
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

    public static IEnumerable<Enum> GetAllMeasureUnits(MeasureUnitTypeCode measureUnitTypeCode)
    {
        return measureUnitTypeCode.GetAllMeasureUnits();
    }

    public static IEnumerable<Enum> GetAllMeasureUnits()
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
        ValidateMeasureUnit(measureUnit, nameof(measureUnit));

        Type measureUnitType = measureUnit.GetType();
        string measureUnitName = Enum.GetName(measureUnitType, measureUnit)!;
        string measureUnitTypeName = measureUnit.GetType().Name;

        return measureUnitName + measureUnitTypeName;
    }

    public static IEnumerable<string> GetDefaultNames(MeasureUnitTypeCode measureUnitTypeCode)
    {
        return GetAllMeasureUnits(measureUnitTypeCode).Select(x => GetDefaultName(x));
    }

    public static IEnumerable<string> GetDefaultNames()
    {
        return GetAllMeasureUnits().Select(x => GetDefaultName(x));
    }

    public static Type GetMeasureUnitType(MeasureUnitTypeCode measureUnitTypeCode)
    {
        ValidateMeasureUnitTypeCode(measureUnitTypeCode, nameof(measureUnitTypeCode));

        return MeasureUnitTypeCollection[measureUnitTypeCode];
    }

    public static MeasureUnitTypeCode GetValidMeasureUnitTypeCode(Enum measureUnit)
    {
        ValidateMeasureUnit(measureUnit, nameof(measureUnit));

        Type measureUnitType = measureUnit.GetType();

        return GetMeasureUnitTypeCollection().First(x => x.Value == measureUnitType).Key;
    }

    public static IEnumerable<MeasureUnitTypeCode> GetMeasureUnitTypeCodes()
    {
        return Enum.GetValues<MeasureUnitTypeCode>();
    }

    public static IEnumerable<Type> GetMeasureUnitTypes()
    {
        return MeasureUnitTypeSet;
    }

    public static MeasureUnitTypeCode GetMeasureUnitTypeCode(Enum measureUnit)
    {
        string name = NullChecked(measureUnit, nameof(measureUnit)).GetType().Name;

        return GetMeasureUnitTypeCode(name);
    }

    public static MeasureUnitTypeCode GetMeasureUnitTypeCode(Type measureUnitType)
    {
        string name = NullChecked(measureUnitType, nameof(measureUnitType)).Name;

        return GetMeasureUnitTypeCode(name);
    }

    public static MeasureUnitTypeCode GetMeasureUnitTypeCode(string name)
    {
        return GetMeasureUnitTypeCodes().First(x => Enum.GetName(x) == name);
    }

    public static bool HasMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode, Enum measureUnit)
    {
        return IsDefinedMeasureUnit(measureUnit)
            && measureUnitTypeCode == GetMeasureUnitTypeCode(measureUnit!);
    }

    public static bool IsDefinedMeasureUnit(Enum measureUnit)
    {
        if (measureUnit == null) return false;

        Type measureUnitType = measureUnit.GetType();

        return GetMeasureUnitTypes().Contains(measureUnitType)
            && Enum.IsDefined(measureUnitType, measureUnit);
    }

    public static void ValidateMeasureUnit(Enum measureUnit, string paramName)
    {
        _ = DefinedMeasureUnit(measureUnit, paramName);
    }

    public static void ValidateMeasureUnit(Enum measureUnit, string measureUnitName, MeasureUnitTypeCode measureUnitTypeCode)
    {
        ValidateMeasureUnit(measureUnit, measureUnitName);
        ValidateMeasureUnitTypeCode(measureUnitTypeCode, nameof(measureUnitTypeCode));

        string measureUnitTypeCodeName = Enum.GetName(measureUnitTypeCode)!;
        string measureUnitTypeName = measureUnit.GetType().Name;

        if (measureUnitTypeName == measureUnitTypeCodeName) return;

        throw InvalidMeasureUnitEnumArgumentException(measureUnit);
    }

    public static void ValidateMeasureUnitType(Type measureUnitType)
    {
        if (MeasureUnitTypeSet.Contains(NullChecked(measureUnitType, nameof(measureUnitType)))) return;

        throw MeasureUnitTypeArgumentOutOfRangeException(measureUnitType);
    }

    public static void ValidateMeasureUnitType(Type measureUnitType, MeasureUnitTypeCode measureUnitTypeCode)
    {
        ValidateMeasureUnitType(measureUnitType);
        ValidateMeasureUnitTypeCode(measureUnitTypeCode, nameof(measureUnitTypeCode));

        if (measureUnitTypeCode == GetMeasureUnitTypeCode(measureUnitType)) return;

        throw MeasureUnitTypeArgumentOutOfRangeException(measureUnitType);
    }

    public static void ValidateMeasureUnit(Enum measureUnit, Type measureUnitType)
    {
        MeasureUnitTypeCode measureUnitTypeCode = GetMeasureUnitTypeCode(measureUnit);

        if (NullChecked(measureUnitType, nameof(measureUnitType)) == GetMeasureUnitType(measureUnitTypeCode)) return;

        throw InvalidMeasureUnitEnumArgumentException(measureUnit);
    }

    public static void ValidateMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode, string paramName)
    {
        if (Enum.IsDefined(measureUnitTypeCode)) return;

        throw InvalidMeasureUnitTypeCodeEnumArgumentException(measureUnitTypeCode, paramName);
    }
    #endregion

    #region Private methods
    private static IDictionary<MeasureUnitTypeCode, Type> InitMeasureUnitTypeCollection()
    {
        return initMeasureUnitTypeCollection().ToDictionary(x => x.Key, x => x.Value);

        #region Local methods
        IEnumerable<KeyValuePair<MeasureUnitTypeCode, Type>> initMeasureUnitTypeCollection()
        {
            foreach (MeasureUnitTypeCode item in GetMeasureUnitTypeCodes())
            {
                Type measureUnitType = MeasureUnitTypeSet.First(x => x.Name == Enum.GetName(item));

                yield return new KeyValuePair<MeasureUnitTypeCode, Type>(item, measureUnitType);
            }
        }
        #endregion
    }
    #endregion
}
