﻿using CsabaDu.FooVaria.BaseTypes.Measurables.Enums;
using System;

namespace CsabaDu.FooVaria.Tests.TestHelpers.Params;

public sealed class RandomParams
{
    #region Private fields
    private static readonly Random Random = Random.Shared;
    //private static readonly IEnumerable<MeasureUnitCode> CustomMeasureUnitCodes = MeasureUnitCodes.Where(x => x.IsCustomMeasureUnitCode());
    //private static readonly IEnumerable<MeasureUnitCode> ConstantMeasureUnitCodes = MeasureUnitCodes.Where(x => !x.IsCustomMeasureUnitCode());
    //private static readonly IEnumerable<MeasureUnitCode> SpreadMeasureUnitCodes = MeasureUnitCodes.Where(x => x.IsSpreadMeasureUnitCode());
    #endregion

    #region Public methods
    public Enum GetRandomConstantMeasureUnit(Enum excluded = null)
    {
        MeasureUnitCode constantMeasureUnitCode = GetRandomConstantMeasureUnitCode();

        Enum measureUnit = getRandomConstantMeasureUnit();

        while (measureUnit.Equals(excluded))
        {
            measureUnit = getRandomConstantMeasureUnit();
        }

        return measureUnit;

        #region Local methods
        Enum getRandomConstantMeasureUnit()
        {
            return GetRandomMeasureUnit(constantMeasureUnitCode);
        }
        #endregion
    }

    public MeasureUnitCode GetRandomConstantMeasureUnitCode(MeasureUnitCode? excluded = null)
    {
        return GetRandomItem(ConstantMeasureUnitCodes, excluded);
    }

    public MeasureUnitCode GetRandomCustomMeasureUnitCode(MeasureUnitCode? excluded = null)
    {
        return GetRandomItem(CustomMeasureUnitCodes, excluded);

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
        return GetRandomItem(MeasureUnitCodes, excluded);
    }

    public Enum GetRandomMeasureUnit(MeasureUnitCode? measureUnitCode = null, Enum excluded = null)
    {
        measureUnitCode ??= GetRandomMeasureUnitCode();
        Enum randomMeasureUnit = getRandomMeasureUnit();

        while (randomMeasureUnit.Equals(excluded))
        {
            randomMeasureUnit = getRandomMeasureUnit();
        }

        return randomMeasureUnit;

        #region Local methods
        Enum getRandomMeasureUnit()
        {
            return GetRandomItem(measureUnitCode.Value.GetAllMeasureUnits());
        }
        #endregion
    }


    public MeasureUnitCode GetRandomMeasureUnitCode(IEnumerable<MeasureUnitCode> excludeds)
    {
        MeasureUnitCode randomMeasureUnitCode = getRandomMeasureUnitCode();

        while (excludeds.Contains(randomMeasureUnitCode))
        {
            randomMeasureUnitCode = getRandomMeasureUnitCode();
        }

        return randomMeasureUnitCode;

        #region Local methods
        MeasureUnitCode getRandomMeasureUnitCode()
        {
            return GetRandomMeasureUnitCode(excludeds.First());
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
            decimal exchangeRate = GetRandomNotNegativeDecimal();

            SetCustomMeasureUnit(customame, measureUnitCode, exchangeRate);
        }

        return isCustomMeasureUnit ?
            (Enum)Enum.ToObject(measureUnit.GetType(), 1)
            : GetRandomMeasureUnit(measureUnitCode);
    }

    public decimal GetRandomDecimal(decimal? excluded = null)
    {
        decimal quantity = getRandomDecimal();

        while (quantity == excluded)
        {
            quantity = getRandomDecimal();
        }

        return quantity;

        decimal getRandomDecimal()
        {
            return (decimal)GetRandomValueType(TypeCode.Decimal);
        }
    }

    public decimal GetRandomPositiveDecimal(decimal? excluded = null)
    {
        decimal positiveDecimal = getRandomPositiveDecimal();

        while (positiveDecimal == 0 || positiveDecimal == excluded)
        {
            positiveDecimal = getRandomPositiveDecimal();
        }

        return positiveDecimal;

        #region Local methods
        decimal getRandomPositiveDecimal()
        {
            return Convert.ToDecimal(Random.NextInt64(uint.MaxValue)) + Convert.ToDecimal(Random.NextDouble());
        }
        #endregion
    }

    public decimal GetRandomNotNegativeDecimal(decimal? excluded = null)
    {
        decimal notNegativeDecimal = getRandomNotNegativeDecimal();

        while (notNegativeDecimal == excluded)
        {
            notNegativeDecimal = getRandomNotNegativeDecimal();
        }

        return notNegativeDecimal;

        #region Local methods
        decimal getRandomNotNegativeDecimal()
        {
            decimal randomDecimal = GetRandomDecimal();

            return randomDecimal < 0 ? 0 : randomDecimal;
        }
        #endregion
    }

    public decimal GetRandomNegativeDecimal(decimal? excluded = null)
    {
        decimal negativeDecimal = getRandomNegativeDecimal();

        while (negativeDecimal == excluded)
        {
            negativeDecimal = getRandomNegativeDecimal();
        }

        return negativeDecimal;

        #region Local methods
        decimal getRandomNegativeDecimal()
        {
            return Convert.ToDecimal(Random.NextInt64(int.MinValue, 1)) - Convert.ToDecimal(Random.NextDouble());
        }
        #endregion
    }

