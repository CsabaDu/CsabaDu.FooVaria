using CsabaDu.FooVaria.Common.Enums;
using System.Xml.Linq;

namespace CsabaDu.FooVaria.Measurements.Types.Implementations
{
    internal abstract class MeasurementBase : Measurable, IMeasurementBase
    {
        #region Constructors
        #region Static constructor
        static MeasurementBase()
        {
            ConstantExchangeRateCollection = InitConstantExchangeRateCollection();
            ExchangeRateCollection = new Dictionary<object, decimal>(ConstantExchangeRateCollection);
        }
        #endregion

        private protected MeasurementBase(IMeasurementFactory factory, Enum measureUnit) : base(factory, measureUnit)
        {
        }
        #endregion

        #region Properties
        public abstract decimal ExchangeRate { get; init; }

        #region Static properties
        public static IDictionary<object, decimal> ExchangeRateCollection { get; protected set; }
        public static IDictionary<object, decimal> ConstantExchangeRateCollection { get; }
        #endregion
        #endregion

        #region Public methods
        public int CompareTo(IMeasurementBase? other)
        {
            if (other == null) return 1;

            other.ValidateMeasureUnitTypeCode(MeasureUnitTypeCode, nameof(other));

            return ExchangeRate.CompareTo(other.ExchangeRate);
        }

        public bool Equals(IMeasurementBase? other)
        {
            return MeasureUnitTypeCode.Equals(other?.MeasureUnitTypeCode)
                && other.ExchangeRate == ExchangeRate;
        }

        public IDictionary<object, decimal> GetConstantExchangeRateCollection()
        {
            return GetConstantExchangeRateCollection(MeasureUnitTypeCode);
        }

        public IDictionary<object, decimal> GetConstantExchangeRateCollection(MeasureUnitTypeCode measureUnitTypeCode)
        {
            return GetMeasureUnitBasedCollection(ConstantExchangeRateCollection, measureUnitTypeCode);
        }

        public decimal GetExchangeRate()
        {
            return ExchangeRate;
        }

        public decimal GetExchangeRate(Enum measureUnit)
        {
            decimal exchangeRate = ExchangeRateCollection.FirstOrDefault(x => x.Key.Equals(measureUnit)).Value;

            if (exchangeRate != default) return exchangeRate;

            throw InvalidMeasureUnitEnumArgumentException(measureUnit);
        }

        public IDictionary<object, decimal> GetExchangeRateCollection(MeasureUnitTypeCode measureUnitTypeCode)
        {
            return GetMeasureUnitBasedCollection(ExchangeRateCollection, measureUnitTypeCode);
        }

        public IDictionary<object, decimal> GetExchangeRateCollection()
        {
            return GetExchangeRateCollection(MeasureUnitTypeCode);
        }

        public bool IsExchangeableTo(Enum? context)
        {
            if (context is MeasureUnitTypeCode measureUnitTypeCode) return HasMeasureUnitTypeCode(measureUnitTypeCode);

            return IsValidMeasureUnit(context) && MeasureUnitTypes.HasMeasureUnitTypeCode(MeasureUnitTypeCode, context!);
        }

        public bool IsValidExchangeRate(decimal exchangeRate, Enum measureUnit)
        {
            return exchangeRate == GetExchangeRate(measureUnit);
        }

        public decimal ProportionalTo(IMeasurementBase other)
        {
            MeasureUnitTypeCode measureUnitTypeCode = NullChecked(other, nameof(other)).MeasureUnitTypeCode;

            if (HasMeasureUnitTypeCode(measureUnitTypeCode)) return ExchangeRate / other.ExchangeRate;

            throw new ArgumentOutOfRangeException(nameof(other), measureUnitTypeCode, null);
        }

        public void ValidateExchangeRate(decimal exchangeRate, string paramName, Enum measureUnit)
        {
            if (IsValidExchangeRate(exchangeRate, measureUnit)) return;

            throw DecimalArgumentOutOfRangeException(paramName, exchangeRate);
        }

        public void ValidateExchangeRate(decimal exchangeRate, string paramName)
        {
            ValidateExchangeRate(exchangeRate, paramName, GetMeasureUnit());
        }

