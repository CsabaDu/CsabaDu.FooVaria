using CsabaDu.FooVaria.BaseTypes.BaseQuantifiables.Statics;

namespace CsabaDu.FooVaria.Tests.TestHelpers.Params;

public sealed class RandomParams
{
    #region Private fields
    private static readonly Random Random = Random.Shared;
    private static readonly IEnumerable<MeasureUnitCode> CustomMeasureUnitCodes = MeasureUnitCodes.Where(x => x.IsCustomMeasureUnitCode());
    private static readonly IEnumerable<MeasureUnitCode> ConstantMeasureUnitCodes = MeasureUnitCodes.Where(x => !x.IsCustomMeasureUnitCode());
    #endregion

    #region Public methods
    public Enum GetRandomConstantMeasureUnit(Enum excluded = null)
    {
        Enum measureUnit = (Enum)GetRandomItem(ConstantExchangeRateCollection.Keys);

        if (excluded == null) return measureUnit;

        MeasureUnitCode measureUnitCode = GetMeasureUnitCode(excluded);

        while (measureUnit.Equals(excluded) || GetMeasureUnitCode(measureUnit) != measureUnitCode)
        {
            measureUnit = (Enum)GetRandomItem(ConstantExchangeRateCollection.Keys);
        }

        return measureUnit;
    }

    public MeasureUnitCode GetRandomConstantMeasureUnitCode()
    {
        return GetRandomItem(ConstantMeasureUnitCodes);
    }

    public Enum GetRandomMeasureUnitOrMeasureUnitCode()
    {
        IEnumerable<Enum> measureUnitsAndMeasureUnitCodes = GetAllMeasureUnits().Union([.. MeasureUnitCodes]);

        return GetRandomItem(measureUnitsAndMeasureUnitCodes);
    }

    public Enum GetRandomValidMeasureUnitOrMeasureUnitCode()
    {
        Enum context = GetRandomMeasureUnitOrMeasureUnitCode();

        while (!IsValidMeasureUnit(context) && context is not MeasureUnitCode)
        {
            context = GetRandomMeasureUnitOrMeasureUnitCode();
        }

        return context;
    }

    public MeasureUnitCode GetRandomMeasureUnitCode(MeasureUnitCode? excluded = null)
    {
        MeasureUnitCode randomMeasureUnitCode = getRandomMeasureUnitCode();

        while (randomMeasureUnitCode == excluded)
        {
            randomMeasureUnitCode = getRandomMeasureUnitCode();
        }

        return randomMeasureUnitCode;

        #region Local methods
        static MeasureUnitCode getRandomMeasureUnitCode()
        {
            return GetRandomItem(MeasureUnitCodes);
        }
        #endregion
    }

    public Enum GetRandomMeasureUnit(MeasureUnitCode? measureUnitTypeCode = null, Enum excluded = null)
    {
        measureUnitTypeCode ??= GetRandomMeasureUnitCode();
        Enum randomMeasureUnit = getRandomMeasureUnit();

        while (randomMeasureUnit.Equals(excluded))
        {
            randomMeasureUnit = getRandomMeasureUnit();
        }

        return randomMeasureUnit;

        #region Local methods
        Enum getRandomMeasureUnit()
        {
            return GetRandomItem(measureUnitTypeCode.Value.GetAllMeasureUnits());
        }
        #endregion
    }

    public Enum GetRandomNotDefinedMeasureUnit(MeasureUnitCode? excluded = null)
    {
        MeasureUnitCode measureUnitCode = GetRandomMeasureUnitCode(excluded);

        return SampleParams.GetNotDefinedMeasureUnit(measureUnitCode);
    }

    public Enum GetRandomValidMeasureUnit(Enum excluded = null)
    {
        object randomMeasureUnit = getRandomValidMeasureUnit();

        while (randomMeasureUnit.Equals(excluded))
        {
            randomMeasureUnit = getRandomValidMeasureUnit();
        }

        return (Enum)randomMeasureUnit;

        #region Local methods
        static object getRandomValidMeasureUnit()
        {
            return GetRandomItem(ExchangeRateCollection.Keys);
        }
        #endregion
    }

    public Enum GetRandomValidMeasureUnit(MeasureUnitCode measureUnitCode)
    {
        return (Enum)GetRandomItem(GetValidMeasureUnits(measureUnitCode));
    }

    public string GetRandomParamName()
    {
        IEnumerable<string> paramNames = ParamNames.GetParamNames();

        return GetRandomItem(paramNames);
    }

    public Enum GetRandomNotUsedCustomMeasureUnit()
    {
        MeasureUnitCode customMeasureUnitCode = GetRandomItem(CustomMeasureUnitCodes);

        Enum measureUnit = GetRandomMeasureUnit(customMeasureUnitCode);

        while (ExchangeRateCollection.ContainsKey(measureUnit))
        {
            measureUnit = GetRandomMeasureUnit(customMeasureUnitCode);
        }

        return measureUnit;
    }

    public MeasureUnitCode[] GetRandomCountRandomMeasureUnitCodes()
    {
        int count = Random.Next(1, MeasureUnitCodes.Length);

        return GetRandomItems(MeasureUnitCodes, count);
    }

