namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.BaseQuantifiablesTests;

internal class DynamicDataSource : DataFields
{
    #region Methods
    internal IEnumerable<object[]> GetEqualsArgsArrayList()
    {
        // null
        obj = null;
        measureUnit = RandomParams.GetRandomValidMeasureUnit();
        defaultQuantity = RandomParams.GetRandomDecimal();
        isTrue = false;
        yield return toObjectArray();

        // object
        obj = new();
        yield return toObjectArray();

        // Different MeasureUnitCode
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode(GetMeasureUnitCode(measureUnit));
        obj = new BaseQuantifiableChild(RootObject, null)
        {
            Return = new()
            {
                GetBaseMeasureUnit = RandomParams.GetRandomMeasureUnit(measureUnitCode),
                GetDefaultQuantity = defaultQuantity,
            }
        };
        yield return toObjectArray();

        // Same MeasureUnit, different defaultQuantity
        obj = new BaseQuantifiableChild(RootObject, null)
        {
            Return = new()
            {
                GetBaseMeasureUnit = measureUnit,
                GetDefaultQuantity = RandomParams.GetRandomDecimal(defaultQuantity),
            }
        };
        yield return toObjectArray();

        // Same MeasureUnit, same defaultQuantity
        obj = new BaseQuantifiableChild(RootObject, null)
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
            Bool_Object_Enum_Decimal_args item = new(isTrue, obj, measureUnit, defaultQuantity);

            return item.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetFitsInArgsArrayList()
    {
        // null
        measureUnit = null;
        limiter = null;
        yield return toObjectArray();

        // Not IBaseQuantifiable
        limiter = new LimiterObject();
        yield return toObjectArray();

        // Different MeasureUnitCode
        measureUnit = RandomParams.GetRandomMeasureUnit();
        measureUnitCode = RandomParams.GetRandomMeasureUnitCode(GetMeasureUnitCode(measureUnit));
        limiter = new LimiterBaseQuantifiableOblect(RootObject, null)
        {
            Return = new()
            {
                GetBaseMeasureUnit = RandomParams.GetRandomMeasureUnit(GetMeasureUnitCode(measureUnitCode)),
            }
        };

        #region toObjectArray method
        object[] toObjectArray()
        {
            Enum_ILimiter_args item = new(measureUnit, limiter);

            return item.ToObjectArray();
        }
        #endregion
    }

    internal IEnumerable<object[]> GetValidateQuantityInvalidQuantityTypeCodeArgArrayList()
    {
        foreach (TypeCode item in SampleParams.InvalidValueTypeCodes)
        {
            yield return new TypeCode_arg(item).ToObjectArray();
        }
    }

    internal IEnumerable<object[]> GetValidateQuantityValidQuantityTypeCodeArgArrayList()
    {
        foreach (TypeCode item in GetQuantityTypeCodes())
        {
            yield return new TypeCode_arg(item).ToObjectArray();
        }
    }
    //internal IEnumerable<object[]> GetExchangeRateCollectionArgArrayList()
    //{
    //    measureUnit = RandomParams.GetRandomMeasureUnit();
    //    measureUnitCode = GetMeasureUnitCode(measureUnit);
    //    yield return toObjectArray();

    //    measureUnit = RandomParams.GetRandomNotUsedCustomMeasureUnit();
    //    _ = TrySetCustomMeasureUnit(measureUnit, RandomParams.GetRandomPositiveDecimal(), RandomParams.GetRandomParamName());
    //    measureUnitCode = GetMeasureUnitCode(measureUnit);
    //    measureUnit = measureUnitCode.GetDefaultMeasureUnit();
    //    yield return toObjectArray();

    //    #region toObjectArray method
    //    object[] toObjectArray()
    //    {
    //        Enum_MeasureUnitCode_args item = new(measureUnit, measureUnitCode);

    //        return item.ToObjectArray();
    //    }
    //    #endregion
    //}

    //internal IEnumerable<object[]> GetIsExchangeableToArgArrayList()
    //{
    //    // null -  false
    //    isTrue = false;
    //    measureUnit = RandomParams.GetRandomValidMeasureUnit();
    //    context = null;
    //    yield return toObjectArray();

    //    // Not measureUnit not MeasureUnitCode Enum - false
    //    context = TypeCode.Empty;
    //    yield return toObjectArray();

    //    // other MeasureUnitCode - false
    //    measureUnitCode = GetMeasureUnitCode(measureUnit);
    //    context = RandomParams.GetRandomMeasureUnitCode(measureUnitCode);
    //    yield return toObjectArray();

    //    // same type not defined measureUnit - false
    //    context = SampleParams.GetNotDefinedMeasureUnit(measureUnitCode);
    //    yield return toObjectArray();

    //    // same MeasureUnitCode - true
    //    isTrue = true;
    //    context = measureUnitCode;
    //    yield return toObjectArray();

    //    // same type valid measureUnit - true
    //    context = RandomParams.GetRandomValidMeasureUnit(measureUnitCode);
    //    yield return toObjectArray();

    //    // other type measureUnit - false
    //    isTrue = false;
    //    measureUnitCode = RandomParams.GetRandomMeasureUnitCode(measureUnitCode);
    //    context = RandomParams.GetRandomMeasureUnit(measureUnitCode);
    //    yield return toObjectArray();

    //    // same type invalid measureUnit - false
    //    context = RandomParams.GetRandomNotUsedCustomMeasureUnit();
    //    measureUnitCode = GetMeasureUnitCode(measureUnit);
    //    measureUnit = measureUnitCode.GetDefaultMeasureUnit();
    //    yield return toObjectArray();

    //    #region toObjectArray method
    //    object[] toObjectArray()
    //    {
    //        Bool_Enum_Enum_args item = new(isTrue, measureUnit, context);

    //        return item.ToObjectArray();
    //    }
    //    #endregion
    //}

    //internal IEnumerable<object[]> GetValidateExchangeRateArgArrayList()
    //{
    //    measureUnit = RandomParams.GetRandomValidMeasureUnit();
    //    exchangeRate = 0;
    //    yield return toObjectArray();

    //    exchangeRate = RandomParams.GetRandomNegativeDecimal();
    //    yield return toObjectArray();

    //    exchangeRate = RandomParams.GetRandomPositiveDecimal(GetExchangeRate(measureUnit, null));
    //    #region toObjectArray method
    //    object[] toObjectArray()
    //    {
    //        Enum_Decimal_args item = new(measureUnit, exchangeRate);

    //        return item.ToObjectArray();
    //    }
    //    #endregion
    //}

    //internal IEnumerable<object[]> GetValidateMeasureUnitValidArgsArrayList()
    //{
    //    // MeasureUnitCode
    //    measureUnit = RandomParams.GetRandomValidMeasureUnit();
    //    context = GetMeasureUnitCode(measureUnit);
    //    yield return toObjectArray();

    //    // measureUnit
    //    context = RandomParams.GetRandomSameTypeValidMeasureUnit(measureUnit);
    //    yield return toObjectArray();

    //    #region toObjectArray method
    //    object[] toObjectArray()
    //    {
    //        Enum_Enum_args item = new(measureUnit, context);

    //        return item.ToObjectArray();
    //    }
    //    #endregion
    //}
    #endregion
}
