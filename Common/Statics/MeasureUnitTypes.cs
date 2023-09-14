using CsabaDu.FooVaria.Common.Behaviors;
using System.Collections.Generic;

namespace CsabaDu.FooVaria.Common.Statics;

public static class MeasureUnitTypes
{
    #region Properties
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
    #endregion

    #region Public methods
    public static IEnumerable<Enum> GetAllMeasureUnits(MeasureUnitTypeCode? measureUnitTypeCode = null)
    {
        if (measureUnitTypeCode == null)
        {
            IEnumerable<Enum> allMeasureUnits = new List<Enum>();

            foreach (Type item in GetMeasureUnitTypes())
            {
                allMeasureUnits = allMeasureUnits.Union(getAllMeasureUnits(item));
            }

            return allMeasureUnits;
        }

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
        return MeasureUnitTypeSet.ToDictionary
            (
                x => (MeasureUnitTypeCode)Enum.Parse(typeof(MeasureUnitTypeCode), x.Name),
                x => x
            );
    }

    public static Enum GetDefaultMeasureUnit(Type measureUnitType)
    {
        ValidateMeasureUnitType(measureUnitType);

        return (Enum)Enum.ToObject(measureUnitType, default(int));
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

    public static IEnumerable<Type> GetMeasureUnitTypes()
    {
        return MeasureUnitTypeSet;
    }

    public static MeasureUnitTypeCode GetValidMeasureUnitTypeCode(Enum measureUnit)
    {
        ValidateMeasureUnit(measureUnit);

        string measureUnitTypeName = measureUnit.GetType().Name;

        return (MeasureUnitTypeCode)Enum.Parse(typeof(MeasureUnitTypeCode), measureUnitTypeName);
    }

    public static bool IsDefinedMeasureUnit(Enum measureUnit)
    {
        Type measureUnitType = NullChecked(measureUnit, nameof(measureUnit)).GetType();

        return MeasureUnitTypeSet.Contains(measureUnitType)
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
        if (MeasureUnitTypeSet.Contains(NullChecked(measureUnitType, nameof(measureUnitType))))
        {
            if (measureUnitTypeCode == null) return;

            ValidateMeasureUnitTypeCode(measureUnitTypeCode.Value);

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
