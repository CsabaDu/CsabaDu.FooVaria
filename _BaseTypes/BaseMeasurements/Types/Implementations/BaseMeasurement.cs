using System.Reflection;

namespace CsabaDu.FooVaria.BaseTypes.BaseMeasurements.Types.Implementations;

/// <summary>
/// Represents the base class for measurements.
/// </summary>
/// <param name="rootObject">The root object associated with the measurement.</param>
/// <param name="paramName">The parameter name associated with the measurement.</param>
public abstract class BaseMeasurement(IRootObject rootObject, string paramName) : Measurable(rootObject, paramName), IBaseMeasurement
{
    #region Records
    /// <summary>
    /// Represents the elements of a measurement.
    /// </summary>
    /// <param name="MeasureUnit">The measure unit.</param>
    /// <param name="MeasureUnitCode">The code of the measure unit.</param>
    /// <param name="ExchangeRate">The exchange rate of the measure unit.</param>
    public sealed record MeasurementElements(Enum MeasureUnit, MeasureUnitCode MeasureUnitCode, decimal ExchangeRate) : MeasureUnitElements(MeasureUnit, MeasureUnitCode);
    #endregion

    #region Static fields
    /// <summary>
    /// The count of constant exchange rates.
    /// </summary>
    public static readonly int ConstantExchangeRateCount;

    #region Private static fields
    private const string Unit = nameof(Unit);
    private const string ExchangeRates = nameof(ExchangeRates);

    private static readonly decimal[] AreaExchangeRates = [100, 10000, 1000000];
    private static readonly decimal[] DistanceExchangeRates = [1000];
    private static readonly decimal[] ExtentExchangeRates = [10, 100, 1000];
    private static readonly decimal[] TimePeriodExchangeRates = [60, 1440, 10080, 14400];
    private static readonly decimal[] VolumeExchangeRates = [1000, 1000000, 1000000000];
    private static readonly decimal[] WeightExchangeRates = [1000, 1000000];
    #endregion
    #endregion

    #region Constructors
    #region Static constructor
    /// <summary>
    /// Initializes static members of the <see cref="BaseMeasurement"/> class.
    /// </summary>
    static BaseMeasurement()
    {
        ConstantExchangeRateCollection = InitConstantExchangeRateCollection();
        ConstantExchangeRateCount = ConstantExchangeRateCollection.Count;
        ExchangeRateCollection = new(ConstantExchangeRateCollection);
        CustomNameCollection = [];
    }

    #endregion
    #endregion

    #region Properties
    #region Static properties
    /// <summary>
    /// Gets the collection of constant exchange rates.
    /// </summary>
    public static Dictionary<object, decimal> ConstantExchangeRateCollection { get; }

    /// <summary>
    /// Gets or sets the collection of exchange rates.
    /// </summary>
    public static Dictionary<object, decimal> ExchangeRateCollection { get; protected set; }

    /// <summary>
    /// Gets or sets the collection of custom names.
    /// </summary>
    public static Dictionary<object, string> CustomNameCollection { get; protected set; }
    #endregion
    #endregion

    #region Public methods
    #region Static methods
    /// <summary>
    /// Gets all custom measure units that are not used.
    /// </summary>
    /// <returns>An enumerable collection of unused custom measure units.</returns>
    public static IEnumerable<Enum> GetAllNotUsedCustomMeasureUnits()
    {
        IEnumerable<Enum> notUsedCustomMeasureUnits = GetNotUsedCustomMeasureUnits(MeasureUnitCodes[0]);

        for (int i = 1; i < MeasureUnitCodes.Length; i++)
        {
            IEnumerable<Enum> next = GetNotUsedCustomMeasureUnits(MeasureUnitCodes[i]);
            notUsedCustomMeasureUnits = notUsedCustomMeasureUnits.Union(next);
        }

        return notUsedCustomMeasureUnits;
    }

    /// <summary>
    /// Gets the custom name for the specified measure unit.
    /// </summary>
    /// <param name="measureUnit">The measure unit.</param>
    /// <returns>The custom name if found; otherwise, null.</returns>
    public static string? GetCustomName(Enum measureUnit)
    {
        return CustomNameCollection.FirstOrDefault(x => x.Key == measureUnit).Value;
    }

    /// <summary>
    /// Gets the collection of custom names for the specified measure unit code.
    /// </summary>
    /// <param name="measureUnitCode">The measure unit code.</param>
    /// <returns>A dictionary of custom names.</returns>
    public static Dictionary<object, string> GetCustomNameCollection(MeasureUnitCode measureUnitCode)
    {
        return GetMeasureUnitBasedCollection(CustomNameCollection, measureUnitCode);
    }