        #region Override methods
        #region Sealed methods
        public override sealed bool Equals(object? obj)
        {
            return obj is IMeasurementBase other
                && Equals(other);
        }

        public override sealed int GetHashCode()
        {
            return HashCode.Combine(MeasureUnitTypeCode, ExchangeRate);
        }

        public override sealed void ValidateMeasureUnit(Enum measureUnit, string paramName)
        {
            if (ExchangeRateCollection.ContainsKey(measureUnit)) return;

            throw InvalidMeasureUnitEnumArgumentException(measureUnit, paramName);
        }
        #endregion
        #endregion

        #region Abstract methods
        public abstract decimal GetExchangeRate(string name);
        public abstract Enum GetMeasureUnit();
        public abstract string GetName();
        public abstract void RestoreConstantExchangeRates();
        #endregion
        #endregion

        #region Protected methods
        #region Static methods
        protected static IDictionary<object, T> GetMeasureUnitBasedCollection<T>(IDictionary<object, T> measureUnitBasedCollection, MeasureUnitTypeCode measureUnitTypeCode) where T : notnull
        {
            MeasureUnitTypes.ValidateMeasureUnitTypeCode(measureUnitTypeCode, nameof(measureUnitTypeCode));

            return measureUnitBasedCollection
                .Where(x => x.Key.GetType().Name.Equals(Enum.GetName(measureUnitTypeCode)))
                .OrderBy(x => x.Key)
                .ToDictionary(x => x.Key, x => x.Value);
        }
        #endregion
        #endregion

        #region Private methods
        #region Static methods
        private static IDictionary<object, decimal> InitConstantExchangeRateCollection()
        {
            return initConstantExchangeRates<AreaUnit>(100, 10000, 1000000)
                .Union(initConstantExchangeRates<Currency>())
                .Union(initConstantExchangeRates<DistanceUnit>(1000))
                .Union(initConstantExchangeRates<ExtentUnit>(10, 100, 1000))
                .Union(initConstantExchangeRates<Pieces>())
                .Union(initConstantExchangeRates<TimePeriodUnit>(60, 1440, 10080, 14400))
                .Union(initConstantExchangeRates<VolumeUnit>(1000, 1000000, 1000000000))
                .Union(initConstantExchangeRates<WeightUnit>(1000, 1000000))
                .ToDictionary(x => x.Key, x => x.Value);

            #region Local methods
            static IEnumerable<KeyValuePair<object, decimal>> initConstantExchangeRates<T>(params decimal[] exchangeRates) where T : struct, Enum
            {
                yield return getMeasureUnitExchangeRatePair(default(T), decimal.One);

                int exchangeRateCount = exchangeRates?.Length ?? 0;

                if (exchangeRateCount > 0)
                {
                    T[] measureUnits = Enum.GetValues<T>();
                    int measureUnitCount = measureUnits.Length;

                    if (measureUnitCount == 0 || measureUnitCount != exchangeRateCount + 1) throw new InvalidOperationException(null);

                    int i = 0;

                    foreach (decimal item in exchangeRates!)
                    {
                        yield return getMeasureUnitExchangeRatePair(measureUnits[++i], item);
                    }
                }
            }

            static KeyValuePair<object, decimal> getMeasureUnitExchangeRatePair(Enum measureUnit, decimal exchangeRate)
            {
                return new KeyValuePair<object, decimal>(measureUnit, exchangeRate);
            }
            #endregion
        }
        #endregion
        #endregion
    }

    internal abstract class Measurement : MeasurementBase, IMeasurement
    {
        static Measurement()
        {
            CustomNameCollection = new Dictionary<object, string>();
        }

        private protected Measurement(IMeasurementFactory factory, Enum measureUnit) : base(factory, measureUnit)
        {
            ExchangeRate = GetExchangeRate(measureUnit);
            MeasureUnit = measureUnit;
        }

        public object MeasureUnit { get; init; }
        public override sealed decimal ExchangeRate { get; init; }
        public static IDictionary<object, string> CustomNameCollection { get; protected set; }

        public string? GetCustomName(Enum measureUnit)
        {
            return CustomNameCollection.FirstOrDefault(x => x.Key == measureUnit).Value;
        }

