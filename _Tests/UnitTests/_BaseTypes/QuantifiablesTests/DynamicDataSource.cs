using CsabaDu.FooVaria.BaseTypes.BaseQuantifiables.Statics;

namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.QuantifiablesTests;

internal class DynamicDataSource : DataFields
{
    #region Methods
    internal IEnumerable<object[]> GetEqualsArgs()
    {
        // null
        IQuantifiable quantifiable = null;
        measureUnit = RandomParams.GetRandomValidMeasureUnit();
        defaultQuantity = RandomParams.GetRandomDecimal();
        isTrue = false;
        yield return toObjectArray();

        // Different MeasureUnitCode
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode(GetMeasureUnitCode(measureUnit));
        quantifiable = new QuantifiableChild(RootObject, null)
        {
            Return = new()
            {
                GetBaseMeasureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode),
                GetDefaultQuantity = defaultQuantity,
            }
        };
        yield return toObjectArray();

        // Same MeasureUnit, different defaultQuantity
        quantifiable = new QuantifiableChild(RootObject, null)
        {
            Return = new()
            {
                GetBaseMeasureUnit = measureUnit,
                GetDefaultQuantity = RandomParams.GetRandomDecimal(defaultQuantity),
            }
        };
        yield return toObjectArray();

        // Same MeasureUnit, same defaultQuantity
        quantifiable = new QuantifiableChild(RootObject, null)
        {
            Return = new()
            {
                GetBaseMeasureUnit = measureUnit,
                GetDefaultQuantity = defaultQuantity,
            }
        };
        isTrue = true;
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            Enum_Decimal_Bool_IQuantifiable_args item = new(measureUnit, defaultQuantity, isTrue, quantifiable);

