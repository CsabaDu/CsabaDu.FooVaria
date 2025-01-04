namespace CsabaDu.FooVaria.BaseTypes.Measurables.Types.Implementations;

/// <summary>
/// Represents an abstract base class for measurable entities.
/// </summary>
/// <param name="rootObject">The root object associated with the measurable entity.</param>
/// <param name="paramName">The parameter name associated with the measurable entity.</param>
public abstract class Measurable(IRootObject rootObject, string paramName) : CommonBase(rootObject, paramName), IMeasurable
{
    #region Enums
    /// <summary>
    /// Defines the modes for summing measurable entities.
    /// </summary>
    protected enum SummingMode
    {
        Add,
        Subtract,
    }
    #endregion

    #region Records
    /// <summary>
    /// Represents the elements of a measure unit.
    /// </summary>
    /// <param name="MeasureUnit">The measure unit.</param>
    /// <param name="MeasureUnitCode">The code of the measure unit.</param>
    public record MeasureUnitElements(Enum MeasureUnit, MeasureUnitCode MeasureUnitCode);
    #endregion

    #region Constants
    /// <summary>
    /// The default string constant.
    /// </summary>
    public const string Default = nameof(Default);
    /// <summary>
    /// The namespace name of the measure units.
    /// </summary>
    private const string MeasureUnitsNamespace = "CsabaDu.FooVaria.BaseTypes.Measurables.Enums.MeasureUnits";
    #endregion

    #region Constructors
    #region Static constructor
    /// <summary>
    /// Initializes static members of the <see cref="Measurable"/> class.
    /// </summary>
    static Measurable()
    {
        MeasureUnitTypes = Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(x => x.IsEnum && x.Namespace == MeasureUnitsNamespace)
            .ToArray();

        MeasureUnitCodes = Enum.GetValues<MeasureUnitCode>();

        if (MeasureUnitCodes.Length != MeasureUnitTypes.Length) throw new InvalidOperationException(null);

        MeasureUnitTypeCollection = MeasureUnitCodes
            .ToDictionary(x => x, getMeasureUnitType);

        #region Local methods
        static Type getMeasureUnitType(MeasureUnitCode measureUnitCode)
        {
            string? measureUnitCodeName = Enum.GetName(measureUnitCode);

            return MeasureUnitTypes.First(x => x.Name == measureUnitCodeName);
        }
        #endregion
    }

    #endregion
    #endregion

    #region Properties
    #region Static properties
    /// <summary>
    /// Gets the collection of measure unit types.
    /// </summary>
    public static Dictionary<MeasureUnitCode, Type> MeasureUnitTypeCollection { get; }

    /// <summary>
    /// Gets the set of measure unit types.
    /// Measure unit types are defined as enums in the <see cref="MeasureUnitsNamespace"/> namespace.
    /// Constant measure unit types names must start with the captured measure type name and must be ended with "Unit".
    /// Custom measure unit types names should be different from the captured measure type name and may not be ended with "Unit".
    /// </summary>
    public static Type[] MeasureUnitTypes { get; }

    /// <summary>
    /// Gets the array of measure unit codes.
    /// </summary>
    public static MeasureUnitCode[] MeasureUnitCodes { get; }
    #endregion
    #endregion

    #region Public methods
    #region Static methods
    /// <summary>
    /// Gets the measure unit for the specified measure unit code and value.
    /// </summary>
    /// <param name="measureUnitCode">The measure unit code.</param>
    /// <param name="value">The value of the measure unit.</param>
    /// <returns>The measure unit if found; otherwise, null.</returns>
    public static Enum? GetMeasureUnit(MeasureUnitCode measureUnitCode, int value)
    {
        return GetAllMeasureUnits()
            .Where(itemTypeNameEqualsMeasureUnitCodeName)
            .FirstOrDefault(itemValueEqualsValue);

        #region Local methods
        bool itemTypeNameEqualsMeasureUnitCodeName(Enum measureUnit)
        {
            string? measureUnitCodeName = Enum.GetName(Defined(measureUnitCode, nameof(measureUnitCode)));
            string measureUnitTypeName = measureUnit.GetType().Name;

            return measureUnitTypeName == measureUnitCodeName;
        }

        bool itemValueEqualsValue(Enum measureUnit)
        {
            return (int)(object)measureUnit == value;
        }
        #endregion
    }

    /// <summary>
    /// Gets all measure units.
    /// </summary>
    /// <returns>An enumerable collection of all measure units.</returns>
    public static IEnumerable<Enum> GetAllMeasureUnits()
    {
        IEnumerable<Enum> allMeasureUnits = MeasureUnitCodes[0].GetAllMeasureUnits();

        for (int i = 1; i < MeasureUnitCodes.Length; i++)
        {
            IEnumerable<Enum> next = MeasureUnitCodes[i].GetAllMeasureUnits();
            allMeasureUnits = allMeasureUnits.Union(next);
        }

        return allMeasureUnits;
    }