        public string? GetCustomName()
        {
            return GetCustomName(GetMeasureUnit());
        }

        public IDictionary<object, string> GetCustomNameCollection(MeasureUnitTypeCode measureUnitTypeCode)
        {
            return GetMeasureUnitBasedCollection(CustomNameCollection, measureUnitTypeCode);
        }

        public IMeasurement GetDefault(MeasureUnitTypeCode measureUnitTypeCode)
        {
            return GetFactory().CreateDefault(measureUnitTypeCode);
        }

        public string GetDefaultName()
        {
            return Enum.GetName(MeasureUnit.GetType(), MeasureUnit)!;
        }

        public string GetDefaultName(Enum measureUnit)
        {
            Type measureUnitType = DefinedMeasureUnit(measureUnit, nameof(MeasureUnit)).GetType();

            return Enum.GetName(measureUnitType, measureUnit)!;
        }

        public IMeasurement GetMeasurement(Enum measureUnit)
        {
            return GetFactory().Create(measureUnit);
        }

        public IMeasurement GetMeasurement(IMeasurement other)
        {
            return GetFactory().Create(other);
        }

        public IMeasurement GetMeasurement(string name)
        {
            return GetFactory().Create(name);
        }

        public IMeasurement GetMeasurement(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate)
        {
            return GetFactory().Create(customName, measureUnitTypeCode, exchangeRate);
        }

        public IMeasurement GetMeasurement(Enum measureUnit, decimal exchangeRate, string customName)
        {
            return GetFactory().Create(measureUnit, exchangeRate, customName);
        }

        public Enum? GetMeasureUnit(string name)
        {
            name ??= string.Empty;

            return (Enum)GetMeasureUnitCollection().FirstOrDefault(x => x.Key == name).Value;
        }

        public IDictionary<string, object> GetMeasureUnitCollection(MeasureUnitTypeCode measureUnitTypeCode)
        {
            IEnumerable<object> validMeasureUnits = GetValidMeasureUnits(measureUnitTypeCode);
            IDictionary<object, string> customNameCollection = GetMeasureUnitBasedCollection(CustomNameCollection, measureUnitTypeCode);

            return GetMeasureUnitCollection(validMeasureUnits, customNameCollection);
        }

        public IDictionary<string, object> GetMeasureUnitCollection()
        {
            IEnumerable<object> validMeasureUnits = Statics.MeasureUnits.GetValidMeasureUnits();

            return GetMeasureUnitCollection(validMeasureUnits, CustomNameCollection);
        }

        public IEnumerable<object> GetValidMeasureUnits(MeasureUnitTypeCode measureUnitTypeCode)
        {
            return Statics.MeasureUnits.GetValidMeasureUnits().Where(x => x.GetType().Equals(measureUnitTypeCode.GetMeasureUnitType()));
        }

        public void RestoreCustomNameCollection()
        {
            CustomNameCollection.Clear();
        }

        public void SetCustomName(Enum measureUnit, string customName)
        {
            if (TrySetCustomName(measureUnit, customName)) return;

            throw NameArgumentOutOfRangeException(customName);
        }

        public void SetOrReplaceCustomName(string customName)
        {
            ValidateCustomName(customName);

            Enum measureUnit = GetMeasureUnit();

            if (CustomNameCollection.TryAdd(measureUnit, customName)) return;

            if (CustomNameCollection.Remove(measureUnit)
                && CustomNameCollection.TryAdd(measureUnit, customName))
                return;

            throw new InvalidOperationException(null);
        }

        public bool TryGetMeasurement(decimal exchangeRate, [NotNullWhen(true)] out IMeasurement? measurement)
        {
            if (TryGetMeasureUnit(MeasureUnitTypeCode, exchangeRate, out Enum? measureUnit) && measureUnit != null)
            {
                measurement = GetMeasurement(measureUnit);

                return true;
            }

            measurement = null;

            return false;
        }

        public bool TryGetMeasureUnit(MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, [NotNullWhen(true)] out Enum? measureUnit)
        {
            measureUnit = (Enum)GetExchangeRateCollection(measureUnitTypeCode).FirstOrDefault(x => x.Value == exchangeRate).Key;

            return measureUnit != null;
        }

