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

    #region Constructors
    static Measurable()
    {
        MeasureUnitTypeCollection = InitMeasureUnitTypeCollection();
    }

    protected Measurable(IMeasurableFactory factory, MeasureUnitCode measureUnitCode) : base(factory)
    {
        MeasureUnitCode = Defined(measureUnitCode, nameof(measureUnitCode));
    }

    protected Measurable(IMeasurableFactory factory, Enum measureUnit) : base(factory)
    {
        MeasureUnitCode = GetValidMeasureUnitCode(measureUnit);
    }

    protected Measurable(IMeasurableFactory factory, IMeasurable measurable) : base(factory, measurable)
    {
        MeasureUnitCode = measurable.MeasureUnitCode;
    }

    protected Measurable(IMeasurableFactory factory, MeasureUnitCode measureUnitCode, params IMeasurable[] measurables) : base(factory, measurables)
    {
        MeasureUnitCode = Defined(measureUnitCode, nameof(measureUnitCode));
    }

    protected Measurable(IMeasurable other) : base(other)
    {
        MeasureUnitCode = other.MeasureUnitCode;
    }
    #endregion

    #region Properties
    public MeasureUnitCode MeasureUnitCode { get; init; }
    private static IDictionary<MeasureUnitCode, Type> MeasureUnitTypeCollection { get; }

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
    private static readonly IEnumerable<MeasureUnitCode> MeasureUnitCodes = Enum.GetValues<MeasureUnitCode>();
    public const string DefaultCustomMeasureUnitDefaultName = "Default";


    #endregion

    #region Public methods
    public Enum GetDefaultMeasureUnit()
    {
        return MeasureUnitCode.GetDefaultMeasureUnit();
    }

    public IEnumerable<string> GetDefaultMeasureUnitNames()
    {
        return GetDefaultNames(MeasureUnitCode);
    }

    public Type GetMeasureUnitType()
    {
        return MeasureUnitTypeCollection[MeasureUnitCode];
    }

    public virtual TypeCode GetQuantityTypeCode()
    {
        return MeasureUnitCode.GetQuantityTypeCode();
    }

    public bool HasMeasureUnitCode(MeasureUnitCode measureUnitCode)
    {
        return measureUnitCode == MeasureUnitCode;
    }

    public bool IsValidMeasureUnitCode(MeasureUnitCode measureUnitCode)
    {
        return GetMeasureUnitCodes().Contains(measureUnitCode);
    }

    #region Override methods
    public override bool Equals(object? obj)
    {
        return obj is IMeasurable other
            && MeasureUnitCode.Equals(other?.MeasureUnitCode);
    }

    public override IMeasurableFactory GetFactory()
    {
        return (IMeasurableFactory)Factory;
    }

    public override int GetHashCode()
    {
        return MeasureUnitCode.GetHashCode();
    }
    #endregion

    #region Virtual methods
    public virtual IEnumerable<MeasureUnitCode> GetMeasureUnitCodes()
    {
        return MeasureUnitCodes;
    }

    public virtual void ValidateMeasureUnit(Enum measureUnit, string paramName)
    {
        ValidateMeasureUnitByDefinition(measureUnit, paramName);
    }

    public virtual void ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, string paramName)
    {
        ValidateMeasureUnitCodeByDefinition(measureUnitCode, paramName);
    }
    #endregion

    #region Abstract methods
    public abstract Enum GetMeasureUnit();
    #endregion
    #endregion

    #region Protected methods
    #region Static methods
    protected static TSelf GetDefault<TSelf>(TSelf measurable) where TSelf : class, IMeasurable, IDefaultMeasurable
    {
        MeasureUnitCode measureUnitCode = measurable.MeasureUnitCode;

        return (TSelf)measurable.GetDefault(measureUnitCode)!;
    }
    #endregion
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

    public static IEnumerable<Enum> GetAllMeasureUnits()
    {
        IEnumerable<Enum> allMeasureUnits = MeasureUnitCodes.First().GetAllMeasureUnits();

        for (int i = 1; i < MeasureUnitCodes.Count(); i++)
        {
            IEnumerable<Enum> next = MeasureUnitCodes.ElementAt(i).GetAllMeasureUnits();
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

    public static MeasureUnitCode GetValidMeasureUnitCode(Enum measureUnit)
    {
        ValidateMeasureUnitByDefinition(measureUnit, nameof(measureUnit));

        Type measureUnitType = measureUnit.GetType();

        return GetMeasureUnitTypeCollection().First(x => x.Value == measureUnitType).Key;
    }

    //public static IEnumerable<MeasureUnitCode> GetMeasureUnitCodes()
    //{
    //    return Enum.GetValues<MeasureUnitCode>();
    //}

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
        return MeasureUnitCodes.First(x => Enum.GetName(x) == name);
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

    public static void ValidateMeasureUnitByDefinition(Enum measureUnit, string paramName)
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
        MeasureUnitCode measureUnitCode = GetMeasureUnitCode(measureUnit);

        if (NullChecked(measureUnitType, nameof(measureUnitType)) == GetMeasureUnitType(measureUnitCode)) return;

        throw InvalidMeasureUnitEnumArgumentException(measureUnit);
    }

    public static void ValidateMeasureUnitCodeByDefinition(MeasureUnitCode measureUnitCode, string paramName)
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
            foreach (MeasureUnitCode item in MeasureUnitCodes)
            {
                Type measureUnitType = MeasureUnitTypeSet.First(x => x.Name == Enum.GetName(item));

                yield return new KeyValuePair<MeasureUnitCode, Type>(item, measureUnitType);
            }
        }
        #endregion
    }
    #endregion

}