    /// <summary>
    /// Gets the default measure unit for the specified measure unit type.
    /// </summary>
    /// <param name="measureUnitType">The measure unit type.</param>
    /// <returns>The default measure unit.</returns>
    public static Enum GetDefaultMeasureUnit(Type measureUnitType)
    {
        ValidateMeasureUnitType(measureUnitType);

        return (Enum)Enum.ToObject(measureUnitType, default(int));
    }

    /// <summary>
    /// Gets the default name for the specified measure unit.
    /// </summary>
    /// <param name="measureUnit">The measure unit.</param>
    /// <returns>The default name of the measure unit.</returns>
    public static string GetDefaultName(Enum measureUnit)
    {
        Type measureUnitType = DefinedMeasureUnit(measureUnit, nameof(measureUnit)).GetType();
        string measureUnitName = Enum.GetName(measureUnitType, measureUnit)!;
        string measureUnitTypeName = measureUnitType.Name;

        return measureUnitName + measureUnitTypeName;
    }

    /// <summary>
    /// Gets the default names for the specified measure unit code.
    /// </summary>
    /// <param name="measureUnitCode">The measure unit code.</param>
    /// <returns>An enumerable collection of default names.</returns>
    public static IEnumerable<string> GetDefaultNames(MeasureUnitCode measureUnitCode)
    {
        return measureUnitCode.GetAllMeasureUnits().Select(GetDefaultName);
    }

    /// <summary>
    /// Gets the default names for all measure units.
    /// </summary>
    /// <returns>An enumerable collection of default names.</returns>
    public static IEnumerable<string> GetDefaultNames()
    {
        return GetAllMeasureUnits().Select(GetDefaultName);
    }

    /// <summary>
    /// Gets the defined measure unit code for the specified measure unit.
    /// </summary>
    /// <param name="measureUnit">The measure unit.</param>
    /// <returns>The defined measure unit code.</returns>
    public static MeasureUnitCode GetDefinedMeasureUnitCode(Enum? measureUnit)
    {
        string name = DefinedMeasureUnit(measureUnit, nameof(measureUnit)).GetType().Name;

        return GetMeasureUnitCode(name);
    }

    /// <summary>
    /// Gets the measure unit code for the specified measure unit type.
    /// </summary>
    /// <param name="measureUnitType">The measure unit type.</param>
    /// <returns>The measure unit code.</returns>
    public static MeasureUnitCode GetMeasureUnitCode(Type measureUnitType)
    {
        const string paramName = nameof(measureUnitType);

        if (MeasureUnitTypes.Contains(NullChecked(measureUnitType, paramName)))
        {
            return MeasureUnitTypeCollection.First(x => x.Value == measureUnitType).Key;
        }

        throw new ArgumentOutOfRangeException(paramName);
    }

    /// <summary>
    /// Gets the measure unit code for the specified name.
    /// </summary>
    /// <param name="name">The name of the measure unit.</param>
    /// <returns>The measure unit code.</returns>
    public static MeasureUnitCode GetMeasureUnitCode(string name)
    {
        return MeasureUnitCodes.First(x => Enum.GetName(x) == name);
    }

    /// <summary>
    /// Gets the measure unit code for the specified measure unit.
    /// </summary>
    /// <param name="measureUnit">The measure unit.</param>
    /// <returns>The measure unit code.</returns>
    public static MeasureUnitCode GetMeasureUnitCode(Enum? measureUnit)
    {
        if (measureUnit is not MeasureUnitCode measureUnitCode) return GetDefinedMeasureUnitCode(measureUnit);

        return Defined(measureUnitCode, nameof(measureUnit));
    }

    /// <summary>
    /// Gets the measure unit elements for the specified context and parameter name.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="paramName">The parameter name.</param>
    /// <returns>The measure unit elements.</returns>
    public static MeasureUnitElements GetMeasureUnitElements(Enum? context, string paramName)
    {
        Enum? measureUnit = context is MeasureUnitCode measureUnitCode ?
            Defined(measureUnitCode, paramName).GetDefaultMeasureUnit()
            : DefinedMeasureUnit(context, paramName);
        measureUnitCode = context!.Equals(measureUnit) ?
            GetMeasureUnitCode(context)
            : (MeasureUnitCode)context!;

        return new(measureUnit!, measureUnitCode);
    }