        public bool TryGetMeasureUnit(string name, [NotNullWhen(true)] out Enum? measureUnit)
        {
            measureUnit = GetMeasureUnit(name);

            return measureUnit != null;
        }

        public bool TrySetCustomName(Enum measureUnit, string customName)
        {
            if (measureUnit == null) return false;

            if (customName == null) return false;

            if (!IsValidMeasureUnit(measureUnit)) return false;

            if (CustomNameCollection.FirstOrDefault(x => x.Key == measureUnit).Value == customName) return true;

            if (!IsValidCustomNameParam(customName)) return false;

            return CustomNameCollection.TryAdd(measureUnit, customName);
        }

        public void ValidateCustomName(string? customName)
        {
            if (IsValidCustomNameParam(NullChecked(customName, nameof(customName)))) return;

            throw NameArgumentOutOfRangeException(nameof(customName), customName!);
        }

        #region Override methods
        public override IMeasurementFactory GetFactory()
        {
            return (IMeasurementFactory)Factory;
        }

        #region Sealed methods
        public override sealed decimal GetExchangeRate(string name)
        {
            Enum? measureUnit = GetMeasureUnit(NullChecked(name, nameof(name)));

            if (measureUnit != null) return GetExchangeRate(measureUnit);

            throw NameArgumentOutOfRangeException(name);
        }

        public override sealed Enum GetMeasureUnit()
        {
            return (Enum)MeasureUnit;
        }

        public override sealed string GetName()
        {
            return GetCustomName() ?? GetDefaultName();
        }

        public override sealed void RestoreConstantExchangeRates()
        {
            RestoreCustomNameCollection();

            ExchangeRateCollection = new Dictionary<object, decimal>(ConstantExchangeRateCollection);
        }
        #endregion
        #endregion

        protected static bool IsValidCustomNameParam(string? customName)
        {
            return !string.IsNullOrWhiteSpace(customName)
                && customName != string.Empty
                && doesNotContainCustomName(CustomNameCollection.Values)
                && doesNotContainCustomName(GetDefaultNames());

            #region Local methods
            bool doesNotContainCustomName(IEnumerable<string> names)
            {
                return !names.Any(x => x.ToLower() == customName.ToLower());
            }
            #endregion
        }

        private static IDictionary<string, object> GetMeasureUnitCollection(IEnumerable<object> validMeasureUnits, IDictionary<object, string> customNameCollection)
        {
            IDictionary<string, object> measureUnitCollection = validMeasureUnits.ToDictionary
                (
                    x => MeasureUnitTypes.GetDefaultName((Enum)x),
                    x => x
                );

            foreach (KeyValuePair<object, string> item in customNameCollection)
            {
                measureUnitCollection.Add(item.Value, item.Key);
            }

            return measureUnitCollection;
        }
    }

    internal sealed class ConstantMeasurement : Measurement, IConstantMeasurement
    {
        internal ConstantMeasurement(IMeasurementFactory factory, Enum measureUnit) : base(factory, measureUnit)
        {
        }

        public override IEnumerable<MeasureUnitTypeCode> GetMeasureUnitTypeCodes()
        {
            return base.GetMeasureUnitTypeCodes().Where(x => !x.IsCustomMeasureUnitTypeCode());
        }
    }

    internal sealed class CustomMeasurement : Measurement, ICustomMeasurement
    {
        internal CustomMeasurement(IMeasurementFactory factory, Enum measureUnit) : base(factory, measureUnit)
        {
        }

        #region Public methods
        public IEnumerable<Enum> GetNotUsedCustomMeasureUnits(MeasureUnitTypeCode measureUnitTypeCode)
        {
            ValidateCustomMeasureUnitTypeCode(measureUnitTypeCode);

            Type measureUnitType = MeasureUnitTypes.GetMeasureUnitType(measureUnitTypeCode);
            Array customMeasureUnits = Enum.GetValues(measureUnitType);

            foreach (Enum item in customMeasureUnits)
            {
                if (!ExchangeRateCollection.ContainsKey(item))
                {
                    yield return item;
                }
            }
        }