    /// <summary>
    /// Gets the exchange rate for the specified name.
    /// </summary>
    /// <param name="name">The name of the measure unit.</param>
    /// <returns>The exchange rate.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the name is out of range.</exception>
    public static decimal GetExchangeRate(string name)
    {
        const string paramName = nameof(name);
        Enum? measureUnit = GetMeasureUnit(NullChecked(name, paramName));

        if (measureUnit is not null) return GetExchangeRate(measureUnit, paramName);

        throw NameArgumentOutOfRangeException(name);
    }

    /// <summary>
    /// Gets the name of the specified measure unit.
    /// </summary>
    /// <param name="measureUnit">The measure unit.</param>
    /// <returns>The name of the measure unit.</returns>
    public static string GetMeasureUnitName(Enum measureUnit)
    {
        return GetCustomName(measureUnit) ?? GetDefaultName(measureUnit);
    }

    /// <summary>
    /// Gets the measure unit for the specified name.
    /// </summary>
    /// <param name="name">The name of the measure unit.</param>
    /// <returns>The measure unit if found; otherwise, null.</returns>
    public static Enum? GetMeasureUnit(string name)
    {
        name ??= string.Empty;

        return (Enum)CustomNameCollection.FirstOrDefault(x => x.Value == name).Key
            ?? (Enum)GetMeasureUnitCollection().FirstOrDefault(x => x.Key == name).Value;
    }

    /// <summary>
    /// Gets the collection of measure units for the specified measure unit code.
    /// </summary>
    /// <param name="measureUnitCode">The measure unit code.</param>
    /// <returns>A dictionary of measure units.</returns>
    public static Dictionary<string, object> GetMeasureUnitCollection(MeasureUnitCode measureUnitCode)
    {
        IEnumerable<object> validMeasureUnits = GetValidMeasureUnits(measureUnitCode);
        IDictionary<object, string> customNameCollection = GetMeasureUnitBasedCollection(CustomNameCollection, measureUnitCode);

        return GetMeasureUnitCollection(validMeasureUnits, customNameCollection);
    }

    /// <summary>
    /// Gets the collection of measure units.
    /// </summary>
    /// <returns>A dictionary of measure units.</returns>
    public static Dictionary<string, object> GetMeasureUnitCollection()
    {
        IEnumerable<object> validMeasureUnits = GetValidMeasureUnits();

        return GetMeasureUnitCollection(validMeasureUnits, CustomNameCollection);
    }

    /// <summary>
    /// Gets the custom measure units that are not used for the specified measure unit code.
    /// </summary>
    /// <param name="measureUnitCode">The measure unit code.</param>
    /// <returns>An enumerable collection of unused custom measure units.</returns>
    public static IEnumerable<Enum> GetNotUsedCustomMeasureUnits(MeasureUnitCode measureUnitCode)
    {
        ValidateCustomMeasureUnitCode(measureUnitCode);

        Type measureUnitType = measureUnitCode.GetMeasureUnitType();
        IEnumerable<Enum> customMeasureUnits = Enum.GetValues(measureUnitType).Cast<Enum>();

        return customMeasureUnits.Where(x => !ExchangeRateCollection.ContainsKey(x));
    }

    /// <summary>
    /// Initializes custom exchange rates for the specified measure unit code.
    /// </summary>
    /// <param name="measureUnitCode">The measure unit code.</param>
    /// <param name="customExchangeRateCollection">The collection of custom exchange rates.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the exchange rate is out of range.</exception>
    public static void InitCustomExchangeRates(MeasureUnitCode measureUnitCode, IDictionary<string, decimal> customExchangeRateCollection)
    {
        ValidateCustomMeasureUnitCode(measureUnitCode);

        int count = NullChecked(customExchangeRateCollection, nameof(customExchangeRateCollection)).Count;

        for (int i = 0; i < count; i++)
        {
            KeyValuePair<string, decimal> namedCustomExchangeRate = customExchangeRateCollection.ElementAt(i);
            string? customName = namedCustomExchangeRate.Key;
            decimal exchangeRate = namedCustomExchangeRate.Value;
            Enum measureUnit = GetNotUsedCustomMeasureUnits(measureUnitCode).First();

            if (!TrySetCustomMeasureUnit(measureUnit, exchangeRate, customName))
            {
                throw DecimalArgumentOutOfRangeException(nameof(exchangeRate), exchangeRate);
            }
        }
    }

