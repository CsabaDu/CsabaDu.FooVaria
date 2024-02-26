namespace CsabaDu.FooVaria.BaseTypes.Measurables.Types.Implementations;

public abstract class Measurable : CommonBase, IMeasurable
{
    #region Enums
    protected enum SummingMode
    {
        Add,
        Subtract,
    }
    #endregion

    #region Constants
    public const string DefaultCustomMeasureUnitName = "Default";
    #endregion

    #region Constructors
    #region Static constructor
    static Measurable()
    {
        MeasureUnitTypeSet =
        [
            typeof(AreaUnit),
            typeof(Currency),
            typeof(DistanceUnit),
            typeof(ExtentUnit),
            typeof(TimePeriodUnit),
            typeof(Pieces),
            typeof(VolumeUnit),
            typeof(WeightUnit),
        ];

        MeasureUnitCodes = Enum.GetValues<MeasureUnitCode>();

        if (MeasureUnitCodes.Length != MeasureUnitTypeSet.Count) throw new InvalidOperationException(null);

        MeasureUnitTypeCollection = MeasureUnitCodes.ToDictionary
            (
                x => x,
                getMeasureUnitType
            );

        #region Local methods
        static Type getMeasureUnitType(MeasureUnitCode measureUnitCode)
        {
            string? measureUnitCodeName = Enum.GetName(measureUnitCode);

            return MeasureUnitTypeSet.First(x => x.Name == measureUnitCodeName);
        }
        #endregion
    }
    #endregion

    protected Measurable(IMeasurableFactory factory) : base(factory)
    {
    }

    protected Measurable(IMeasurable other) : base(other)
    {
    }
    #endregion

    #region Properties
    #region Static properties
    public static Dictionary<MeasureUnitCode, Type> MeasureUnitTypeCollection { get; }
    public static HashSet<Type> MeasureUnitTypeSet { get; }
    public static MeasureUnitCode[] MeasureUnitCodes { get; }
    #endregion
    #endregion

    #region Public methods
    public Enum GetDefaultMeasureUnit()
    {
        return GetMeasureUnitCode().GetDefaultMeasureUnit();
    }

    public IEnumerable<string> GetDefaultMeasureUnitNames()
    {
        return GetDefaultNames(GetMeasureUnitCode());
    }

    public Type GetMeasureUnitType()
    {
        return MeasureUnitTypeCollection[GetMeasureUnitCode()];
    }

    public bool HasMeasureUnitCode(MeasureUnitCode measureUnitCode)
    {
        return measureUnitCode == GetMeasureUnitCode();
    }

    public bool IsValidMeasureUnitCode(MeasureUnitCode measureUnitCode)
    {
        return GetMeasureUnitCodes().Contains(measureUnitCode);
    }

    public void ValidateMeasureUnitCode(IMeasurable? measurable, [DisallowNull] string paramName)
    {
        MeasureUnitCode measureUnitCode = NullChecked(measurable, paramName).GetMeasureUnitCode();

        ValidateMeasureUnitCode(measureUnitCode, paramName);
    }

    #region Override methods
    public override bool Equals(object? obj)
    {
        return obj is IMeasurable other
            && GetMeasureUnitCode().Equals(other.GetMeasureUnitCode());
    }

    public override IMeasurableFactory GetFactory()
    {
        return (IMeasurableFactory)Factory;
    }

    public override int GetHashCode()
    {
        return GetMeasureUnitCode().GetHashCode();
    }
    #endregion

    #region Virtual methods
    public virtual MeasureUnitCode GetMeasureUnitCode()
    {
        return GetMeasureUnitCode(GetMeasureUnit());
    }
    
    public virtual IEnumerable<MeasureUnitCode> GetMeasureUnitCodes()
    {
        yield return GetMeasureUnitCode();
    }

    public virtual TypeCode GetQuantityTypeCode()
    {
        return GetMeasureUnitCode().GetQuantityTypeCode();
    }

    public virtual void ValidateMeasureUnit(Enum? measureUnit, string paramName)
    {
        if (measureUnit is not MeasureUnitCode measureUnitCode)
        {
            Type enumType = NullChecked(measureUnit, paramName).GetType();
            measureUnitCode = MeasureUnitTypeCollection.FirstOrDefault(x => x.Value == enumType).Key;
        }
        else
        {
            measureUnit = measureUnitCode.GetDefaultMeasureUnit();
        }

        if (IsDefinedMeasureUnit(measureUnit) && HasMeasureUnitCode(measureUnitCode)) return;

        throw InvalidMeasureUnitEnumArgumentException(measureUnit!, paramName);
    }