        public IEnumerable<Enum> GetNotUsedCustomMeasureUnits()
        {
            IEnumerable<MeasureUnitTypeCode> measureUnitTypeCodes = GetMeasureUnitTypeCodes();
            IEnumerable<Enum> notUsedCustomMeasureUnits = GetNotUsedCustomMeasureUnits(measureUnitTypeCodes.First());

            for (int i = 1; i < measureUnitTypeCodes.Count(); i++)
            {
                IEnumerable<Enum> next = GetNotUsedCustomMeasureUnits(measureUnitTypeCodes.ElementAt(i));
                notUsedCustomMeasureUnits = notUsedCustomMeasureUnits.Union(next);
            }

            return notUsedCustomMeasureUnits;
        }

        public void InitializeCustomExchangeRates(MeasureUnitTypeCode measureUnitTypeCode, IDictionary<string, decimal> customExchangeRateCollection)
        {
            ValidateCustomMeasureUnitTypeCode(measureUnitTypeCode);

            int count = NullChecked(customExchangeRateCollection, nameof(customExchangeRateCollection)).Count;

            if (count == 0) throw new ArgumentOutOfRangeException(nameof(customExchangeRateCollection), count, null);

            for (int i = 0; i < count; i++)
            {
                string? customName = customExchangeRateCollection.Keys.ElementAt(i);
                decimal exchangeRate = customExchangeRateCollection!.Values.ElementAt(i);
                Enum measureUnit = GetNotUsedCustomMeasureUnits(measureUnitTypeCode).ElementAt(i);

                if (customName == null
                    || customName == CustomNameCollection.FirstOrDefault(x => x.Key == measureUnit).Value
                    || IsValidCustomNameParam(customName)
                    && CustomNameCollection.TryAdd(measureUnit, customName))
                {
                    SetExchangeRate(measureUnit, exchangeRate);
                }
            }
        }

        public void SetCustomMeasureUnit(Enum measureUnit, decimal exchangeRate, string customName)
        {
            if (TrySetCustomMeasureUnit(measureUnit, exchangeRate, customName)) return;

            throw InvalidMeasureUnitEnumArgumentException(measureUnit);
        }

        public void SetCustomMeasureUnit(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate)
        {
            ValidateCustomMeasureUnitTypeCode(measureUnitTypeCode);

            Enum measureUnit = GetNotUsedCustomMeasureUnits(measureUnitTypeCode).OrderBy(x => x).First();

            SetCustomMeasureUnit(measureUnit, exchangeRate, customName);
        }

        public void SetOrReplaceCustomMeasureUnit(Enum measureUnit, decimal exchangeRate, string customName)
        {
            throw new NotImplementedException();
        }

        public bool TryGetCustomMeasurement(Enum measureUnit, decimal exchangeRate, string customName, [NotNullWhen(true)] out ICustomMeasurement? customMeasurement)
        {
            throw new NotImplementedException();
        }

        public bool TrySetCustomMeasurement(Enum measureUnit, decimal exchangeRate, string customName)
        {
            throw new NotImplementedException();
        }

        public bool TrySetCustomMeasureUnit(Enum measureUnit, decimal exchangeRate, string customName)
        {
            throw new NotImplementedException();
        }

        public bool TrySetCustomMeasureUnit(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate)
        {
            throw new NotImplementedException();
        }

        public void ValidateCustomMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode)
        {
            throw new NotImplementedException();
        }

        #region Override methods
        public override IEnumerable<MeasureUnitTypeCode> GetMeasureUnitTypeCodes()
        {
            return base.GetMeasureUnitTypeCodes().Where(x => x.IsCustomMeasureUnitTypeCode());
        }
        #endregion
        #endregion

        #region Private methods
        #region Static methods
        private static void SetExchangeRate(Enum measureUnit, decimal exchangeRate)
        {
            if (TrySetExchangeRate(measureUnit, exchangeRate)) return;

            throw InvalidMeasureUnitEnumArgumentException(measureUnit);
        }

        private static bool TrySetExchangeRate(Enum measureUnit, decimal exchangeRate)
        {
            exchangeRate.ValidateExchangeRate();

            if (!IsDefinedMeasureUnit(measureUnit)) return false;

            if (ExchangeRateCollection.ContainsKey(measureUnit) && exchangeRate == ExchangeRateCollection[measureUnit]) return true;

            return  ExchangeRateCollection.TryAdd(measureUnit, exchangeRate);
        }
        #endregion
        #endregion
    }

}