    /// <summary>
    /// Sets a custom measure unit with the specified parameters.
    /// </summary>
    /// <param name="measureUnit">The measure unit.</param>
    /// <param name="exchangeRate">The exchange rate.</param>
    /// <param name="customName">The custom name.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the measure unit is invalid.</exception>
    public static void SetCustomMeasureUnit(Enum measureUnit, decimal exchangeRate, string customName)
    {
        if (TrySetCustomMeasureUnit(measureUnit, exchangeRate, customName)) return;

        throw InvalidMeasureUnitEnumArgumentException(measureUnit);
    }

    /// <summary>
    /// Sets a custom measure unit with the specified parameters.
    /// </summary>
    /// <param name="customName">The custom name.</param>
    /// <param name="measureUnitCode">The measure unit code.</param>
    /// <param name="exchangeRate">The exchange rate.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the measure unit code is invalid.</exception>
    public static void SetCustomMeasureUnit(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate)
    {
        ValidateCustomMeasureUnitCode(measureUnitCode);

        Enum measureUnit = GetNotUsedCustomMeasureUnits(measureUnitCode).OrderBy(x => x).First();

        SetCustomMeasureUnit(measureUnit, exchangeRate, customName);
    }

    /// <summary>
    /// Sets a custom name for the specified measure unit.
    /// </summary>
    /// <param name="measureUnit">The measure unit.</param>
    /// <param name="customName">The custom name.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the custom name is invalid.</exception>
    public static void SetCustomName(Enum measureUnit, string customName)
    {
        if (TrySetCustomName(measureUnit, customName)) return;

        throw NameArgumentOutOfRangeException(customName);
    }

    /// <summary>
    /// Tries to get the measure unit for the specified name.
    /// </summary>
    /// <param name="name">The name of the measure unit.</param>
    /// <param name="measureUnit">When this method returns, contains the measure unit if found; otherwise, null.</param>
    /// <returns>true if the measure unit was found; otherwise, false.</returns>
    public static bool TryGetMeasureUnit(string name, [NotNullWhen(true)] out Enum? measureUnit)
    {
        measureUnit = GetMeasureUnit(name);

        return measureUnit is not null;
    }

    /// <summary>
    /// Tries to get the measure unit for the specified measure unit code and exchange rate.
    /// </summary>
    /// <param name="measureUnitCode">The measure unit code.</param>
    /// <param name="exchangeRate">The exchange rate.</param>
    /// <param name="measureUnit">When this method returns, contains the measure unit if found; otherwise, null.</param>
    /// <returns>true if the measure unit was found; otherwise, false.</returns>
    public static bool TryGetMeasureUnit(MeasureUnitCode measureUnitCode, decimal exchangeRate, [NotNullWhen(true)] out Enum? measureUnit)
    {
        measureUnit = (Enum)GetExchangeRateCollection(measureUnitCode).FirstOrDefault(x => x.Value == exchangeRate).Key;

        return measureUnit is not null;
    }

    /// <summary>
    /// Tries to set a custom measure unit with the specified parameters.
    /// </summary>
    /// <param name="measureUnit">The measure unit.</param>
    /// <param name="exchangeRate">The exchange rate.</param>
    /// <param name="customName">The custom name.</param>
    /// <returns>true if the custom measure unit was set successfully; otherwise, false.</returns>
    public static bool TrySetCustomMeasureUnit(Enum measureUnit, decimal exchangeRate, string customName)
    {
        if (!IsCustomMeasureUnit(measureUnit)) return false;

        if ((int)(object)measureUnit == default) return false;

        if (!exchangeRate.IsValidExchangeRate()) return false;

        if (!TrySetExchangeRate(measureUnit, exchangeRate)) return false;

        if (TrySetCustomName(measureUnit, customName)) return true;

        if (RemoveExchangeRate(measureUnit)) return false;

        throw new InvalidOperationException(null);
    }

    /// <summary>
    /// Tries to set a custom measure unit with the specified parameters.
    /// </summary>
    /// <param name="customName">The custom name.</param>
    /// <param name="measureUnitCode">The measure unit code.</param>
    /// <param name="exchangeRate">The exchange rate.</param>
    /// <returns>true if the custom measure unit was set successfully; otherwise, false.</returns>
    public static bool TrySetCustomMeasureUnit(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate)
    {
        if (!measureUnitCode.IsCustomMeasureUnitCode()) return false;

        Enum measureUnit = GetNotUsedCustomMeasureUnits(measureUnitCode).OrderBy(x => x).First();

        return TrySetCustomMeasureUnit(measureUnit, exchangeRate, customName);
    }

