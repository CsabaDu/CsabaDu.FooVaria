﻿namespace CsabaDu.FooVaria.Common.Statics;

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

        return DefinedEnum(measureUnitTypeCode.Value, nameof(measureUnitTypeCode)).GetAllMeasureUnits();
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
        ValidateMeasureUnit(measureUnit, null);

        Type measureUnitType = measureUnit.GetType();

        return measureUnit.GetType().Name + "." + Enum.GetName(measureUnitType, measureUnit)!;
    
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
        return MeasureUnitTypeCollection.Keys;
    }

    public static IEnumerable<Type> GetMeasureUnitTypes()
    {
        return MeasureUnitTypeCollection.Values;
    }

    public static MeasureUnitTypeCode GetMeasureUnitTypeCode(Enum measureUnit)
    {
        string measureUnitTypeName = NullChecked(measureUnit, nameof(measureUnit)).GetType().Name;

        return GetMeasureUnitTypeCodes().First(x => Enum.GetName(x) == measureUnitTypeName);
    }

    public static bool IsDefinedMeasureUnit(Enum measureUnit)
    {
        if (measureUnit == null) return false;

        Type measureUnitType = measureUnit.GetType();

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