//internal abstract class BaseMeasurement : Measurable, IMeasurementBase
//{
//    #region Constructors
//    #region Static constructor
//    static BaseMeasurement()
//    {
//        ConstantExchangeRateCollection = InitConstantExchangeRateCollection();
//        ExchangeRateCollection = new Dictionary<object, decimal>(ConstantExchangeRateCollection);
//        CustomNameCollection = new Dictionary<object, string>();
//    }
//    #endregion

//    private protected BaseMeasurement(IBaseMeasurementFactory factory, Enum measureUnit) : base(factory, measureUnit)
//    {
//        ValidateMeasureUnit(measureUnit, nameof(measureUnit));
//    }
//    #endregion

//    #region Properties
//    #region Static properties
//    public static IDictionary<object, decimal> ConstantExchangeRateCollection { get; }
//    public static IDictionary<object, string> CustomNameCollection { get; protected set; }
//    public static IDictionary<object, decimal> ExchangeRateCollection { get; protected set; }
//    #endregion
//    #endregion

//    #region Public methods
//    public IDictionary<object, decimal> GetConstantExchangeRateCollection()
//    {
//        return GetConstantExchangeRateCollection(MeasureUnitTypeCode);
//    }

//    public IDictionary<object, decimal> GetConstantExchangeRateCollection(MeasureUnitTypeCode measureUnitTypeCode)
//    {
//        return GetMeasureUnitBasedCollection(ConstantExchangeRateCollection, measureUnitTypeCode);
//    }

//    public string? GetCustomName(Enum measureUnit)
//    {
//        return CustomNameCollection.FirstOrDefault(x => x.Key == measureUnit).Value;
//    }

//    public string? GetCustomName()
//    {
//        return GetCustomName(GetMeasureUnit());
//    }

//    public IDictionary<object, string> GetCustomNameCollection(MeasureUnitTypeCode measureUnitTypeCode)
//    {
//        return GetMeasureUnitBasedCollection(CustomNameCollection, measureUnitTypeCode);
//    }

//    public IDictionary<object, string> GetCustomNameCollection()
//    {
//        return CustomNameCollection;
//    }

//    public string GetDefaultName()
//    {
//        return GetDefaultName(GetMeasureUnit());
//    }

//    public string GetDefaultName(Enum measureUnit)
//    {
//        return MeasureUnitTypes.GetDefaultName(measureUnit);
//    }

//    public decimal GetExchangeRate(Enum measureUnit)
//    {
//        return MeasureUnits.GetExchangeRate(measureUnit);
//    }

//    public decimal GetExchangeRate(string name)
//    {
//        Enum? measureUnit = GetMeasureUnit(NullChecked(name, nameof(name)));

//        if (measureUnit != null) return GetExchangeRate(measureUnit);

//        throw NameArgumentOutOfRangeException(name);
//    }

//    public IDictionary<object, decimal> GetExchangeRateCollection()
//    {
//        return GetExchangeRateCollection(MeasureUnitTypeCode);
//    }

//    public IDictionary<object, decimal> GetExchangeRateCollection(MeasureUnitTypeCode measureUnitTypeCode)
//    {
//        return GetMeasureUnitBasedCollection(ExchangeRateCollection, measureUnitTypeCode);
//    }

//    public Enum? GetMeasureUnit(string name)
//    {
//        return (Enum)GetMeasureUnitCollection().FirstOrDefault(x => x.Key == NullChecked(name, nameof(name))).Value;
//    }

//    public IDictionary<string, object> GetMeasureUnitCollection(MeasureUnitTypeCode measureUnitTypeCode)
//    {
//        IEnumerable<object> validMeasureUnits = GetValidMeasureUnits(measureUnitTypeCode);
//        IDictionary<object, string> customNameCollection = GetMeasureUnitBasedCollection(CustomNameCollection, measureUnitTypeCode);

//        return GetMeasureUnitCollection(validMeasureUnits, customNameCollection);
//    }