            return item.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetExchangeToArgs()
    {
        // null
        measureUnit = RandomParams.GetRandomMeasureUnit();
        measureUnitCode = GetMeasureUnitCode(measureUnit);
        context = null;
        IQuantifiable quantifiable = null;
        yield return toObjectArray();

        // not measureUnit Enum
        context = TypeCode.Empty;
        yield return toObjectArray();

        // same type not defined measureUnit
        context = SampleParams.GetNotDefinedMeasureUnit(measureUnitCode);
        yield return toObjectArray();

        // same type defined measureUnit
        context = RandomParams.GetRandomMeasureUnit(measureUnitCode);
        quantifiable = new QuantifiableChild(RootObject, paramName)
        {
            Return = new()
            {
                GetBaseMeasureUnit = context,
                GetDefaultQuantity = RandomParams.GetRandomDecimal(),
            }
        };
        yield return toObjectArray();

        // different type measureUnit
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode(measureUnitCode);
        context = RandomParams.GetRandomMeasureUnit(measureUnitCode);
        quantifiable = null;
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            Enum_Enum_IQuantifiable_args item = new(measureUnit, context, quantifiable);

            return item.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetFitsInILimiterArgs()
    {
        // Not IQuantifiable
        limiter = new LimiterObject();
        yield return toObjectArray();

        // Different MeasureUnitCode
        limiter = new LimiterQuantifiableOblect(RootObject, null)
        {
            Return = new()
            {
                GetDefaultQuantity = RandomParams.GetRandomDecimal(),
            }
        };

        #region toObjectArray method
        object[] toObjectArray()
        {
            ILimiter_arg item = new(limiter);

            return item.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetFitsInIQuantifiableLimitModeArgs()
    {
        // IQuantifiable, Not defined limitMode
        limitMode = SampleParams.NotDefinedLimitMode;
        measureUnit = RandomParams.GetRandomMeasureUnit();
        measureUnitCode = GetMeasureUnitCode(measureUnit);
        IQuantifiable quantifiable = new QuantifiableChild(RootObject, null)
        {
            Return = new()
            {
                GetBaseMeasureUnit = measureUnit,
                GetDefaultQuantity = RandomParams.GetRandomDecimal(),
            }
        };
        yield return toObjectArray();

        // Different IQuantifiable, valid LimitMode
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode(measureUnitCode);
        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode);
        limitMode = RandomParams.GetRandomLimitMode();
        yield return toObjectArray();

        // null, valid LimitMode
        quantifiable = null;
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            Enum_LimitMode_IQuantifiable_arg item = new(measureUnit, limitMode, quantifiable);

            return item.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetQuantifiableInvalidArgs()
    {
        measureUnit = RandomParams.GetRandomMeasureUnit();
        measureUnitCode = SampleParams.NotDefinedMeasureUnitCode;
        yield return toObjectArray();

        measureUnitCode = GetMeasureUnitCode(measureUnit);
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode(measureUnitCode);
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            Enum_MeasureUnitCode_args item = new(measureUnit, measureUnitCode);

            return item.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetGetQuantityRoundingModeArgs()
    {
        // double
        measureUnitCode = RandomParams.GetRandomConstantMeasureUnitCode();
        measureUnit = measureUnitCode.GetDefaultMeasureUnit();
        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode, measureUnit);
        defaultQuantity = RandomParams.GetRandomDecimal();
        RoundingMode[] roundingModes = Enum.GetValues<RoundingMode>();

        foreach (RoundingMode item in roundingModes)
        {
            roundingMode = item;
            obj = Convert.ToDouble(defaultQuantity).Round(item);
            yield return toObjectArray();
        }

        // decimal
        measureUnit = MeasureUnitCode.Currency.GetDefaultMeasureUnit();

        foreach (RoundingMode item in roundingModes)
        {
            roundingMode = item;
            obj = defaultQuantity.Round(item);
            yield return toObjectArray();
        }

        #region toObjectArray method
        object[] toObjectArray()
        {
            Enum_Decimal_Object_RoundingMode_args item = new(measureUnit, defaultQuantity, obj, roundingMode);

            return item.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetGetQuantityInvalidTypeCodeArgs()
    {
        return SampleParams.GetInvalidQuantityTypeCodes()
            .Select(toObjectArray)
            .Append(toObjectArray(SampleParams.NotDefinedTypeCode))
            .Append(toObjectArray(TypeCode.UInt64));

        #region toObjectArray method
        static object[] toObjectArray(TypeCode typeCode)
        {
            TypeCode_arg item = new(typeCode);

            return item.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetGetQuantityValidTypeCodeArgs()
    {
        measureUnitCode = RandomParams.GetRandomConstantMeasureUnitCode();
        measureUnit = measureUnitCode.GetDefaultMeasureUnit();
        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode, measureUnit);
        quantity = RandomParams.GetRandomDouble();
        defaultQuantity = (decimal)quantity.ToQuantity(TypeCode.Decimal);

        foreach (var item in GetQuantityTypeCodes())
        {
            if (item is not (TypeCode.Int64 or TypeCode.UInt64))
            {
                typeCode = item;
                obj = convertDefaultQuantity();
                yield return toObjectArray();
            }
        }

        measureUnit = MeasureUnitCode.Currency.GetDefaultMeasureUnit();
        typeCode = TypeCode.Double;
        obj = convertDefaultQuantity();
        yield return toObjectArray();

        typeCode = TypeCode.Int64;
        obj = convertDefaultQuantity();
        yield return toObjectArray();

        defaultQuantity = RandomParams.GetRandomPositiveDecimal();
        typeCode = TypeCode.UInt64;
        obj = convertDefaultQuantity();
        yield return toObjectArray();

        #region Local methods
        #region toObjectArray method
        object[] toObjectArray()
        {
            Enum_Decimal_Object_TypeCode_args item = new(measureUnit, defaultQuantity, obj, typeCode);

            return item.ToObjectArray();
        }
        #endregion

        object convertDefaultQuantity()
        {
            return defaultQuantity.ToQuantity(typeCode);
        }
        #endregion
    }

    #endregion
}
