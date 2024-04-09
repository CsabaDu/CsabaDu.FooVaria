namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.BaseMeasuresTests;

internal class DynamicDataSource : CommonDynamicDataSource
{
    #region Methods
    //internal IEnumerable<object[]> GetCompareToInvalidArgs()
    //{
    //    measureUnitCode = RandomParams.GetRandomCustomMeasureUnitCode();
    //    measureUnit = measureUnitCode.GetDefaultMeasureUnit();
    //    quantity = (ValueType)RandomParams.GetRandomQuantity();
    //    IBaseMeasure baseMeasure = GetBaseMeasureChild(measureUnit, quantity);
    //    measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode);
    //    yield return toObjectArray();

    //    measureUnitCode = RandomParams.GetRandomMeasureUnitCode(measureUnitCode);
    //    measureUnit = RandomParams.GetRandomValidMeasureUnit(measureUnitCode);
    //    yield return toObjectArray();

    //    #region toObjectArray method
    //    object[] toObjectArray()
    //    {
    //        IBaseMeasure_Enum_arg item = new(baseMeasure, measureUnit);

    //        return item.ToObjectArray();
    //    }
    //    #endregion
    //}

    internal IEnumerable<object[]> GetEqualsArg()
    {
        IBaseMeasure baseMeasure = null;
        yield return toObjectArray();

        measureUnit = RandomParams.GetRandomMeasureUnit();
        quantity = (ValueType)RandomParams.GetRandomQuantity();
        baseMeasure = GetBaseMeasureChild(measureUnit, quantity);
        yield return toObjectArray();

        #region toObjectArray method
        object[] toObjectArray()
        {
            IBaseMeasure_arg item = new(baseMeasure);

            return item.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetEqualsArgs()
    {
        // null - null
        isTrue = true;
        IBaseMeasure leftBaseMeasure = null;
        IBaseMeasure rightBaseMeasure = null;
        yield return toObjectArray();

        // not null - null
        isTrue = false;
        measureUnit = RandomParams.GetRandomValidMeasureUnit();
        quantity = (ValueType)RandomParams.GetRandomQuantity();
        rateComponentCode = RandomParams.GetRandomRateComponentCode();
        limitMode = RandomParams.GetRandomNullableLimitMode();
        leftBaseMeasure = getBaseMeasureChild();
        yield return toObjectArray();

        // null - not null
        leftBaseMeasure = null;
        rightBaseMeasure = getBaseMeasureChild();
        yield return toObjectArray();

        // same
        isTrue = true;
        leftBaseMeasure = getBaseMeasureChild();
        yield return toObjectArray();

        // Different LimitMode
        isTrue = false;
        limitMode = RandomParams.GetRandomNullableLimitMode(limitMode);
        rightBaseMeasure = getBaseMeasureChild();
        yield return toObjectArray();

        // Different RateComponentCode
        leftBaseMeasure = getBaseMeasureChild();
        rateComponentCode = RandomParams.GetRandomRateComponentCode(rateComponentCode);
        rightBaseMeasure = getBaseMeasureChild();
        yield return toObjectArray();

        // Different quantity
        leftBaseMeasure = getBaseMeasureChild();
        quantityTypeCode = Type.GetTypeCode(quantity.GetType());
        quantity = (ValueType)RandomParams.GetRandomQuantity(quantityTypeCode, quantity);
        rightBaseMeasure = getBaseMeasureChild();
        yield return toObjectArray();

        // Different measureUnit
        leftBaseMeasure = getBaseMeasureChild();
        measureUnit = RandomParams.GetRandomValidMeasureUnit(measureUnit);
        rightBaseMeasure = getBaseMeasureChild();
        yield return toObjectArray();

        // Equals when exchanged
        isTrue = true;
        decimalQuantity = RandomParams.GetRandomDecimal();
        quantity = decimalQuantity;
        leftBaseMeasure = getBaseMeasureChild();
        decimalQuantity *= getExchangeRate();
        measureUnit = RandomParams.GetRandomSameTypeValidMeasureUnit(measureUnit);
        decimalQuantity /= getExchangeRate();
        quantity = decimalQuantity;
        rightBaseMeasure = getBaseMeasureChild();
        yield return toObjectArray();

        #region Local methods
        decimal getExchangeRate()
        {
            return GetExchangeRate(measureUnit, nameof(measureUnit));
        }

        BaseMeasureChild getBaseMeasureChild()
        {
            return GetBaseMeasureChild(measureUnit, quantity, rateComponentCode, limitMode);
        }

        #region toObjectArray method
        object[] toObjectArray()
        {
            IBaseMeasure_bool_IBaseMeasure_arg item = new(leftBaseMeasure, isTrue, rightBaseMeasure);

            return item.ToObjectArray();
        }
        #endregion
        #endregion

        //throw new NotImplementedException();
    }

    //internal IEnumerable<object[]> GetFitsInILimiterArgs()
    //{
    //    // Not IBaseQuantifiable
    //    measureUnit = RandomParams.GetRandomMeasureUnit();
    //    limiter = new LimiterObject();
    //    yield return toObjectArray();

    //    // Different MeasureUnitCode
    //    measureUnitCode = GetMeasureUnitCode(measureUnit);
    //    limiter = GetLimiterQuantifiableObject(null, RandomParams.GetRandomMeasureUnit(measureUnitCode), defaultQuantity);
    //    yield return toObjectArray();

    //    #region toObjectArray method
    //    object[] toObjectArray()
    //    {
    //        Enum_ILimiter_args item = new(measureUnit, limiter);

    //        return item.ToObjectArray();
    //    }
    //    #endregion
    //}

    //internal IEnumerable<object[]> GetFitsInIQuantifiableLimitModeArgs()
    //{
    //    // IQuantifiable, Not defined limitMode
    //    limitMode = SampleParams.NotDefinedLimitMode;
    //    measureUnit = RandomParams.GetRandomMeasureUnit();
    //    measureUnitCode = GetMeasureUnitCode(measureUnit);
    //    IQuantifiable quantifiable = GetQuantifiableChild(RandomParams.GetRandomDecimal(), measureUnit);
    //    yield return toObjectArray();

    //    // Different IQuantifiable, valid LimitMode
    //    measureUnitCode = RandomParams.GetRandomMeasureUnitCode(measureUnitCode);
    //    measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode);
    //    limitMode = RandomParams.GetRandomLimitMode();
    //    yield return toObjectArray();

    //    // null, valid LimitMode
    //    quantifiable = null;
    //    yield return toObjectArray();

    //    #region toObjectArray method
    //    object[] toObjectArray()
    //    {
    //        Enum_LimitMode_IQuantifiable_arg item = new(measureUnit, limitMode, quantifiable);

    //        return item.ToObjectArray();
    //    }
    //    #endregion
    //}

    //internal IEnumerable<object[]> GetQuantifiableInvalidArgs()
    //{
    //    measureUnit = RandomParams.GetRandomMeasureUnit();
    //    measureUnitCode = SampleParams.NotDefinedMeasureUnitCode;
    //    yield return toObjectArray();

    //    measureUnitCode = GetMeasureUnitCode(measureUnit);
    //    measureUnitCode = RandomParams.GetRandomMeasureUnitCode(measureUnitCode);
    //    yield return toObjectArray();

    //    #region toObjectArray method
    //    object[] toObjectArray()
    //    {
    //        Enum_MeasureUnitCode_args item = new(measureUnit, measureUnitCode);

    //        return item.ToObjectArray();
    //    }
    //    #endregion
    //}

    //internal IEnumerable<object[]> GetGetQuantityRoundingModeArgs()
    //{
    //    // double
    //    measureUnitCode = RandomParams.GetRandomConstantMeasureUnitCode();
    //    measureUnit = measureUnitCode.GetDefaultMeasureUnit();
    //    measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode, measureUnit);
    //    defaultQuantity = RandomParams.GetRandomDecimal();
    //    RoundingMode[] roundingModes = Enum.GetValues<RoundingMode>();

    //    foreach (RoundingMode item in roundingModes)
    //    {
    //        roundingMode = item;
    //        obj = Convert.ToDouble(defaultQuantity).Round(item);
    //        yield return toObjectArray();
    //    }

    //    // decimal
    //    measureUnit = MeasureUnitCode.Currency.GetDefaultMeasureUnit();

    //    foreach (RoundingMode item in roundingModes)
    //    {
    //        roundingMode = item;
    //        obj = defaultQuantity.Round(item);
    //        yield return toObjectArray();
    //    }

    //    #region toObjectArray method
    //    object[] toObjectArray()
    //    {
    //        Enum_decimal_object_RoundingMode_args item = new(measureUnit, defaultQuantity, obj, roundingMode);

    //        return item.ToObjectArray();
    //    }
    //    #endregion
    //}

    //internal IEnumerable<object[]> GetGetQuantityInvalidTypeCodeArgs()
    //{
    //    return SampleParams.GetInvalidQuantityTypeCodes()
    //        .Select(toObjectArray)
    //        .Append(toObjectArray(SampleParams.NotDefinedTypeCode))
    //        .Append(toObjectArray(TypeCode.UInt64));

    //    #region toObjectArray method
    //    static object[] toObjectArray(TypeCode typeCode)
    //    {
    //        TypeCode_arg item = new(typeCode);

    //        return item.ToObjectArray();
    //    }
    //    #endregion
    //}
    //internal IEnumerable<object[]> GetGetQuantityValidTypeCodeArgs()
    //{
    //    measureUnitCode = RandomParams.GetRandomConstantMeasureUnitCode();
    //    measureUnit = measureUnitCode.GetDefaultMeasureUnit();
    //    measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode, measureUnit);
    //    quantity = RandomParams.GetRandomDouble();
    //    defaultQuantity = (decimal)quantity.ToQuantity(TypeCode.Decimal);

    //    foreach (var item in QuantityTypeCodes)
    //    {
    //        if (item is not (TypeCode.Int64 or TypeCode.UInt64))
    //        {
    //            quantityTypeCode = item;
    //            obj = convertDefaultQuantity();
    //            yield return toObjectArray();
    //        }
    //    }
    //    measureUnit = MeasureUnitCode.Currency.GetDefaultMeasureUnit();
    //    quantityTypeCode = TypeCode.Double;
    //    obj = convertDefaultQuantity();
    //    yield return toObjectArray();

    //    quantityTypeCode = TypeCode.Int64;
    //    obj = convertDefaultQuantity();
    //    yield return toObjectArray();

    //    defaultQuantity = RandomParams.GetRandomPositiveDecimal();
    //    quantityTypeCode = TypeCode.UInt64;
    //    obj = convertDefaultQuantity();
    //    yield return toObjectArray();

    //    #region Local methods
    //    #region toObjectArray method
    //    object[] toObjectArray()
    //    {
    //        Enum_decimal_object_TypeCode_args item = new(measureUnit, defaultQuantity, obj, quantityTypeCode);

    //        return item.ToObjectArray();
    //    }
    //    #endregion

    //    object convertDefaultQuantity()
    //    {
    //        return defaultQuantity.ToQuantity(quantityTypeCode);
    //    }
    //    #endregion
    //}

    //internal IEnumerable<object[]> GetIsExchangeableToArg()
    //{
    //    // 1. null -  false
    //    isTrue = false;
    //    measureUnit = RandomParams.GetRandomValidMeasureUnit();
    //    context = null;
    //    yield return toObjectArray();

    //    // 2. Not measureUnit not MeasureUnitCode Enum - false
    //    context = TypeCode.Empty;
    //    yield return toObjectArray();

    //    // 3. other MeasureUnitCode - false
    //    measureUnitCode = GetMeasureUnitCode(measureUnit);
    //    context = RandomParams.GetRandomMeasureUnitCode(measureUnitCode);
    //    yield return toObjectArray();

    //    // 4. same type not defined measureUnit - false
    //    context = SampleParams.GetNotDefinedMeasureUnit(measureUnitCode);
    //    yield return toObjectArray();

    //    // 5. same MeasureUnitCode - true
    //    isTrue = true;
    //    context = measureUnitCode;
    //    yield return toObjectArray();

    //    // 6. same type valid measureUnit - true
    //    context = RandomParams.GetRandomValidMeasureUnit(measureUnitCode);
    //    yield return toObjectArray();

    //    // 7. other type measureUnit - false
    //    isTrue = false;
    //    measureUnitCode = RandomParams.GetRandomMeasureUnitCode(measureUnitCode);
    //    context = RandomParams.GetRandomMeasureUnit(measureUnitCode);
    //    yield return toObjectArray();

    //    #region toObjectArray method
    //    object[] toObjectArray()
    //    {
    //        bool_Enum_Enum_args item = new(isTrue, measureUnit, context);

    //        return item.ToObjectArray();
    //    }
    //    #endregion
    //}

    //internal IEnumerable<object[]> GetTryExchangeToArgs()
    //{
    //    // null
    //    measureUnit = RandomParams.GetRandomMeasureUnit();
    //    measureUnitCode = GetMeasureUnitCode(measureUnit);
    //    context = null;
    //    IQuantifiable quantifiable = null;
    //    yield return toObjectArray();

    //    // not measureUnit Enum
    //    context = TypeCode.Empty;
    //    yield return toObjectArray();

    //    // same type not defined measureUnit
    //    context = SampleParams.GetNotDefinedMeasureUnit(measureUnitCode);
    //    yield return toObjectArray();

    //    // same type defined measureUnit
    //    context = RandomParams.GetRandomMeasureUnit(measureUnitCode);
    //    quantifiable = GetQuantifiableChild(RandomParams.GetRandomDecimal(), context);
    //    yield return toObjectArray();

    //    // different type measureUnit
    //    measureUnitCode = RandomParams.GetRandomMeasureUnitCode(measureUnitCode);
    //    context = RandomParams.GetRandomMeasureUnit(measureUnitCode);
    //    quantifiable = null;
    //    yield return toObjectArray();

    //    #region toObjectArray method
    //    object[] toObjectArray()
    //    {
    //        Enum_Enum_IQuantifiable_args item = new(measureUnit, context, quantifiable);

    //        return item.ToObjectArray();
    //    }
    //    #endregion
    //}
    #endregion
}