//    public IDictionary<string, object> GetMeasureUnitCollection()
//    {
//        IEnumerable<object> validMeasureUnits = Statics.MeasureUnits.GetValidMeasureUnits();
//        IDictionary<object, string> customNameCollection = CustomNameCollection;

//        return GetMeasureUnitCollection(validMeasureUnits, customNameCollection);
//    }

//    public string GetName()
//    {
//        return GetCustomName() ?? GetDefaultName();
//    }

//    public IEnumerable<object> GetValidMeasureUnits(MeasureUnitTypeCode measureUnitTypeCode)
//    {
//        return MeasureUnits.GetValidMeasureUnits().Where(x => x.GetType().Equals(measureUnitTypeCode.GetMeasureUnitType()));
//    }

//    public bool IsValidExchangeRate(decimal exchangeRate, Enum measureUnit)
//    {
//        return exchangeRate == GetExchangeRate(measureUnit);
//    }

//    public void RestoreConstantExchangeRates()
//    {
//        CustomNameCollection.Clear();
//        ExchangeRateCollection = new Dictionary<object, decimal>(ConstantExchangeRateCollection);
//    }

//    public void SetCustomName(Enum measureUnit, string customName)
//    {
//        if (TrySetCustomName(measureUnit, customName)) return;

//        throw NameArgumentOutOfRangeException(customName);
//    }

//    public void SetOrReplaceCustomName(string customName)
//    {
//        ValidateCustomName(customName);

//        Enum measureUnit = GetMeasureUnit();

//        if (CustomNameCollection.TryAdd(measureUnit, customName)) return;

//        if (CustomNameCollection.Remove(measureUnit) && CustomNameCollection.TryAdd(measureUnit, customName)) return;

//        throw new InvalidOperationException(null);
//    }

//    public bool TryGetMeasureUnit(MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, [NotNullWhen(true)] out Enum? measureUnit)
//    {
//        measureUnit = (Enum)GetExchangeRateCollection(measureUnitTypeCode).FirstOrDefault(x => x.Value == exchangeRate).Key;

//        return measureUnit != null;
//    }

//    public bool TryGetMeasureUnit(string name, [NotNullWhen(true)] out Enum? measureUnit)
//    {
//        measureUnit = GetMeasureUnit(name);

//        return measureUnit != null;
//    }

//    public bool TrySetCustomName(Enum measureUnit, string customName)
//    {
//        if (measureUnit == null) return false;

//        if (customName == null) return false;

//        if (!MeasureUnits.IsValidMeasureUnit(measureUnit)) return false;

//        if (CustomNameCollection.FirstOrDefault(x => x.Key == measureUnit).Value == customName) return true;

//        if (!IsValidCustomNameParam(customName)) return false;

//        return CustomNameCollection.TryAdd(measureUnit, customName);
//    }

//    public void ValidateCustomName(string? customName)
//    {
//        if (IsValidCustomNameParam(NullChecked(customName, nameof(customName)))) return;

//        throw NameArgumentOutOfRangeException(customName!);
//    }

//    public void ValidateExchangeRate(decimal exchangeRate, string paramName, Enum measureUnit)
//    {
//        if (IsValidExchangeRate(exchangeRate, measureUnit)) return;

//        throw DecimalArgumentOutOfRangeException(paramName, exchangeRate);
//    }

//    public void ValidateExchangeRate(decimal exchangeRate, string paramName)
//    {
//        ValidateExchangeRate(exchangeRate, paramName, GetMeasureUnit());
//    }

//    #region Override methods
//    public override IBaseMeasurementFactory GetFactory()
//    {
//        return (IBaseMeasurementFactory)Factory;
//    }

//    #region Sealed methods
//    public override sealed void ValidateMeasureUnit(Enum measureUnit, string paramName)
//    {
//        if (!ExchangeRateCollection.ContainsKey(measureUnit))
//        {
//            throw InvalidMeasureUnitEnumArgumentException(measureUnit, paramName);
//        }
//    }
//    #endregion
//    #endregion

//    #region Abstract methods
//    public abstract decimal GetExchangeRate();
//    public abstract Enum GetMeasureUnit();
//    #endregion
//    #endregion