    /// <summary>
    /// Tries to set a custom name for the specified measure unit.
    /// </summary>
    /// <param name="measureUnit">The measure unit.</param>
    /// <param name="customName">The custom name.</param>
    /// <returns>true if the custom name was set successfully; otherwise, false.</returns>
    public static bool TrySetCustomName(Enum? measureUnit, string? customName)
    {
        if (measureUnit is null) return false;

        if (customName is null) return false;

        if (!IsValidMeasureUnit(measureUnit)) return false;

        if (CustomNameCollection.FirstOrDefault(x => x.Key == measureUnit).Value == customName) return true;

        if (!IsValidCustomNameParam(customName)) return false;

        return CustomNameCollection.TryAdd(measureUnit, customName);
    }

    /// <summary>
    /// Validates the custom measure unit code.
    /// </summary>
    /// <param name="measureUnitCode">The measure unit code.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the measure unit code is invalid.</exception>
    public static void ValidateCustomMeasureUnitCode(MeasureUnitCode measureUnitCode)
    {
        if (measureUnitCode.IsCustomMeasureUnitCode()) return;

        throw InvalidMeasureUnitCodeEnumArgumentException(measureUnitCode);
    }

    /// <summary>
    /// Gets the collection of constant exchange rates for the specified measure unit code.
    /// </summary>
    /// <param name="measureUnitCode">The measure unit code.</param>
    /// <returns>A dictionary of constant exchange rates.</returns>
    public static Dictionary<object, decimal> GetConstantExchangeRateCollection(MeasureUnitCode measureUnitCode)
    {
        return GetMeasureUnitBasedCollection(ConstantExchangeRateCollection, measureUnitCode);
    }

    /// <summary>
    /// Gets all constant measure units.
    /// </summary>
    /// <returns>An enumerable collection of constant measure units.</returns>
    public static IEnumerable<object> GetConstantMeasureUnits()
    {
        foreach (object item in ConstantExchangeRateCollection.Keys)
        {
            yield return item;
        }
    }

    /// <summary>
    /// Gets the exchange rate for the specified measure unit.
    /// </summary>
    /// <param name="measureUnit">The measure unit.</param>
    /// <param name="paramName">The parameter name.</param>
    /// <returns>The exchange rate.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the measure unit is invalid.</exception>
    public static decimal GetExchangeRate(Enum? measureUnit, string paramName)
    {
        measureUnit = GetMeasureUnitElements(measureUnit, paramName).MeasureUnit;
        decimal exchangeRate = ExchangeRateCollection.FirstOrDefault(x => x.Key.Equals(measureUnit)).Value;

        if (exchangeRate != default) return exchangeRate;

        throw InvalidMeasureUnitEnumArgumentException(measureUnit!, paramName);
    }

    /// <summary>
    /// Gets the collection of exchange rates for the specified measure unit code.
    /// </summary>
    /// <param name="measureUnitCode">The measure unit code.</param>
    /// <returns>A dictionary of exchange rates.</returns>
    public static Dictionary<object, decimal> GetExchangeRateCollection(MeasureUnitCode measureUnitCode)
    {
        return GetMeasureUnitBasedCollection(ExchangeRateCollection, measureUnitCode);
    }

    /// <summary>
    /// Gets the measurement elements for the specified context and parameter name.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="paramName">The parameter name.</param>
    /// <returns>The measurement elements.</returns>
    public static MeasurementElements GetMeasurementElements(Enum? context, string paramName)
    {
        Enum? measureUnit = context is MeasureUnitCode measureUnitCode ?
            Defined(measureUnitCode, paramName).GetDefaultMeasureUnit()
            : DefinedMeasureUnit(context, paramName);
        decimal exchangeRate = GetExchangeRate(measureUnit, paramName);
        measureUnitCode = context!.Equals(measureUnit) ?
            GetMeasureUnitCode(context)
            : (MeasureUnitCode)context!;

        return new(measureUnit!, measureUnitCode, exchangeRate);
    }

    /// <summary>
    /// Gets all valid measure units.
    /// </summary>
    /// <returns>An enumerable collection of valid measure units.</returns>
    public static IEnumerable<object> GetValidMeasureUnits()
    {
        return ExchangeRateCollection.Keys;
    }

