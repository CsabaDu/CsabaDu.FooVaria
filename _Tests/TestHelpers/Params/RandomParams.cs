using CsabaDu.FooVaria.BaseTypes.BaseQuantifiables.Statics;
using System;

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

        if (excluded is null) return measureUnit;

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

    public MeasureUnitCode GetRandomCustomMeasureUnitCode()
    {
        return GetRandomItem(CustomMeasureUnitCodes);

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

    public Enum GetRandomMeasureUnit(MeasureUnitCode? measureUnutCode = null, Enum excluded = null)
    {
        measureUnutCode ??= GetRandomMeasureUnitCode();
        Enum randomMeasureUnit = getRandomMeasureUnit();

        while (randomMeasureUnit.Equals(excluded))
        {
            randomMeasureUnit = getRandomMeasureUnit();
        }

        return randomMeasureUnit;

        #region Local methods
        Enum getRandomMeasureUnit()
        {
            return GetRandomItem(measureUnutCode.Value.GetAllMeasureUnits());
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

        return GetRandomNotUsedCustomMeasureUnit(customMeasureUnitCode);
    }

    public Enum GetRandomNotUsedCustomMeasureUnit(MeasureUnitCode customMeasureUnitCode)
    {
        if (!CustomMeasureUnitCodes.Contains(customMeasureUnitCode)) throw new InvalidOperationException("Not custom MeaureUnitCode: " + Enum.GetName(customMeasureUnitCode));

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

    public double GetRandomDouble()
    {
        return (double)GetRandomValueType(TypeCode.Double);
    }

    public LimitMode GetRandomLimitMode()
    {
        return GetRandomItem(Enum.GetValues<LimitMode>());
    }

    public RoundingMode GetRandomRoundingMode()
    {
        return GetRandomItem(Enum.GetValues<RoundingMode>());
    }

    public RateComponentCode GetRandomRateComponentCode(RateComponentCode? excluded = null)
    {
        RateComponentCode rateComponentCode = getRandomRateComponentCode();

        if (!excluded.HasValue) return rateComponentCode;

        while (rateComponentCode == excluded.Value)
        {
            rateComponentCode = getRandomRateComponentCode();
        }

        return rateComponentCode;

        #region
        RateComponentCode getRandomRateComponentCode()
        {
            return GetRandomItem(Enum.GetValues<RateComponentCode>());
        }
        #endregion
    }

    public TypeCode GetRandomTypeCode()
    {
        return GetRandomItem(Enum.GetValues<TypeCode>());
    }

    public TypeCode GetRandomQuantityTypeCode()
    {
        return GetRandomItem(QuantityTypeCodes);
    }

    public object GetRandomQuantity(TypeCode? quantityTypeCode = null, ValueType excluded = null)
    {
        quantityTypeCode ??= GetRandomQuantityTypeCode();
        ValueType quantity = getRandomValueType();

        if (excluded is null) return getQuantity();

        object excludedDecimal = convertToDecimal(excluded);

        while (excludedDecimal.Equals(convertToDecimal(quantity)))
        {
            quantity = getRandomValueType();
        }

        return getQuantity();

        #region Local methods
        object convertToDecimal(ValueType quantity)
        {
            return quantity.ToQuantity(TypeCode.Decimal);
        }

        ValueType getRandomValueType()
        {
            return GetRandomValueType(quantityTypeCode.Value);
        }

        object getQuantity()
        {
            return quantity.ToQuantity(quantityTypeCode.Value);
        }
        #endregion
    }

    public LimitMode? GetRandomNullableLimitMode(LimitMode? excluded)
    {
        LimitMode? limitMode = GetRandomNullableLimitMode();

        while (limitMode == excluded)
        {
            limitMode = GetRandomNullableLimitMode();
        }

        return limitMode;
    }

    public LimitMode? GetRandomNullableLimitMode()
    {
        return GetRandomItemOrNull(Enum.GetValues<LimitMode>());
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

    private static T? GetRandomItemOrNull<T>(T[] items)
        where T : struct
    {
        T randomItem = GetRandomItem(items);

        return GetRandomItem([default(T?), randomItem]);
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
            TypeCode.UInt64 => Convert.ToUInt64(Random.NextInt64() + Random.Next()),
            TypeCode.Single => Convert.ToSingle(randomNextInt64()) + Random.NextSingle(),
            TypeCode.Double => Convert.ToDouble(randomNextInt64()) + Random.NextDouble(),
            TypeCode.Decimal => Convert.ToDecimal(randomNextInt64()) + Convert.ToDecimal(Random.NextDouble()),
            TypeCode.DateTime => DateTime.Now,

            _ => throw new ArgumentOutOfRangeException(nameof(typeCode), typeCode, null),
        };

        static long randomNextInt64() => Random.NextInt64(long.MinValue, long.MaxValue);
    }

    public ValueType GetRandomValidValueType()
    {
        TypeCode quantityTypeCode = GetRandomQuantityTypeCode();

        return GetRandomValueType(quantityTypeCode);
    }

    public TypeCode GetRandomQuantityTypeCode(TypeCode? excluded = null)
    {
        TypeCode quantityTypeCode = GetRandomItem(QuantityTypeCodes);

        while (quantityTypeCode == excluded)
        {
            quantityTypeCode = GetRandomItem(QuantityTypeCodes);
        }

        return quantityTypeCode;
    }

    public TypeCode GetRandomQuantityTypeCode(RateComponentCode rateComponentCode)
    {
        return rateComponentCode == RateComponentCode.Limit ?
            TypeCode.UInt64
            : GetRandomQuantityTypeCode(TypeCode.UInt64);
    }

    public TypeCode GetRandomInvalidQuantityTypeCode()
    {
        TypeCode typeCode = GetRandomTypeCode();

        while (QuantityTypeCodes.Contains(typeCode))
        {
            typeCode = GetRandomTypeCode();
        }

        return typeCode;
    }
    #endregion
    #endregion
}