//    #region Private methods
//    #region Static methods
//    private static IDictionary<object, T> GetMeasureUnitBasedCollection<T>(IDictionary<object, T> measureUnitBasedCollection, MeasureUnitTypeCode measureUnitTypeCode) where T : notnull
//    {
//        _ = Defined(measureUnitTypeCode, nameof(measureUnitTypeCode));

//        return measureUnitBasedCollection
//            .Where(x => x.Key.GetType().Name.Equals(Enum.GetName(measureUnitTypeCode)))
//            .OrderBy(x => x.Key)
//            .ToDictionary(x => x.Key, x => x.Value);
//    }

//    private static IDictionary<string, object> GetMeasureUnitCollection(IEnumerable<object> validMeasureUnits, IDictionary<object, string> customNameCollection)
//    {
//        IDictionary<string, object> measureUnitCollection = validMeasureUnits.ToDictionary
//            (
//                x => MeasureUnitTypes.GetDefaultName((Enum)x),
//                x => x
//            );

//        foreach (KeyValuePair<object, string> item in customNameCollection)
//        {
//            measureUnitCollection.Add(item.Value, item.Key);
//        }

//        return measureUnitCollection;
//    }

//    private static IDictionary<object, decimal> InitConstantExchangeRateCollection()
//    {
//        return initConstantExchangeRates<AreaUnit>(100, 10000, 1000000)
//            .Union(initConstantExchangeRates<Currency>())
//            .Union(initConstantExchangeRates<DistanceUnit>(1000))
//            .Union(initConstantExchangeRates<ExtentUnit>(10, 100, 1000))
//            .Union(initConstantExchangeRates<Pieces>())
//            .Union(initConstantExchangeRates<TimePeriodUnit>(60, 1440, 10080, 14400))
//            .Union(initConstantExchangeRates<VolumeUnit>(1000, 1000000, 1000000000))
//            .Union(initConstantExchangeRates<WeightUnit>(1000, 1000000))
//            .ToDictionary(x => x.Key, x => x.Value);

//        #region Local methods
//        static IEnumerable<KeyValuePair<object, decimal>> initConstantExchangeRates<T>(params decimal[] exchangeRates) where T : struct, Enum
//        {
//            yield return getMeasureUnitExchangeRatePair(default(T), decimal.One);

//            int exchangeRateCount = exchangeRates?.Length ?? 0;

//            if (exchangeRateCount > 0)
//            {
//                T[] measureUnits = Enum.GetValues<T>();
//                int measureUnitCount = measureUnits.Length;

//                if (measureUnitCount == 0 || measureUnitCount != exchangeRateCount + 1) throw new InvalidOperationException(null);

//                int i = 0;

//                foreach (decimal item in exchangeRates!)
//                {
//                    yield return getMeasureUnitExchangeRatePair(measureUnits[++i], item);
//                }
//            }
//        }

//        static KeyValuePair<object, decimal> getMeasureUnitExchangeRatePair(Enum measureUnit, decimal exchangeRate)
//        {
//            return new KeyValuePair<object, decimal>(measureUnit, exchangeRate);
//        }
//        #endregion
//    }

//    private static bool IsValidCustomNameParam(string? customName)
//    {
//        return !string.IsNullOrWhiteSpace(customName)
//            && customName != string.Empty
//            && doesNotContainCustomName(CustomNameCollection.Values)
//            && doesNotContainCustomName(MeasureUnitTypes.GetDefaultNames());

//        #region Local methods
//        bool doesNotContainCustomName(IEnumerable<string> names)
//        {
//            return !names.Any(x => x.ToLower() == customName.ToLower());
//        }
//        #endregion
//    }

//    public bool TryGetMeasureUnit(decimal exchangeRate, [NotNullWhen(true)] out Enum? measureUnit)
//    {
//        throw new NotImplementedException();
//    }

//    public bool IsExchangeableTo(Enum? context)
//    {
//        if (context is MeasureUnitTypeCode measureUnitTypeCode) return HasMeasureUnitTypeCode(measureUnitTypeCode);

//        return MeasureUnits.IsValidMeasureUnit(context) && MeasureUnitTypes.HasMeasureUnitTypeCode(MeasureUnitTypeCode, context!);
//    }
//    #endregion
//    #endregion
//}