    /// <summary>
    /// Determines whether the specified measure unit has the specified measure unit code.
    /// </summary>
    /// <param name="measureUnitCode">The measure unit code.</param>
    /// <param name="measureUnit">The measure unit.</param>
    /// <returns>true if the measure unit has the specified measure unit code; otherwise, false.</returns>
    public static bool HasMeasureUnitCode(MeasureUnitCode measureUnitCode, Enum measureUnit)
    {
        return IsDefinedMeasureUnit(measureUnit)
            && measureUnitCode == GetDefinedMeasureUnitCode(measureUnit);
    }

    /// <summary>
    /// Determines whether the specified measure unit is the default measure unit.
    /// </summary>
    /// <param name="measureUnit">The measure unit.</param>
    /// <returns>true if the measure unit is the default measure unit; otherwise, false.</returns>
    public static bool IsDefaultMeasureUnit(Enum measureUnit)
    {
        return IsDefinedMeasureUnit(measureUnit)
            && (int)(object)measureUnit == default;
    }

    /// <summary>
    /// Determines whether the specified measure unit is defined.
    /// </summary>
    /// <param name="measureUnit">The measure unit.</param>
    /// <returns>true if the measure unit is defined; otherwise, false.</returns>
    public static bool IsDefinedMeasureUnit(Enum? measureUnit)
    {
        if (measureUnit is null) return false;

        Type measureUnitType = measureUnit.GetType();

        return MeasureUnitTypes.Contains(measureUnitType)
            && Enum.IsDefined(measureUnitType, measureUnit);
    }

    /// <summary>
    /// Tries to get the measure unit code for the specified measure unit.
    /// </summary>
    /// <param name="measureUnit">The measure unit.</param>
    /// <param name="measureUnitCode">When this method returns, contains the measure unit code if the measure unit is defined; otherwise, null.</param>
    /// <returns>true if the measure unit code was retrieved successfully; otherwise, false.</returns>
    public static bool TryGetMeasureUnitCode(Enum? measureUnit, [NotNullWhen(true)] out MeasureUnitCode? measureUnitCode)
    {
        measureUnitCode = default;

        if (!IsDefinedMeasureUnit(measureUnit)) return false;

        measureUnitCode = GetDefinedMeasureUnitCode(measureUnit);

        return true;
    }

    /// <summary>
    /// Validates the measure unit by definition.
    /// </summary>
    /// <param name="measureUnit">The measure unit.</param>
    /// <param name="paramName">The parameter name.</param>
    public static void ValidateMeasureUnitByDefinition(Enum? measureUnit, string paramName)
    {
        _ = DefinedMeasureUnit(measureUnit, paramName);
    }

    /// <summary>
    /// Validates the measure unit.
    /// </summary>
    /// <param name="measureUnit">The measure unit.</param>
    /// <param name="measureUnitName">The name of the measure unit.</param>
    /// <param name="measureUnitCode">The measure unit code.</param>
    public static void ValidateMeasureUnit(Enum measureUnit, string measureUnitName, MeasureUnitCode measureUnitCode)
    {
        ValidateMeasureUnitByDefinition(measureUnit, measureUnitName);
        ValidateMeasureUnitCodeByDefinition(measureUnitCode, nameof(measureUnitCode));

        string measureUnitCodeName = Enum.GetName(measureUnitCode)!;
        string measureUnitTypeName = measureUnit.GetType().Name;

        if (measureUnitTypeName == measureUnitCodeName) return;

        throw InvalidMeasureUnitEnumArgumentException(measureUnit);
    }

    /// <summary>
    /// Validates the measure unit type.
    /// </summary>
    /// <param name="measureUnitType">The measure unit type.</param>
    public static void ValidateMeasureUnitType(Type measureUnitType)
    {
        if (MeasureUnitTypes.Contains(NullChecked(measureUnitType, nameof(measureUnitType)))) return;

        throw MeasureUnitTypeArgumentOutOfRangeException(measureUnitType);
    }

    /// <summary>
    /// Validates the measure unit type and measure unit code.
    /// </summary>
    /// <param name="measureUnitType">The measure unit type.</param>
    /// <param name="measureUnitCode">The measure unit code.</param>
    public static void ValidateMeasureUnitType(Type measureUnitType, MeasureUnitCode measureUnitCode)
    {
        ValidateMeasureUnitType(measureUnitType);
        ValidateMeasureUnitCodeByDefinition(measureUnitCode, nameof(measureUnitCode));

        if (measureUnitCode == GetMeasureUnitCode(measureUnitType)) return;

        throw MeasureUnitTypeArgumentOutOfRangeException(measureUnitType);
    }