    public double GetRandomDouble()
    {
        return (double)GetRandomValueType(TypeCode.Double);
    }

    public double GetRandomPositiveDouble(double? excluded = null)
    {
        double positiveDouble = getRandomPositiveDouble();

        while (positiveDouble == 0 || positiveDouble == excluded)
        {
            positiveDouble = getRandomPositiveDouble();
        }

        return positiveDouble;

        #region Local methods
        double getRandomPositiveDouble()
        {
            return Convert.ToDouble(Random.NextInt64(uint.MaxValue)) + Random.NextDouble();
        }
        #endregion
    }

    public double GetRandomNegativeDouble(double? excluded = null)
    {
        double negativeDouble = getRandomNegativeDouble();

        while (negativeDouble == excluded)
        {
            negativeDouble = getRandomNegativeDouble();
        }

        return negativeDouble;

        #region Local methods
        double getRandomNegativeDouble()
        {
            return Convert.ToDouble(Random.Next(int.MinValue, 1)) - Random.NextDouble();
        }
        #endregion
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
        return GetRandomItem(Enum.GetValues<RateComponentCode>(), excluded);
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

    public MeasureUnitCode GetRandomSpreadMeasureUnitCode()
    {
        return GetRandomItem(SpreadMeasureUnitCodes);
    }

    public Enum GetRandomSpreadMeasureUnit(MeasureUnitCode? excluded = null)
    {
        MeasureUnitCode measureUnitCode = excluded.HasValue ?
            GetOtherSpreadMeasureUnitCode(excluded.Value)
            : GetRandomSpreadMeasureUnitCode();

        return GetRandomMeasureUnit(measureUnitCode);
    }

    public MeasureUnitCode GetRandomDifferentMeasureUnitCode(MeasureUnitCode[] measureUnitCodes)
    {
        MeasureUnitCode measureUnitCode = GetRandomMeasureUnitCode();

        if (measureUnitCodes.Length >= MeasureUnitCodeCount) throw new InvalidOperationException(null);

        while (measureUnitCodes.Contains(measureUnitCode))
        {
            measureUnitCode = GetRandomMeasureUnitCode();
        }

        return measureUnitCode;
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
            TypeCode.Boolean => Convert.ToBoolean(Random.Next(2)),
            TypeCode.Char => Convert.ToChar(Random.Next(char.MaxValue)),
            TypeCode.SByte => Convert.ToSByte(Random.Next(sbyte.MinValue, sbyte.MaxValue)),
            TypeCode.Byte => Convert.ToByte(Random.Next(byte.MaxValue)),
            TypeCode.Int16 => Convert.ToInt16(Random.Next(short.MinValue, short.MaxValue)),
            TypeCode.UInt16 => Convert.ToUInt16(Random.Next(ushort.MaxValue)),
            TypeCode.Int32 => Random.Next(int.MinValue, int.MaxValue),
            TypeCode.UInt32 => Convert.ToUInt32(Random.Next()) + Random.Next(),
            TypeCode.Int64 => randomNextInt64(),
            TypeCode.UInt64 => Convert.ToUInt64(Random.NextInt64() + Random.Next()),
            TypeCode.Single => Convert.ToSingle(randomNextInt64()) + Random.NextSingle(),
            TypeCode.Double => Convert.ToDouble(randomNextInt64()) + Random.NextDouble(),
            TypeCode.Decimal => Convert.ToDecimal(randomNextInt64()) + Convert.ToDecimal(Random.NextDouble()),
            TypeCode.DateTime => DateTime.Now,

            _ => throw new ArgumentOutOfRangeException(nameof(typeCode), typeCode, null),
        };

        static long randomNextInt64() => Random.NextInt64(int.MinValue, uint.MaxValue);
    }

    public ValueType GetRandomValidValueType()
    {
        TypeCode quantityTypeCode = GetRandomQuantityTypeCode();

        return GetRandomValueType(quantityTypeCode);
    }

    public TypeCode GetRandomQuantityTypeCode(TypeCode? excluded = null)
    {
        return GetRandomItem(QuantityTypeCodes, excluded);
    }

    public TypeCode GetRandomQuantityTypeCode(RateComponentCode rateComponentCode)
    {
        return rateComponentCode == RateComponentCode.Limit ?
            TypeCode.UInt64
            : GetRandomQuantityTypeCode(TypeCode.UInt64);
    }

    public TypeCode GetRandomInvalidTypeCode()
    {
        TypeCode typeCode = GetRandomTypeCode();

        while (QuantityTypeCodes.Contains(typeCode))
        {
            typeCode = GetRandomTypeCode();
        }

        return typeCode;
    }

    public TypeCode GetRandomInvalidQuantityTypeCode()
    {
        return GetRandomItem(SampleParams.InvalidValueTypeCodes);
    }


    private static T GetRandomItem<T>(T[] items, T? excluded = null)
        where T : struct
    {
        T randomItem = getRandomItem();

        if (!excluded.HasValue) return randomItem;

        while (randomItem.Equals(excluded.Value))
        {
            randomItem = getRandomItem();
        }

        return randomItem;

        #region Local methods
        T getRandomItem()
        {
            return GetRandomItem(items);
        }
        #endregion
    }

    private static T GetRandomItem<T>(IEnumerable<T> items, T? excluded = null)
        where T : struct
    {
        return GetRandomItem(items.ToArray(), excluded);
    }

    #endregion
    #endregion
}