    public virtual void ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, [DisallowNull] string paramName)
    {
        if (HasMeasureUnitCode(measureUnitCode)) return;

        throw InvalidMeasureUnitCodeEnumArgumentException(measureUnitCode, paramName);
    }
    #endregion

    #region Abstract methods
    public abstract Enum GetMeasureUnit();
    #endregion

    #region Static methods
    public static Enum GetMeasureUnit(MeasureUnitCode measureUnitCode, int value)
    {
        Type measureUnitType = GetMeasureUnitType(measureUnitCode);

        return DefinedMeasureUnit((Enum)Enum.ToObject(measureUnitType, value), nameof(value));
    }

    public static Enum? GetDefaultMeasureUnit(MeasureUnitCode measureUnitCode)
    {
        if (!Enum.IsDefined(measureUnitCode)) return null;

        return measureUnitCode.GetDefaultMeasureUnit();
    }

    public static IEnumerable<Enum> GetAllMeasureUnits()
    {
        IEnumerable<Enum> allMeasureUnits = MeasureUnitCodes.First().GetAllMeasureUnits();

        for (int i = 1; i < MeasureUnitCodes.Length; i++)
        {
            IEnumerable<Enum> next = MeasureUnitCodes.ElementAt(i).GetAllMeasureUnits();
            allMeasureUnits = allMeasureUnits.Union(next);
        }

        return allMeasureUnits;
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
        return measureUnitCode.GetAllMeasureUnits().Select(x => GetDefaultName(x));
    }

    public static IEnumerable<string> GetDefaultNames()
    {
        return GetAllMeasureUnits().Select(x => GetDefaultName(x));
    }

    public static Type GetMeasureUnitType(MeasureUnitCode measureUnitCode)
    {
        ValidateMeasureUnitCodeByDefinition(measureUnitCode, nameof(measureUnitCode));

        return MeasureUnitTypeCollection[measureUnitCode];
    }

    public static MeasureUnitCode GetDefinedMeasureUnitCode(Enum measureUnit)
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
        return MeasureUnitCodes.First(x => Enum.GetName(x) == name);
    }

    public static MeasureUnitCode GetMeasureUnitCode(Enum measureUnit)
    {
        if (measureUnit is not MeasureUnitCode measureUnitCode) return GetDefinedMeasureUnitCode(measureUnit);

        return Defined(measureUnitCode, nameof(measureUnit));
    }

    public static bool HasMeasureUnitCode(MeasureUnitCode measureUnitCode, Enum measureUnit)
    {
        return IsDefinedMeasureUnit(measureUnit)
            && measureUnitCode == GetDefinedMeasureUnitCode(measureUnit!);
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

        return MeasureUnitTypeSet.Contains(measureUnitType)
            && Enum.IsDefined(measureUnitType, measureUnit);
    }

    public static bool TryGetMeasureUnitCode(Enum? measureUnit, [NotNullWhen(true)] out MeasureUnitCode? measureUnitCode)
    {
        measureUnitCode = default;

        if (!IsDefinedMeasureUnit(measureUnit)) return false;

        measureUnitCode = GetDefinedMeasureUnitCode(measureUnit!);

        return true;
    }

    public static void ValidateMeasureUnitByDefinition(Enum? measureUnit, string paramName)
    {
        _ = DefinedMeasureUnit(measureUnit, paramName);
    }

    public static void ValidateMeasureUnit(Enum measureUnit, string measureUnitName, MeasureUnitCode measureUnitCode)
    {
        ValidateMeasureUnitByDefinition(measureUnit, measureUnitName);
        ValidateMeasureUnitCodeByDefinition(measureUnitCode, nameof(measureUnitCode));

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
        ValidateMeasureUnitCodeByDefinition(measureUnitCode, nameof(measureUnitCode));

        if (measureUnitCode == GetMeasureUnitCode(measureUnitType)) return;

        throw MeasureUnitTypeArgumentOutOfRangeException(measureUnitType);
    }

    public static void ValidateMeasureUnit(Enum measureUnit, Type measureUnitType)
    {
        MeasureUnitCode measureUnitCode = GetDefinedMeasureUnitCode(measureUnit);

        if (NullChecked(measureUnitType, nameof(measureUnitType)) == GetMeasureUnitType(measureUnitCode)) return;

        throw InvalidMeasureUnitEnumArgumentException(measureUnit);
    }

    public static void ValidateMeasureUnitCodeByDefinition(MeasureUnitCode measureUnitCode, string paramName)
    {
        if (Enum.IsDefined(measureUnitCode)) return;

        throw InvalidMeasureUnitCodeEnumArgumentException(measureUnitCode, paramName);
    }
    #endregion
    #endregion

    #region Protected methods
    #region Static methods
    #endregion
    #endregion
}