    /// <summary>
    /// Gets the valid measure units for the specified measure unit code.
    /// </summary>
    /// <param name="measureUnitCode">The measure unit code.</param>
    /// <returns>An enumerable collection of valid measure units.</returns>
    public static IEnumerable<object> GetValidMeasureUnits(MeasureUnitCode measureUnitCode)
    {
        Type measureUnitType = measureUnitCode.GetMeasureUnitType();

        return GetValidMeasureUnits().Where(x => x.GetType() == measureUnitType);
    }

    /// <summary>
    /// Determines whether the specified measure unit is a custom measure unit.
    /// </summary>
    /// <param name="measureUnit">The measure unit.</param>
    /// <returns>true if the measure unit is a custom measure unit; otherwise, false.</returns>
    public static bool IsCustomMeasureUnit(Enum measureUnit)
    {
        if (!IsDefinedMeasureUnit(measureUnit)) return false;

        MeasureUnitCode measureUnitCode = GetDefinedMeasureUnitCode(measureUnit);

        return measureUnitCode.IsCustomMeasureUnitCode();
    }

    /// <summary>
    /// Determines whether the specified exchange rate is valid for the given measure unit.
    /// </summary>
    /// <param name="exchangeRate">The exchange rate.</param>
    /// <param name="measureUnit">The measure unit.</param>
    /// <returns>true if the exchange rate is valid; otherwise, false.</returns>
    public static bool IsValidExchangeRate(decimal exchangeRate, Enum measureUnit)
    {
        return exchangeRate == GetExchangeRate(measureUnit, nameof(measureUnit));
    }

    /// <summary>
    /// Determines whether the specified measure unit is valid.
    /// </summary>
    /// <param name="measureUnit">The measure unit.</param>
    /// <returns>true if the measure unit is valid; otherwise, false.</returns>
    public static bool IsValidMeasureUnit(Enum? measureUnit)
    {
        return measureUnit is not null
            && GetValidMeasureUnits().Contains(measureUnit);
    }

    /// <summary>
    /// Determines whether the specified measure unit is valid for the given measure unit code.
    /// </summary>
    /// <param name="measureUnit">The measure unit.</param>
    /// <param name="measureUnitCode">The measure unit code.</param>
    /// <returns>true if the measure unit is valid for the given measure unit code; otherwise, false.</returns>
    public static bool IsValidMeasureUnit(Enum? measureUnit, MeasureUnitCode measureUnitCode)
    {
        return measureUnit is not null
            && GetValidMeasureUnits(measureUnitCode).Contains(measureUnit);
    }