    /// <summary>
    /// Validates the measure unit and measure unit type.
    /// </summary>
    /// <param name="measureUnit">The measure unit.</param>
    /// <param name="measureUnitType">The measure unit type.</param>
    public static void ValidateMeasureUnit(Enum measureUnit, Type measureUnitType)
    {
        MeasureUnitCode measureUnitCode = GetDefinedMeasureUnitCode(measureUnit);

        if (NullChecked(measureUnitType, nameof(measureUnitType)) == measureUnitCode.GetMeasureUnitType()) return;

        throw InvalidMeasureUnitEnumArgumentException(measureUnit);
    }

    /// <summary>
    /// Validates the measure unit code by definition.
    /// </summary>
    /// <param name="measureUnitCode">The measure unit code.</param>
    /// <param name="paramName">The parameter name.</param>
    public static void ValidateMeasureUnitCodeByDefinition(MeasureUnitCode measureUnitCode, string paramName)
    {
        if (Enum.IsDefined(measureUnitCode)) return;

        throw InvalidMeasureUnitCodeEnumArgumentException(measureUnitCode, paramName);
    }
    #endregion

    /// <summary>
    /// Gets the default measure unit.
    /// </summary>
    /// <returns>The default measure unit.</returns>
    public Enum GetDefaultMeasureUnit()
    {
        return GetMeasureUnitCode().GetDefaultMeasureUnit()!;
    }

    /// <summary>
    /// Gets the default measure unit names.
    /// </summary>
    /// <returns>An enumerable collection of default measure unit names.</returns>
    public IEnumerable<string> GetDefaultMeasureUnitNames()
    {
        return GetDefaultNames(GetMeasureUnitCode());
    }

    /// <summary>
    /// Gets the measure unit type.
    /// </summary>
    /// <returns>The measure unit type.</returns>
    public Type GetMeasureUnitType()
    {
        return MeasureUnitTypeCollection[GetMeasureUnitCode()];
    }

    /// <summary>
    /// Validates the measure unit code for the specified measurable entity.
    /// </summary>
    /// <param name="measurable">The measurable entity.</param>
    /// <param name="paramName">The parameter name.</param>
    public void ValidateMeasureUnitCode(IMeasurable? measurable, [DisallowNull] string paramName)
    {
        MeasureUnitCode measureUnitCode = NullChecked(measurable, paramName).GetMeasureUnitCode();

        ValidateMeasureUnitCode(measureUnitCode, paramName);
    }

    #region Override methods
    /// <summary>
    /// Determines whether the specified object is equal to the current object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
    public override bool Equals(object? obj)
    {
        return obj is IMeasurable other
            && GetMeasureUnitCode().Equals(other.GetMeasureUnitCode());
    }

    /// <summary>
    /// Serves as the default hash function.
    /// </summary>
    /// <returns>A hash code for the current object.</returns>
    public override int GetHashCode()
    {
        return GetMeasureUnitCode().GetHashCode();
    }
    #endregion

    #region Virtual methods
    /// <summary>
    /// Gets the measure unit code.
    /// </summary>
    /// <returns>The measure unit code.</returns>
    public /*virtual*/ MeasureUnitCode GetMeasureUnitCode()
    {
        return GetMeasureUnitCode(GetBaseMeasureUnit());
    }

    /// <summary>
    /// Gets the quantity type code.
    /// </summary>
    /// <returns>The quantity type code.</returns>
    public virtual TypeCode GetQuantityTypeCode()
    {
        return GetMeasureUnitCode().GetQuantityTypeCode();
    }

    /// <summary>
    /// Determines whether the specified measure unit code is present.
    /// </summary>
    /// <param name="measureUnitCode">The measure unit code.</param>
    /// <returns>true if the measure unit code is present; otherwise, false.</returns>
    public virtual bool HasMeasureUnitCode(MeasureUnitCode measureUnitCode)
    {
        return measureUnitCode == GetMeasureUnitCode();
    }

    /// <summary>
    /// Validates the measure unit.
    /// </summary>
    /// <param name="measureUnit">The measure unit.</param>
    /// <param name="paramName">The parameter name.</param>
    public virtual void ValidateMeasureUnit(Enum? measureUnit, [DisallowNull] string paramName)
    {
        MeasureUnitElements measureUnitElements = GetMeasureUnitElements(measureUnit, paramName);

        ValidateMeasureUnitCode(measureUnitElements.MeasureUnitCode, paramName);
    }

    /// <summary>
    /// Validates the measure unit code.
    /// </summary>
    /// <param name="measureUnitCode">The measure unit code.</param>
    /// <param name="paramName">The parameter name.</param>

    public virtual void ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, [DisallowNull] string paramName)
    {
        if (HasMeasureUnitCode(measureUnitCode)) return;

        throw InvalidMeasureUnitCodeEnumArgumentException(measureUnitCode, paramName);
    }
    #endregion

    #region Abstract methods
    public abstract Enum GetBaseMeasureUnit();
    #endregion
    #endregion
}
