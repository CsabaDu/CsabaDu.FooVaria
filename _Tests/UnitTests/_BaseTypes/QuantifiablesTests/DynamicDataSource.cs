namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.QuantifiablesTests;

internal sealed class DynamicDataSource : CommonDynamicDataSource
{
    #region Fields
    IQuantifiable quantifiable;
    #endregion

    #region Methods
    internal IEnumerable<object[]> GetEqualsArgs()
    {
        testCase = "null => false";
        quantifiable = null;
        measureUnit = RandomParams.GetRandomValidMeasureUnit();
        defaultQuantity = RandomParams.GetRandomDecimal();
        isTrue = false;
        yield return toObjectArray();

        testCase = "Different MeasureUnitCode => false";
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode(GetMeasureUnitCode());
        quantifiable = GetQuantifiableChild(defaultQuantity, RandomParams.GetRandomMeasureUnit(measureUnitCode));
        yield return toObjectArray();

        testCase = "Same measureUnit, different defaultQuantity => false";
        quantifiable = GetQuantifiableChild(RandomParams.GetRandomDecimal(defaultQuantity), measureUnit);
        yield return toObjectArray();

        testCase = "Same measureUnit, same defaultQuantity => true";
        quantifiable = GetQuantifiableChild(defaultQuantity, measureUnit);
        isTrue = true;
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            TestCase_Enum_decimal_bool_IQuantifiable args = new(testCase, measureUnit, defaultQuantity, isTrue, quantifiable);

            return args.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetFitsInILimiterArgs()
    {
        testCase = "Not IBaseQuantifiable";
        measureUnit = RandomParams.GetRandomMeasureUnit();
        limiter = new LimiterObject();
        yield return toObjectArray();

        testCase = "Different MeasureUnitCode";
        measureUnitCode = GetMeasureUnitCode();
        measureUnitCode = RandomParams.GetRandomCustomMeasureUnitCode(measureUnitCode);
        limitMode = RandomParams.GetRandomLimitMode();
        limiter = GetLimiterQuantifiableObject(limitMode.Value, RandomParams.GetRandomMeasureUnit(measureUnitCode), defaultQuantity);
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            TestCase_Enum_ILimiter args = new(testCase, measureUnit, limiter);

            return args.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetFitsInIQuantifiableLimitModeArgs()
    {
        testCase = "IQuantifiable, Not defined LimitMode";
        limitMode = SampleParams.NotDefinedLimitMode;
        measureUnit = RandomParams.GetRandomMeasureUnit();
        measureUnitCode = GetMeasureUnitCode();
        quantifiable = GetQuantifiableChild(RandomParams.GetRandomDecimal(), measureUnit);
        yield return toObjectArray();

        testCase = "Different IQuantifiable, valid LimitMode";
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode(measureUnitCode);
        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode);
        limitMode = RandomParams.GetRandomLimitMode();
        yield return toObjectArray();

        testCase = "null, valid LimitMode";
        quantifiable = null;
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            TestCase_Enum_LimitMode_IQuantifiable args = new(testCase, measureUnit, limitMode, quantifiable);

            return args.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetQuantifiableInvalidArgs()
    {
        testCase = "Not defined MeasureUnitCode";
        measureUnit = RandomParams.GetRandomMeasureUnit();
        measureUnitCode = SampleParams.NotDefinedMeasureUnitCode;
        yield return toObjectArray();

        testCase = "Different MeasureUnitCode";
        measureUnitCode = GetMeasureUnitCode();
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode(measureUnitCode);
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            TestCase_Enum_MeasureUnitCode args = new(testCase, measureUnit, measureUnitCode);

            return args.ToObjectArray();
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
            testCase = "RoundingMode." + Enum.GetName(roundingMode);
            TestCase_Enum_decimal_object_RoundingMode args = new(testCase, measureUnit, defaultQuantity, obj, roundingMode);

            return args.ToObjectArray();
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
        object[] toObjectArray(TypeCode typeCode)
        {
            testCase = GetTypeCodeName(typeCode);
            TestCase_TypeCode item = new(testCase, typeCode);

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
                testCase = GetTypeCodeName(item);
                obj = convertDefaultQuantity();
                yield return toObjectArray();
            }
        }

        measureUnit = MeasureUnitCode.Currency.GetDefaultMeasureUnit();
        quantityTypeCode = TypeCode.Double;
        testCase = GetTypeCodeName(quantityTypeCode);
        obj = convertDefaultQuantity();
        yield return toObjectArray();

        quantityTypeCode = TypeCode.Int64;
        testCase = GetTypeCodeName(quantityTypeCode);
        obj = convertDefaultQuantity();
        yield return toObjectArray();

        defaultQuantity = RandomParams.GetRandomNotNegativeDecimal();
        quantityTypeCode = TypeCode.UInt64;
        testCase = GetTypeCodeName(quantityTypeCode);
        obj = convertDefaultQuantity();
        yield return toObjectArray();

        #region Local methods
        #region toObjectArray method
        object[] toObjectArray()
        {
            TestCase_Enum_decimal_object_TypeCode args = new(testCase, measureUnit, defaultQuantity, obj, quantityTypeCode);

            return args.ToObjectArray();
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
        testCase = "null => false";
        isTrue = false;
        measureUnit = RandomParams.GetRandomValidMeasureUnit();
        context = null;
        yield return toObjectArray();

        testCase = "Not measureUnit not MeasureUnitCode Enum => false";
        context = TypeCode.Empty;
        yield return toObjectArray();

        testCase = "Different MeasureUnitCode => false";
        measureUnitCode = GetMeasureUnitCode();
        context = RandomParams.GetRandomMeasureUnitCode(measureUnitCode);
        yield return toObjectArray();

        testCase = "Same type not defined measureUnit => false";
        context = SampleParams.GetNotDefinedMeasureUnit(measureUnitCode);
        yield return toObjectArray();

        testCase = "Same MeasureUnitCode => true";
        isTrue = true;
        context = measureUnitCode;
        yield return toObjectArray();

        testCase = "Same type valid measureUnit => true";
        context = RandomParams.GetRandomValidMeasureUnit(measureUnitCode);
        yield return toObjectArray();

        testCase = "Different type measureUnit => false";
        isTrue = false;
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode(measureUnitCode);
        context = RandomParams.GetRandomMeasureUnit(measureUnitCode);
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            TestCase_bool_Enum_Enum args = new(testCase, isTrue, measureUnit, context);

            return args.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetTryExchangeToArgs()
    {
        testCase = "null => false, out null";
        measureUnit = RandomParams.GetRandomMeasureUnit();
        measureUnitCode = GetMeasureUnitCode();
        context = null;
        quantifiable = null;
        yield return toObjectArray();

        testCase = "Not measureUnit not MeasureUnitCode Enum => false, out null";
        context = TypeCode.Empty;
        yield return toObjectArray();

        testCase = "Same type not defined measureUnit => false, out null";
        context = SampleParams.GetNotDefinedMeasureUnit(measureUnitCode);
        yield return toObjectArray();

        testCase = "Same type defined measureUnit => true, out exchanged";
        context = RandomParams.GetRandomMeasureUnit(measureUnitCode);
        quantifiable = GetQuantifiableChild(RandomParams.GetRandomDecimal(), context);
        yield return toObjectArray();

        testCase = "Different type measureUnit => false, out null";
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode(measureUnitCode);
        context = RandomParams.GetRandomMeasureUnit(measureUnitCode);
        quantifiable = null;
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            TestCase_Enum_Enum_IQuantifiable args = new(testCase, measureUnit, context, quantifiable);

            return args.ToObjectArray();
        }
        #endregion
    }
    #endregion

    private static string GetTypeCodeName(TypeCode typeCode)
    {
        return "TypeCode." + Enum.GetName(typeCode);
    }
}
