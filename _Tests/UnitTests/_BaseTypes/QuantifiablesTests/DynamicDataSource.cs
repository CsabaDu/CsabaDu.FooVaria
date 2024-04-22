namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.QuantifiablesTests;

internal sealed class DynamicDataSource : CommonDynamicDataSource
{
    #region Fields
    IQuantifiable quantifiable;
    #endregion

    #region Methods
    internal IEnumerable<object[]> GetEqualsArgs()
    {
        // null
        quantifiable = null;
        measureUnit = RandomParams.GetRandomValidMeasureUnit();
        defaultQuantity = RandomParams.GetRandomDecimal();
        isTrue = false;
        yield return toObjectArray();

        // Different MeasureUnitCode
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode(GetMeasureUnitCode());
        quantifiable = GetQuantifiableChild(defaultQuantity, RandomParams.GetRandomMeasureUnit(measureUnitCode));
        yield return toObjectArray();

        // Same MeasureUnit, different defaultQuantity
        quantifiable = GetQuantifiableChild(RandomParams.GetRandomDecimal(defaultQuantity), measureUnit);
        yield return toObjectArray();

        // Same MeasureUnit, same defaultQuantity
        quantifiable = GetQuantifiableChild(defaultQuantity, measureUnit);
        isTrue = true;
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            TestCase_Enum_decimal_bool_IQuantifiable item = new(measureUnit, defaultQuantity, isTrue, quantifiable);

            return item.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetFitsInILimiterArgs()
    {
        // Not IBaseQuantifiable
        measureUnit = RandomParams.GetRandomMeasureUnit();
        limiter = new LimiterObject();
        yield return toObjectArray();

        // Different MeasureUnitCode
        measureUnitCode = GetMeasureUnitCode();
        measureUnitCode = RandomParams.GetRandomCustomMeasureUnitCode(measureUnitCode);
        limitMode = RandomParams.GetRandomLimitMode();
        limiter = GetLimiterQuantifiableObject(limitMode.Value, RandomParams.GetRandomMeasureUnit(measureUnitCode), defaultQuantity);
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            TestCase_Enum_ILimiter item = new(measureUnit, limiter);

            return item.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetFitsInIQuantifiableLimitModeArgs()
    {
        // IQuantifiable, Not defined limitMode
        limitMode = SampleParams.NotDefinedLimitMode;
        measureUnit = RandomParams.GetRandomMeasureUnit();
        measureUnitCode = GetMeasureUnitCode();
        quantifiable = GetQuantifiableChild(RandomParams.GetRandomDecimal(), measureUnit);
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

        measureUnitCode = GetMeasureUnitCode();
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode(measureUnitCode);
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            TestCase_Enum_MeasureUnitCode item = new(measureUnit, measureUnitCode);

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
            TestCase_Enum_decimal_object_RoundingMode item = new(measureUnit, defaultQuantity, obj, roundingMode);

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

        foreach (var item in QuantityTypeCodes)
        {
            if (item is not (TypeCode.Int64 or TypeCode.UInt64))
            {
                quantityTypeCode = item;
                obj = convertDefaultQuantity();
                yield return toObjectArray();
            }
        }
        measureUnit = MeasureUnitCode.Currency.GetDefaultMeasureUnit();
        quantityTypeCode = TypeCode.Double;
        obj = convertDefaultQuantity();
        yield return toObjectArray();

        quantityTypeCode = TypeCode.Int64;
        obj = convertDefaultQuantity();
        yield return toObjectArray();

        defaultQuantity = RandomParams.GetRandomNotNegativeDecimal();
        quantityTypeCode = TypeCode.UInt64;
        obj = convertDefaultQuantity();
        yield return toObjectArray();

        #region Local methods
        #region toObjectArray method
        object[] toObjectArray()
        {
            TestCase_Enum_decimal_object_TypeCode item = new(measureUnit, defaultQuantity, obj, quantityTypeCode);

            return item.ToObjectArray();
        }
        #endregion

        object convertDefaultQuantity()
        {
            return defaultQuantity.ToQuantity(quantityTypeCode);
        }
        #endregion
    }

    internal IEnumerable<object[]> GetIsExchangeableToArg()
    {
        // 1. null -  false
        isTrue = false;
        measureUnit = RandomParams.GetRandomValidMeasureUnit();
        context = null;
        yield return toObjectArray();

        // 2. Not measureUnit not MeasureUnitCode Enum - false
        context = TypeCode.Empty;
        yield return toObjectArray();

        // 3. other MeasureUnitCode - false
        measureUnitCode = GetMeasureUnitCode();
        context = RandomParams.GetRandomMeasureUnitCode(measureUnitCode);
        yield return toObjectArray();

        // 4. same type not defined measureUnit - false
        context = SampleParams.GetNotDefinedMeasureUnit(measureUnitCode);
        yield return toObjectArray();

        // 5. same MeasureUnitCode - true
        isTrue = true;
        context = measureUnitCode;
        yield return toObjectArray();

        // 6. same type valid measureUnit - true
        context = RandomParams.GetRandomValidMeasureUnit(measureUnitCode);
        yield return toObjectArray();

        // 7. other type measureUnit - false
        isTrue = false;
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode(measureUnitCode);
        context = RandomParams.GetRandomMeasureUnit(measureUnitCode);
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            TestCase_bool_Enum_Enum item = new(isTrue, measureUnit, context);

            return item.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetTryExchangeToArgs()
    {
        // null
        measureUnit = RandomParams.GetRandomMeasureUnit();
        measureUnitCode = GetMeasureUnitCode();
        context = null;
        quantifiable = null;
        yield return toObjectArray();

        // not measureUnit Enum
        context = TypeCode.Empty;
        yield return toObjectArray();

        // same type not defined measureUnit
        context = SampleParams.GetNotDefinedMeasureUnit(measureUnitCode);
        yield return toObjectArray();

        // same type defined measureUnit
        context = RandomParams.GetRandomMeasureUnit(measureUnitCode);
        quantifiable = GetQuantifiableChild(RandomParams.GetRandomDecimal(), context);
        yield return toObjectArray();

        // different type measureUnit
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode(measureUnitCode);
        context = RandomParams.GetRandomMeasureUnit(measureUnitCode);
        quantifiable = null;
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            TestCase_Enum_Enum_IQuantifiable item = new(measureUnit, context, quantifiable);

            return item.ToObjectArray();
        }
        #endregion
    }
    #endregion
}