    /// <summary>
    /// Determines whether the specified names are equal.
    /// </summary>
    /// <param name="name">The first name.</param>
    /// <param name="otherName">The second name.</param>
    /// <returns>true if the names are equal; otherwise, false.</returns>
    public static bool NameEquals(string name, string otherName)
    {
        return string.Equals(name, otherName, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Validates the custom name.
    /// </summary>
    /// <param name="customName">The custom name.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the custom name is invalid.</exception>
    public static void ValidateCustomName(string? customName)
    {
        if (IsValidCustomNameParam(NullChecked(customName, nameof(customName)))) return;

        throw NameArgumentOutOfRangeException(nameof(customName), customName!);
    }

    /// <summary>
    /// Validates the exchange rate for the specified measure unit.
    /// </summary>
    /// <param name="exchangeRate">The exchange rate.</param>
    /// <param name="paramName">The parameter name.</param>
    /// <param name="measureUnit">The measure unit.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the exchange rate is invalid.</exception>
    public static void ValidateExchangeRate(decimal exchangeRate, string paramName, Enum measureUnit)
    {
        if (IsValidExchangeRate(exchangeRate, measureUnit)) return;

        throw DecimalArgumentOutOfRangeException(paramName, exchangeRate);
    }
    #endregion

    #region Abstract methods
    /// <summary>
    /// Gets the name of the measurement.
    /// </summary>
    /// <returns>The name of the measurement.</returns>
    public abstract string GetName();
    #endregion

    #region Override methods
    #region Sealed methods
    /// <summary>
    /// Determines whether the specified object is equal to the current object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
    public override sealed bool Equals(object? obj)
    {
        return obj is IBaseMeasurement other
            && Equals(other);
    }

    /// <summary>
    /// Serves as the default hash function.
    /// </summary>
    /// <returns>A hash code for the current object.</returns>
    public override sealed int GetHashCode()
    {
        return HashCode.Combine(GetMeasureUnitCode(), GetExchangeRate());
    }

    /// <summary>
    /// Gets the quantity type code.
    /// </summary>
    /// <returns>The quantity type code.</returns>
    public override sealed TypeCode GetQuantityTypeCode()
    {
        return base.GetQuantityTypeCode();
    }

    /// <summary>
    /// Determines whether the specified measure unit code is present.
    /// </summary>
    /// <param name="measureUnitCode">The measure unit code.</param>
    /// <returns>true if the measure unit code is present; otherwise, false.</returns>
    public override sealed bool HasMeasureUnitCode(MeasureUnitCode measureUnitCode)
    {
        return base.HasMeasureUnitCode(measureUnitCode);
    }

    /// <summary>
    /// Validates the measure unit.
    /// </summary>
    /// <param name="measureUnit">The measure unit.</param>
    /// <param name="paramName">The parameter name.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the measure unit is invalid.</exception>
    public override sealed void ValidateMeasureUnit(Enum? measureUnit, string paramName)
    {
        if (IsExchangeableTo(NullChecked(measureUnit, paramName))) return;

        throw InvalidMeasureUnitEnumArgumentException(measureUnit!, paramName);
    }
    #endregion
    #endregion

    /// <summary>
    /// Compares the current object with another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>A value that indicates the relative order of the objects being compared.</returns>
    public int CompareTo(IBaseMeasurement? other)
    {
        if (other is null) return 1;

        other.ValidateMeasureUnitCode(GetMeasureUnitCode(), nameof(other));

        return GetExchangeRate().CompareTo(other.GetExchangeRate());
    }

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>true if the current object is equal to the other parameter; otherwise, false.</returns>
    public bool Equals(IBaseMeasurement? other)
    {
        return base.Equals(other)
            && GetExchangeRate() == other.GetExchangeRate();
    }

    /// <summary>
    /// Gets the base measurement for the specified context.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <returns>The base measurement if found; otherwise, null.</returns>
    public IBaseMeasurement? GetBaseMeasurement(Enum context)
    {
        IBaseMeasurementFactory factory = (IBaseMeasurementFactory)GetFactory();

        return factory.CreateBaseMeasurement(context);
    }

    /// <summary>
    /// Gets the collection of constant exchange rates.
    /// </summary>
    /// <returns>A dictionary of constant exchange rates.</returns>
    public IDictionary<object, decimal> GetConstantExchangeRateCollection()
    {
        return GetConstantExchangeRateCollection(GetMeasureUnitCode());
    }

    /// <summary>
    /// Gets the exchange rate for the base measure unit.
    /// </summary>
    /// <returns>The exchange rate.</returns>
    public decimal GetExchangeRate()
    {
        Enum measureUnit = GetBaseMeasureUnit();

        return GetExchangeRate(measureUnit, string.Empty);
    }

    /// <summary>
    /// Gets the collection of exchange rates for the measure unit code.
    /// </summary>
    /// <returns>A dictionary of exchange rates.</returns>
    public IDictionary<object, decimal> GetExchangeRateCollection()
    {
        return GetExchangeRateCollection(GetMeasureUnitCode());
    }

    /// <summary>
    /// Determines whether the current object is exchangeable to the specified context.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <returns>true if the current object is exchangeable to the context; otherwise, false.</returns>
    public bool IsExchangeableTo(Enum? context)
    {
        if (context is MeasureUnitCode measureUnitCode) return HasMeasureUnitCode(measureUnitCode);

        return IsValidMeasureUnit(context) && HasMeasureUnitCode(GetMeasureUnitCode(), context!);
    }

    /// <summary>
    /// Calculates the proportional value to another measurement.
    /// </summary>
    /// <param name="other">The other measurement.</param>
    /// <returns>The proportional value.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the measure unit code is invalid.</exception>
    public decimal ProportionalTo(IBaseMeasurement? other)
    {
        const string paramName = nameof(other);

        MeasureUnitCode measureUnitCode = NullChecked(other, paramName).GetMeasureUnitCode();

        if (HasMeasureUnitCode(measureUnitCode)) return GetExchangeRate() / other!.GetExchangeRate();

        throw InvalidMeasureUnitCodeEnumArgumentException(measureUnitCode, paramName);
    }

    /// <summary>
    /// Validates the exchange rate for the base measure unit.
    /// </summary>
    /// <param name="exchangeRate">The exchange rate.</param>
    /// <param name="paramName">The parameter name.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the exchange rate is invalid.</exception>
    public void ValidateExchangeRate(decimal exchangeRate, string paramName)
    {
        ValidateExchangeRate(exchangeRate, paramName, GetBaseMeasureUnit());
    }
    #endregion

    #region Internal methods
    #region Static methods
    /// <summary>
    /// Restores the constant exchange rates to their default values.
    /// </summary>
    /// <remarks>
    /// This method is intended for use in unit tests to reset the state of the exchange rate collections.
    /// </remarks>
    internal static void RestoreConstantExchangeRates() // for unittest class restore purposes
    {
        CustomNameCollection.Clear();
        ExchangeRateCollection = new(ConstantExchangeRateCollection);
    }
    #endregion
    #endregion

    #region Protected methods
    #region Static methods
    /// <summary>
    /// Determines whether the specified custom name is valid.
    /// </summary>
    /// <param name="customName">The custom name to validate.</param>
    /// <returns><c>true</c> if the custom name is valid; otherwise, <c>false</c>.</returns>
    protected static bool IsValidCustomNameParam(string? customName)
    {
        return !string.IsNullOrWhiteSpace(customName)
            && customName != string.Empty
            && doesNotContainCustomName(CustomNameCollection.Values)
            && doesNotContainCustomName(GetDefaultNames());

        #region Local methods
        bool doesNotContainCustomName(IEnumerable<string> names)
        {
            return !names.Any(x => NameEquals(x, customName));
        }
        #endregion
    }

    /// <summary>
    /// Gets a collection of measure unit-based items for the specified measure unit code.
    /// </summary>
    /// <typeparam name="T">The type of the items in the collection.</typeparam>
    /// <param name="measureUnitBasedCollection">The collection of measure unit-based items.</param>
    /// <param name="measureUnitCode">The measure unit code.</param>
    /// <returns>A dictionary of measure unit-based items.</returns>
    protected static Dictionary<object, T> GetMeasureUnitBasedCollection<T>(IDictionary<object, T> measureUnitBasedCollection, MeasureUnitCode measureUnitCode)
        where T : notnull
    {
        ValidateMeasureUnitCodeByDefinition(measureUnitCode, nameof(measureUnitCode));

        string measureUnitCodeName = Enum.GetName(measureUnitCode)!;

        return measureUnitBasedCollection
            .Where(x => x.Key.GetType().Name == measureUnitCodeName)
            .OrderBy(x => x.Key)
            .ToDictionary(x => x.Key, x => x.Value);
    }

    /// <summary>
    /// Tries to set the exchange rate for the specified measure unit.
    /// </summary>
    /// <param name="measureUnit">The measure unit.</param>
    /// <param name="exchangeRate">The exchange rate.</param>
    /// <returns><c>true</c> if the exchange rate was set successfully; otherwise, <c>false</c>.</returns>
    protected static bool TrySetExchangeRate(Enum measureUnit, decimal exchangeRate)
    {
        if (!IsDefinedMeasureUnit(measureUnit)) return false;

        if (ExchangeRateCollection.ContainsKey(measureUnit) && exchangeRate == ExchangeRateCollection[measureUnit]) return true;

        if (exchangeRate <= 0) return false;

        return ExchangeRateCollection.TryAdd(measureUnit, exchangeRate);
    }
    #endregion
    #endregion

    #region Private methods
    #region Static methods
    /// <summary>
    /// Removes the exchange rate for the specified measure unit.
    /// </summary>
    /// <param name="measureUnit">The measure unit.</param>
    /// <returns><c>true</c> if the exchange rate was removed successfully; otherwise, <c>false</c>.</returns>
    private static bool RemoveExchangeRate(Enum measureUnit)
    {
        return ExchangeRateCollection.Remove(measureUnit);
    }

    /// <summary>
    /// Gets a collection of measure units and their corresponding names.
    /// </summary>
    /// <param name="validMeasureUnits">The collection of valid measure units.</param>
    /// <param name="customNameCollection">The collection of custom names.</param>
    /// <returns>A dictionary of measure units and their corresponding names.</returns>
    private static Dictionary<string, object> GetMeasureUnitCollection(IEnumerable<object> validMeasureUnits, IDictionary<object, string> customNameCollection)
    {
        Dictionary<string, object> measureUnitCollection = validMeasureUnits.ToDictionary(getDefaultName, x => x);

        foreach (KeyValuePair<object, string> item in customNameCollection)
        {
            measureUnitCollection.Add(item.Value, item.Key);
        }

        return measureUnitCollection;

        #region Local methods
        static string getDefaultName(object measureUnit)
        {
            return GetDefaultName((Enum)measureUnit);
        }
        #endregion
    }

    /// <summary>
    /// Initializes the collection of constant exchange rates.
    /// </summary>
    /// <returns>A dictionary of constant exchange rates.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the number of measure units does not match the number of exchange rates.</exception>
    private static Dictionary<object, decimal> InitConstantExchangeRateCollection()
    {
        FieldInfo[] privateStaticFields = typeof(BaseMeasurement).GetFields(BindingFlags.NonPublic | BindingFlags.Static);
        Dictionary<object, decimal> exchangeRateCollection = [];

        foreach (Type item in MeasureUnitTypes)
        {
<<<<<<< Updated upstream
            addExchangeRatesToCollection(getMeasureUnits(item), getExchangeRates(item));
=======
            string exchangeRatesName = item.Name.Replace("Unit", "ExchangeRates");
            FieldInfo? exchangeRatesField = privateStaticFields.FirstOrDefault(x => x.FieldType == typeof(decimal[]) && x.Name == exchangeRatesName);
            decimal[]? exchangeRates = (decimal[]?)exchangeRatesField?.GetValue(null);
            Array measureUnits = Enum.GetValues(item);
            int exchangeRateCount = exchangeRates?.Length ?? 0;

            if (exchangeRateCount != 0 && measureUnits.Length != exchangeRateCount + 1) throw new InvalidOperationException(null);

            exchangeRateCollection[measureUnits.GetValue(0)!] = decimal.One;

            for (int i = 0; i < exchangeRateCount; i++)
            {
                exchangeRateCollection[measureUnits.GetValue(i + 1)!] = exchangeRates![i];
            }
>>>>>>> Stashed changes
        }

        return exchangeRateCollection;

<<<<<<< Updated upstream
        #region Local methods
        static Array getMeasureUnits(Type measureUnitType)
        {
            return Enum.GetValues(measureUnitType);
        }

        decimal[]? getExchangeRates(Type measureUnitType)
        {
            string exchangeRatesName = measureUnitType.Name.Replace(Unit, ExchangeRates);
            FieldInfo? exchangeRatesField = privateStaticFields
                .FirstOrDefault(x => x.FieldType == typeof(decimal[]) && x.Name == exchangeRatesName);

            return (decimal[]?)exchangeRatesField?.GetValue(null);
        }

        void addExchangeRatesToCollection(Array measureUnits, decimal[]? exchangeRates)
        {
            int exchangeRateCount = exchangeRates?.Length ?? 0;

            if (exchangeRateCount != 0 && measureUnits.Length != exchangeRateCount + 1) throw new InvalidOperationException(null);

            exchangeRateCollection[measureUnits.GetValue(0)!] = decimal.One;

            for (int i = 0; i < exchangeRateCount; i++)
            {
                exchangeRateCollection[measureUnits.GetValue(i + 1)!] = exchangeRates![i];
            }
        }
        #endregion
=======
        //return initConstantExchangeRates<AreaUnit>(AreaExchangeRates)
        //    .Union(initConstantExchangeRates<Currency>())
        //    .Union(initConstantExchangeRates<DistanceUnit>(DistanceExchangeRates))
        //    .Union(initConstantExchangeRates<ExtentUnit>(ExtentExchangeRates))
        //    .Union(initConstantExchangeRates<Pieces>())
        //    .Union(initConstantExchangeRates<TimePeriodUnit>(TimePeriodExchangeRates))
        //    .Union(initConstantExchangeRates<VolumeUnit>(VolumeExchangeRates))
        //    .Union(initConstantExchangeRates<WeightUnit>(WeightExchangeRates))
        //    .ToDictionary(x => x.Key, x => x.Value);

        //#region Local methods
        //static IEnumerable<KeyValuePair<object, decimal>> initConstantExchangeRates<T>(params decimal[] exchangeRates)
        //    where T : struct, Enum
        //{
        //    yield return getMeasureUnitExchangeRatePair(default(T), decimal.One);

        //    int exchangeRateCount = exchangeRates?.Length ?? 0;

        //    if (exchangeRateCount > 0)
        //    {
        //        T[] measureUnits = Enum.GetValues<T>();
        //        int measureUnitCount = measureUnits.Length;

        //        if (measureUnitCount != exchangeRateCount + 1) throw new InvalidOperationException(null);

        //        int i = 0;

        //        foreach (decimal item in exchangeRates!)
        //        {
        //            yield return getMeasureUnitExchangeRatePair(measureUnits[++i], item);
        //        }
        //    }
        //}

        //static KeyValuePair<object, decimal> getMeasureUnitExchangeRatePair(Enum measureUnit, decimal exchangeRate)
        //{
        //    return new KeyValuePair<object, decimal>(measureUnit, exchangeRate);
        //}
        //#endregion
>>>>>>> Stashed changes
    }
    #endregion
    #endregion
}