    public  Enum GetRandomSameTypeValidMeasureUnit(Enum measureUnit)
    {
        if (!IsValidMeasureUnit(measureUnit)) throw InvalidMeasureUnitEnumArgumentException(measureUnit);

        MeasureUnitCode measureUnitCode = GetMeasureUnitCode(measureUnit);
        bool isCustomMeasureUnit = IsCustomMeasureUnit(measureUnit);

        if (isCustomMeasureUnit)
        {
            string customame = GetRandomParamName();
            decimal exchangeRate = GetRandomPositiveDecimal();

            SetCustomMeasureUnit(customame, measureUnitCode, exchangeRate);
        }

        return isCustomMeasureUnit ?
            (Enum)Enum.ToObject(measureUnit.GetType(), 1)
            : GetRandomMeasureUnit(measureUnitCode);
    }

    public decimal GetRandomDecimal()
    {
        return (decimal)GetRandomValueType(TypeCode.Decimal);
    }

    public decimal GetRandomDecimal(decimal excluded)
    {
        decimal quantity = GetRandomDecimal();

        while (quantity == excluded)
        {
            quantity = GetRandomDecimal();
        }

        return quantity;
    }

    public decimal GetRandomPositiveDecimal()
    {
        return Convert.ToDecimal(Random.NextInt64(long.MaxValue) + Random.NextDouble());
    }

    public decimal GetRandomPositiveDecimal(decimal excluded)
    {
        decimal excchangeRate = GetRandomPositiveDecimal();

        while (excchangeRate == excluded)
        {
            excchangeRate = GetRandomPositiveDecimal();
        }

        return excchangeRate;
    }

    public decimal GetRandomNegativeDecimal()
    {
        return Convert.ToDecimal(Random.NextInt64(long.MinValue, 1) - Random.NextDouble());
    }

    public LimitMode GetRandomLimitMode()
    {
        return GetRandomItem(Enum.GetValues<LimitMode>());
    }
    #endregion

    #region Private methods
    #region Static methods
    private static T GetRandomItem<T>(T[] items)
    {
        return Random.GetItems(items, 1)[0];
    }

    private static T[] GetRandomItems<T>(T[] items, int count)
    {
        return Random.GetItems(items, count);
    }

    private static T GetRandomItem<T>(IEnumerable<T> items)
    {
        return GetRandomItem(items.ToArray());
    }

    private static T[] GetRandomItems<T>(IEnumerable<T> items, int count)
    {
        return GetRandomItems(items.ToArray(), count);
    }

    private static int[] GetRandomIndexArray(int maxValue, int count)
    {
        int[] randomIndexArray = new int[count];

        for (int i = 0; i  <= count; i++)
        {
            int item = Random.Next(maxValue);

            while (randomIndexArray.Contains(item))
            {
                item = Random.Next(1, maxValue);
            }

            randomIndexArray[i] = item;
        }

        return randomIndexArray;
    }

    public ValueType GetRandomValueType(TypeCode typeCode)
    {
        return typeCode switch
        {
            TypeCode.Boolean => Convert.ToBoolean(Random.Next(1)),
            TypeCode.Char => Convert.ToChar(Random.Next(char.MaxValue)),
            TypeCode.SByte => Convert.ToSByte(Random.Next(sbyte.MinValue, sbyte.MaxValue)),
            TypeCode.Byte => Convert.ToByte(Random.Next(byte.MaxValue)),
            TypeCode.Int16 => Convert.ToInt16(Random.Next(short.MinValue, short.MaxValue)),
            TypeCode.UInt16 => Convert.ToUInt16(Random.Next(ushort.MaxValue)),
            TypeCode.Int32 => Random.Next(int.MinValue, int.MaxValue),
            TypeCode.UInt32 => Convert.ToUInt32(Random.Next()) + Random.Next(),
            TypeCode.Int64 => randomNextInt64() + randomNextInt64(),
            TypeCode.UInt64 => Convert.ToUInt64(Random.NextInt64()) + Convert.ToUInt64(Random.NextInt64()),
            TypeCode.Single => Random.NextSingle() + randomNextInt64(),
            TypeCode.Double => Random.NextDouble() + randomNextInt64(),
            TypeCode.Decimal => Convert.ToDecimal(Random.NextDouble() + randomNextInt64()),
            TypeCode.DateTime => DateTime.Now,

            _ => throw new ArgumentOutOfRangeException(nameof(typeCode), typeCode, null),
        };

        long randomNextInt64() => Random.NextInt64(long.MinValue, long.MaxValue);
    }

    public ValueType GetRandomValidValueType()
    {
        TypeCode typeCode = GetRandomItem(GetQuantityTypeCodes());

        return GetRandomValueType(typeCode);
    }

    public TypeCode GetRandomQuantityTypeCode(TypeCode? excluded = null)
    {
        IEnumerable<TypeCode> quantityTypeCodes = GetQuantityTypeCodes();
        TypeCode typeCode = GetRandomItem(quantityTypeCodes);

        while (typeCode == excluded)
        {
            typeCode = GetRandomItem(GetQuantityTypeCodes());
        }

        return typeCode;
    }

    public TypeCode GetRandomInvalidQuantityTypeCode()
    {
        IEnumerable<TypeCode> quantityTypeCodes = GetQuantityTypeCodes();
        IEnumerable<int> quantityTypeCodeValues = quantityTypeCodes.Select(x => (int)(object)x);
        int maxValue = Enum.GetNames(typeof(TypeCode)).Length;
        int typeCodeValue = Random.Next(maxValue);

        while (quantityTypeCodeValues.Contains(typeCodeValue))
        {
            typeCodeValue = Random.Next(maxValue);
        }

        return (TypeCode)typeCodeValue;
    }

    #endregion
    #endregion
}
